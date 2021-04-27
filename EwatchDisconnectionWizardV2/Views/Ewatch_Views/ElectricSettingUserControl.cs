using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class ElectricSettingUserControl : Ewatch_Field4UserControl
    {
        /// <summary>
        /// 電表主畫面
        /// </summary>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public ElectricSettingUserControl(Ewatch_MySqlMethod ewatch_MySqlMethod)
        {
            InitializeComponent();
            Ewatch_MySqlMethod = ewatch_MySqlMethod;
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
        private List<ElectricSetting> FilterElectricSetting { get; set; } = new List<ElectricSetting>();
        public override void Search_Setting()
        {
            var ElectricSetting = Ewatch_MySqlMethod.ElectricLoad();
            gridControl1.DataSource = ElectricSetting;
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
                gridView1.Columns["ElectricNo"].Caption = "電表編號";
                gridView1.Columns["ElectricName"].Caption = "電表名稱";
                gridView1.Columns["PhaseTypeEnum"].Caption = "相位類型";
                gridView1.Columns["NotifyFlag"].Caption = "推播功能";
                gridView1.Columns["TimeoutSpan"].Caption = "延遲推播(h)";
                gridView1.Columns["MTimeoutSpan"].Caption = "延遲推播(m)";
                gridView1.Columns["SendTime"].Visible = false;
                gridView1.Columns["ConnectionFlag"].Visible = false;
                #region 報表行聚焦
                gridView1.FocusedRowChanged += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    if ((view.FindFilterText == "" || view.ActiveFilterString != "") & !SortGlyphFlag)
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            CaseNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "CaseNo").ToString();
                            ElectricNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "ElectricNo").ToString();
                            ElectricNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "ElectricName").ToString();
                            PhaseTypeEnumcomboBoxEdit.SelectedIndex = Convert.ToInt32(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "PhaseTypeEnum"));
                            NotifyFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyFlag"));
                            TimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "TimeoutSpan").ToString();
                            MTimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "MTimeoutSpan").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterElectricSetting.Count > 0 && FilterElectricSetting.Count>ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterElectricSetting[ex.FocusedRowHandle].CaseNo;
                                ElectricNotextEdit.Text = FilterElectricSetting[ex.FocusedRowHandle].ElectricNo.ToString();
                                ElectricNametextEdit.Text = FilterElectricSetting[ex.FocusedRowHandle].ElectricName;
                                PhaseTypeEnumcomboBoxEdit.SelectedIndex = FilterElectricSetting[ex.FocusedRowHandle].PhaseTypeEnum;
                                NotifyFlagtoggleSwitch.IsOn = FilterElectricSetting[ex.FocusedRowHandle].NotifyFlag;
                                TimeoutSpantextEdit.Text = FilterElectricSetting[ex.FocusedRowHandle].TimeoutSpan.ToString();
                                MTimeoutSpantextEdit.Text = FilterElectricSetting[ex.FocusedRowHandle].MTimeoutSpan.ToString();
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
                    int ElectricNo = Convert.ToInt32(ElectricNotextEdit.Text);
                    Ewatch_MySqlMethod.Delete_ElectricSetting(CaseNo, ElectricNo);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "" || view.ActiveFilterString != "")
                    {
                        FilterElectricSetting = new List<ElectricSetting>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as ElectricSetting;
                            if (entity == null)
                                continue;
                            FilterElectricSetting.Add(entity);
                        }
                    }
                    if (FilterElectricSetting.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterElectricSetting[0].CaseNo;
                        ElectricNotextEdit.Text = FilterElectricSetting[0].ElectricNo.ToString();
                        ElectricNametextEdit.Text = FilterElectricSetting[0].ElectricName;
                        PhaseTypeEnumcomboBoxEdit.SelectedIndex = FilterElectricSetting[0].PhaseTypeEnum;
                        NotifyFlagtoggleSwitch.IsOn = FilterElectricSetting[0].NotifyFlag;
                        TimeoutSpantextEdit.Text = FilterElectricSetting[0].TimeoutSpan.ToString();
                        MTimeoutSpantextEdit.Text = FilterElectricSetting[0].MTimeoutSpan.ToString();
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterElectricSetting = new List<ElectricSetting>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as ElectricSetting;
                        if (entity == null)
                            continue;
                        FilterElectricSetting.Add(entity);
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
            ElectricSetting electricSetting = new ElectricSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                ElectricNo = Convert.ToInt32(ElectricNotextEdit.Text),
                ElectricName = ElectricNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                TimeoutSpan = Convert.ToInt32(TimeoutSpantextEdit.Text),
                MTimeoutSpan = Convert.ToInt32(MTimeoutSpantextEdit.Text)
            };
            Ewatch_MySqlMethod.Update_ElectricSetting(electricSetting);
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
                    Size = new Size(465, 467)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                ElectricSettingUserControl1 CaseSettingUserControl = new ElectricSettingUserControl1(this, Form1, Ewatch_MySqlMethod);
                panelControl.Controls.Add(CaseSettingUserControl);
                flyout.Show();
            }
        }
    }
}
