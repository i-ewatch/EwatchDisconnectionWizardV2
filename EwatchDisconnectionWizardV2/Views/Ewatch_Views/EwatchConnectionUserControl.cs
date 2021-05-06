using DevExpress.Utils;
using DevExpress.XtraEditors;
using EwatchDisconnectionWizardV2.Components.Ewatch_Components;
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
    public partial class EwatchConnectionUserControl : Ewatch_Field4UserControl
    {
        private Color highPriority = Color.Red;
        private Color normalPriority = Color.Gray;
        private Color lowPriority = Color.Lime;
        private int markWidth = 16;
        private List<CaseSetting> CaseSettings { get; set; } = new List<CaseSetting>();
        private Ewatch_MySqlComponent Ewatch_MySqlComponent { get; set; }
        /// <summary>
        /// 斷線偵測畫面
        /// </summary>
        /// <param name="ewatch_MySqlComponent">資料庫方法</param>
        public EwatchConnectionUserControl(Ewatch_MySqlComponent ewatch_MySqlComponent)
        {
            Ewatch_MySqlComponent = ewatch_MySqlComponent;
            CaseSettings = ewatch_MySqlComponent.CaseSettings;
            AiSettings = ewatch_MySqlComponent.AiSettings;
            ElectricSettings = ewatch_MySqlComponent.ElectricSettings;
            InitializeComponent();
            #region AI斷線資訊
            AigridControl.DataSource = AiSettings;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].BestFit();
            }
            gridView1.Columns["PK"].Visible = false;
            gridView1.Columns["NotifyFlag"].Visible = false;
            gridView1.Columns["CaseNo"].Caption = "案場名稱";
            gridView1.Columns["AiNo"].Caption = "Ai編號";
            gridView1.Columns["AiName"].Caption = "Ai名稱";
            gridView1.Columns["NotifyFlag"].Visible = false;
            gridView1.Columns["TimeoutSpan"].Visible = false;
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
                      e.Cache.FillEllipse(e.Bounds.X + 204, e.Bounds.Y + 1, markWidth, markWidth, color);
                  }
              };
            #endregion
            #endregion
            #region 電表斷線資訊
            ElectricgridControl.DataSource = ElectricSettings;
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            for (int i = 0; i < gridView2.Columns.Count; i++)
            {
                gridView2.Columns[i].BestFit();
            }
            gridView2.Columns["PK"].Visible = false;
            gridView2.Columns["CaseNo"].Caption = "案場編號";
            gridView2.Columns["ElectricNo"].Caption = "電表編號";
            gridView2.Columns["PhaseTypeEnum"].Caption = "相位類型";
            gridView2.Columns["ElectricName"].Caption = "電表名稱";
            gridView2.Columns["NotifyFlag"].Visible = false;
            gridView2.Columns["TimeoutSpan"].Visible = false;
            gridView2.Columns["MTimeoutSpan"].Visible = false;
            gridView2.Columns["SendTime"].Visible = false;
            gridView2.Columns["ConnectionFlag"].Caption = "連線狀態";
            #region 相位類型顯示功能
            gridView2.CustomColumnDisplayText += (s, e) =>
             {
                 if (e.Column.FieldName.ToString() == "PhaseTypeEnum")
                 {
                     int cellValue = Convert.ToInt32(e.Value);
                     if (cellValue == 0)
                     {
                         e.DisplayText = "三相";
                     }
                     else if (cellValue == 1)
                     {
                         e.DisplayText = "單相";
                     }
                 }
                 else if (e.Column.FieldName.ToString() == "CaseNo")
                 {
                     string cellValue = e.Value.ToString();
                     var data = CaseSettings.SingleOrDefault(g => g.CaseNo == cellValue);
                     if (data != null)
                     {
                         e.DisplayText = data.TitleName;
                     }
                 }
             };
            #endregion
            #region 斷線燈號顯示功能
            gridView2.CustomDrawCell += (s, e) =>
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
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                AigridControl.DataSource = Ewatch_MySqlComponent.AiSettings;
                AigridControl.Refresh();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                ElectricgridControl.DataSource = Ewatch_MySqlComponent.ElectricSettings;
                ElectricgridControl.Refresh();
            }
        }
    }
}
