using System;
using System.Configuration;
using System.IO;

namespace SimpleLogger
{
    public class Logging
    {
        private static readonly object locker = new object();

        private static string _logFile;
        private static string logFile
        {
            get
            {
                if (String.IsNullOrEmpty(_logFile))
                {
                    lock (locker)
                    {
                        if (String.IsNullOrEmpty(_logFile))
                        {
                            _logFile = ConfigurationManager.AppSettings["logFile"]; 
                        }
                    }
                }

                return _logFile;
            }
        }

        private static int _logFileSize = 0; //kB
        private static int logFileSize
        {
            get
            {
                if (_logFileSize <= 0)
                {
                    lock (locker)
                    {
                        if (_logFileSize <= 0)
                        {
                            string size = ConfigurationManager.AppSettings["logFileSize"];
                            if (!Int32.TryParse(size, out _logFileSize))
                                _logFileSize = 1024;                                
                        }
                    }
                }

                return _logFileSize;
            }
        }

        public static void WriteToLog(string message)
        {
            try
            {
                lock (locker)
                {
                    FileInfo fi = new FileInfo(logFile);

                    //check directory existance
                    if (!fi.Directory.Exists)
                        fi.Directory.Create();

                    //check file size
                    if (fi.Exists && fi.Length > 1024 * logFileSize)
                        RotateLogFile();

                    StreamWriter SW;
                    SW = File.AppendText(logFile);
                    SW.Write(String.Format("{0:hh\\:mm\\:ss}: ", DateTime.Now));
                    SW.WriteLine(message);
                    SW.Close();
                }
            }
            catch (Exception)
            {
                //logging system shouldn't throw
            }
        }

        public static void RotateLogFile()
        {
            try
            {
                lock (locker)
                {
                    int maxRotate = 7;

                    //delete oldest file
                    FileInfo fi = new FileInfo(logFile + "." + maxRotate);
                    if (fi.Exists)
                        fi.Delete();

                    //rotate old files
                    for (int i = maxRotate; i > 0; i--)
                    {
                        fi = new FileInfo(logFile + "." + i);
                        if (fi.Exists)
                            fi.MoveTo(logFile + "." + (i + 1));
                    }

                    //rotate newest file
                    fi = new FileInfo(logFile);
                    if (fi.Exists)
                        fi.MoveTo(logFile + ".0");
                }
            }
            catch (Exception)
            {
                //logging system shouldn't throw
            }
        }
    }
}
;