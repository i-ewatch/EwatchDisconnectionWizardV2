using EwatchDisconnectionWizardV2.Enums;
using EwatchDisconnectionWizardV2.Methods;
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

namespace EwatchDisconnectionWizardV2.Components.ChungHsin_Components
{
    public partial class ChungHsin_MySqlComponent : ChungHsin_Field4Component
    {
        public ChungHsin_MySqlComponent(ChungHsin_MySqlMethod chungHsin_MySqlMethod)
        {
            InitializeComponent();
            ChungHsin_MySqlMethod = chungHsin_MySqlMethod;
        }

        public ChungHsin_MySqlComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                CaseSettings = ChungHsin_MySqlMethod.CaseLoad();
                ReceiveSettings = ChungHsin_MySqlMethod.ReceiveLoad();
                DeviceConfigs = ChungHsin_MySqlMethod.DevicesLoad();
                ConnectionThread = new Thread(Connection_Mysql);
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
                    var NewCaseSettings = ChungHsin_MySqlMethod.CaseLoad();
                    var NewReceiveSettings = ChungHsin_MySqlMethod.ReceiveLoad();
                    var NewDeviceConfigs = ChungHsin_MySqlMethod.DevicesLoad();
                    foreach (var Caseitem in NewCaseSettings)
                    {
                        #region 接收
                        var ReceiveValue = ChungHsin_MySqlMethod.ReceiveLoad(Caseitem);
                        foreach (var Receiveitem in ReceiveValue)
                        {
                            if (Receiveitem.NotifyFlag)
                            {
                                if (ChungHsin_MySqlMethod.AI64Load(Receiveitem))
                                {
                                    var TimeValue = ChungHsin_MySqlMethod.Receive_Time(Receiveitem);
                                    if (TimeValue == null || TimeValue >= Receiveitem.HTimeoutSpan)
                                    {
                                        if (NewReceiveSettings.SingleOrDefault(g => g.PK == Receiveitem.PK) != null)
                                        {
                                            NewReceiveSettings.Single(g => g.PK == Receiveitem.PK).ConnectionFlag = "斷線";
                                            NotifyTypeEnum notifyTypeEnum = (NotifyTypeEnum)Caseitem.NotifyTypeEnum;
                                            switch (notifyTypeEnum)
                                            {
                                                case NotifyTypeEnum.None:
                                                    break;
                                                case NotifyTypeEnum.Line:
                                                    {
                                                        LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                        lineNotifyClass.LineNotifyFunction(Caseitem.NotifyToken, $"設備名稱:{Receiveitem.ReceiveName} \r最後上傳時間 : {ChungHsin_MySqlMethod.AI_LastTime(Receiveitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                                case NotifyTypeEnum.Telegram:
                                                    {
                                                        TelegramBotClass telegramBotClass = new TelegramBotClass(Caseitem.NotifyApi) { Chat_ID = Caseitem.NotifyToken };
                                                        telegramBotClass.Send_Message_Group($"設備名稱:{Receiveitem.ReceiveName} \r最後上傳時間 : {ChungHsin_MySqlMethod.AI_LastTime(Receiveitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                            }
                                            ChungHsin_MySqlMethod.UpdataReceive_Time(Receiveitem, true);
                                        }
                                    }
                                    else
                                    {
                                        NewReceiveSettings.Single(g => g.PK == Receiveitem.PK).ConnectionFlag = "斷線";
                                    }
                                }
                                else
                                {
                                    if (NewReceiveSettings.SingleOrDefault(g => g.PK == Receiveitem.PK) != null)
                                    {
                                        NewReceiveSettings.Single(g => g.PK == Receiveitem.PK).ConnectionFlag = "連線";
                                        ChungHsin_MySqlMethod.UpdataReceive_Time(Receiveitem, false);
                                    }
                                }
                            }
                            else
                            {
                                if (ChungHsin_MySqlMethod.AI64Load(Receiveitem))
                                {
                                    if (NewReceiveSettings.SingleOrDefault(g => g.PK == Receiveitem.PK) != null)
                                    {
                                        NewReceiveSettings.Single(g => g.PK == Receiveitem.PK).ConnectionFlag = "斷線";
                                        //ChungHsin_MySqlMethod.UpdataReceive_Time(Receiveitem, true);
                                    }
                                }
                                else
                                {
                                    if (NewReceiveSettings.SingleOrDefault(g => g.PK == Receiveitem.PK) != null)
                                    {
                                        NewReceiveSettings.Single(g => g.PK == Receiveitem.PK).ConnectionFlag = "連線";
                                        ChungHsin_MySqlMethod.UpdataReceive_Time(Receiveitem, false);
                                    }
                                }

                            }
                        }
                        #endregion
                    }
                    CaseSettings = NewCaseSettings;
                    ReceiveSettings = NewReceiveSettings;
                    DeviceConfigs = NewDeviceConfigs;
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

