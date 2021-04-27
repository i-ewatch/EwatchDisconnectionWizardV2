using DevExpress.Utils;
using DevExpress.XtraEditors;
using EwatchDisconnectionWizardV2.Components.ChungHsin_Components;
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
    public partial class ChumgHsinConnectionUserControl : ChungHsin_Field4UserControl
    {
        private Color highPriority = Color.Red;
        private Color normalPriority = Color.Gray;
        private Color lowPriority = Color.Lime;
        private int markWidth = 16;
        private List<CaseSetting> CaseSettings { get; set; } = new List<CaseSetting>();
        private List<DeviceConfig> DeviceConfigs { get; set; } = new List<DeviceConfig>();
        public ChumgHsinConnectionUserControl(ChungHsin_MySqlComponent chungHsin_MySqlComponent)
        {
            CaseSettings = chungHsin_MySqlComponent.CaseSettings;
            ReceiveSettings = chungHsin_MySqlComponent.ReceiveSettings;
            DeviceConfigs = chungHsin_MySqlComponent.DeviceConfigs;
            InitializeComponent();
            #region Receive斷線資訊
            AigridControl.DataSource = ReceiveSettings;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].BestFit();
            }
            gridView1.Columns["PK"].Visible = false;
            gridView1.Columns["NotifyFlag"].Visible = false;
            gridView1.Columns["DeviceTypeEnum"].Caption = "設備類型";
            gridView1.Columns["CaseNo"].Caption = "案場名稱";
            gridView1.Columns["ReceiveNo"].Caption = "Receive編號";
            gridView1.Columns["ReceiveName"].Caption = "Receive名稱";
            gridView1.Columns["NotifyFlag"].Visible = false;
            gridView1.Columns["HTimeoutSpan"].Visible = false;
            gridView1.Columns["MTimeoutSpan"].Visible = false;
            gridView1.Columns["SendTime"].Visible = false;
            gridView1.Columns["ConnectionFlag"].Caption = "連線狀態";
            #region 案場名稱顯示功能
            gridView1.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName.ToString() == "CaseNo")
                {
                    string cellValue = e.Value.ToString();
                    var data = CaseSettings.SingleOrDefault(g => g.CaseNo == cellValue);
                    if (data != null)
                    {
                        e.DisplayText = data.TitleName;
                    }
                }
                else if(e.Column.FieldName.ToString() == "DeviceTypeEnum")
                {
                    int cellValue = Convert.ToInt32(e.Value);
                    var data = DeviceConfigs.SingleOrDefault(g => g.DeviceTypeEnum == cellValue);
                    if (data != null)
                    {
                        e.DisplayText = data.DeviceName;
                    }
                }
            };
            #endregion
            #region 斷線燈號顯示功能
            gridView1.CustomDrawCell += (s, e) =>
            {
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Appearance.Options.UseTextOptions = true;
                e.DefaultDraw();
                if (e.Column.FieldName == "ConnectionFlag")
                {
                    Color color;
                    string cellValue = e.CellValue.ToString();
                    if (cellValue == "不使用")
                        color = normalPriority;
                    else if (cellValue == "斷線")
                        color = highPriority;
                    else
                        color = lowPriority;
                    e.Cache.FillEllipse(e.Bounds.X + 150, e.Bounds.Y + 1, markWidth, markWidth, color);
                }
            };
            #endregion
            #endregion
        }
        public override void TextChange()
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0 )
            {
                AigridControl.DataSource = ReceiveSettings;
                AigridControl.Refresh();
            }
        }
    }
}
