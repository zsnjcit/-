/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 15:16:09
 * 文 件 名：Class1
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/


namespace MetaCrafts.Gateway.Model
{
    /// <summary>
    /// 产品代码
    /// </summary>
    public enum EnumHostCode : byte
    {
        /// <summary>
        /// 海康
        /// </summary>
        补光灯HK_SJM2 = 0x00A0,
        /// <summary>
        /// 藏愚
        /// </summary>
        智能电源CMFU2 = 0x00FF,
        /// <summary>
        /// 立地
        /// </summary>
        智能电箱ONT52 = 0x00A2,
    }
    /// <summary>
    /// 通讯类型
    /// </summary>
    public enum EnumUDPType : byte
    {
        直接通讯 = 0x00,
        网关通讯 = 0x01
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EnumPacketType : byte
    {
        数据上报 = 0x01,
        时间设置 = 0xB2,
        亮度设置 = 0xB6,
        灯自动开关时间配置 = 0xBA
    }

}
