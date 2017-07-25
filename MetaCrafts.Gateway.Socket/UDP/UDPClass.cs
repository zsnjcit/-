/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 14:26:58
 * 文 件 名：UDPClass
 * 
 * 描述说明：UDP通信对象封装
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using System.Net;
using System.Net.Sockets;

namespace MetaCrafts.Gateway.Socket.UDP
{
    public class UDPClass<T>
    {
        public IPEndPoint IPEndPoint;

        public UdpClient UDPClient { get; set; }
        public T Packet { get; set; }
        public int Counter { get; set; }
    }
}
