/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 15:01:53
 * 文 件 名：BasePacket
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
using NetworkSocket;
using System;

namespace MetaCrafts.Gateway.Socket
{
    public class BasePacket : PacketBase
    {
        /// <summary>
        /// 低位在前，高位在后      
        /// </summary>
        private const bool LITTLE_ENDIAN = false;
        /// <summary>
        /// 高位在前，低位在后   
        /// </summary>
        private const bool BIG_ENDIAN = !LITTLE_ENDIAN;
        /// <summary>
        ///  主机发送的帧头
        /// </summary>
        private const byte SendHeadByte = 0xBB ;
        /// <summary>
        ///  主机发送的帧尾
        /// </summary>
        private const byte SendEndByte = 0x66;
        /// <summary>
        /// 主机接收的帧头
        /// </summary>
        private const byte ReciveHeadByte = 0xAA;
        /// <summary>
        /// 主机接收的帧尾
        /// </summary>
        private const byte ReciveEndByte = 0x55 ;

        /// <summary>
        /// 产品代码
        /// </summary>
        public EnumHostCode HostCodeByte { get; private set; }
        /// <summary>
        /// 通讯类型
        /// </summary>
        public EnumUDPType UDPTypeByte { get; private set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public EnumPacketType PacketTypeByte { get; private set; }

        private byte[] IPAddress = new byte[4];

        /// <summary>
        /// 数据体的数据
        /// </summary>
        public byte[] Body { get; private set; }
        /// <summary>
        /// 通讯协议的封包
        /// </summary>
        public BasePacket()
        {

        }
        public BasePacket(EnumPacketType type, byte[] body)
        {
            this.PacketTypeByte = type;
            this.Body = body;
        }
        /// <summary>
        /// 通讯协议的封包
        /// </summary>
        /// <param name="body">数据体</param>
        public BasePacket(EnumPacketType type,byte[] ipAddress, byte[] body)
        {
            this.PacketTypeByte = type;
            this.IPAddress = ipAddress;
            this.Body = body;
        }

        public override byte[] ToByteArray()
        {
            const int headLength = 13; //13+N
            // 总长度
            int totalLength = this.Body == null ? headLength : headLength + this.Body.Length;
            var builder = new ByteBuilder(totalLength);

            builder.Add(SendHeadByte);
            builder.Add(ByteConverter.ToBytes((short)HostCodeByte, BIG_ENDIAN));//转换成2个byte位
            builder.Add((byte)UDPTypeByte);
            builder.Add((byte)PacketTypeByte);
            builder.Add(ByteConverter.ToBytes(this.Body.Length, BIG_ENDIAN));
            builder.Add(this.Body);
            builder.Add(IPAddress);
            builder.Add(GetCheckByte(builder.ToArray()));
            builder.Add(SendEndByte);

            return builder.Source;
        }
        /// <summary>
        ///  将参数序列化并写入为Body
        /// </summary>
        /// <param name="parameters"></param>
        public void SetBodyBinary(ISerializer serializer, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return;
            }
            var builder = new ByteBuilder(8);
            foreach (var item in parameters)
            {
                // 序列化参数为二进制内容
                var paramBytes = serializer.Serialize(item);
                // 添加参数内容长度            
                builder.Add(paramBytes == null ? 0 : paramBytes.Length, BIG_ENDIAN);
                // 添加参数内容
                builder.Add(paramBytes);
            }
            this.Body = builder.ToArray();
        }
        /// <summary>
        /// 将二进制消息体转序列化成实体对象
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public virtual object GetBodyEntity<T>(ISerializer serializer)
        {
            if (this.Body == null || this.Body.Length < 4)
            {
                return null;
            }
            return serializer.Deserialize(this.Body, typeof(T));
        }

      //  public virtual object GetBodyEntity(ISerializer serializer) { return null; }

        /// <summary>
        /// 解析一个数据包       
        /// 不足一个封包时返回null
        /// </summary>
        /// <param name="builder">接收到的历史数据</param>
        /// <returns></returns>
        public static BasePacket GetPacket(ByteBuilder builder)
        {
            BasePacket basePacket = null;

            // 包长
            int totalLength = builder.Length;
            // 不会少于13
            if (totalLength < 13)
            {
                return basePacket;
            }
            // 头尾校验
            if (ReciveHeadByte != builder.ToByte(0) || ReciveEndByte != builder.ToByte(totalLength - 1))
            {
                return basePacket;
            }

            try
            {
                EnumPacketType PacketType = (EnumPacketType)Enum.ToObject(typeof(EnumPacketType), builder.ToByte(4));

                byte[] ipAddress = builder.ToArray(totalLength - 6, 4);
                byte checkByte = builder.ToByte(totalLength - 2);//检验位
                if (checkByte != GetCheckByte(builder.ToArray(0, totalLength - 2)))
                {
                    return null;
                }
                int bodyLength = ByteConverter.ToInt32(builder.ToArray(5, 2), 0,BIG_ENDIAN);

                byte[] body = builder.ToArray(7, bodyLength);

                // 清空本条数据
                builder.Remove(totalLength);

                basePacket = new BasePacket(PacketType, ipAddress, body);
            }
            catch (Exception ex)
            {
                throw new Exception("数据包解析出错", ex);
            }
            return basePacket;
        }
        /// <summary>
        /// 获取校验字
        /// </summary>
        /// <param name="allByte"></param>
        /// <returns></returns>
        private static byte GetCheckByte(byte[] allByte)
        {
            int x = 0;
            for (int i = 0; i < allByte.Length; i++)
                x += allByte[i];
            return (byte)(x & 0x000000FF);
        }

    }
}
