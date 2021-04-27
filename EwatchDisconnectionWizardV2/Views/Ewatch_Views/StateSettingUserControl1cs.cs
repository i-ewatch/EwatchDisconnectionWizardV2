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
    public partial class StateSettingUserControl1cs : Ewatch_Field4UserControl
    {
        /// <summary>
        /// 狀態新增畫面
        /// </summary>
        /// <param name="CaseUserControl">狀態主畫面</param>
        /// <param name="form1">繼承主物件-泡泡視窗使用</param>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public StateSettingUserControl1cs(Ewatch_Field4UserControl CaseUserControl, Form1 form1, Ewatch_MySqlMethod ewatch_MySqlMethod)
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
            StateSetting stateSetting = new StateSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                StateNo = Convert.ToInt32(StateNotextEdit.Text),
                StateName = StateNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                StateFlag = Convert.ToInt32(StateFlagcomboBoxEdit.SelectedIndex),
                StateHigh = StateHightextEdit.Text,
                StateLow = StateLowtextEdit.Text
            };
            Ewatch_MySqlMethod.Insert_StateSetting(stateSetting);
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
