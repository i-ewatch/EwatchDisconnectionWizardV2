using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
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
    public partial class DeviceConfigUserControl : ChungHsin_Field4UserControl
    {
        public DeviceConfigUserControl(ChungHsin_MySqlMethod chungHsin_MySqlMethod)
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
        private List<DeviceConfig> FilterDeviceConfig { get; set; } = new List<DeviceConfig>();
        public override void Search_Setting()
        {
            var DeviceConfig = ChungHsin_MySqlMethod.DevicesLoad();
            gridControl1.DataSource = DeviceConfig;
            if (!Flag)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].BestFit();
                }
                gridView1.Columns["PK"].Visible = false;
                gridView1.Columns["DeviceTypeEnum"].Caption = "設備類型";
                gridView1.Columns["DeviceName"].Caption = "設備名稱";
                #region 報表行聚焦
                gridView1.FocusedRowChanged += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    if ((view.FindFilterText == "" || view.ActiveFilterString != "") & !SortGlyphFlag)
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            DeviceTypeEnumtextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "DeviceTypeEnum").ToString();
                            DeviceNametextEdit.Text = view.GetListSourceRowCellValue(ex.FocusedRowHandle, "DeviceName").ToString();
                        }
                    }
                    else
                    {
                        if (ex.FocusedRowHandle > -1)
                        {
                            if (FilterDeviceConfig.Count > 0 && FilterDeviceConfig.Count > ex.FocusedRowHandle)
                            {
                                DeviceTypeEnumtextEdit.Text = FilterDeviceConfig[ex.FocusedRowHandle].DeviceName;
                                DeviceNametextEdit.Text = FilterDeviceConfig[ex.FocusedRowHandle].DeviceTypeEnum.ToString();
                            }
                        }
                    }
                };
                #endregion
                #region 報表行刪除
                gridView1.RowDeleting += (s, ex) =>
                {
                    ColumnView view = (ColumnView)s;
                    int DeviceTypeEnum = Convert.ToInt32(DeviceTypeEnumtextEdit.Text);
                    ChungHsin_MySqlMethod.Delete_DeviceConfig( DeviceTypeEnum);
                };
                #endregion
                #region 關鍵字搜尋
                gridView1.ColumnFilterChanged += (s, e) =>
                {
                    GridView view = s as GridView;
                    if (view.FindFilterText != "" || view.ActiveFilterString != "")
                    {
                        FilterDeviceConfig = new List<DeviceConfig>();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (view.IsGroupRow(i))
                                continue;
                            var entity = view.GetRow(i) as DeviceConfig;
                            if (entity == null)
                                continue;
                            FilterDeviceConfig.Add(entity);
                        }
                    }
                    if (FilterDeviceConfig.Count > 0)
                    {
                        DeviceNametextEdit.Text = FilterDeviceConfig[0].DeviceName;
                        DeviceTypeEnumtextEdit.Text = FilterDeviceConfig[0].DeviceTypeEnum.ToString();
                    }
                };
                #endregion
                #region 表頭篩選
                gridView1.EndSorting += (s, e) =>
                {
                    GridView view = s as GridView;
                    FilterDeviceConfig = new List<DeviceConfig>();
                    for (int i = 0; i < view.RowCount; i++)
                    {
                        if (view.IsGroupRow(i))
                            continue;
                        var entity = view.GetRow(i) as DeviceConfig;
                        if (entity == null)
                            continue;
                        FilterDeviceConfig.Add(entity);
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
            DeviceConfig deviceConfig = new DeviceConfig()
            {
                DeviceTypeEnum = Convert.ToInt32(DeviceTypeEnumtextEdit.Text),
                DeviceName = DeviceNametextEdit.Text
            };
            ChungHsin_MySqlMethod.Update_DeviceConfig(deviceConfig);
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
                    Size = new Size(459, 178)
                };
                flyout = new FlyoutDialog(Form1, panelControl);
                flyout.Properties.Style = FlyoutStyle.Popup;
                DeviceConfigUserControl1 deviceConfigUserControl1 = new DeviceConfigUserControl1(this, Form1, ChungHsin_MySqlMethod);
                panelControl.Controls.Add(deviceConfigUserControl1);
                flyout.Show();
            }
        }
    }
}
