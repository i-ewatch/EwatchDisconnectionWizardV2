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

namespace EwatchDisconnectionWizardV2.Components.Ewatch_Components
{
    public partial class Ewatch_MySqlComponent : Ewatch_Field4Component
    {
        public Ewatch_MySqlComponent(Ewatch_MySqlMethod ewatch_MySqlMethod)
        {
            InitializeComponent();
            Ewatch_MySqlMethod = ewatch_MySqlMethod;
        }

        public Ewatch_MySqlComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                CaseSettings = Ewatch_MySqlMethod.CaseLoad();
                AiSettings = Ewatch_MySqlMethod.AiLoad();
                ElectricSettings = Ewatch_MySqlMethod.ElectricLoad();
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
                if (timeSpan.TotalMilliseconds > 5000) //每一秒查詢動作
                {
                    var NewCaseSettings = Ewatch_MySqlMethod.CaseLoad();
                    var NewAiSettings = Ewatch_MySqlMethod.AiLoad();
                    var NewElectricSettings = Ewatch_MySqlMethod.ElectricLoad();
                    var casevalue = Ewatch_MySqlMethod.CaseLoad();
                    foreach (var caseitem in casevalue)
                    {
                        #region AI接收
                        var aivalue = Ewatch_MySqlMethod.AiLoad(caseitem);
                        foreach (var aiitem in aivalue)
                        {
                            if (aiitem.NotifyFlag)//AI點位需要發報
                            {
                                if (Ewatch_MySqlMethod.AI64Load(aiitem))//檢查即時數值是否超過時間
                                {
                                    var TimeValue = Ewatch_MySqlMethod.Ai_Time(aiitem);//檢查發報數值是否達到
                                    if (TimeValue == null || TimeValue >= aiitem.TimeoutSpan)
                                    {
                                        if (NewAiSettings.SingleOrDefault(g => g.PK == aiitem.PK) != null)
                                        {
                                            NewAiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "斷線";
                                            NotifyTypeEnum notifyTypeEnum = (NotifyTypeEnum)caseitem.NotifyTypeEnum;
                                            switch (notifyTypeEnum)
                                            {
                                                case NotifyTypeEnum.None:
                                                    break;
                                                case NotifyTypeEnum.Line:
                                                    {
                                                        LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                        lineNotifyClass.LineNotifyFunction(caseitem.NotifyToken, $"設備名稱:{aiitem.AiName} \r最後上傳時間 : {Ewatch_MySqlMethod.AI_LastTime(aiitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                                case NotifyTypeEnum.Telegram:
                                                    {
                                                        TelegramBotClass telegramBotClass = new TelegramBotClass(caseitem.NotifyApi) { Chat_ID = caseitem.NotifyToken };
                                                        telegramBotClass.Send_Message_Group($"設備名稱:{aiitem.AiName} \r最後上傳時間 : {Ewatch_MySqlMethod.AI_LastTime(aiitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                            }
                                            Ewatch_MySqlMethod.UpdataAi_Time(aiitem, true);
                                        }
                                    }
                                    else
                                    {
                                        NewAiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "斷線";
                                    }
                                }
                                else
                                {
                                    if (NewAiSettings.SingleOrDefault(g => g.PK == aiitem.PK) != null)
                                    {
                                        NewAiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "連線";
                                        Ewatch_MySqlMethod.UpdataAi_Time(aiitem, false);
                                    }
                                }
                            }
                            else
                            {
                                //AiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "不使用";
                                if (Ewatch_MySqlMethod.AI64Load(aiitem))//檢查即時數值是否超過時間
                                {
                                    if (NewAiSettings.SingleOrDefault(g => g.PK == aiitem.PK) != null)
                                    {
                                        NewAiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "斷線";
                                        //Ewatch_MySqlMethod.UpdataAi_Time(aiitem, true);
                                    }
                                }
                                else
                                {
                                    if (NewAiSettings.SingleOrDefault(g => g.PK == aiitem.PK) != null)
                                    {
                                        NewAiSettings.Single(g => g.PK == aiitem.PK).ConnectionFlag = "連線";
                                        Ewatch_MySqlMethod.UpdataAi_Time(aiitem, false);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 電表接收
                        var electricvalue = Ewatch_MySqlMethod.ElectricLoad(caseitem);
                        foreach (var electricitem in electricvalue)
                        {
                            if (electricitem.NotifyFlag)//電表點位需要發報
                            {
                                if (Ewatch_MySqlMethod.ElectricMeterLoad(electricitem))
                                {
                                    var TimeValue = Ewatch_MySqlMethod.ElectricMeter_Time(electricitem);
                                    if (TimeValue == null || TimeValue >= electricitem.TimeoutSpan)
                                    {
                                        if (NewElectricSettings.SingleOrDefault(g => g.PK == electricitem.PK) != null)
                                        {
                                            NewElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "斷線";
                                            NotifyTypeEnum notifyTypeEnum = (NotifyTypeEnum)caseitem.NotifyTypeEnum;
                                            switch (notifyTypeEnum)
                                            {
                                                case NotifyTypeEnum.None:
                                                    break;
                                                case NotifyTypeEnum.Line:
                                                    {
                                                        LineNotifyClass lineNotifyClass = new LineNotifyClass();
                                                        lineNotifyClass.LineNotifyFunction(caseitem.NotifyToken, $"設備名稱:{electricitem.ElectricName} \r最後上傳時間 : {Ewatch_MySqlMethod.ElectricMeter_LastTime(electricitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                                case NotifyTypeEnum.Telegram:
                                                    {
                                                        TelegramBotClass telegramBotClass = new TelegramBotClass(caseitem.NotifyApi) { Chat_ID = caseitem.NotifyToken };
                                                        telegramBotClass.Send_Message_Group($"設備名稱:{electricitem.ElectricName} \r最後上傳時間 : {Ewatch_MySqlMethod.ElectricMeter_LastTime(electricitem)}\r上傳逾時請檢查");
                                                    }
                                                    break;
                                            }
                                            Ewatch_MySqlMethod.UpdataElectricMeter_Time(electricitem, true);
                                        }
                                    }
                                    else
                                    {
                                        NewElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "斷線";
                                    }
                                }
                                else
                                {
                                    if (NewElectricSettings.SingleOrDefault(g => g.PK == electricitem.PK) != null)
                                    {
                                        NewElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "連線";
                                        Ewatch_MySqlMethod.UpdataElectricMeter_Time(electricitem, false);
                                    }
                                }
                            }
                            else
                            {
                                //ElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "不使用";
                                var TimeValue = Ewatch_MySqlMethod.ElectricMeter_Time(electricitem);
                                if (TimeValue == null || TimeValue >= electricitem.TimeoutSpan)
                                {
                                    if (NewElectricSettings.SingleOrDefault(g => g.PK == electricitem.PK) != null)
                                    {
                                        NewElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "斷線";
                                    }
                                }
                                else
                                {
                                    if (NewElectricSettings.SingleOrDefault(g => g.PK == electricitem.PK) != null)
                                    {
                                        NewElectricSettings.Single(g => g.PK == electricitem.PK).ConnectionFlag = "連線";
                                        Ewatch_MySqlMethod.UpdataElectricMeter_Time(electricitem, false);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    CaseSettings = NewCaseSettings;
                    AiSettings = NewAiSettings;
                    ElectricSettings = NewElectricSettings;
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
