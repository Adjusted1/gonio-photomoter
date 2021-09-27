using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO.Ports;

using Goniometer_Controller.Models;
using Goniometer_Controller.Sensors;

namespace Minolta_Controller
{
    public abstract class MinoltaBaseSensor : BaseSensor
    {

        protected SerialPort _port;


        #region construction
        public MinoltaBaseSensor(SerialPort port)
        {
            _port = port;
        }

        /// <summary>
        /// Open SerialPort
        /// </summary>
        public virtual void Connect()
        {
            if (!_port.IsOpen)
            {
                _port.Open();
            }
        }

        public virtual void Disconnect()
        {
        }

        public abstract bool TestStatus();
        #endregion

        #region communication
        /// <summary>
        /// Send Command to sensor
        /// </summary>
        /// <param sensorname="receptor"></param>
        /// <param sensorname="command"></param>
        /// <param sensorname="data"></param>
        protected void SendCommand(int receptor, int command, string data)
        {
            if (!_port.IsOpen)
                Connect();

            string cmd = "";

            cmd += receptor.ToString("0#");
            cmd += command.ToString("0#");
            cmd += data;
            cmd += "\u0003";            //end bytes

            string bcc = BlockCheckChar(cmd);

            _port.Write("\u0002");       //start bytes
            _port.Write(cmd);
            _port.Write(bcc);
            _port.Write("\u000D\u000A"); //new line with carriage return
        }

        /// <summary>
        /// Read Response from sensor
        /// </summary>
        /// <param sensorname="receptor"></param>
        /// <param sensorname="command"></param>
        /// <returns></returns>
        protected string ReadResponse(out int receptor, out int command)
        {
            if (!_port.IsOpen)
                Connect();

            string res = _port.ReadLine();

            /* 
             * res.Substring(0, 1);    //Start of Text     \u0002
             * res.Substring(1, 2);    //Receptor #        "00"
             * res.Substring(3, 2);    //Command #         "10"
             * 
             * //Data, Length Unknown
             * res.Substring(4, L-6);
             * 
             * res.Substring(L-5, 1);   //End of Text       \u0003
             * res.Substring(L-4, 2);   //BCC
             * res.Substring(L-2, 2);   //newline
             * */

            //length validation
            //minimum message contains 10 char
            if (res.Length < 10)
                throw new Exception("Message Malformed");

            //REMOVE, the device doesn't provide valid checksums back
            //checksum validation
            string bcc = BlockCheckChar(res.Substring(1, 27));
            //if (bcc != res.Substring(28, 2))
            //   throw new Exception("Message Malformed");

            receptor = Int32.Parse(res.Substring(1, 2));
            command = Int32.Parse(res.Substring(3, 2));

            return res.Substring(5, res.Length - 5);
        }

        protected virtual void ErrorCheckChar(string error)
        {
            //error information
            switch (error)
            {
                case " ":
                    //normal operation
                    break;

                case "1":
                    throw new Exception("Receptor Head Off");

                case "2":
                    throw new Exception("EEPROM Error 1");

                case "3":
                    throw new Exception("EEPROM Error 2");

                case "4":
                    //normal operation
                    break;

                case "5":
                    throw new Exception("Measurement over Value error");

                case "6":
                    //normal operation
                    break;

                case "7":
                    //normal operation
                    break;

                default:
                    throw new Exception("Unknown Error");
            }
        }

        /// <summary>
        /// Block Check Character
        /// </summary>
        /// <param sensorname="payload">string to checksum</param>
        /// <returns>checksum byte, in hex, as a string</returns>
        protected string BlockCheckChar(string payload)
        {
            if (String.IsNullOrEmpty(payload))
                throw new ArgumentNullException("payload cannot be null or empty");

            byte[] payloadBytes = Encoding.ASCII.GetBytes(payload);

            int value = 0x00;
            for (int i = 0; i < payloadBytes.Length; i++)
                value ^= payloadBytes[i];

            //return hex as 2 digits
            return value.ToString("X2");
        }

        /// <summary>
        /// Parse Minolta Long-Communication Number-Format
        /// </summary>
        /// <param sensorname="payload">Value to Parse</param>
        /// <returns></returns>
        protected double ParseReadingValue(string payload)
        {
            if (payload == null || payload.Length != 6)
                throw new ArgumentException("reading must be length of 6");

            /*
             * payload.Substring(0, 1);   //Sign              "+, -, ="
             * payload.Substring(1, 4);   //Reading           "0 - 9"
             * payload.Substring(5, 1);   //Magnitude         "0 - 9" => (10^-4) - (10^5)
             * 
             * example:
             * 
             * "+ 3343" becomes 33.4
             * "-00011" becomes -0.001
             * */

            double reading = Double.Parse(payload.Substring(1, 4).Trim());
            double power = Double.Parse(payload.Substring(5, 1)) - 4;

            if (payload.Substring(0, 1) == "-")
                reading *= -1;

            reading *= Math.Pow(10, power);

            return reading;
        } 
        #endregion

        public override void Dispose()
        {
            this.Disconnect();
        }
    }
}
