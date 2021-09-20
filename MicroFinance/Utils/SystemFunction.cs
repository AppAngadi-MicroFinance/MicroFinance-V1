using System;
using System.Collections.Generic;
using System.IO;
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


        public static string DriveBasePath
        {
            get
            {
                return BasePath();
            }
        }

        private static string BasePath()
        {
            string BasePath = string.Empty;
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {

                if (d.IsReady == true)
                {
                    //sb.Append("\n"+d.Name+" "+" "+d.VolumeLabel);

                    if (d.VolumeLabel.Equals("Google Drive"))
                    {
                        string dir = d.Name + "My Drive" + "\\MicroFinance";

                        if (Directory.Exists(dir))
                        {
                            BasePath = dir;
                        }
                        else
                        {

                            Directory.CreateDirectory(dir);
                            BasePath = dir;
                        }
                        break;
                    }
                }
            }
            return BasePath;
        }
    }
}
