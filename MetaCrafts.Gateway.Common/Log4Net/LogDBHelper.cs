using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MetaCrafts.Gateway.Common.Helper;
namespace MetaCrafts.Gateway.Common.Log4Net
{
    public static class LogDBHelper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("DBSysLog");
        private static bool IsLoadConfig = false;

        static LogDBHelper()
        {
            SetConfig();
        }

        private static void SetConfig()
        {
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(
            //      new FileInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(FileHelper.GetAbsolutePath("\\Log4net.config")));
            IsLoadConfig = true;
        }

        public static void Info(EnumEventCategory Category, string strInfoMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(new LogContent { ActionCategoryID = (int)EnumActionCategory.None, ActionCategoryName = EnumActionCategory.None.ToString(), Description = strInfoMsg, EventCategoryID = (int)Category, EventCategoryName = Category.ToString() });
            }
        }

        public static void Info(EnumEventCategory Category, EnumActionCategory eAction, string strInfoMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(new LogContent { ActionCategoryID = (int)eAction, ActionCategoryName = eAction.ToString(), Description = strInfoMsg, EventCategoryID = (int)Category, EventCategoryName = Category.ToString() });
            }
        }

        public static void Warn(EnumEventCategory Category, EnumActionCategory eAction, string strWarnMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(new LogContent { ActionCategoryID = (int)eAction, ActionCategoryName = eAction.ToString(), Description = strWarnMsg, EventCategoryID = (int)Category, EventCategoryName = Category.ToString() });
            }
        }

        public static void Error(EnumEventCategory Category, EnumActionCategory eAction, Exception ex, string strErrorMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }

            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(new LogContent { ActionCategoryID = (int)eAction, ActionCategoryName = eAction.ToString(), Description = strErrorMsg, EventCategoryID = (int)Category, EventCategoryName = Category.ToString() }, ex);
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
                loginfo.Debug(new LogContent { ActionCategoryID = (int)EnumActionCategory.None, ActionCategoryName = EnumActionCategory.None.ToString(), Description = strDebugMsg, EventCategoryID = (int)EnumEventCategory.None, EventCategoryName = EnumEventCategory.None.ToString() });
            }
        }

        public static void Debug(EnumEventCategory Category, EnumActionCategory eAction, string strDebugMsg)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }

            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(new LogContent { ActionCategoryID = (int)eAction, ActionCategoryName = eAction.ToString(), Description = strDebugMsg, EventCategoryID = (int)Category, EventCategoryName = Category.ToString() });
            }
        }
    }
}