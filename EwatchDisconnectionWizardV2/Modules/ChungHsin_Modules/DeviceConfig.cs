using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules
{
    public class DeviceConfig
    {
        /// <summary>
        /// 流水號
        /// </summary>
        public int PK { get; set; }
        /// <summary>
        /// 設備類型
        /// </summary>
        public int DeviceTypeEnum { get; set; }
        /// <summary>
        /// 設備類型
        /// </summary>
        public string DeviceName { get; set; }
    }
}
