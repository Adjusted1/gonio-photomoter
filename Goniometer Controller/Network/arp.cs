using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Goniometer_Controller
{
    public static class NetworkUtil
    {
        public static void ARP(IPAddress ip, int mac)
        {
            // Start the child process.
            Process p = new Process();

            //p.StartInfo.Arguments = String.Format("{0} {1}", ip, mac);

            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "arp.bat";
            p.Start();

            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }
    }
}
