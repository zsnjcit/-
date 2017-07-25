/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 9:59:05
 * 文 件 名：SokectCodeAttribute
 * 
 * 描述说明：通信实体解析关键字CODE
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using System;

namespace MetaCrafts.Gateway.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SokectCodeAttribute : Attribute
    {
        /// <summary>
        /// 是否启用解析
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 代码名称
        /// </summary>
        public string CodeName { get; set; }
        public SokectCodeAttribute(string code)
        {
            this.CodeName = code;
            this.IsEnable = true;
        }

        public SokectCodeAttribute(string code, bool isEnable)
        {
            this.CodeName = code;
            this.IsEnable = isEnable;
        }
    }
}
