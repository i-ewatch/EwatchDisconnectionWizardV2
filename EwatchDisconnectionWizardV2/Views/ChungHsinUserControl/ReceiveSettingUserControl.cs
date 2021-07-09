using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class ReceiveSettingUserControl : ChungHsin_Field4UserControl
    {
        public ReceiveSettingUserControl(ChungHsin_MySqlMethod chungHsin_MySqlMethod)
        {
            InitializeComponent();
            ChungHsin_MySqlMethod = chungHsin_MySqlMethod;
            Search_Setting();
        }
        /// <summary>
        /// 第一次讀取
        /// </summary>
        private bool Flag = false;
        /// <summary>
        /// 表頭篩選旗標
        /// </summary>
        private bool SortGlyphFlag = false;
        /// <summary>
        /// 篩選過後的值
        /// </summary>
        private List<ReceiveSetting> FilterReceiveSetting { get; set; } = new List<ReceiveSetting>();
        public override void Search_Setting()
        {
            var ReceiveSetting = ChungHsin_MySqlMethod.ReceiveLoad();
            gridControl1.DataSource = ReceiveSetting;
            var DeviceConfig = ChungHsin_MySqlMethod.DevicesLoad();
            if (DeviceTypeEnumcomboBoxEdit.Properties.Items.Count>0)
            {
                DeviceTypeEnumcomboBoxEdit.Properties.Items.Clear();

            }
            foreach (var item in DeviceConfig)
            {
                DeviceTypeEnumcomboBoxEdit.Properties.Items.Add(item.DeviceName);
            }
            if (!Flag)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].BestFit();
                }
                RepositoryItemToggleSwitch toggleSwitch = new RepositoryItemToggleSwitch();
                gridControl1.RepositoryItems.Add(toggleSwitch);
                gridView1.Columns["PK"].Visible = false;
                gridView1.Columns["NotifyFlag"].ColumnEdit = toggleSwitch;
                gridView1.Columns["CaseNo"].Caption = "案場編號";
                gridView1.Columns["DeviceTypeEnum"].Caption = "設備類型";
                gridView1.Columns["ReceiveNo"].Caption = "接收編號";
                gridView1.Columns["ReceiveName"].Caption = "接收名稱";
                gridView1.Columns["NotifyFlag"].Caption = "推播功能";
                gridView1.Columns["HTimeoutSpan"].Caption = "延遲推播(h)";
                gridView1.Columns["MTimeoutSpan"].Caption = "延遲推播(m)";
                gridView1.Columns["SendTime"].Visible = false;
                gridView1.Columns["ConnectionFlag"].Visible = false;
                gridView1.CustomColumnDisplayText += (s, ex) =>
                {
                    if (ex.Column.FieldName == "DeviceTypeEnum")
                    {
                        int val = (int)ex.Value;
                        ex.DisplayText = DeviceConfig.Single(g => g.DeviceTypeEnum == val).DeviceName;
                    }
                };
                #region 報表行聚焦
                gridView1.FocusedRowChanged += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    if ((view.FindFilterText == "" || view.ActiveFilterString != "") & !SortGlyphFlag)
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            CaseNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "CaseNo").ToString();
                            DeviceTypeEnumcomboBoxEdit.SelectedIndex = Convert.ToInt32(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "DeviceTypeEnum"));
                            ReceiveNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "ReceiveNo").ToString();
                            ReceiveNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "ReceiveName").ToString();
                            NotifyFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyFlag"));
                            TimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "HTimeoutSpan").ToString();
                            MTimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "MTimeoutSpan").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterReceiveSetting.Count > 0 && FilterReceiveSetting.Count > ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterReceiveSetting[ex.FocusedRowHandle].CaseNo;
                                DeviceTypeEnumcomboBoxEdit.SelectedIndex = Convert.ToInt32(FilterReceiveSetting[ex.FocusedRowHandle].DeviceTypeEnum) ;
                                ReceiveNotextEdit.Text = FilterReceiveSetting[ex.FocusedRowHandle].ReceiveNo.ToString();
                                ReceiveNametextEdit.Text = FilterReceiveSetting[ex.FocusedRowHandle].ReceiveName;
                                NotifyFlagtoggleSwitch.IsOn = FilterReceiveSetting[ex.FocusedRowHandle].NotifyFlag;
                                TimeoutSpantextEdit.Text = FilterReceiveSetting[ex.FocusedRowHandle].HTimeoutSpan.ToString();
                                MTimeoutSpantextEdit.Text = FilterReceiveSetting[ex.FocusedRowHandle].MTimeoutSpan.ToString();
                            }
                        }
                    }

                };
                #endregion
                #region 報表行刪除
                gridView1.RowDeleting += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    string CaseNo = CaseNotextEdit.Text;
                    int ReceiveNo = Convert.ToInt32(ReceiveNotextEdit.Text);
                    ChungHsin_MySqlMethod.Delete_ReceiveSetting(CaseNo, ReceiveNo);
                    ChungHsin_MySqlMethod.Delete_AiSetting(CaseNo, ReceiveNo);
                    ChungHsin_MySqlMethod.Delete_StateSetting(CaseNo, ReceiveNo);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "")
                    {
                        FilterReceiveSetting = new List<ReceiveSetting>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as ReceiveSetting;
                            if (entity == null)
                                continue;
                            FilterReceiveSetting.Add(entity);
                        }
                    }
                    if (FilterReceiveSetting.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterReceiveSetting[0].CaseNo;
                        DeviceTypeEnumcomboBoxEdit.SelectedIndex = FilterReceiveSetting[0].DeviceTypeEnum;
                        ReceiveNotextEdit.Text = FilterReceiveSetting[0].ReceiveNo.ToString();
                        ReceiveNametextEdit.Text = FilterReceiveSetting[0].ReceiveName;
                        NotifyFlagtoggleSwitch.IsOn = FilterReceiveSetting[0].NotifyFlag;
                        TimeoutSpantextEdit.Text = FilterReceiveSetting[0].HTimeoutSpan.ToString();
                        MTimeoutSpantextEdit.Text = FilterReceiveSetting[0].MTimeoutSpan.ToString();
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterReceiveSetting = new List<ReceiveSetting>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as ReceiveSetting;
                        if (entity == null)
                            continue;
                        FilterReceiveSetting.Add(entity);
                    }
                    SortGlyphFlag = true;
                    gridView1.FocusedRowHandle = 0;
                };
                #endregion
                gridView1.FocusedRowHandle = 1;
                Flag = true;
            }
            else
            {
                gridControl1.Refresh();
            }
        }
        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            Search_Setting();
        }

        private void ChangesimpleButton_Click(object sender, EventArgs e)
        {
            ReceiveSetting receiveSetting = new ReceiveSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                DeviceTypeEnum = Convert.ToInt32(DeviceTypeEnumcomboBoxEdit.SelectedIndex),
                ReceiveNo = Convert.ToInt32(ReceiveNotextEdit.Text),
                ReceiveName = ReceiveNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                HTimeoutSpan = Convert.ToInt32(TimeoutSpantextEdit.Text),
                MTimeoutSpan = Convert.ToInt32(MTimeoutSpantextEdit.Text)
            };
            ChungHsin_MySqlMethod.Updata_ReceiveSetting(receiveSetting);
            Search_Setting();
        }

        private void DeleteCasesimpleButton_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void AddsimpleButton_Click(object sender, EventArgs e)
        {
            if (!FlyoutFlag)
            {
                FlyoutFlag = true;
                PanelControl panelControl = new PanelControl()
                {
                    Size = new Size(458, 488)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                ReceiveSettingUserControl1 receiveSettingUserControl1 = new ReceiveSettingUserControl1(this, Form1, ChungHsin_MySqlMethod);
                panelControl.Controls.Add(receiveSettingUserControl1);
                flyout.Show();
            }
        }
    }
}
