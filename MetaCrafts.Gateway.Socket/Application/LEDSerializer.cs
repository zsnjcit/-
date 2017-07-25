/****************************************************************
 * 作    者：訾松松
 * CLR 版本：4.0.30319.42000
 * 创建时间：2017/7/24 星期一 9:07:58
 * 文 件 名：LEDSerializer
 * 
 * 描述说明：
 *
 * 修改历史：
 *
*****************************************************************
 * Copyright @杭州元工科技有限公司 2017 All rights reserved
*****************************************************************/

using System;
using MetaCrafts.Gateway.Common;
using MetaCrafts.Gateway.Model;
using static MetaCrafts.Gateway.Model.LEDEntity;

namespace MetaCrafts.Gateway.Socket.Application
{
    public class LEDSerializer : ISerializer
    {
        public object Deserialize(byte[] bytes, Type type)
        {
            LEDEntity led = new LEDEntity();
            led.RptModel = (ReportingModel)Enum.ToObject(typeof(ReportingModel), bytes[0]);
            led.Interval = Convert.ToInt32(bytes[1]);
            led.Status = (CircuitStatus)Enum.ToObject(typeof(CircuitStatus), bytes[2]);
            led.Voltage = (bytes[3] << 8) + bytes[4];
            led.Current = (bytes[5] << 8) + bytes[6];
            led.WorkModel = (LightWorkModel)Enum.ToObject(typeof(LightWorkModel), bytes[7]);
            led.Switch = (LightSwitch)Enum.ToObject(typeof(LightSwitch), bytes[8]);
            led.WorkOpenModel = (LightWorkOpenModel)Enum.ToObject(typeof(LightWorkOpenModel), bytes[9]);
            return led;
        }

        public byte[] Serialize(object model)
        {
            throw new NotImplementedException();
        }
    }
}
