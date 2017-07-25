using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace MetaCrafts.Gateway.Socket
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<ServiceRunner>(s =>
                {
                    s.ConstructUsing(name => new ServiceRunner());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription("MetaGeteWay网关解析服务");
                x.SetDisplayName("MetaGeteWaySokect");
                x.SetServiceName("MetaGeteWay");
            });
        }
    }
}
