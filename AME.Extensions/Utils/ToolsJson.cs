// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace AME;

public class ToolsJson
{

    public delegate void SyncLogHandler(string status);
    // 基于上面的委托定义事件
    public event SyncLogHandler SyncEventLog;
    protected void OnBoilerEventLog(string message)
    {
        SyncEventLog?.Invoke(message);
    }

    public string 处理数据(int WxDto, string WxAPI, string conString = null)
    {
        var message = "";
        try
        {

            string fjson = HttpClientUtils.DownloadString("https://" + WxAPI + ".firebaseio.com/users.json");

            if (!string.IsNullOrEmpty(fjson))
            {

                JObject GetDataArray = (JObject)JsonConvert.DeserializeObject(fjson);
                if (GetDataArray != null && GetDataArray.HasValues)
                {
                    foreach (var json in GetDataArray)
                    {
                        ClassB2T account = JsonConvert.DeserializeObject<ClassB2T>(json.Value.ToString());
                        //listB2T.Add(new ClassB2TLite
                        //{
                        //    用户名 = account.username ?? "",
                        //    编号 = json.Key.ToString(),
                        //    ConsumerOnOrder = account.ConsumerOnOrderInt
                        //});
                    }

                    //message = 处理客户信息(conString, listB2T, WxDto, WxAPI);
                    OnBoilerEventLog(message);

                }
            }

        }
        catch (Exception ex)
        {
            message = $"出错 : {ex.Message}";
            OnBoilerEventLog(message);
        }
        return message;

    }

}


public class ClassB2T
{
    public int ConsumerOnOrderInt
    {
        get
        {
            try
            {
                decimal x = Convert.ToDecimal(ConsumerOnOrder);
                return Convert.ToInt32(x);
            }
            catch
            {
                return 0;
            }
        }
    }

    public string ConsumerOnOrder { get; set; } = "0";
    public string email { get; set; }
    public string photoURL { get; set; }
    public string type { get; set; }
    public string username { get; set; }
}
