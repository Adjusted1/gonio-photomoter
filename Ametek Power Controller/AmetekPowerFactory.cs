using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ametek.AmetekACPwr.Interop;

using Ivi.Visa.Interop;

namespace Ametek_Power_Controller
{
    public class AmetekPowerFactory
    {
        private static ISerial session;
        private static IAmetekACPwr controller;
        
        public static IAmetekACPwr GetController()
        {
            string logicalName = "AmetekACPwr";
            string sessionName = "AmetekACPwr Session";
            string resourceName = "ASRL8::INSTR";
            string aliasName = "COM8";
            string driverProgID = "AmetekACPwr.AmetekACPwr";

            IResourceManager3 manager = new ResourceManager();
            session = (ISerial)manager.Open(aliasName);
            session.BaudRate = 115200;

            controller = new AmetekACPwr();
            controller.Initialize(aliasName, true, false);
            
            return controller;
        }
    }
}
