using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
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
    public partial class CaseUserControl : Ewatch_Field4UserControl
    {
        /// <summary>
        /// 案場主畫面
        /// </summary>
        /// <param name="ewatch_MySqlMethod">資料庫方法</param>
        public CaseUserControl(Ewatch_MySqlMethod ewatch_MySqlMethod)
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
        private List<CaseSetting> FilterCaseSetting { get; set; } = new List<CaseSetting>();
        /// <summary>
        /// 查詢案場
        /// </summary>
        public override void Search_Setting()
        {
            var CaseSetting = Ewatch_MySqlMethod.CaseLoad();
            gridControl1.DataSource = CaseSetting;
            if (!Flag)
            {
                gridView1.CustomColumnDisplayText += (s, ex) =>
            {
                if (ex.Column.FieldName == "NotifyTypeEnum")
                {
                    int val = (int)ex.Value;
                    switch (val)
                    {
                        case 0:
                            {
                                ex.DisplayText = "None";
                            }
                            break;
                        case 1:
                            {
                                ex.DisplayText = "LineNotify";
                            }
                            break;
                        case 2:
                            {
                                ex.DisplayText = "TelegramBot";
                            }
                            break;
                    }
                }
            };
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].BestFit();
                }
                gridView1.Columns["PK"].Visible = false;
                gridView1.Columns["CaseNo"].Caption = "案場編號";
                gridView1.Columns["Address"].Caption = "地址";
                gridView1.Columns["Contacter"].Caption = "聯絡人";
                gridView1.Columns["Phone"].Caption = "聯絡電話";
                gridView1.Columns["TitleName"].Caption = "案場名稱";
                gridView1.Columns["NotifyTypeEnum"].Caption = "推播類型";
                gridView1.Columns["NotifyApi"].Caption = "推播網址";
                gridView1.Columns["NotifyToken"].Caption = "推播權杖";
                gridView1.Columns["Longitude"].Caption = "經度";
                gridView1.Columns["Latitude"].Caption = "緯度";
                #region 報表行聚焦
                gridView1.FocusedRowChanged += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    if ((view.FindFilterText == "" || view.ActiveFilterString != "") & !SortGlyphFlag)
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            CaseNotextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "CaseNo").ToString();
                            AddresstextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Address").ToString();
                            ContactertextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Contacter").ToString();
                            PhonetextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Phone").ToString();
                            TitleNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "TitleName").ToString();
                            NotifyTypeEnumcomboBoxEdit.SelectedIndex = Convert.ToInt32(view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyTypeEnum"));
                            NotifyApitextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyApi").ToString();
                            NotifyTokentextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "NotifyToken").ToString();
                            LongitudetextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Longitude").ToString();
                            LatitudetextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "Latitude").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterCaseSetting.Count > 0 && FilterCaseSetting.Count > ex.FocusedRowHandle)
                            {
                                CaseNotextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].CaseNo;
                                AddresstextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].Address;
                                ContactertextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].Contacter;
                                PhonetextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].Phone;
                                TitleNametextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].TitleName;
                                NotifyTypeEnumcomboBoxEdit.SelectedIndex = FilterCaseSetting[ex.FocusedRowHandle].NotifyTypeEnum;
                                NotifyApitextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].NotifyApi;
                                NotifyTokentextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].NotifyToken;
                                LongitudetextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].Longitude.ToString();
                                LatitudetextEdit.Text = FilterCaseSetting[ex.FocusedRowHandle].Latitude.ToString();
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
                    Ewatch_MySqlMethod.Delete_CaseSetting(CaseNo);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "" || view.ActiveFilterString != "")
                    {
                        FilterCaseSetting = new List<CaseSetting>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as CaseSetting;
                            if (entity == null)
                                continue;
                            FilterCaseSetting.Add(entity);
                        }
                    }
                    if (FilterCaseSetting.Count > 0)
                    {
                        CaseNotextEdit.Text = FilterCaseSetting[0].CaseNo;
                        AddresstextEdit.Text = FilterCaseSetting[0].Address;
                        ContactertextEdit.Text = FilterCaseSetting[0].Contacter;
                        PhonetextEdit.Text = FilterCaseSetting[0].Phone;
                        TitleNametextEdit.Text = FilterCaseSetting[0].TitleName;
                        NotifyTypeEnumcomboBoxEdit.SelectedIndex = FilterCaseSetting[0].NotifyTypeEnum;
                        NotifyApitextEdit.Text = FilterCaseSetting[0].NotifyApi;
                        NotifyTokentextEdit.Text = FilterCaseSetting[0].NotifyToken;
                        LongitudetextEdit.Text = FilterCaseSetting[0].Longitude.ToString();
                        LatitudetextEdit.Text = FilterCaseSetting[0].Latitude.ToString();
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterCaseSetting = new List<CaseSetting>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as CaseSetting;
                        if (entity == null)
                            continue;
                        FilterCaseSetting.Add(entity);
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
        /// <summary>
        /// 修改按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangesimpleButton_Click(object sender, EventArgs e)
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
            Ewatch_MySqlMethod.Update_CaseSetting(caseSetting);
            Search_Setting();
        }
        /// <summary>
        /// 新增按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddsimpleButton_Click(object sender, EventArgs e)
        {
            if (!FlyoutFlag)
            {
                FlyoutFlag = true;
                PanelControl panelControl = new PanelControl()
                {
                    Size = new Size(464, 628)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                CaseSettingUserControl CaseSettingUserControl = new CaseSettingUserControl(this, Form1, Ewatch_MySqlMethod);
                panelControl.Controls.Add(CaseSettingUserControl);
                flyout.Show();
            }
        }

        private void DeleteCasesimpleButton_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }
    }
}
