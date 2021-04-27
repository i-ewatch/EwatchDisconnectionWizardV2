using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using EwatchDisconnectionWizardV2.Components;
using EwatchDisconnectionWizardV2.Components.ChungHsin_Components;
using EwatchDisconnectionWizardV2.Components.Ewatch_Components;
using EwatchDisconnectionWizardV2.Configuration;
using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Views;
using EwatchDisconnectionWizardV2.Views.ChungHsinUserControl;
using EwatchDisconnectionWizardV2.Views.Ewatch_Views;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EwatchDisconnectionWizardV2
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        private MySqlSetting MySqlSetting { get; set; }
        /// <summary>
        /// 按鈕資訊
        /// </summary>
        private ButtonSetting ButtonSetting { get; set; }
        /// <summary>
        /// 按鈕方法
        /// </summary>
        private ButtonMethod ButtonMethod { get; set; }
        /// <summary>
        /// 切換畫面物件
        /// </summary>
        private NavigationFrame NavigationFrame { get; set; }

        #region ChungHsin平台
        /// <summary>
        /// ChungHsin資料庫方法
        /// </summary>
        private ChungHsin_MySqlMethod ChungHsin_MySqlMethod { get; set; }
        /// <summary>
        /// ChungHsin斷線偵測
        /// </summary>
        private ChungHsin_MySqlComponent ChungHsin_MySqlComponent { get; set; }
        /// <summary>
        /// 總ChumgHsin執行緒物件
        /// </summary>
        private List<ChungHsin_Field4Component> ChungHsin_Field4Component { get; set; } = new List<ChungHsin_Field4Component>();
        /// <summary>
        /// ChungHsin畫面
        /// </summary>
        public List<ChungHsin_Field4UserControl> ChungHsin_Field4UserControl { get; set; } = new List<ChungHsin_Field4UserControl>();
        #endregion
        #region Ewatch平台
        /// <summary>
        /// Ewatch資料庫方法
        /// </summary>
        private Ewatch_MySqlMethod Ewatch_MySqlMethod { get; set; }
        /// <summary>
        /// Ewatch斷線偵測
        /// </summary>
        private Ewatch_MySqlComponent Ewatch_MySqlComponent { get; set; }
        /// <summary>
        /// Ewatch上下限偵測
        /// </summary>
        private Ewatch_AlarmComponent Ewatch_AlarmComponent { get; set; }
        /// <summary>
        /// Ewatch畫面
        /// </summary>
        private List<Ewatch_Field4UserControl> Ewatch_Field4UserControl { get; set; } = new List<Ewatch_Field4UserControl>();
        /// <summary>
        /// 總Ewatch執行緒物件
        /// </summary>
        private List<Ewatch_Field4Component> Ewatch_Field4Component { get; set; } = new List<Ewatch_Field4Component>();
        #endregion

        public Form1()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();        //宣告Serilog初始化

            MySqlSetting = InitialMethod.MySqlLoad();
            ButtonSetting = InitialMethod.InitialButtonLoad();
            InitializeComponent();
            NavigationFrame = new NavigationFrame() { Dock = DockStyle.Fill };
            NavigationFrame.Parent = DisPlaypanelControl;

            ButtonMethod = new ButtonMethod() { Form1 = this, navigationFrame = NavigationFrame };
            ButtonMethod.AccordionLoad(accordionControl1, ButtonSetting);

            #region Ewatch平台
            Ewatch_MySqlMethod = new Ewatch_MySqlMethod(MySqlSetting);
            Ewatch_MySqlComponent = new Ewatch_MySqlComponent(Ewatch_MySqlMethod);
            Ewatch_MySqlComponent.MyWorkState = true;
            Ewatch_Field4Component.Add(Ewatch_MySqlComponent);
            Ewatch_AlarmComponent = new Ewatch_AlarmComponent(Ewatch_MySqlMethod);
            Ewatch_AlarmComponent.MyWorkState = true;
            Ewatch_Field4Component.Add(Ewatch_AlarmComponent);

            EwatchConnectionUserControl ewatchConnectionUserControl = new EwatchConnectionUserControl(Ewatch_MySqlComponent) { Dock = DockStyle.Fill };
            NavigationFrame.AddPage(ewatchConnectionUserControl);
            Ewatch_Field4UserControl.Add(ewatchConnectionUserControl);

            CaseUserControl caseUserControl = new CaseUserControl(Ewatch_MySqlMethod) { Form1 = this, Dock = DockStyle.Fill };
            NavigationFrame.AddPage(caseUserControl);
            Ewatch_Field4UserControl.Add(caseUserControl);

            AiSettingUserControl aiSettingUserControl = new AiSettingUserControl(Ewatch_MySqlMethod) { Form1 = this, Dock = DockStyle.Fill };
            NavigationFrame.AddPage(aiSettingUserControl);
            Ewatch_Field4UserControl.Add(aiSettingUserControl);

            AiConfigUserControl aiConfigUserControl = new AiConfigUserControl(Ewatch_MySqlMethod) { Form1 = this, Dock = DockStyle.Fill };
            NavigationFrame.AddPage(aiConfigUserControl);
            Ewatch_Field4UserControl.Add(aiConfigUserControl);

            ElectricSettingUserControl electricSettingUserControl = new ElectricSettingUserControl(Ewatch_MySqlMethod) { Form1 = this, Dock = DockStyle.Fill };
            NavigationFrame.AddPage(electricSettingUserControl);
            Ewatch_Field4UserControl.Add(electricSettingUserControl);

            StateSettingUserControl stateSettingUserControl = new StateSettingUserControl(Ewatch_MySqlMethod) { Form1 = this, Dock = DockStyle.Fill };
            NavigationFrame.AddPage(stateSettingUserControl);
            Ewatch_Field4UserControl.Add(stateSettingUserControl);
            #endregion

            #region ChungHsin平台
            ChungHsin_MySqlMethod = new ChungHsin_MySqlMethod(MySqlSetting);
            ChungHsin_MySqlComponent = new ChungHsin_MySqlComponent(ChungHsin_MySqlMethod);
            ChungHsin_MySqlComponent.MyWorkState = true;
            ChungHsin_Field4Component.Add(ChungHsin_MySqlComponent);

            ChumgHsinConnectionUserControl chumgHsinConnectionUserControl = new ChumgHsinConnectionUserControl(ChungHsin_MySqlComponent) { Dock = DockStyle.Fill };
            NavigationFrame.AddPage(chumgHsinConnectionUserControl);
            ChungHsin_Field4UserControl.Add(chumgHsinConnectionUserControl);

            DeviceConfigUserControl deviceConfigUserControl = new DeviceConfigUserControl(ChungHsin_MySqlMethod) { Dock = DockStyle.Fill };
            NavigationFrame.AddPage(deviceConfigUserControl);
            ChungHsin_Field4UserControl.Add(deviceConfigUserControl);

            CH_CaseSettingUserControl cH_CaseSettingUserControl = new CH_CaseSettingUserControl(ChungHsin_MySqlMethod) { Dock = DockStyle.Fill };
            NavigationFrame.AddPage(cH_CaseSettingUserControl);
            ChungHsin_Field4UserControl.Add(cH_CaseSettingUserControl);

            ReceiveSettingUserControl receiveSettingUserControl = new ReceiveSettingUserControl(ChungHsin_MySqlMethod) { Dock = DockStyle.Fill };
            NavigationFrame.AddPage(receiveSettingUserControl);
            ChungHsin_Field4UserControl.Add(receiveSettingUserControl);
            #endregion

            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var Ewatchitem in Ewatch_Field4Component)
            {
                Ewatchitem.MyWorkState = false;
            }
            foreach (var ChungHsinitem in ChungHsin_Field4Component)
            {
                ChungHsinitem.MyWorkState = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Ewatch_Field4UserControl.Count > ButtonMethod.ViewIndex & ButtonMethod.ViewIndex <= 5)
            {
                if (Ewatch_Field4UserControl.Count > ButtonMethod.ViewIndex)
                {
                    Ewatch_Field4UserControl[ButtonMethod.ViewIndex].TextChange();
                }
            }
            else
            {
                if (ChungHsin_Field4UserControl.Count > (ButtonMethod.ViewIndex - 6))
                {
                    ChungHsin_Field4UserControl[ButtonMethod.ViewIndex - 6].TextChange();
                }
            }
        }
    }
}
