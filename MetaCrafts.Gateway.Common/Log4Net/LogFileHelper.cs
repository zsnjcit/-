using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCrafts.Gateway.Common.Log4Net
{
    public static class LogFileHelper
    {
        static LogFileHelper()
        {
            SetConfig();
        }
        static EnumEventCategory Category = EnumEventCategory.WindowsService;
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("TextLocalLog");
        private static bool IsLoadConfig = false;
        private static void SetConfig()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(
                  new FileInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Log4net.config"));
            IsLoadConfig = true;
        }


        public static void Info(string strInfoMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(strInfoMsg);
            }
        }

        public static void Warn(string strWarnMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(strWarnMsg);
            }
        }

        public static void Error(Exception ex, string strErrorMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }

            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(strErrorMsg,ex);
            }
        }

        public static void Debug(string strDebugMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }

            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(strDebugMsg);
            }
        }
    }
}