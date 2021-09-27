using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Com6srvr;

namespace Goniometer_Controller.Motors
{
    internal static class MotorSocketProvider
    {
        private static object _lock = new object();
        private static INet _socket;

        public static void SetIPAddress(IPAddress ipaddress)
        {
            lock (_lock)
            {
                _socket = new NetClass();
                _socket.Connect(ipaddress.ToString());
                _socket.AutoReconnect(true);
                _socket.FSEnabled = true;
            }
        }

        #region standard socket commands
        public static void Write(string cmd)
        {
            lock (_lock)
            {
                CheckSocketStatus();
                _socket.Write(cmd);
            }
        }

        public static string Read()
        {
            lock (_lock)
            {
                CheckSocketStatus();
                return _socket.Read();
            }
        }

        public static string WriteForResponse(string cmd)
        {
            lock (_lock)
            {
                CheckSocketStatus();
                _socket.Write(cmd);
                _socket.Flush();
                return _socket.Read(); 
            }
        }
        #endregion

        public static int GetEncoderPosition(short axis)
        {
            lock (_lock)
            {
                CheckSocketStatus();
                return _socket.get_EncoderPos(axis);
            }
        }

        public static int GetMotorPosition(short axis)
        {
            lock (_lock)
            {
                CheckSocketStatus();
                return _socket.get_MotorPos(axis);
            }
        }

        private static bool CheckSocketStatus()
        {
            if (_socket == null)
                    throw new InvalidOperationException("Configure the connection before sending commands");

            return true;
        }
    }
}
