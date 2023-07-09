using System;
using System.Linq;

namespace AME
{
    public partial class ConventUtil
    {
        //var str = @"C:\Users\Alex\Documents\Tencent Files\239548611\FileRecv\刪除面.mac";
        //var files = File.ReadAllBytes(str);
        //ConventUtil.IsGB2312(files);

        public static bool IsGB2312(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length ; i+=2)
            {
                var x = bytes.Skip(i).Take(2).ToArray();
                IsGB23122(x);
            }
            return true;
        }
        public static  bool IsGB23122(byte[] bytes)
        {
            //在C#中，可以使用System.Text.Encoding类来判断一个中文字符串的编码是BIG5还是GB2312。具体实现方法如下：

            //1. 首先将中文字符串转换为字节数组：

            //byte[] bytes = System.Text.Encoding.Default.GetBytes(str);

            //2. 判断字节数组的前两个字节是否符合BIG5编码或GB2312编码的规范：

            if (bytes[0] >= 0x81 && bytes[0] <= 0xFE && bytes[1] >= 0x40 && bytes[1] <= 0x7E || bytes[1] >= 0xA1 && bytes[1] <= 0xFE)
            {
                // 是BIG5编码
                Console.WriteLine("是BIG5编码");

            }
            else if (bytes[0] >= 0xB0 && bytes[0] <= 0xF7 && bytes[1] >= 0xA1 && bytes[1] <= 0xFE)
            {
                // 是GB2312编码
                Console.WriteLine("是GB2312编码");
            }
            else
            {
                // 不是BIG5编码也不是GB2312编码
                Console.WriteLine("其他编码");
            }

            //以上代码中，判断BIG5编码的规范是第一个字节在0x81到0xFE之间，第二个字节在0x40到0x7E之间或在0xA1到0xFE之间；判断GB2312编码的规范是第一个字节在0xB0到0xF7之间，第二个字节在0xA1到0xFE之间。如果不符合以上两种编码的规范，则认为不是BIG5编码也不是GB2312编码。

            return true;
        }
    }
}
