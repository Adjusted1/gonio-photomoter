using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Goniometer_Controller
{
    /// <summary>
    /// The SerialPort Provider will go on to claim all the serial ports on a system. This will be problamatic if the system has
    /// other applications that need to utilize serialports. Take care to unregister ports if you don't need them.
    /// </summary>
    public static class SerialPortProvider
    {
        private static object _lock = new object();
        private static List<SerialPort> _ports = new List<SerialPort>();

        public static SerialPort GetPort(string portname)
        {
            SerialPort port;
            
            //check to see if we are managing this portname already
            port = _ports.FirstOrDefault(p => p.PortName == portname);
            if (port == null)
            {
                lock (_lock)
                {
                    //check now that we've secured a lock
                    port = _ports.FirstOrDefault(p => p.PortName == portname);
                    if (port == null)
                    {
                        //create a new port and manage it
                        port = new SerialPort(portname);

                        port.BaudRate = 9600;
                        port.DataBits = 7;
                        port.StopBits = StopBits.One;
                        port.Parity = Parity.Even;
                        port.ReadTimeout = 100;
                        port.WriteTimeout = 100;

                        _ports.Add(port);
                    }
                }
            }

            return port;
        }

        public static void ReturnPort(SerialPort port)
        {
            throw new NotImplementedException();
        }
    }
}
