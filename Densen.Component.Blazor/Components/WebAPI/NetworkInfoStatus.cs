// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************


using System;
using System.ComponentModel;

namespace AmeBlazor.Components
{

    /// <summary>
    /// 网络信息类
    /// </summary>
    public class NetworkInfoStatus
    {

        /// <summary>
        /// 连接方式
        /// </summary>
        /// <returns></returns>
        [DisplayName("连接方式")]
        public string? type { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("类型")]
        public string? effectiveType { get; set; }

        /// <summary>
        /// 下行Mb/s
        /// </summary>
        /// <returns></returns>
        [DisplayName("下行[Mb/s]")]
        public decimal? downlink { get; set; }

        /// <summary>
        /// 最大下行Mb/s
        /// </summary>
        /// <returns></returns>
        [DisplayName("最大下行[Mb/s]")]
        public decimal? downlinkMax { get; set; }

        /// <summary>
        /// RTT[ms]
        /// </summary>
        /// <returns></returns>
        [DisplayName("RTT[ms]")]
        public int? rtt { get; set; }

        public bool saveData { get; set; }

    }
}
