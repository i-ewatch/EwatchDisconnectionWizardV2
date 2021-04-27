using DevExpress.XtraEditors;
using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Modules.Ewatch_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchDisconnectionWizardV2.Views.Ewatch_Views
{
    public partial class ElectricSettingUserControl1 : Ewatch_Field4UserControl
    {
        /// <summary>
        /// 電表新增畫面
        /// </summary>
        /// <param name="CaseUserControl">電表主畫面</param>
        /// <param name="form1">繼承主物件-泡泡視窗使用</param>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public ElectricSettingUserControl1(Ewatch_Field4UserControl CaseUserControl, Form1 form1, Ewatch_MySqlMethod ewatch_MySqlMethod)
        {
            InitializeComponent();
            caseUserControl = CaseUserControl;
            Form1 = form1;
            Ewatch_MySqlMethod = ewatch_MySqlMethod;
        }
        private Ewatch_Field4UserControl caseUserControl { get; set; }

        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }

        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            ElectricSetting electricSetting = new ElectricSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                ElectricNo = Convert.ToInt32(ElectricNotextEdit.Text),
                ElectricName = ElectricNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                TimeoutSpan = Convert.ToInt32(TimeoutSpantextEdit.Text),
                MTimeoutSpan = Convert.ToInt32(MTimeoutSpantextEdit.Text)
            };
            Ewatch_MySqlMethod.Insert_ElectricSetting(electricSetting);
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
