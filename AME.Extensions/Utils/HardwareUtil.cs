using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if NET48 || WINDOWS10_0_19041_0_OR_GREATER
using System.Management;
#endif

namespace AME.Util
{
    /// <summary>
    /// 硬件工具类
    /// </summary>
    public static class HardwareUtil
    {

#if NET48 || WINDOWS10_0_19041_0_OR_GREATER
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
                foreach (var share in searcher.Get())
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
        public class DiskDriveID
        {
            public string MediaType { get; set; }
            public string Model { get; set; }
            public string SerialNumber { get; set; }
            public string InterfaceType { get; set; }
            public string Size { get; set; }
        }
        public static List<DiskDriveID> GetDiskDriveIDS(bool orderByInterfaceType=true)
        {

            var ids = new List<DiskDriveID>();
            var keys = new List<string>() {
                "MediaType",
                "Model",
                "SerialNumber",
                "InterfaceType",
            };
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                var id = new DiskDriveID();
                foreach (var share in searcher.Get())
                {
                    id = new DiskDriveID();
                    foreach (var key in keys)
                    {
                        if (share[key] != null && !string.IsNullOrEmpty(share[key].ToString()))
                        {
                            var value = share[key].ToString().Trim();
                            switch (key)
                            {
                                case "MediaType":
                                    id.MediaType = value;
                                    break;
                                case "Model":
                                    id.Model = value;
                                    break;
                                case "SerialNumber":
                                    id.SerialNumber = value;
                                    break;
                                case "InterfaceType":
                                    id.InterfaceType = value;
                                    break;
                                case "Size":
                                    id.Size = ((Convert.ToDouble(share["Size"]) / 1024 / 1024 / 1024) < 1 ? (Math.Round(Convert.ToDouble(share["Size"]) / 1024 / 1024) + " MB") : (Math.Round(Convert.ToDouble(share["Size"]) / 1024 / 1024 / 1024) + " GB"));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    ids.Add(id);
                }
            }
            catch
            {
            }
            return orderByInterfaceType?ids.OrderBy(a => a.InterfaceType).ToList(): ids;
        }

        public static List<string> GetDiskDriveID()
        { 
            var ids = new List<string>();
            var keys = new List<string>() {
                "MediaType",
                "Model",
                "SerialNumber",
                "InterfaceType", 
                //"Partitions", 
                //"Signature", 
                //"FirmwareRevision", 
                //"TotalCylinders" ,
                //"TotalSectors",
                //"TotalHeads",
                //"TotalTracks"
            };
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                var id = "";
                foreach (var share in searcher.Get())
                {
                    keys.ForEach(a => id += a + " : " + share[a]?.ToString() + System.Environment.NewLine);
                    id += "Size" + " : " + ((Convert.ToDouble(share["Size"]) / 1024 / 1024 / 1024) < 1 ? (Math.Round(Convert.ToDouble(share["Size"]) / 1024 / 1024) + " MB") : (Math.Round(Convert.ToDouble(share["Size"]) / 1024 / 1024 / 1024) + " GB"));
                    ids.Add(id);
                    id = "";
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
