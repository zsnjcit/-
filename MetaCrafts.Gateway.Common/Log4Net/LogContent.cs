using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MetaCrafts.Gateway.Common
{
    /// <summary>
    /// 包含了所有的自定字段属性
    /// </summary>
    public class LogContent
    {
        /// <summary>
        /// 日志分类号
        /// </summary>
        public int EventCategoryID { get; set; }
        /// <summary>
        /// 日志分类描述
        /// </summary>
        public string EventCategoryName { get; set; }
        /// <summary>
        /// 操作类型ID
        /// </summary>
        public int ActionCategoryID { get; set; }
        /// <summary>
        /// 操作类型描述
        /// </summary>
        public string ActionCategoryName { get; set; }
        /// <summary>
        /// 日志描述信息
        /// </summary>
        public string Description { get; set; }

    }


    public enum EnumEventCategory
    {
        [Description("无")]
        None,
        [Description("网站")]
        WebSite,
        [Description("Windows服务")]
        WindowsService,
        [Description("Web服务")]
        WebService,
        /*具体*/
        [Description("MetaSMS短信猫收发服务")]
        SmsService,
        [Description("MetaIIA图像智能分析服务")]
        IIAService,
    }

    public enum EnumActionCategory
    {
        [Description("无")]
        None,
        [Description("停止操作")]
        STOP,
        [Description("恢复操作")]
        RESUME,
        [Description("运行操作")]
        RUN,
        [Description("暂停操作")]
        PAUSE,
        [Description("重启操作")]
        RESTART,
        [Description("新增操作")]
        CREATE,
        [Description("修改操作")]
        MODIFY,
        [Description("删除操作")]
        DELETE,
        [Description("获取操作")]
        GAIN,
        [Description("初始化")]
        INITIALIZE,
        [Description("资源释放")]
        RELEASE,
        [Description("卸载任务")]
        UNLOAD,
        [Description("节点异常")]
        NODEERROR
    }
}