using DevExpress.XtraEditors;
using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchDisconnectionWizardV2.Views.ChungHsinUserControl
{
    public partial class ReceiveSettingUserControl1 : ChungHsin_Field4UserControl
    {
        public ReceiveSettingUserControl1(ChungHsin_Field4UserControl CaseUserControl, Form1 form1, ChungHsin_MySqlMethod chungHsin_MySqlMethod)
        {
            InitializeComponent();
            caseUserControl = CaseUserControl;
            Form1 = form1;
            ChungHsin_MySqlMethod = chungHsin_MySqlMethod;
            var DeviceSetting = ChungHsin_MySqlMethod.DevicesLoad();
            foreach (var item in DeviceSetting)
            {
                DeviceTypeEnumcomboBoxEdit.Properties.Items.Add(item.DeviceName);
            }
            DeviceTypeEnumcomboBoxEdit.SelectedIndex = 0;
        }
        private ChungHsin_Field4UserControl caseUserControl { get; set; }

        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }

        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            ReceiveSetting receiveSetting = new ReceiveSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                DeviceTypeEnum = DeviceTypeEnumcomboBoxEdit.SelectedIndex,
                ReceiveNo = Convert.ToInt32(ReceiveNotextEdit.Text),
                ReceiveName = ReceiveNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                HTimeoutSpan = Convert.ToInt32(TimeoutSpantextEdit.Text),
                MTimeoutSpan = Convert.ToInt32(MTimeoutSpantextEdit.Text)
            };
            ChungHsin_MySqlMethod.Insert_ReceiveSettig(receiveSetting);
            var aiconfig = ChungHsin_MySqlMethod.AiConfigLoad(receiveSetting.DeviceTypeEnum);
            var stateconfig = ChungHsin_MySqlMethod.StateConfigLoad(receiveSetting.DeviceTypeEnum);
            List<AiSetting> aiSettings = new List<AiSetting>();
            List<StateSetting> stateSettings = new List<StateSetting>();
            if (aiconfig != null)
            {
                foreach (var item in aiconfig)
                {
                    AiSetting setting = new AiSetting()
                    {
                        CaseNo = receiveSetting.CaseNo,
                        ReceiveNo = receiveSetting.ReceiveNo,
                        AINo = item.AINo,
                    };
                    aiSettings.Add(setting);
                }
                ChungHsin_MySqlMethod.Insert_AiSetting(aiSettings);
            }
            if (stateconfig != null)
            {
                foreach (var item in stateconfig)
                {
                    StateSetting setting = new StateSetting()
                    {
                        CaseNo = receiveSetting.CaseNo,
                        ReceiveNo = receiveSetting.ReceiveNo,
                        StateNo = item.StateNo
                    };
                    stateSettings.Add(setting);
                }
                ChungHsin_MySqlMethod.Insert_StateSetting(stateSettings);
            }
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
