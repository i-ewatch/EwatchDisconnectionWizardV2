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
    public partial class AiConfigUserControl : Ewatch_Field4UserControl
    {
        /// <summary>
        /// AI點位主畫面
        /// </summary>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public AiConfigUserControl(Ewatch_MySqlMethod ewatch_MySqlMethod)
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
        private List<AiConfig> FilterAiConfig { get; set; } = new List<AiConfig>();
        public override void Search_Setting()
        {
            var AiConfig = Ewatch_MySqlMethod.AiConfigLoad();
            gridControl1.DataSource = AiConfig;
            if (!Flag)
            {
                //gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;

                RepositoryItemToggleSwitch toggleSwitch = new RepositoryItemToggleSwitch();
                gridControl1.RepositoryItems.Add(toggleSwitch);
                gridView1.Columns["PK"].Visible = false;
                gridView1.Columns["CaseNo"].Caption = "案場編號";
                gridView1.Columns["AiNo"].Caption = "Ai編號";
                gridView1.Columns["Ai"].Caption = "Ai點位";
                gridView1.Columns["AiName"].Caption = "Ai名稱";
                gridView1.Columns["AiCalculateFlag"].ColumnEdit = toggleSwitch;
                gridView1.Columns["AiCalculateFlag"].Caption = "累積功能";
                gridView1.Columns["Aiunit"].Caption = "Ai單位";
                gridView1.Columns["CompareFlag"].ColumnEdit = toggleSwitch;
                gridView1.Columns["CompareFlag"].Caption = "上下限功能";
                gridView1.Columns["AiMax"].Caption = "上限值";
                gridView1.Columns["AiMin"].Caption = "下限值";
                gridView1.Columns["GroupNo"].Visible = false;
                gridView1.Columns["CompareType"].Visible = false;
                gridView1.Columns["EnumFlag"].ColumnEdit = toggleSwitch;
                gridView1.Columns["EnumFlag"].Caption = "類型旗標";
                gridView1.Columns["EnumType"].Visible = false;
                gridView1.Columns["Enum_Array"].Caption = "類型內容";
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].BestFit();
                }
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
                            AitextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Ai").ToString();
                            AiNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiName").ToString();
                            AicalculateFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiCalculateFlag"));
                            CompareFlagFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "CompareFlag"));
                            AiMaxtextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiMax").ToString();
                            AiMintextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "AiMin").ToString();
                            AiunittextEdit.Text = (view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Aiunit") == null ? "" : view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Aiunit").ToString());
                            EnumFlagtoggleSwitch.IsOn = Convert.ToBoolean(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "EnumFlag"));
                            Enum_ArraytextEdit.Text = (view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Enum_Array") == null ? "" : view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Enum_Array").ToString());
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterAiConfig.Count > 0 && FilterAiConfig.Count > ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].CaseNo;
                                AiNotextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].AiNo.ToString();
                                AitextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].Ai;
                                AiNametextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].AiName;
                                AicalculateFlagtoggleSwitch.IsOn = FilterAiConfig[ex.FocusedRowHandle].AiCalculateFlag;
                                CompareFlagFlagtoggleSwitch.IsOn = FilterAiConfig[ex.FocusedRowHandle].CompareFlag;
                                AiMaxtextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].AiMax.ToString();
                                AiMintextEdit.Text = FilterAiConfig[ex.FocusedRowHandle].AiMin.ToString();
                                AiunittextEdit.Text = (FilterAiConfig[ex.FocusedRowHandle].Aiunit == null ? "" : FilterAiConfig[ex.FocusedRowHandle].Aiunit);
                                EnumFlagtoggleSwitch.IsOn = FilterAiConfig[ex.FocusedRowHandle].EnumFlag;
                                Enum_ArraytextEdit.Text = (FilterAiConfig[ex.FocusedRowHandle].Enum_Array == null ? "" : FilterAiConfig[ex.FocusedRowHandle].Enum_Array);
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
                    string Ai = AitextEdit.Text;
                    Ewatch_MySqlMethod.Delete_AiConfig(CaseNo, AiNo, Ai);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "" || view.ActiveFilterString != "")
                    {
                        FilterAiConfig = new List<AiConfig>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as AiConfig;
                            if (entity == null)
                                continue;
                            FilterAiConfig.Add(entity);
                        }
                    }
                    if (FilterAiConfig.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterAiConfig[0].CaseNo;
                        AiNotextEdit.Text = FilterAiConfig[0].AiNo.ToString();
                        AitextEdit.Text = FilterAiConfig[0].Ai;
                        AiNametextEdit.Text = FilterAiConfig[0].AiName;
                        AicalculateFlagtoggleSwitch.IsOn = FilterAiConfig[0].AiCalculateFlag;
                        CompareFlagFlagtoggleSwitch.IsOn = FilterAiConfig[0].CompareFlag;
                        AiMaxtextEdit.Text = FilterAiConfig[0].AiMax.ToString();
                        AiMintextEdit.Text = FilterAiConfig[0].AiMin.ToString();
                        AiunittextEdit.Text = (FilterAiConfig[0].Aiunit == null ? "" : FilterAiConfig[0].Aiunit);
                        EnumFlagtoggleSwitch.IsOn = FilterAiConfig[0].EnumFlag;
                        Enum_ArraytextEdit.Text = (FilterAiConfig[0].Enum_Array == null ? "" : FilterAiConfig[0].Enum_Array);
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterAiConfig = new List<AiConfig>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as AiConfig;
                        if (entity == null)
                            continue;
                        FilterAiConfig.Add(entity);
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
                Enum_Array = Enum_ArraytextEdit.Text
            };
            Ewatch_MySqlMethod.Update_AiConfig(aiConfig);
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
                    Size = new Size(460, 694)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                AiConfigUserControl1 CaseSettingUserControl = new AiConfigUserControl1(this, Form1, Ewatch_MySqlMethod);
                panelControl.Controls.Add(CaseSettingUserControl);
                flyout.Show();
            }
        }
    }
}
