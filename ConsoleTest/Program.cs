using AME;
using static AME.Enums;
using static AME.EnumExtensions;
using System;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var CurrentDirectory = "";

            //var FileNames2 = CurrentDirectory.GetDirFiles("*.*");
            //FileNames2 = "".GetDirFiles();
            //FileNames2 = "F:\\Repos\\wwwroot\\uploads\\demo\\Product".GetDirFiles(exclude: "chk");
            //foreach (string file in FileNames2)
            //{
            //    Console.WriteLine(file);
            //}
            //Console.WriteLine("-------------- \n------------");

            var enums = "SectionType";
            var xxx= typeof(SectionType);
            var res3 =typeof(销售单状态_销售经营历程_补充).GetEnumValueAndDescriptions();
            var res = GetEnumValueAndDescriptionsFromEnumsName("销售登记表付款方式", assemblyString: "AME.API");
            var res1 = GetValuesFromEnumsName(enums, assemblyString: "AME.API");

        }
    }
}
