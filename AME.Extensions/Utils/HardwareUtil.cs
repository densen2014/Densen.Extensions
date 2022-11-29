using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if NET48
using System.Management;
#endif

namespace AME.Util
{
    /// <summary>
    /// 硬件工具类
    /// </summary>
    public static class HardwareUtil
    {

#if NET48
   public static string GetFirstPhysicalMediaID()
        {
            return GetPhysicalMediaID().FirstOrDefault();
        }
        public static string GetFirstMacAddress()
        {
            string ids = "";
            var searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
            foreach (ManagementObject share in searcher.Get())
            {
                if (share["MACAddress"] != null && !string.IsNullOrEmpty(share["MACAddress"].ToString()))
                {
                    return share["MACAddress"].ToString();
                }
            }
            return ids;
        }
        public static List<string> GetPhysicalMediaID()
        {
            //ManagementScope
            //  "\\\\.\\root\\cimv2"
            //  "\\.\root\cimv2"
            var ids = new List<string>();
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                foreach (ManagementObject share in searcher.Get())
                {
                    if (share["SerialNumber"] != null && !string.IsNullOrEmpty(share["SerialNumber"].ToString()))
                    {
                        ids.Add(share["SerialNumber"].ToString().Trim());
                    }
                }
            }
            catch
            {
            }
            return ids;
        }
#endif
    }

}
