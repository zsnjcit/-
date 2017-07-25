/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:31:15
 * 文 件 名：PowerIISerialize
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using MetaCrafts.Gateway.Common;
using MetaCrafts.Gateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MetaCrafts.Gateway.Socket.Application
{
    internal sealed class PowerIISerialize : ISerializer
    {
        /// <summary>
        /// 序列化为二进制
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public byte[] Serialize(object model)
        {
            if (model == null)
            {
                return null;
            }
            return Encoding.UTF8.GetBytes(model.ToString());
        }

        /// <summary>
        /// 反序列化为实体
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public object Deserialize(byte[] bytes, Type type)
        {
            if (bytes == null || bytes.Length == 0 || type == null)
            {
                return null;
            }
            try
            {
                var strMsg = Encoding.UTF8.GetString(bytes);
                //switch (strMsg)
                //{
                //    case "Set Socket ok"://继电器端口设置成功

                //        break;
                //    case "Set Time ok"://时间设置成功

                //        break;
                //    case "Set Task ok"://时控设置成功

                //        break;
                //    case "Set Task error"://时控设置失败

                //        break;
                //    case "Flush TaskList ok"://时控设置提交成功

                //        break;
                //    case "gate:open": //电源门禁开启 

                //        break;
                //    case "gate:close": //电源门禁关闭

                //        break;
                //    case "water sensor:warning"://水浸警告

                //        break;
                //    case "water sensor:off"://水浸恢复正常

                //        break;
                //    default://实时数据

                //        break;
                //}



                List<string> list = new List<string>(strMsg.Split(new string[] { " " }, StringSplitOptions.None));

                if (list == null || list.Count < 15)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("接收消息：" + strMsg);
                    return null;
                }
                else
                {
                    return InitEntity<PowerIIEntity>(list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据实体自定义特性实例化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private T InitEntity<T>(List<string> values)
        {
            Type type = typeof(T);
            T obj = (T)type.FastNew();
            PropertyInfo[] properties = type.GetCanWritePropertyInfo();
            foreach (string item in values)
            {
                string[] value = item.Split(':');
                if (value == null || value.Length != 2)
                    continue;

                PropertyInfo pInfo = properties.FirstOrDefault(e =>
                {
                    SokectCodeAttribute attr = e.GetCustomAttribute<SokectCodeAttribute>();
                    if (attr != null && attr.IsEnable)
                    {
                        return e.GetCustomAttribute<SokectCodeAttribute>().CodeName.Equals(value[0]);
                    }
                    return false;
                });

                if (pInfo != null)
                {
                    Type ptype = pInfo.PropertyType;
                    if (!ptype.IsEnum)
                    {
                        pInfo.FastSetValue(obj, Convert.ChangeType(value[1], ptype));
                    }
                    else
                    {
                        pInfo.FastSetValue(obj, Enum.ToObject(ptype, value[1]));
                    }
                }
            }
            return obj;
        }
    }
}
