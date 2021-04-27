using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Components
{
    public class ChungHsin_Field4Component:Component
    {
        #region ChungHsin平台
        /// <summary>
        /// 案場資訊
        /// </summary>
        public List<CaseSetting> CaseSettings { get; set; } = new List<CaseSetting>();
        /// <summary>
        /// 接收資訊
        /// </summary>
        public List<ReceiveSetting> ReceiveSettings { get; set; } = new List<ReceiveSetting>();
        /// <summary>
        /// 設備資訊
        /// </summary>
        public List<DeviceConfig> DeviceConfigs { get; set; } = new List<DeviceConfig>();
        #endregion
        public ChungHsin_MySqlMethod ChungHsin_MySqlMethod { get; set; } 
        #region 初始化
        /// <summary>
        /// 連線執行續
        /// </summary>
        protected Thread ConnectionThread { get; set; }
        /// <summary>
        /// 最後輪巡時間
        /// </summary>
        public DateTime ConnectionTime { get; set; }
        public ChungHsin_Field4Component()
        {
            OnMyWorkStateChanged += new MyWorkStateChanged(AfterMyWorkStateChanged);
        }
        /// <summary>
        /// 系統工作路徑
        /// </summary>
        protected readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        public delegate void MyWorkStateChanged(object sender, EventArgs e);
        public event MyWorkStateChanged OnMyWorkStateChanged;
        /// <summary>
        /// 通訊功能啟動判斷旗標
        /// </summary>
        protected bool myWorkState;
        /// <summary>
        /// 通訊功能啟動旗標
        /// </summary>
        public bool MyWorkState
        {
            get { return myWorkState; }
            set
            {
                if (value != myWorkState)
                {
                    myWorkState = value;
                    WhenMyWorkStateChange();
                }
            }
        }
        /// <summary>
        /// 執行續工作狀態改變觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AfterMyWorkStateChanged(object sender, EventArgs e) { }
        protected void WhenMyWorkStateChange()
        {
            OnMyWorkStateChanged?.Invoke(this, null);
        }
        #endregion
    }
}
