using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xitron_280x_Driver;

namespace Xitron_Controller
{
    public class Xitron280xController
    {
        private cls_Xitron_USB usb;
        private IEnumerable<string> devices;

        public Xitron280xController()
        {
            usb = new cls_Xitron_USB();
            refreshDeviceList();
        }

        public void refreshDeviceList()
        {
            string devicelist = usb.Get_ALL_Available_DeviceName();
            devices = devicelist.Split('\r').ToList();
        }

        public IEnumerable<string> getDeviceList()
        {
            return devices.ToList();
        }


    }
}
