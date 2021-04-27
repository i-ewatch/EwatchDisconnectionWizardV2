using EwatchDisconnectionWizardV2.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Methods
{
    public class InitialMethod
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        private static string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        #region 資料庫Json 建檔與讀取
        /// <summary>
        /// 資料庫Json 建檔與讀取
        /// </summary>
        /// <returns></returns>
        public static MySqlSetting MySqlLoad()
        {
            MySqlSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\MySql.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<MySqlSetting>(json);
                }
                else
                {
                    MySqlSetting Setting = new MySqlSetting()
                    {
                        DataSource = "192.168.100.104",
                        InitialCatalog = "test",
                        UserID = "root",
                        Password = "Iewatch001007"
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " MySQLDB資訊設定載入錯誤");
            }
            return setting;
        }
        #endregion
        #region 按鈕Json 建檔與讀取
        /// <summary>
        /// 按鈕Json 建檔與讀取
        /// </summary>
        /// <returns></returns>
        public static ButtonSetting InitialButtonLoad()
        {
            ButtonSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\button.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<ButtonSetting>(json);
                }
                else
                {
                    ButtonSetting Setting = new ButtonSetting()
                    {
                        //群組與列表按鈕設定
                        ButtonGroupSettings =
                        {
                            new ButtonGroupSetting()
                            {
                                // 0 = 群組，1 = 列表
                                ButtonStyle = 1,
                                //群組名稱
                                GroupName = "群組名稱",
                                // 群組標註
                                GroupTag = 0,
                                //列表按鈕設定
                                ButtonItemSettings=
                                {
                                    new ButtonItemSetting()
                                    {
                                        //列表名稱
                                        ItemName = "列表名稱",
                                        //列表標註
                                        ItemTag = 0,
                                        //控制畫面顯示
                                        ControlVisible = true
                                    }
                                }
                            }
                        }
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "按鈕載入失敗");
            }
            return setting;
        }
        #endregion
    }
}
