using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AME.Util
{
    public static class HttpClientUtils
    {
        /// <summary>
        /// api 在winform中的get 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<T> JsonObject<T>(Uri url)
        {

            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(url).Result;

                List<T> ds = JsonConvert.DeserializeObject<List<T>>(result);
                return ds;
            }

        }

        public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
        {
            using (var s = await client.GetStreamAsync(uri))
            {
                using (var fs = new FileStream(FileName, FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        } 
        public static async Task<string> DownloadStringAsync(Uri uri)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(uri);
        } 

        public static async Task<string> DownloadStringAsync(string url)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(url);
        } 
        public static string DownloadString(string url)
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(url).Result;
                return result;
            }
        }
    }
}
