using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCrafts.Gateway.Common.Helper
{
    public class GuidHelper
    {
        /// <summary>
        /// GUID生成
        /// </summary>
        /// <param name="format">格式 可填写N、D、B、P、X</param>
        /// <returns></returns>
        public static string GetNewGuId(string format = "D")
        {
            if (string.IsNullOrWhiteSpace(format))
                return Guid.NewGuid().ToString();
            else
                return Guid.NewGuid().ToString(format);
        }
    }
}
