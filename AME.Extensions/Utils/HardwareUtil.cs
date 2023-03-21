using System;
using System.Collections.Generic;
using System.Linq;
#if NET48 || WINDOWS
using System.Management;
#endif

namespace AME.Util
{
    /// <summary>
    /// 硬件工具类
    /// </summary>
    public static class HardwareUtil
    {

#if NET48 || WINDOWS 
        public static string GetFirstPhysicalMediaID()=> GetPhysicalMediaID().FirstOrDefault(); 
        public static string GetFirstMacAddress()=> GetMacAddress().FirstOrDefault()?.MACAddress;

        public class NetworkAdapter
        {
            public string MACAddress { get; set; }
            public string Manufacturer { get; set; } 
            public bool PhysicalAdapter { get; set; } 
        }

        public static List<NetworkAdapter> GetMacAddress(string filter= "Microsoft", string filter2= "PhysicalAdapter=true", string filter3 = "Manufacturer <> 'TeamViewer Germany GmbH'")
        {
            var filters = $"(MACAddress Is Not NULL) {(filter.Length > 0 ? $"AND (Manufacturer <> '{filter}')" : "")} {(filter2.Length > 0 ? $"AND ({filter2})" : "")} {(filter3.Length > 0 ? $"AND ({filter3})" : "")}";
            var searcher = new ManagementObjectSearcher($"SELECT MACAddress,Manufacturer,PhysicalAdapter FROM Win32_NetworkAdapter WHERE ({filters})");
            var items = searcher.Get().Cast<ManagementObject>().
                Select(x=> new NetworkAdapter()
                    {
                        MACAddress = x["MACAddress"]?.ToString(),
                        Manufacturer = x["Manufacturer"]?.ToString(),
                        PhysicalAdapter = x["PhysicalAdapter"]?.ToString()=="true",
                    }
                ).ToList();
            items= items.Where (x=>!string.IsNullOrWhiteSpace(x.MACAddress)).ToList(); 
            return items;
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
                var items = searcher.Get().Cast<ManagementObject>().Select(x => x["SerialNumber"]?.ToString()).ToList();
                items = items.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                return items;
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

        public static List<DiskDriveID> GetDiskDriveIDS(bool orderByInterfaceType=true, bool filter = true)
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
                        if (filter)
                        {
                            if (share[key] != null && !string.IsNullOrEmpty(share[key].ToString()))
                            {
                                GetSerialNumber(id, share, key);
                            }
                        }
                        else
                        {
                            GetSerialNumber(id, share, key);
                        }
                    }
                    if (!filter || id.SerialNumber !=null) ids.Add(id);
                }
            }
            catch
            {
            }
            return orderByInterfaceType?ids.OrderBy(a => a.InterfaceType).ToList(): ids;
        }

        private static void GetSerialNumber(DiskDriveID id, ManagementBaseObject share, string key)
        {
            var value = share[key]?.ToString()?.Trim();
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
                var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_DiskDrive");
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
