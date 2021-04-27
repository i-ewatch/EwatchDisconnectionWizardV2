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
    public partial class StateSettingUserControl : Ewatch_Field4UserControl
    {
        /// <summary>
        /// 狀態主畫面
        /// </summary>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public StateSettingUserControl(Ewatch_MySqlMethod ewatch_MySqlMethod)
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
        private List<StateSetting> FilterStateSetting { get; set; } = new List<StateSetting>();
        public override void Search_Setting()
        {
            var StateSetting = Ewatch_MySqlMethod.StateLoad();
            gridControl1.DataSource = StateSetting;
            if (!Flag)
            {
                gridView1.OptionsBehavior.Editable = false; //不允取編輯
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;//不允許欄位聚焦
                gridView1.OptionsFind.FindMode = FindMode.FindClick;//搜尋需要點擊 Find或Enter
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].BestFit();
                }
                RepositoryItemToggleSwitch toggleSwitch = new RepositoryItemToggleSwitch();
                gridControl1.RepositoryItems.Add(toggleSwitch);
                gridView1.Columns["PK"].Visible = false;
                gridView1.Columns["NotifyFlag"].ColumnEdit = toggleSwitch;
                gridView1.Columns["CaseNo"].Caption = "案場編號";
                gridView1.Columns["StateNo"].Caption = "狀態編號";
                gridView1.Columns["StateName"].Caption = "狀態名稱";
                gridView1.Columns["NotifyFlag"].Caption = "推播功能";
                gridView1.Columns["StateFlag"].Caption = "狀態類型";
                gridView1.Columns["StateHigh"].Caption = "狀態高位元";
                gridView1.Columns["StateLow"].Caption = "狀態低位元";
                gridView1.Columns["LastStateFlag"].Visible = false;
                #region DI/O狀態顯示
                gridView1.CustomColumnDisplayText += (s, e) =>
                {
                    if (e.Column.FieldName == "StateFlag")
                    {
                        string valueCell = e.DisplayText.ToString();
                        if (valueCell == "0")
                        {
                            e.DisplayText = "DI狀態";
                        }
                        else if (valueCell == "1")
                        {
                            e.DisplayText = "DO狀態";
                        }
                    }
                };
                #endregion
                #region 報表行聚焦
                gridView1.FocusedRowChanged += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    if ((view.FindFilterText == "" || view.ActiveFilterString != "") & !SortGlyphFlag)
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            CaseNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "CaseNo").ToString();
                            StateNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "StateNo").ToString();
                            StateNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "StateName").ToString();
                            NotifyFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyFlag"));
                            StateFlagcomboBoxEdit.SelectedIndex = Convert.ToInt32(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "StateFlag"));
                            StateHightextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "StateHigh").ToString();
                            StateLowtextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "StateLow").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterStateSetting.Count > 0 && FilterStateSetting.Count > ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterStateSetting[ex.FocusedRowHandle].CaseNo;
                                StateNotextEdit.Text = FilterStateSetting[ex.FocusedRowHandle].StateNo.ToString();
                                StateNametextEdit.Text = FilterStateSetting[ex.FocusedRowHandle].StateName;
                                NotifyFlagtoggleSwitch.IsOn = FilterStateSetting[ex.FocusedRowHandle].NotifyFlag;
                                StateFlagcomboBoxEdit.SelectedIndex = FilterStateSetting[ex.FocusedRowHandle].StateFlag;
                                StateHightextEdit.Text = FilterStateSetting[ex.FocusedRowHandle].StateHigh;
                                StateLowtextEdit.Text = FilterStateSetting[ex.FocusedRowHandle].StateLow;
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
                    int StateNo = Convert.ToInt32(StateNotextEdit.Text);
                    Ewatch_MySqlMethod.Delete_StateSetting(CaseNo, StateNo);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "" ||view.ActiveFilterString != "")
                    {
                        FilterStateSetting = new List<StateSetting>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as StateSetting;
                            if (entity == null)
                                continue;
                            FilterStateSetting.Add(entity);
                        }
                    }
                    if (FilterStateSetting.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterStateSetting[0].CaseNo;
                        StateNotextEdit.Text = FilterStateSetting[0].StateNo.ToString();
                        StateNametextEdit.Text = FilterStateSetting[0].StateName;
                        NotifyFlagtoggleSwitch.IsOn = FilterStateSetting[0].NotifyFlag;
                        StateFlagcomboBoxEdit.SelectedIndex = FilterStateSetting[0].StateFlag;
                        StateHightextEdit.Text = FilterStateSetting[0].StateHigh;
                        StateLowtextEdit.Text = FilterStateSetting[0].StateLow;
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                 {
                     GridView view = s as GridView;
                     FilterStateSetting = new List<StateSetting>();
                     for (int i = 0; i < view.RowCount; i++)
                     {
                         if (view.IsGroupRow(i))
                             continue;
                         var entity = view.GetRow(i) as StateSetting;
                         if (entity == null)
                             continue;
                         FilterStateSetting.Add(entity);
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

        private void ChangesimpleButton_Click(object sender, EventArgs e)
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
            Ewatch_MySqlMethod.Update_StateSetting(stateSetting);
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
                    Size = new Size(443, 451)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                StateSettingUserControl1cs StateSettingUserControl1cs = new StateSettingUserControl1cs(this, Form1, Ewatch_MySqlMethod);
                panelControl.Controls.Add(StateSettingUserControl1cs);
                flyout.Show();
            }
        }

        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            Search_Setting();
        }
    }
}
