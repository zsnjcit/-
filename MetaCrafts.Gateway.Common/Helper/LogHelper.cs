using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaCrafts.Gateway.Common.Log4Net;

namespace MetaCrafts.Gateway.Common
{
    public class LogHelper
    {
        static EnumEventCategory Category = EnumEventCategory.WindowsService;
        public static void WriteLog(EnumActionCategory eAction, string info)
        {
            LogFileHelper.Info(info);
            LogDBHelper.Info(Category, eAction, info);
        }

        public static void WriteLog(string info)
        {
            LogFileHelper.Info(info);
            LogDBHelper.Info(Category,info);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(EnumActionCategory eAction, Exception se, string info)
        {
            LogFileHelper.Error(se, info);
            LogDBHelper.Error(Category, eAction, se, info);
        }

        public static void WriteLog(string info, Exception se)
        {
            LogFileHelper.Error(se, info);
            LogDBHelper.Error(Category, EnumActionCategory.None, se, info);
        }

    }
}
