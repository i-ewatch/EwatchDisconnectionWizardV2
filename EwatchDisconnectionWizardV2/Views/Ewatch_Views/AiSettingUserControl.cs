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
    public partial class AiSettingUserControl : Ewatch_Field4UserControl
    {
        /// <summary>
        /// AI資訊畫面
        /// </summary>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public AiSettingUserControl(Ewatch_MySqlMethod ewatch_MySqlMethod)
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
        private List<AiSetting> FilterAiSetting { get; set; } = new List<AiSetting>();
        public override void Search_Setting()
        {
            var AiSetting = Ewatch_MySqlMethod.AiLoad();
            gridControl1.DataSource = AiSetting;
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
                gridView1.Columns["AiNo"].Caption = "Ai編號";
                gridView1.Columns["AiName"].Caption = "Ai名稱";
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
                            AiNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiNo").ToString();
                            AiNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiName").ToString();
                            NotifyFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyFlag"));
                            TimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "TimeoutSpan").ToString();
                            MTimeoutSpantextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "MTimeoutSpan").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterAiSetting.Count > 0 && FilterAiSetting.Count > ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterAiSetting[ex.FocusedRowHandle].CaseNo;
                                AiNotextEdit.Text = FilterAiSetting[ex.FocusedRowHandle].AiNo.ToString();
                                AiNametextEdit.Text = FilterAiSetting[ex.FocusedRowHandle].AiName;
                                NotifyFlagtoggleSwitch.IsOn = FilterAiSetting[ex.FocusedRowHandle].NotifyFlag;
                                TimeoutSpantextEdit.Text = FilterAiSetting[ex.FocusedRowHandle].TimeoutSpan.ToString();
                                MTimeoutSpantextEdit.Text = FilterAiSetting[ex.FocusedRowHandle].MTimeoutSpan.ToString();
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
                    int AiNo = Convert.ToInt32(AiNotextEdit.Text);
                    Ewatch_MySqlMethod.Delete_AiSetting(CaseNo, AiNo);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "")
                    {
                        FilterAiSetting = new List<AiSetting>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as AiSetting;
                            if (entity == null)
                                continue;
                            FilterAiSetting.Add(entity);
                        }
                    }
                    if (FilterAiSetting.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterAiSetting[0].CaseNo;
                        AiNotextEdit.Text = FilterAiSetting[0].AiNo.ToString();
                        AiNametextEdit.Text = FilterAiSetting[0].AiName;
                        NotifyFlagtoggleSwitch.IsOn = FilterAiSetting[0].NotifyFlag;
                        TimeoutSpantextEdit.Text = FilterAiSetting[0].TimeoutSpan.ToString();
                        MTimeoutSpantextEdit.Text = FilterAiSetting[0].MTimeoutSpan.ToString();
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterAiSetting = new List<AiSetting>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as AiSetting;
                        if (entity == null)
                            continue;
                        FilterAiSetting.Add(entity);
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
            AiSetting aiSetting = new AiSetting()
            {
                CaseNo = CaseNotextEdit.Text,
                AiNo = Convert.ToInt32(AiNotextEdit.Text),
                AiName = AiNametextEdit.Text,
                NotifyFlag = NotifyFlagtoggleSwitch.IsOn,
                TimeoutSpan = Convert.ToInt32(TimeoutSpantextEdit.Text),
                MTimeoutSpan = Convert.ToInt32(MTimeoutSpantextEdit.Text)
            };
            Ewatch_MySqlMethod.Update_AiSetting(aiSetting);
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
                    Size = new Size(458, 443)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                AiSettingUserControl1 CaseSettingUserControl = new AiSettingUserControl1(this, Form1, Ewatch_MySqlMethod);
                panelControl.Controls.Add(CaseSettingUserControl);
                flyout.Show();
            }
        }

        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            Search_Setting();
        }
    }
}
