/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/20 星期四 17:06:47
 * 文 件 名：LEDEntity
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/


using System;

namespace MetaCrafts.Gateway.Model
{
    public class LEDEntity
    {
        /// <summary>
        /// 汇报模式
        /// </summary>
        public ReportingModel RptModel { get; set; }
        /// <summary>
        /// 表示主动汇报模式下时间间隔
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 电路状态
        /// </summary>
        public CircuitStatus Status { get; set; }
        /// <summary>
        /// 电压 V
        /// </summary>
        public int Voltage { get; set; }
        /// <summary>
        /// 电流 mA
        /// </summary>
        public int Current { get; set; }
        /// <summary>
        /// 常亮频闪模式状态
        /// </summary>
        public LightWorkModel WorkModel { get; set; }
        /// <summary>
        /// 灯开关状态
        /// </summary>
        public LightSwitch Switch { get; set; }
        /// <summary>
        /// 常亮下控制模式配置
        /// </summary>
        public LightWorkOpenModel WorkOpenModel { get; set; }

        public LEDEntity()
        {   
        }

        public override string ToString()
        {
            return string.Format(Environment.NewLine + @"汇报模式:{0}主动汇报时间间隔:{1}电路状态:{2}电压V:{3}电流mA:{4}常亮频闪模式状态:{5}灯开关状态:{6}常亮下控制模式配置:{7}",

                RptModel.ToString() + Environment.NewLine,
                Interval + Environment.NewLine,
                Status.ToString() + Environment.NewLine,
                Voltage + Environment.NewLine,
                Current + Environment.NewLine,
                WorkModel.ToString() + Environment.NewLine,
                Switch.ToString() + Environment.NewLine,
                WorkOpenModel.ToString() + Environment.NewLine
                + "**********************");
        }

        //public void ParseData(byte[] by)
        //{
        //    this.RptModel = (ReportingModel)Enum.ToObject(typeof(ReportingModel), by[0]);
        //    this.Interval = Convert.ToInt32(by[1]);
        //    this.Status = (CircuitStatus)Enum.ToObject(typeof(CircuitStatus), by[2]);
        //    this.Voltage = (by[3] << 8) + by[4];
        //    this.Current = (by[5] << 8) + by[6];
        //    this.WorkModel = (LightWorkModel)Enum.ToObject(typeof(LightWorkModel), by[7]);
        //    this.Switch = (LightSwitch)Enum.ToObject(typeof(LightSwitch), by[8]);
        //    this.WorkOpenModel = (LightWorkOpenModel)Enum.ToObject(typeof(LightWorkOpenModel), by[9]);
        //}

        /// <summary>
        /// 汇报模式
        /// </summary>
        public enum ReportingModel : byte
        {
            被动汇报模式 = 0x00,
            主动汇报模式 = 0xFF
        }

        /// <summary>
        /// 电路状态
        /// </summary>
        public enum CircuitStatus : byte
        {
            正常 = 0x00,
            异常 = 0xFF
        }
        /// <summary>
        /// LED灯工作模式
        /// </summary>
        public enum LightWorkModel : byte
        {
            常亮模式 = 0x00,
            频闪模式 = 0xFF
        }
        /// <summary>
        /// 灯开关状态
        /// </summary>
        public enum LightSwitch : byte
        {
            开 = 0x00,
            关 = 0xFF
        }

        /// <summary>
        /// 常亮下控制模式配置
        /// </summary>
        public enum LightWorkOpenModel : byte
        {
            光敏控制 = 0x00,//（默认状态）
            相机控制 = 0xFF,
            时控控制 = 0x55
        }

        /// <summary>
        /// 设置成功失败
        /// </summary>
        public enum SettingStatus : byte
        {
            失败 = 0x00,
            成功 = 0xFF
        }
    }
}
