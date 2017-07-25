/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/24 星期一 17:16:14
 * 文 件 名：ConfigurationHelper
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCrafts.Gateway.Redis
{
    public static class ConfigurationHelper
    {
        internal static T Get<T>(string appSettingsKey, T defaultValue)
        {
            string text = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;
            try
            {
                var value = Convert.ChangeType(text, typeof(T));
                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
