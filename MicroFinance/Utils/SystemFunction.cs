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
           select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

        }

        public static IEnumerable<string> GetMACAddressList()
        {
            return
            (
           from nic in NetworkInterface.GetAllNetworkInterfaces()
           select nic.GetPhysicalAddress().ToString()
            );

        }
    }
}
