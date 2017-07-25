using MetaCrafts.Gateway.Socket.TCP;
using MetaCrafts.Gateway.Socket.UDP;
using System;

namespace MetaCrafts.Gateway.Socket
{
    internal class ServiceRunner
    {

        public void Start()
        {
            try
            {
                var server = new UDPServerBase();
                server.StartListen(4002);
                //var fastServer = new UDPServerRealize();
                //fastServer.StartListen(4000);


                //var fastServer = new EchoUdpServer();
                // fastServer.StartListen(4000);


                //var aus = new AsyncUdpSever();
                //aus.StartListen(4000, 1001);


                //var aus1 = new AsyncUdpSever();
                //aus1.StartListen(5000, 1002);

                //var aus2 = new AsyncUdpSever();
                //aus2.StartListen(6000, 1003);


                //var aus3 = new AsyncUdpSever();
                //aus3.StartListen(7000, 1004);


                //Console.WriteLine("PowerIITcpServer服务启动：" + fastServer.LocalEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Stop()
        {

        }

    }
}