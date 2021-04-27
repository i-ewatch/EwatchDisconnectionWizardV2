using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules
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
        /// 接收編號
        /// </summary>
        public int ReceiveNo { get; set; }
        /// <summary>
        /// 狀態編號
        /// </summary>
        public int StateNo { get; set; }
        /// <summary>
        /// 0 = 不發報 ， 1 =發報
        /// </summary>
        public bool NotifyFlag { get; set; }
        /// <summary>
        /// 最後發送旗標
        /// </summary>
        public bool LastStateFlag { get; set; }
    }
}
