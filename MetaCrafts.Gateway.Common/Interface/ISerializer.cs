/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:30:36
 * 文 件 名：ISerializer
 * 
 * 描述说明：
 *
 * 接口备注：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司  2017 All rights reserved
*****************************************************************/


using System;

namespace MetaCrafts.Gateway.Common
{
    public interface ISerializer
    {
        /// <summary>
        /// 序列化为二进制
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        byte[] Serialize(object model);

        /// <summary>
        /// 反序列化为实体
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        object Deserialize(byte[] bytes, Type type);
    }
}
