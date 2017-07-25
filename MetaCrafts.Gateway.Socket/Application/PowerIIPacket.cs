/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:41:44
 * 文 件 名：PowerIIPacket
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
using System.Collections.Generic;

namespace MetaCrafts.Gateway.Socket.Application
{
    public class PowerIIPacket : PacketBase
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
        /// 获取命令值,值为 0xff(255) 表示普通数据， 0xf(254) 表示为命令
        /// </summary>
        public int Command { get; private set; }
        /// <summary>
        /// 获取数据体的数据
        /// </summary>
        public byte[] Body { get; private set; }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        public int HashCode { get; private set; }
        /// <summary>
        /// 通讯协议的封包
        /// </summary>
        public PowerIIPacket(int cmd)
        {
            this.Command = cmd;
            this.HashCode = Guid.NewGuid().GetHashCode();
        }

        /// <summary>
        /// 通讯协议的封包
        /// </summary>
        /// <param name="body">数据体</param>
        public PowerIIPacket(int cmd, int hashCode, byte[] body)
        {
            this.Command = cmd;
            this.HashCode = hashCode;
            this.Body = body;
        }

        public override byte[] ToByteArray()
        {
            // command + 总长度 + 校验
            const int headLength = 4;
            // 总长度
            int totalLength = this.Body == null ? headLength : headLength + this.Body.Length;
            var builder = new ByteBuilder(totalLength);
            builder.Add(this.Command, BIG_ENDIAN);
            builder.Add(this.HashCode, BIG_ENDIAN);
            builder.Add(this.Body.Length, BIG_ENDIAN);
            builder.Add((byte)((this.Command + this.Body.Length) & 0xff));
            builder.Add(this.Body);
            return builder.Source;
        }

        /// <summary>
        /// 将参数序列化并写入为Body
        /// </summary>
        /// <param name="serializer">序列化工具</param>
        /// <param name="parameters">参数</param>
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
        /// 将Body的数据解析为参数
        /// </summary>        
        /// <returns></returns>
        public List<byte[]> GetBodyParameter()
        {
            var parameterList = new List<byte[]>();

            if (this.Body == null || this.Body.Length < 4)
            {
                return parameterList;
            }

            var index = 0;
            while (index < this.Body.Length)
            {
                // 参数长度
                var length = ByteConverter.ToInt32(this.Body, index, BIG_ENDIAN);
                index = index + 4;
                var paramBytes = new byte[length];
                // 复制出参数的数据
                Buffer.BlockCopy(this.Body, index, paramBytes, 0, length);
                index = index + length;
                parameterList.Add(paramBytes);
            }

            return parameterList;
        }

        /// <summary>
        /// 将Body的数据解析为对象数据
        /// </summary>        
        /// <returns></returns>
        public PowerIIEntity GetBodyEntity(ISerializer serializer)
        {
            if (this.Body == null || this.Body.Length < 4)
            {
                return null;
            }
            return (PowerIIEntity)serializer.Deserialize(this.Body, typeof(PowerIIEntity));
        }

        /// <summary>
        /// 解析一个数据包       
        /// 不足一个封包时返回null
        /// </summary>
        /// <param name="builder">接收到的历史数据</param>
        /// <returns></returns>
        public static PowerIIPacket GetPacket(ByteBuilder builder)
        {
            // 包头长度
            const int headLength = 4;
            // 不会少于4
            if (builder.Length < headLength)
            {
                return null;
            }
            // 包长
            int totalLength = builder.ToByte(2);
            // 包长要小于等于数据长度
            if (totalLength > builder.Length || totalLength < headLength)
            {
                return null;
            }
            int type = builder.ToByte(1);
            // cmd
            int cmd = builder.ToByte(0);
            byte check = builder.ToByte(3);
            //校验
            if (check != ((cmd + type + totalLength) & 0xff))
            {
                return null;
            }
            // 实体数据
            byte[] body = builder.ToArray(4, totalLength);
            // 清空本条数据
            builder.Remove(builder.Length);
            return new PowerIIPacket(cmd, type, body);
        }
    }
}
