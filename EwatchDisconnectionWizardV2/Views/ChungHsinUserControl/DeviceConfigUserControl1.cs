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
    public partial class DeviceConfigUserControl1 : ChungHsin_Field4UserControl
    {
        public DeviceConfigUserControl1(ChungHsin_Field4UserControl UserControl, Form1 form1, ChungHsin_MySqlMethod chungHsin_MySqlMethod)
        {
            InitializeComponent();
            caseUserControl = UserControl;
            Form1 = form1;
            ChungHsin_MySqlMethod = chungHsin_MySqlMethod;
        }
        private ChungHsin_Field4UserControl caseUserControl { get; set; }
        private void CancelsimpleButton_Click(object sender, EventArgs e)
        {
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }

        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            DeviceConfig deviceConfig = new DeviceConfig()
            {
                DeviceTypeEnum = Convert.ToInt32(DeviceTypeEnumtextEdit.Text),
                DeviceName = DeviceNametextEdit.Text
            };
            ChungHsin_MySqlMethod.Insert_DeviceConfig(deviceConfig);
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
