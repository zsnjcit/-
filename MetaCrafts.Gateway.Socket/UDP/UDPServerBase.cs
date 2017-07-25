/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 14:38:39
 * 文 件 名：UDPServerRealize
 * 
 * 描述说明：UDP服务实现
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using MetaCrafts.Gateway.Model;
using MetaCrafts.Gateway.Socket.Application;
using NetworkSocket;
using System;
using System.Net;

namespace MetaCrafts.Gateway.Socket.UDP
{
    public class UDPServerBase : UDPServer<BasePacket>
    {
        /// <summary>
        /// 获取服务是否已处在监听中
        /// </summary>
        public bool IsListening { get; private set; }
        UDPServer<BasePacket> client;
        public UDPServerBase()
        {
            client = new UDPServer<BasePacket>();
            client.SendHandler = (packet) => this.OnSend(client, packet);
            client.ReceiveHandler = (builder) => this.OnReceive(client, builder);
            client.RecvCompleteHandler = (packet) => this.OnRecvComplete(client, packet);
            client.SendCompleteHandler = (packet) => this.OnSendComplete(client, packet);
        }

        protected BasePacket OnReceive(UDPServer<BasePacket> client, ByteBuilder recvBuilder)
        {

            return BasePacket.GetPacket(recvBuilder);
        }

        protected void OnSend(UDPServer<BasePacket> client, BasePacket packet)
        {

            Console.WriteLine("向客户端{0}发送数据：{1}", client, packet.ToByteArray());
        }

        protected void OnRecvComplete(UDPServer<BasePacket> client, BasePacket packet)
        {

            Console.WriteLine(client.UDPReceive.IPEndPoint.ToString());
            //将数据存入数据库
            if (packet != null)
            {


                switch (packet.PacketTypeByte)
                {
                    case Model.EnumPacketType.数据上报:
                        LEDEntity entity = (LEDEntity)packet.GetBodyEntity<LEDEntity>(new LEDSerializer());
                        Console.WriteLine(entity.ToString());
                        break;
                }

            }

        }

        protected void OnSendComplete(UDPServer<BasePacket> client, BasePacket packet)
        {

        }

        /// <summary>
        /// 开始启动监听
        /// </summary>
        /// <param name="localEndPoint">要监听的本地IP和端口</param>        
        public void StartListen(int port)
        {
            if (this.IsListening)
            {
                return;
            }

            try
            {
                client.BindSocket(new IPEndPoint(IPAddress.Any, port));
                client.BeginReceive();

                this.IsListening = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
