using EwatchDisconnectionWizardV2.Enums;
using EwatchDisconnectionWizardV2.Methods;
using EwatchDisconnectionWizardV2.Modules.Ewatch_Modules;
using LineNotifyLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramLibrary;

namespace EwatchDisconnectionWizardV2.Components.Ewatch_Components
{
    public partial class Ewatch_AlarmComponent : Ewatch_Field4Component
    {
        private List<AiConfig> AiConfigs { get; set; } = new List<AiConfig>();
        public Ewatch_AlarmComponent(Ewatch_MySqlMethod ewatch_MySqlMethod)
        {
            InitializeComponent();
            Ewatch_MySqlMethod = ewatch_MySqlMethod;
        }

        public Ewatch_AlarmComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                ConnectionThread = new System.Threading.Thread(Connection_Mysql);
                ConnectionThread.Start();
            }
            else
            {
                if (ConnectionThread != null)
                {
                    ConnectionThread.Abort();
                }
            }
        }
        private void Connection_Mysql()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ConnectionTime);
                if (timeSpan.TotalMilliseconds > 5000)
                {
                    CaseSettings = Ewatch_MySqlMethod.CaseLoad();
                    #region AI上下限
                    var Alarmdata = Ewatch_MySqlMethod.AiConfig_Compare_Load();
                    foreach (var item in Alarmdata)
                    {
                        var caseSetting = CaseSettings.Where(g => g.CaseNo == item.CaseNo).ToList()[0];
                        var value = Ewatch_MySqlMethod.Ai64web(item);
                        if (value != null)
                        {
                            var nowdata = Ewatch_MySqlMethod.Alarm_Procedure(item, value);

                            var Adata = AiConfigs.Where(g => g.PK == item.PK).ToList();
                            if (Adata.Count > 0)
                            {
                                if (item.CompareType != Adata[0].CompareType)
                                {
                                    NotifyTypeEnum notifyTypeEnum = (NotifyTypeEnum)caseSetting.NotifyTypeEnum;
                                    switch (notifyTypeEnum)
                                    {
                                        case NotifyTypeEnum.None:
                                            break;
                                        case NotifyTypeEnum.Line:
                                            {
                                                switch (item.CompareType)
                                                {
                                                    case 0: //上限
                                                        {
                                                            LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                            lineNotifyClass.LineNotifyFunction(caseSetting.NotifyToken, $"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r超過上限值: {item.AiMax}\r目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                    case 1: //正常
                                                        {
                                                            LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                            lineNotifyClass.LineNotifyFunction(caseSetting.NotifyToken, $"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r上限值: {item.AiMax}\r下限值: {item.AiMin}\r恢復正常，目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                    case 2: //下限
                                                        {
                                                            LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                            lineNotifyClass.LineNotifyFunction(caseSetting.NotifyToken, $"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r低於下限值: {item.AiMin}\r目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                        case NotifyTypeEnum.Telegram:
                                            {
                                                switch (item.CompareType)
                                                {
                                                    case 0: //上限
                                                        {
                                                            TelegramBotClass telegramBotClass = new TelegramBotClass(caseSetting.NotifyApi) { Chat_ID = caseSetting.NotifyToken };
                                                            telegramBotClass.Send_Message_Group($"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r超過上限值: {item.AiMax}\r目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                    case 1: //正常
                                                        {
                                                            TelegramBotClass telegramBotClass = new TelegramBotClass(caseSetting.NotifyApi) { Chat_ID = caseSetting.NotifyToken };
                                                            telegramBotClass.Send_Message_Group($"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r上限值: {item.AiMax}\r下限值: {item.AiMin}\r恢復正常，目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                    case 2: //下限
                                                        {
                                                            TelegramBotClass telegramBotClass = new TelegramBotClass(caseSetting.NotifyApi) { Chat_ID = caseSetting.NotifyToken };
                                                            telegramBotClass.Send_Message_Group($"\r案場名稱: {caseSetting.TitleName}\r點位名稱: {item.AiName}\r低於下限值: {item.AiMin}\r目前數值為 {nowdata}");
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    AiConfigs = Alarmdata;
                    ConnectionTime = DateTime.Now;
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }
    }
}
