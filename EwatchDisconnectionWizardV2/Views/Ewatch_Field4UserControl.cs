using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraEditors;
using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Modules.Ewatch_Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Views
{
    public class Ewatch_Field4UserControl : XtraUserControl
    {
        #region 浮動視窗初始化
        /// <summary>
        /// 數值資訊
        /// </summary>
        public List<AiSetting> AiSettings = new List<AiSetting>();
        /// <summary>
        /// 電表資訊
        /// </summary>
        public List<ElectricSetting> ElectricSettings = new List<ElectricSetting>();
        /// <summary>
        /// 浮動視窗
        /// </summary>
        public FlyoutDialog flyout { get; set; }
        /// <summary>
        /// 浮動視窗旗標
        /// </summary>
        public bool FlyoutFlag { get; set; }
        public Form1 Form1 { get; set; }
        #endregion
        /// <summary>
        /// Mysql方法
        /// </summary>
        protected Ewatch_MySqlMethod Ewatch_MySqlMethod { get; set; }
        /// <summary>
        /// 查詢案場
        /// </summary>
        public virtual void Search_Setting() { }
        /// <summary>
        /// 顯示變更
        /// </summary>
        public virtual void TextChange() { }
    }
}
