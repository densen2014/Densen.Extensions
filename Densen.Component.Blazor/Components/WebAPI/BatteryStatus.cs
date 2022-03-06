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
    /// 电池信息类
    /// </summary>
    public class BatteryStatus
    {

        /// <summary>
        /// 正在充电
        /// </summary>
        /// <returns></returns>
        [DisplayName("正在充电")]
        public bool charging { get; set; }

        /// <summary>
        /// 电池充满时间[秒]
        /// </summary>
        /// <returns></returns>
        [DisplayName("电池充满时间[秒]")]
        public int? chargingTime { get; set; }

        /// <summary>
        /// 电量剩余[秒]
        /// </summary>
        /// <returns></returns>
        [DisplayName("电量剩余[秒]")]
        public int? dischargingTime { get; set; }

        /// <summary>
        ///电量等级%
        /// </summary>
        [DisplayName("电量等级%")]
        public int level { get; set; }

    }
}
