using System;
using System.Collections.Generic;
using System.IO;
#if !NET461 
using System.Net.Http;
#endif
using System.Text;
using System.Threading.Tasks;

namespace AME.Util
{
    public static class HttpClientUtils
    {
#if !NET461 
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
#endif
    }
}
