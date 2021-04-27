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
    public partial class CH_CaseSettingUserControl1 : ChungHsin_Field4UserControl
    {
        public CH_CaseSettingUserControl1(ChungHsin_Field4UserControl CaseUserControl, Form1 form1, ChungHsin_MySqlMethod chungHsin_MySqlMethod)
        {
            InitializeComponent();
            caseUserControl = CaseUserControl;
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
            CaseSetting caseSetting = new CaseSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                Address = AddresstextEdit.Text,
                Contacter = ContactertextEdit.Text,
                Phone = PhonetextEdit.Text,
                TitleName = TitleNametextEdit.Text,
                NotifyTypeEnum = NotifyTypeEnumcomboBoxEdit.SelectedIndex,
                NotifyApi = NotifyApitextEdit.Text,
                NotifyToken = NotifyTokentextEdit.Text,
                Longitude = Convert.ToSingle(LongitudetextEdit.Text),
                Latitude = Convert.ToSingle(LatitudetextEdit.Text)
            };
            ChungHsin_MySqlMethod.Insert_CaseSetting(caseSetting);
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
