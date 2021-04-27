using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Modules.Ewatch_Modules
{
    public class StateSetting
    {
        /// <summary>
        /// 流水號
        /// </summary>
        public int PK { get; set; }
        /// <summary>
        /// 案場編號
        /// </summary>
        public string CaseNo { get; set; }
        /// <summary>
        /// AI編號
        /// </summary>
        public int StateNo { get; set; }
        /// <summary>
        /// AI名稱
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 0 = 不發報 ， 1 =發報
        /// </summary>
        public bool NotifyFlag { get; set; }
        /// <summary>
        /// 狀態旗標 0 = DI狀態，1 = DO狀態
        /// </summary>
        public int StateFlag { get; set; }
        /// <summary>
        /// 狀態高位元
        /// </summary>
        public string StateHigh { get; set; }
        /// <summary>
        /// 狀態低位元
        /// </summary>
        public string StateLow { get; set; }
        /// <summary>
        /// 最後發送狀態
        /// </summary>
        public bool LastStateFlag { get; set; }
    }
}
