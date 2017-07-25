/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 10:13:24
 * 文 件 名：PowerIIEntity
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using MetaCrafts.Gateway.Common;

namespace MetaCrafts.Gateway.Model
{
    public class PowerIIEntity
    {
        /// <summary>
        /// 温度
        /// </summary>
        [SokectCode("t")]
        public double Temp { get; set; }
        /// <summary>
        /// 市电电压
        /// </summary>
        [SokectCode("e0")]
        public double MainE0 { get; set; }
        /// <summary>
        /// 市电电流
        /// </summary>
        [SokectCode("c0")]
        public double MainC0 { get; set; }
        /// <summary>
        /// 第1路电流
        /// </summary>
        [SokectCode("c1")]
        public double LineC1 { get; set; }
        /// <summary>
        /// 第2路电流
        /// </summary>
        [SokectCode("c2")]
        public double LineC2 { get; set; }
        /// <summary>
        /// 第3路电流
        /// </summary>
        [SokectCode("c3")]
        public double LineC3 { get; set; }
        /// <summary>
        /// 第4路电流
        /// </summary>
        [SokectCode("c4")]
        public double LineC4 { get; set; }
        /// <summary>
        /// 第5路电流
        /// </summary>
        [SokectCode("c5")]
        public double LineC5 { get; set; }
        /// <summary>
        /// 第6路电流
        /// </summary>
        [SokectCode("c6")]
        public double LineC6 { get; set; }
        /// <summary>
        /// UPS电压
        /// </summary>
        [SokectCode("e")]
        public double UpsE { get; set; }
        /// <summary>
        /// UPS电流
        /// </summary>
        [SokectCode("c")]
        public double UpsC { get; set; }
        /// <summary>
        /// 直流12V第1路电流
        /// </summary>
        [SokectCode("dc12c0")]
        public double DC12Line1 { get; set; }
        /// <summary>
        /// 直流12V第2路电流
        /// </summary>
        [SokectCode("dc12c1")]
        public double DC12Line2 { get; set; }
        /// <summary>
        /// 直流24V第1路电流
        /// </summary>
        [SokectCode("dc24c0")]
        public double DC24Line1 { get; set; }
        /// <summary>
        /// 直流24V第2路电流
        /// </summary>
        [SokectCode("dc24c1")]
        public double DC24Line2 { get; set; }

    }
}
