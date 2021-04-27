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
    public partial class AiConfigUserControl1 : Ewatch_Field4UserControl
    {
        /// <summary>
        /// AI點位新增畫面
        /// </summary>
        /// <param name="CaseUserControl">AI資訊主畫面</param>
        /// <param name="form1">繼承主物件-泡泡視窗使用</param>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public AiConfigUserControl1(Ewatch_Field4UserControl CaseUserControl, Form1 form1, Ewatch_MySqlMethod ewatch_MySqlMethod)
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
            AiConfig aiConfig = new AiConfig()
            {
                CaseNo = CaseNotextEdit.Text,
                AiNo = Convert.ToInt32(AiNotextEdit.Text),
                AiName = AiNametextEdit.Text,
                Ai = AitextEdit.Text,
                AiCalculateFlag = AicalculateFlagtoggleSwitch.IsOn,
                Aiunit = AiunittextEdit.Text,
                CompareFlag = CompareFlagFlagtoggleSwitch.IsOn,
                AiMax = Convert.ToDecimal(AiMaxtextEdit.Text),
                AiMin = Convert.ToDecimal(AiMintextEdit.Text),
                EnumFlag = EnumFlagtoggleSwitch.IsOn,
                Enum_Array = Enum_ArraytextEdit.Text,
            };
            Ewatch_MySqlMethod.Insert_AiConfig(aiConfig);
            caseUserControl.Search_Setting();
            caseUserControl.FlyoutFlag = false;
            caseUserControl.flyout.Close();
        }
    }
}
