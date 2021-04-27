using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules
{
    public class AiSetting
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
        /// 數值編號
        /// </summary>
        public string AINo { get; set; }
        /// <summary>
        /// 最大最小值比較
        /// </summary>
        public bool CompareFlag { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal Max { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal Min { get; set; }
        /// <summary>
        /// 比較類型(超過上限，正常，低於下限)
        /// </summary>
        public decimal CompareType { get; set; }
        /// <summary>
        /// 類型推播
        /// </summary>
        public bool EnumFlag { get; set; }
        /// <summary>
        /// 紀錄最後發報類型
        /// </summary>
        public int EnumType { get; set; }
    }
}
