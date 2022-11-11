using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AME
{
    public class Tools
    {
        /// <summary>
        /// 格式化小数
        /// </summary>
        /// <param name="dblAmt">保留几位小数</param>
        /// <param name="intSumDec">格式化的double数值</param>
        /// <returns></returns>
        public static string FormatNumber(object dblAmt, int intSumDec = 2)
        {
            try
            {
                var res = string.Format("{0:0." + new string('0', intSumDec) + "}", Convert.ToDouble(dblAmt));
                return res;
            }
            catch 
            {
                return dblAmt.ToString ();
            }
        }
        public static object 分钟转小时(int mins)
        {
            int hours = mins / 60;
            int minutes = mins - (hours * 60);
            string timeElapsed = Convert.ToString(hours).PadLeft(2, '0') + ":" + Convert.ToString(minutes).PadLeft(2, '0');
            return timeElapsed;
        }
        public static string ConvNow(string data_v)
        {
            return Convert.ToDateTime(data_v).ToString("yyyy-MM-dd hh-mm");
        }
        public static string RandomPassword(int length)
        {

            string PasswordParent = "0123456789ABCDENGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%&*";

            string RetVal = "";

            //通过日期产生随机数
            Random Rnd = new Random(System.DateTime.Now.Millisecond);


            for (int i = 0; i <= length; i++)
            {
                int iRandNum = Rnd.Next(PasswordParent.Length);
                //每次生一个随机数加入数组
                RetVal += PasswordParent.Substring (iRandNum,1);

            }

            return RetVal;

        }
        public static string IsEncryptString(string EncryptString, int offset, bool IsEncrypt)
        {
            string TempString = string.Empty;
            //定義ASCII Code
            Encoding acsii = Encoding.ASCII;
            //取出字串每一字元的ASCII碼
            for (int i = 0; i <= EncryptString.Length - 1; i++)
            {
                int Data = 0;
                //1.取出字元的ASCII碼
                byte[] DataByte = acsii.GetBytes(EncryptString.Substring(i, 1));
                //2.轉成整數
                Data = Convert.ToInt32(DataByte[0]);

                //3.加入偏移數
                if (IsEncrypt)
                {
                    //加密定義偏移數
                    Data = Data + offset;
                }
                else
                {
                    //解密則將偏移數減回來
                    Data = Data - offset;
                }

                //4.轉成Byte
                byte[] DescByte = new byte[1];
                DescByte[0] = Convert.ToByte(Data);
                //5.取出ASCII碼的String
                TempString = TempString + acsii.GetString(DescByte);
            }
            //回傳字串
            return TempString;
        }
        public static string EnCrakPname(string servername)
        {
            if (servername.IndexOf("&pw=") > 0)
            {
                string[] servername1 = new string[3];
                servername1[0] = servername.Substring(0, servername.IndexOf("&pw="));
                //解密 textBox3.Text = IsEncryptString(textBox2.Text, 10, False)
                servername1[1] = servername.Substring(servername.IndexOf("&pw=") + 4, servername.Length - servername.IndexOf("&pw=") - 4);
                servername1[1] = IsEncryptString(servername1[1], 5, false);
                servername = servername1[0] + "&pw=" + servername1[1];
            }
            return servername;
        }
        public static string GetStrMd5Hash(string input)
        {
            //重载函数，此函数返回字符串的 hash 值

            //创建MD5类实例

            MD5 md5Hasher = MD5.Create();

            //将输入内容转换为byte数组并计算其hash值
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            //创建一个 Stringbuilder 存储 bytes
            //并创建一个字符串
            StringBuilder sBuilder = new StringBuilder();

            // 连接每一个 byte 的 hash 码，并格式化为十六进制字符串
            int i = 0;
            for (i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // 返回十六进制字符串
            return sBuilder.ToString();

        }

        //public static string GetFileHMACSHA1(string filePath)
        //{ 
        //    //创建MD5实例
        //    var md5Hasher = HMAC.Create();
 
        //    //以字节形式读取文件
        //    byte[] originalDate = File.ReadAllBytes(filePath);

        //    //计算MD5
        //    byte[] data = md5Hasher.ComputeHash(originalDate);

        //    //创建可变字符串，Imports System.Text
        //    StringBuilder sBuilder = new StringBuilder();

        //    // 连接每一个 byte 的 hash 码，并格式化为十六进制字符串
        //    int i = 0;
        //    for (i = 0; i <= data.Length - 1; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }

        //    return sBuilder.ToString();
        //}

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>返回32位MD5值</returns>
        /// <remarks></remarks>
        public static string GetFileMd5Hash(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    //创建MD5实例
                    MD5 md5Hasher = MD5.Create();

                    //Dim strm As System.IO.Stream
                    FileInfo fInfo = new FileInfo(filePath);
                    long numBytes = fInfo.Length;

                    FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    byte[] originalDate = br.ReadBytes(Convert.ToInt32(numBytes));

                    br.Close();
                    fStream.Close();



                    //计算MD5
                    byte[] data = md5Hasher.ComputeHash(originalDate);

                    //创建可变字符串，Imports System.Text
                    StringBuilder sBuilder = new StringBuilder();

                    // 连接每一个 byte 的 hash 码，并格式化为十六进制字符串
                    int i = 0;
                    for (i = 0; i <= data.Length - 1; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    return sBuilder.ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "False-" + ex.Message;
            }
        }
        //加密
        public static string MD5Crypto2(string key)
        {
            byte[] hash = (new ASCIIEncoding()).GetBytes(key);
            using (var md5 = MD5.Create())
                hash = md5.ComputeHash(hash);
            return System.BitConverter.ToString(hash).Replace("-", ""); 
        }
        //加密
        public static string MD5Crypto(string key)
        {                
            byte[] hash = (new ASCIIEncoding()).GetBytes(key);
            using (var md5 = MD5.Create())   
                hash = md5.ComputeHash(hash);
            return (new ASCIIEncoding()).GetString(hash); 
        }

        public static string 解释JSON(string fjson, string Field)
        {
            if (!string.IsNullOrEmpty(fjson))
            {
                try
                {
                    //方法1和2都可以
                    JObject GetDataArray = (JObject)JsonConvert.DeserializeObject(fjson);
                    if (GetDataArray != null && GetDataArray.HasValues)
                    {
                        JToken x;
                        GetDataArray.TryGetValue(Field,out x);
                        return x?.ToString();
                    }

                    //方法2, 将字符串转换为字段名
                    //var jsons = JsonConvert.DeserializeObject<CategoriesFjson>(fjson);
                    //return typeof(CategoriesFjson).GetField(Field).GetValue(fjson);

                }
                catch 
                {
                }
            }

            return null;
        }

        public static string 毫秒转日常时间格式(double ss)
        {
            return TimeSpan.FromSeconds(ss / 1000).ToString();
        }
        //// 从一个对象信息生成Json串
        //public static string ObjectToJson(object obj)
        //{
        //    return JsonConvert.SerializeObject(obj);
        //}
        //// 从一个Json串生成对象信息
        //public static object JsonToObject(string jsonString, object obj)
        //{
        //    return JsonConvert.DeserializeObject(jsonString, obj.GetType());
        //}
        public static void LogToFile(string Strs, string FileName = "log.txt")
        {
            WriteTxtToFile(System.Environment.CurrentDirectory, FileName, Strs, true);
        }
        /// <summary>
        /// 将文本写入txt文件中
        /// </summary>
        /// <param name="DirPath">文件路径</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="Strs">字符串</param>
        /// <param name="IsCleanFile">是否先清空文件</param>
        public static void WriteTxtToFile(string DirPath, string FileName, string Strs, bool IsCleanFile = false)
        {
            if (string.IsNullOrEmpty(Strs))
                return;
            if (!Directory.Exists(DirPath))  //如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(DirPath);
            }
            FileStream fs = new FileStream((DirPath + "\\" + FileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (System.IO.File.Exists(DirPath + FileName) && IsCleanFile)
            {
                fs.SetLength(0); //先清空文件
            }
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(Strs);   //写入字符串
            sw.Close();
        }
        #region runCmd
        public static string runCmd(string strCMD)
        {
            Process p = new Process();
            var _with1 = p.StartInfo;
            _with1.FileName = "cmd.exe";
            _with1.Arguments = "/c " + strCMD;
            _with1.UseShellExecute = false;
            _with1.RedirectStandardInput = true;
            _with1.RedirectStandardOutput = true;
            _with1.RedirectStandardError = true;
            _with1.CreateNoWindow = true;
            p.Start();
            string result = p.StandardOutput.ReadToEnd();
            p.Close();
            return result;
        }
        #endregion

        #region 反射得到实体类的字段名称和值
        /// <summary>
        /// 反射得到实体类的字段名称和值
        /// var dict = GetProperties(model);
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实例化</param>
        /// <returns></returns>
        public static Dictionary<object, object> GetProperties<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String", StringComparison.CurrentCulture))
                {
                    ret.Add(name, value);
                }
            }
            return ret;
        }
        #endregion

        #region   DataTable利用泛型填充实体类
        /*         
   IList<Model1> t1 = DataTableToList<Model1>(dt);         
*/
        /// <summary>
        /// DataTable利用泛型填充实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="table">dt</param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable table)
        {
            IList<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        //object value = MSCL.Until.IsNullOrDBNull(row[tempName]) ? null : row[tempName];
                        //pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        #endregion
        public static bool IsNumeric(object anyObject, bool 检测负号后缀 = false)
        {
            try
            {
                if (anyObject == null || anyObject is DBNull) return false;
                string anyString = anyObject.ToString();
                if (anyString.Length > 0)
                {
                    if (检测负号后缀 && anyString.EndsWith("-")) return false;
                    double dummyOut = new double();
                    System.Globalization.CultureInfo cultureInfo =
                        new System.Globalization.CultureInfo("en-US", true);

                    return double.TryParse(anyString, System.Globalization.NumberStyles.Any,
                        cultureInfo.NumberFormat, out dummyOut);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static bool IsDate(object anyObject)
        {

            try
            {
                if (anyObject == null || anyObject is DBNull) return false;
                string anyString = anyObject.ToString();
                if (anyString.Length > 0)
                {
                    DateTime dummydate = DateTime.Parse(anyString);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;

        }
        #region   日期转换
        public static string ConvDate(DateTime data_v)
        {
            return data_v.ToString("d");
        }
        public static string ConvDateES(DateTime data_v)
        {
            return ConvDate(data_v);
            //Return String.Format(CDate(data_v), "dd/MM/yyyy")
        }
        public static string NowDate()
        {
            return ConvDate(DateTime.Now);
            //Return String.Format(CDate(data_v), "dd/MM/yyyy")
        }

        #endregion
        #region   汉字转换成拼音
        //比如：“张三100”,可以生成"zs100"
        /// <summary>
        /// 汉字转换成拼音
        /// 比如：“张三100”,可以生成"zs100"
        /// </summary>
        /// <param name="mystr">带中文的字符串</param>
        /// <returns></returns>
        public static string 汉字转换成拼音II(string mystr)
        {
            string functionReturnValue = null;
            int i = 0;
            int J = 0;
            string Pstr = string.Empty;
            string Py = string.Empty;
            int? k = null;
            try
            {
                k = mystr.Length;
                for (J = 1; J <= k; J++)
                {
                    i = Convert.ToChar(mystr.Substring(J - 1, 1));
                    if (i >= -20319 && i <= -20284) Py = "A";
                    else if (i >= -20283 && i <= -19776) Py = "B";
                    else if (i >= -19775 && i <= -19219) Py = "C";
                    else if (i >= -19218 && i <= -18711) Py = "D";
                    else if (i >= -18710 && i <= -18527) Py = "E";
                    else if (i >= -18526 && i <= -18240) Py = "F";
                    else if (i >= -18239 && i <= -17923) Py = "G";
                    else if (i >= -17922 && i <= -17418) Py = "H";
                    else if (i >= -17417 && i <= -16475) Py = "J";
                    else if (i >= -16474 && i <= -16213) Py = "K";
                    else if (i >= -16212 && i <= -15641) Py = "L";
                    else if (i >= -15640 && i <= -15166) Py = "M";
                    else if (i >= -15165 && i <= -14923) Py = "N";
                    else if (i >= -14922 && i <= -14915) Py = "O";
                    else if (i >= -14914 && i <= -14631) Py = "P";
                    else if (i >= -14630 && i <= -14150) Py = "Q";
                    else if (i >= -14149 && i <= -14091) Py = "R";
                    else if (i >= -14090 && i <= -13319) Py = "S";
                    else if (i >= -13318 && i <= -12839) Py = "T";
                    else if (i >= -12838 && i <= -12557) Py = "W";
                    else if (i >= -12556 && i <= -11848) Py = "X";
                    else if (i >= -11847 && i <= -11056) Py = "Y";
                    else if (i >= -11055 && i <= -10247) Py = "Z";
                    else Py = ((char)(i)).ToString();
                    Pstr = Pstr + Py;
                }
                functionReturnValue = Pstr;
            }
            catch (Exception ex)
            {
                DebugLog.Log("转成失败！" + ex.Message);
                return mystr;
            }
            return functionReturnValue;
        }


        /// <summary>
        /// 取汉字拼音的首字母
        /// </summary>
        /// <param name="UnName">汉字</param>
        /// <returns>首字母</returns>
        public static string 汉字转换成拼音(string UnName)
        {
            int i = 0;
            ushort key = 0;
            string strResult = string.Empty;
            Encoding unicode = Encoding.Unicode;
            Encoding gbk = Encoding.GetEncoding("GB2312");
            byte[] unicodeBytes = unicode.GetBytes(UnName);
            byte[] gbkBytes = Encoding.Convert(unicode, gbk, unicodeBytes);
            while (i < gbkBytes.Length)
            {
                if (gbkBytes[i] <= 127)
                {
                    strResult = strResult + (char)gbkBytes[i];
                    i++;
                }
                #region 生成汉字拼音简码,取拼音首字母
                else
                {
                    key = (ushort)(gbkBytes[i] * 256 + gbkBytes[i + 1]);
                    if (key >= '\uB0A1' && key <= '\uB0C4')
                    {
                        strResult = strResult + "A";
                    }
                    else if (key >= '\uB0C5' && key <= '\uB2C0')
                    {
                        strResult = strResult + "B";
                    }
                    else if (key >= '\uB2C1' && key <= '\uB4ED')
                    {
                        strResult = strResult + "C";
                    }
                    else if (key >= '\uB4EE' && key <= '\uB6E9')
                    {
                        strResult = strResult + "D";
                    }
                    else if (key >= '\uB6EA' && key <= '\uB7A1')
                    {
                        strResult = strResult + "E";
                    }
                    else if (key >= '\uB7A2' && key <= '\uB8C0')
                    {
                        strResult = strResult + "F";
                    }
                    else if (key >= '\uB8C1' && key <= '\uB9FD')
                    {
                        strResult = strResult + "G";
                    }
                    else if (key >= '\uB9FE' && key <= '\uBBF6')
                    {
                        strResult = strResult + "H";
                    }
                    else if (key >= '\uBBF7' && key <= '\uBFA5')
                    {
                        strResult = strResult + "J";
                    }
                    else if (key >= '\uBFA6' && key <= '\uC0AB')
                    {
                        strResult = strResult + "K";
                    }
                    else if (key >= '\uC0AC' && key <= '\uC2E7')
                    {
                        strResult = strResult + "L";
                    }
                    else if (key >= '\uC2E8' && key <= '\uC4C2')
                    {
                        strResult = strResult + "M";
                    }
                    else if (key >= '\uC4C3' && key <= '\uC5B5')
                    {
                        strResult = strResult + "N";
                    }
                    else if (key >= '\uC5B6' && key <= '\uC5BD')
                    {
                        strResult = strResult + "O";
                    }
                    else if (key >= '\uC5BE' && key <= '\uC6D9')
                    {
                        strResult = strResult + "P";
                    }
                    else if (key >= '\uC6DA' && key <= '\uC8BA')
                    {
                        strResult = strResult + "Q";
                    }
                    else if (key >= '\uC8BB' && key <= '\uC8F5')
                    {
                        strResult = strResult + "R";
                    }
                    else if (key >= '\uC8F6' && key <= '\uCBF9')
                    {
                        strResult = strResult + "S";
                    }
                    else if (key >= '\uCBFA' && key <= '\uCDD9')
                    {
                        strResult = strResult + "T";
                    }
                    else if (key >= '\uCDDA' && key <= '\uCEF3')
                    {
                        strResult = strResult + "W";
                    }
                    else if (key >= '\uCEF4' && key <= '\uD188')
                    {
                        strResult = strResult + "X";
                    }
                    else if (key >= '\uD1B9' && key <= '\uD4D0')
                    {
                        strResult = strResult + "Y";
                    }
                    else if (key >= '\uD4D1' && key <= '\uD7F9')
                    {
                        strResult = strResult + "Z";
                    }
                    else
                    {
                        strResult = strResult + "?";
                    }
                    i = i + 2;
                }
                #endregion
            }
            return strResult;
        }


        #endregion
        #region  将 Stream 转成 byte[] 
        public static byte[] StreamToBytes(Stream stream)
        {
            if (stream == null) return null;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        #endregion
        #region   将 byte[] 转成 Stream 
        public static Stream BytesToStream(byte[] bytes)
        {
            if (bytes == null) return null;
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion
        #region 将 Stream 写入文件

        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        public static void BytesToFile(byte[] bytes, string fileName)
        {
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        #endregion
        #region 从文件读取 Stream

        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion

        #region 从文件读取 Stream

        public static Stream FileToMemoryStream(string fileName)
        {
            if (File.Exists(fileName))
            {
                //Console.WriteLine($"图片缓存存在: {photo}"); 
                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(memoryStream);
                }
                memoryStream.Position = 0;
                return memoryStream;
                //return ImageSource.FromFile(photo);
            }
            return null;
        }

#if NET461
        public static System.Drawing.Image FileToImage(string fileName)
        {
            var stream = FileToMemoryStream(fileName);
            return stream == null ? null : System.Drawing.Image.FromStream(stream);
        }
        
 
        /// <summary>
        /// 将图片进行反色处理
        /// </summary>
        /// <param name="mybm">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        /// <returns>被反色后的图片</returns>
        public static Bitmap RePic(Bitmap mybm, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);//初始化一个记录处理后的图片的对象
            int x, y, resultR, resultG, resultB;
            Color pixel;

            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    pixel = mybm.GetPixel(x, y);//获取当前坐标的像素值
                    resultR = 255 - pixel.R;//反红
                    resultG = 255 - pixel.G;//反绿
                    resultB = 255 - pixel.B;//反蓝
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }

            return bm;//返回经过反色处理后的图片
        }
#endif

        #endregion
        public static bool networkUp()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }


        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string String2Unicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }


        public static string ChangeLan(string text)
        {
            byte[] bs = Encoding.GetEncoding("latin1").GetBytes(text);
            bs = Encoding.Convert(Encoding.GetEncoding("latin1"), Encoding.GetEncoding("gbk"), bs);
            return Encoding.GetEncoding("gbk").GetString(bs);
        }


        public static string LanChange(string str)
        {
            Encoding utf8;
            Encoding gb2312;
            utf8 = Encoding.GetEncoding("ASCII");
            gb2312 = Encoding.GetEncoding("GB2312");
            byte[] gb = gb2312.GetBytes(str);
            gb = Encoding.Convert(gb2312, utf8, gb);
            return utf8.GetString(gb);
        }

        public static string GBKToUTF8(string txt)
        {
            byte[] buffer = Encoding.Default.GetBytes(txt);
            byte[] buffers = Encoding.Convert(Encoding.Default, Encoding.UTF8, buffer);
            return Encoding.UTF8.GetString(buffers);
        }

        public static string TransferStr(string str, Encoding originalEncode, Encoding targetEncode)
        {
            try
            {
                byte[] unicodeBytes = originalEncode.GetBytes(str);
                byte[] asciiBytes = Encoding.Convert(originalEncode, targetEncode, unicodeBytes);
                char[] asciiChars = new char[targetEncode.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                targetEncode.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                string result = new string(asciiChars);
                return result;
            }
            catch
            {
                Console.WriteLine("There is an exception.");
                return "";
            }
        }
        public static string ToBase64Encode(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        

        public static void WriteLine(string text, ConsoleColor backgroundColor)
        {
            try //#643
            {
                var bgcolor = Console.BackgroundColor;
                var forecolor = Console.ForegroundColor;
                Console.BackgroundColor = backgroundColor;

                switch (backgroundColor)
                {
                    case ConsoleColor.Yellow:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleColor.DarkGreen:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
                Console.Write(text);
                Console.BackgroundColor = bgcolor;
                Console.ForegroundColor = forecolor;
                Console.WriteLine();
            }
            catch
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine(text);
                }
                catch { }
            }
        }

        // Do this when you start your application
        static public int mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

        /// <summary>
        /// 判断当前线程是否为主线程<para></para>
        /// If called in the non main thread, will return false;
        /// </summary>
        public static bool IsMainThread
        {
            get { return System.Threading.Thread.CurrentThread.ManagedThreadId == mainThreadId; }
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
#if NET6_0_OR_GREATER
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
#endif

            }
        }
        public static string FormatUrl(string sdfd)
        {
            string se = @"((http|https)://)?(www.)?[a-z0-9\.]+(\.(com|es|net|cn|com\.cn|com\.net|net\.cn))(/[^\s\n]*)?";
            Regex rg = new Regex(se);
            string link = rg.Match(sdfd).Value;
            return link;
        }



    }

 

}

namespace DirectoryGet
{
    public class Library : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return $"{Environment.CurrentDirectory}";
            yield return $"{Directory.GetCurrentDirectory()}";
            yield return $"{GetType().Assembly.Location}";
            yield return $"{Process.GetCurrentProcess().MainModule.FileName}";
            yield return $"{AppDomain.CurrentDomain.BaseDirectory}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}