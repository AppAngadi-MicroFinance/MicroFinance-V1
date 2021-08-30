using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Utils
{
   public static class SystemFunction
    {

        public static string GetMACAddress()
        {
            return
            (
           from nic in NetworkInterface.GetAllNetworkInterfaces()
           where nic.OperationalStatus == OperationalStatus.Up
           select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

        }
    }
}
