using AME;
using System;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var CurrentDirectory = "";

            var FileNames2 = CurrentDirectory.GetDirFiles("*.*");
            FileNames2 = "".GetDirFiles();
            FileNames2 = "F:/Repos/wwwroot/uploads/demo/Product".GetDirFiles(include: "chk");
            foreach (string file in FileNames2)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("-------------- \n------------");
            FileNames2 = "F:\\Repos\\wwwroot\\uploads\\demo\\Product".GetDirFiles(include: "\\*1.jpg");
            foreach (string file in FileNames2)
            {
                Console.WriteLine(file);
            }
        }
    }
}
