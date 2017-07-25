/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 13:42:27
 * 文 件 名：UdpServerBase
 * 
 * 描述说明：UDP连接工具
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using NetworkSocket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MetaCrafts.Gateway.Socket.UDP
{
    public class UDPServer<T> where T : PacketBase
    {

        private Thread thrRecv;
        public UDPClass<T> UDPReceive;
        private UDPClass<T> UDPSend;
        private object serverRoot = new object();

        /// <summary>
        /// 接收到的未处理数据
        /// </summary>
        private ByteBuilder RecvBuilder = new ByteBuilder();
        /// <summary>
        /// 发送数据的委托
        /// </summary>
        internal Action<T> SendHandler { get; set; }
        /// <summary>
        /// 处理和分析收到的数据的委托
        /// </summary>
        internal Func<ByteBuilder, T> ReceiveHandler { get; set; }
        /// <summary>
        /// 接收一个数据包委托
        /// </summary>
        internal Action<T> RecvCompleteHandler { get; set; }
        /// <summary>
        /// 发送完成一个数据包委托
        /// </summary>
        internal Action<T> SendCompleteHandler { get; set; }

        public UDPServer() { }

        internal void BindSocket(IPEndPoint localEndPoint)
        {
            UDPReceive = new UDPClass<T>();
            UDPReceive.IPEndPoint = localEndPoint;
            UDPReceive.UDPClient = new UdpClient(localEndPoint);

            UDPSend = new UDPClass<T>();
            UDPSend.UDPClient = new UdpClient();
        }

        internal void BeginReceive()
        {
            thrRecv = new Thread(ReceiveMessage);
            thrRecv.Start();
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                lock (serverRoot)
                {
                    // 调用接收回调函数
                    UDPReceive.UDPClient.BeginReceive(new AsyncCallback(ReceiveCallback), UDPReceive);
                    Thread.Sleep(100);
                }
            }
        }

        // 接收回调函数
        private void ReceiveCallback(IAsyncResult iar)
        {
            UDPClass<T> udpState = iar.AsyncState as UDPClass<T>;
            udpState.Counter++;
            lock (this.RecvBuilder.SyncRoot)
            {
                T packet = null;

                if (iar.IsCompleted)
                {
                    Byte[] receiveBytes = udpState.UDPClient.EndReceive(iar, ref udpState.IPEndPoint);
                    this.RecvBuilder.Add(receiveBytes);
                    packet = this.ReceiveHandler(this.RecvBuilder);
                    if (packet != null)
                    {
                        this.RecvCompleteHandler(packet);
                    }
                }
            }
        }

        // 发送函数
        private void SendMsg(IPEndPoint ipEndPoint, T package)
        {
            UDPSend.UDPClient.Connect(ipEndPoint);
            UDPSend.Counter++;

            this.SendHandler(package);

            if (package == null)
            {
                return;
            }

            Byte[] sendBytes = package.ToByteArray();
            if (sendBytes == null)
            {
                return;
            }
            UDPSend.Packet = package;
            UDPSend.UDPClient.BeginSend(sendBytes, sendBytes.Length, new AsyncCallback(SendCallback), UDPSend);
        }

        // 发送回调函数
        private void SendCallback(IAsyncResult iar)
        {
            UDPClass<T> udpState = iar.AsyncState as UDPClass<T>;

            if (udpState.UDPClient.EndSend(iar) != 0)
            {
                this.SendCompleteHandler(udpState.Packet);
            }
        }

        #region IDisponse成员

        /// <summary>
        /// 获取是否已释放
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 关闭和释放所有相关资源
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed == false)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            this.IsDisposed = true;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~UDPServer()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            UDPReceive.UDPClient.Close();

            if (disposing)
            {
                this.UDPSend = null;
                this.UDPReceive = null;
                this.RecvBuilder = null;
                this.serverRoot = null;
            }
        }
        #endregion
    }
}