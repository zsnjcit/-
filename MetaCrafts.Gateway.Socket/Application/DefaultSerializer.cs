/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:39:07
 * 文 件 名：DefaultSerializer
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using MetaCrafts.Gateway.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetaCrafts.Gateway.Socket.Application
{
    /// <summary>
    /// 默认提供的Json序列化工具
    /// </summary>
    internal sealed class DefaultSerializer : ISerializer
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
            var strMsg = Encoding.UTF8.GetString(bytes);
            List<string> list = new List<string>(strMsg.Split(new string[] { " " }, StringSplitOptions.None));
            return list;
        }
    }
}
