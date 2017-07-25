using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCrafts.Gateway.Common.Helper
{
    /// <summary>
    /// 应用程序域加载者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppDomainHelper<T> where T : class
    {
        /// <summary>
        /// 加载应用程序域，获取相应实例
        /// </summary>
        /// <param name="dllpath"></param>
        /// <param name="classpath"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public T Load(string assemblyName, string typeName, out AppDomain domain)
        {
            AppDomainSetup setup = new AppDomainSetup();
            if (System.IO.File.Exists(assemblyName + ".config"))
                setup.ConfigurationFile = assemblyName + ".config";
            setup.ShadowCopyFiles = "true";
            setup.ApplicationBase = System.IO.Path.GetDirectoryName(assemblyName);
            domain = AppDomain.CreateDomain(System.IO.Path.GetFileName(assemblyName), null, setup);
            AppDomain.MonitoringIsEnabled = true;
            T obj = (T)domain.CreateInstanceFromAndUnwrap(assemblyName, typeName);//"Assembly文件名含路径", "那个辅助类的全名"
            return obj;
        }
        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        /// <param name="domain"></param>
        public void UnLoad(AppDomain domain)
        {
            AppDomain.Unload(domain);
            domain = null;
        }
    }
}
