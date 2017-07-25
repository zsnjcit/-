/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:40:21
 * 文 件 名：PowerIIServer
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
using MetaCrafts.Gateway.Socket.Application;
using NetworkSocket;
using System;
using System.Linq;
using System.Reflection;

namespace MetaCrafts.Gateway.Socket.TCP
{
    public sealed class PowerIIServer : TcpServerBase<PowerIIPacket>
    {
        public ISerializer Serializer { get; set; }

        /// <summary>
        /// 快速构建Tcp服务端
        /// </summary>
        public PowerIIServer()
        {
            this.Serializer = new PowerIISerialize();
        }

        /// <summary>
        /// 接收到客户端连接
        /// </summary>
        /// <param name="client">客户端</param>
        protected override void OnConnect(SocketAsync<PowerIIPacket> client)
        {
            Console.WriteLine("客户端{0}连接进来，当前连接数为：{1}", client, this.AliveClients.Count);
        }

        /// <summary>
        /// 接收到客户端断开连接
        /// </summary>
        /// <param name="client">客户端</param>
        protected override void OnDisconnect(SocketAsync<PowerIIPacket> client)
        {
            Console.WriteLine("客户端{0}断开连接，当前连接数为：{1}", client, this.AliveClients.Count);
        }

        /// <summary>
        /// 发送之前触发
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        protected override void OnSend(SocketAsync<PowerIIPacket> client, PowerIIPacket packet)
        {
            Console.WriteLine("向客户端{0}发送数据：{1}", client, packet.ToByteArray());
        }
        /// <summary>
        /// 当接收到远程端的数据时，将触发此方法
        /// 此方法用于处理和分析收到的数据
        /// 如果得到一个数据包，将触发OnRecvComplete方法
        /// [注]这里只需处理一个数据包的流程
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="recvBuilder">接收到的历史数据</param>
        /// <returns>如果不够一个数据包，则请返回null</returns>
        protected override PowerIIPacket OnReceive(SocketAsync<PowerIIPacket> client, ByteBuilder recvBuilder)
        {
            return PowerIIPacket.GetPacket(recvBuilder);
        }
        /// <summary>
        /// 当接收到客户端数据包时，将触发此方法
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="packet">数据包</param>
        protected override void OnRecvComplete(SocketAsync<PowerIIPacket> client, PowerIIPacket packet)
        {
            //将数据存入数据库
            if (packet != null)
            {
                PowerIIEntity entity = packet.GetBodyEntity(this.Serializer);

                PropertyInfo[] properties = typeof(PowerIIEntity).GetCanWritePropertyInfo();
                foreach (var item in properties)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item.Name + ":" + item.FastGetValue(entity));
                }
            }

            SendToClient(client, "OK");
        }

        bool SendToClient(string strIp, string strMsg)
        {
            var client = this.AliveClients.ToList().Where(o => o.ToString() == strIp).FirstOrDefault();

            return SendToClient(client, strMsg);
        }

        bool SendToClient(SocketAsync<PowerIIPacket> client, string strMsg)
        {
            if (client != null)
            {
                PowerIIPacket packet = new PowerIIPacket(254);
                try
                {
                    packet.SetBodyBinary(this.Serializer, strMsg);
                    OnSend(client, packet);
                }
                catch (Exception ex)
                {
                    OnException(client, ex, packet);
                }
            }
            return true;
        }
        /// <summary>
        /// 当操作中遇到处理异常时，将触发此方法
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="exception">异常</param>
        /// <param name="packet">相关数据事件</param>        
        void OnException(SocketAsync<PowerIIPacket> client, Exception exception, PowerIIPacket packet)
        {
            Console.WriteLine("客户端{0}接进来，异常：{1}", client, exception.ToString());
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
            if (disposing)
            {
                this.Serializer = null;
            }
        }
    }
}
