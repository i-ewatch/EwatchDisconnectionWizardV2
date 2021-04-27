using Dapper;
using EwatchDisconnectionWizardV2.Configuration;
using EwatchDisconnectionWizardV2.Modules.ChungHsin_Modules;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Methods
{
    public class ChungHsin_MySqlMethod
    {
        /// <summary>
        /// Web資料庫連接字串
        /// </summary>
        public MySqlConnectionStringBuilder Webscsb { get; set; } = null;
        /// <summary>
        /// Log資料庫連接字串
        /// </summary>
        public MySqlConnectionStringBuilder Logscsb { get; set; } = null;
        /// <summary>
        /// LOG資料庫連接字串
        /// </summary>
        public MySqlConnectionStringBuilder scsb { get; set; } = null;

        #region Mysql初始化
        /// <summary>
        /// Mysql初始化
        /// </summary>
        /// <param name="mySqlSetting"></param>
        public ChungHsin_MySqlMethod(MySqlSetting mySqlSetting)
        {
            if (mySqlSetting != null)
            {
                scsb = new MySqlConnectionStringBuilder()
                {
                    Database = "ChungHsindb",
                    Server = mySqlSetting.DataSource,
                    UserID = mySqlSetting.UserID,
                    Password = mySqlSetting.Password,
                    CharacterSet = "utf8"
                };
                Logscsb = new MySqlConnectionStringBuilder()
                {
                    Database = "ChungHsinLog",
                    Server = mySqlSetting.DataSource,
                    UserID = mySqlSetting.UserID,
                    Password = mySqlSetting.Password,
                    CharacterSet = "utf8"
                };
                Webscsb = new MySqlConnectionStringBuilder()
                {
                    Database = "ChungHsinWeb",
                    Server = mySqlSetting.DataSource,
                    UserID = mySqlSetting.UserID,
                    Password = mySqlSetting.Password,
                    CharacterSet = "utf8"
                };
            }
        }
        #endregion

        #region 案場功能
        /// <summary>
        /// 案場資訊
        /// </summary>
        /// <returns></returns>
        public List<CaseSetting> CaseLoad()
        {
            List<CaseSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM CaseSetting";
                    setting = Conn.Query<CaseSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "案場資訊" + "錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 案場修改
        /// </summary>
        /// <param name="setting"></param>
        public void Update_CaseSetting(CaseSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE casesetting SET " +
                        "Address = @Address, " +
                        "Contacter = @Contacter," +
                        "Phone = @Phone," +
                        "TitleName = @TitleName," +
                        "NotifyTypeEnum = @NotifyTypeEnum," +
                        "NotifyApi = @NotifyApi," +
                        "NotifyToken = @NotifyToken," +
                        "Longitude = @Longitude," +
                        "Latitude = @Latitude" +
                        " WHERE CaseNo = @CaseNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "案場修改" + "錯誤");
            }

        }
        /// <summary>
        /// 案場新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_CaseSetting(CaseSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO casesetting (CaseNo,Address,Contacter,Phone,TitleName,NotifyTypeEnum,NotifyApi,NotifyToken,Longitude,Latitude)" +
                        " VALUES(@CaseNo,@Address,@Contacter,@Phone,@TitleName,@NotifyTypeEnum,@NotifyApi,@NotifyToken,@Longitude,@Latitude) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "案場新增" + "錯誤");
            }
        }
        /// <summary>
        /// 案場刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        public void Delete_CaseSetting(string CaseNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM casesetting  WHERE CaseNo = @CaseNo ";
                    Conn.Execute(sql, new { CaseNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "案場刪除" + "錯誤");
            }
        }
        #endregion

        #region DeviceConfig(設備類型)
        /// <summary>
        /// 設備類型資訊
        /// </summary>
        /// <returns></returns>
        public List<DeviceConfig> DevicesLoad()
        {
            List<DeviceConfig> configs = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM DeviceConfig";
                    configs = Conn.Query<DeviceConfig>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "設備類型資訊" + "錯誤(ChungHsin)");
            }
            return configs;
        }
        /// <summary>
        /// 設備更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_DeviceConfig(DeviceConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE DeviceConfig SET " +
                        "DeviceName = @DeviceName" +
                        " WHERE DeviceTypeEnum = @DeviceTypeEnum";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeviceConfig更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// 設備刪除
        /// </summary>
        /// <param name="DeviceTypeEnum"></param>
        public void Delete_DeviceConfig(int DeviceTypeEnum)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM DeviceConfig  WHERE  DeviceTypeEnum = @DeviceTypeEnum ";
                    Conn.Execute(sql, new { DeviceTypeEnum });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeviceConfig刪除" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// 設備新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_DeviceConfig(DeviceConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO DeviceConfig (DeviceTypeEnum,DeviceName)" +
                       " VALUES(@DeviceTypeEnum,@DeviceName) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DeviceConfig新增" + "錯誤(ChungHsin)");
            }
        }
        #endregion

        #region Receive(接收資訊)
        /// <summary>
        /// Receive資訊
        /// </summary>
        /// <returns></returns>
        public List<ReceiveSetting> ReceiveLoad()
        {
            List<ReceiveSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM ReceiveSetting";
                    setting = Conn.Query<ReceiveSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receive資訊" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// Receive更新
        /// </summary>
        /// <param name="setting"></param>
        public void Updata_ReceiveSetting(ReceiveSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE ReceiveSetting SET " +
                        "DeviceTypeEnum  = @DeviceTypeEnum," +
                        "ReceiveName = @ReceiveName," +
                        "NotifyFlag = @NotifyFlag," +
                        "HTimeoutSpan = @HTimeoutSpan," +
                        "MTimeoutSpan = @MTimeoutSpan" +
                        " WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receive更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// Receive新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_ReceiveSettig(ReceiveSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO ReceiveSetting (DeviceTypeEnum,CaseNo,ReceiveNo,ReceiveName,NotifyFlag,HTimeoutSpan,MTimeoutSpan)" +
                       " VALUES(@DeviceTypeEnum,@CaseNo,@ReceiveNo,@ReceiveName,@NotifyFlag,@HTimeoutSpan,@MTimeoutSpan) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receive新增" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// Receive刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="ReceiveNo"></param>
        public void Delete_ReceiveSetting(string CaseNo, int ReceiveNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM ReceiveSetting  WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo ";
                    Conn.Execute(sql, new { CaseNo, ReceiveNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receive刪除" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// Receive資訊查案場
        /// </summary>
        /// <param name="caseSetting"></param>
        /// <returns></returns>
        public List<ReceiveSetting> ReceiveLoad(CaseSetting casesetting)
        {
            List<ReceiveSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM ReceiveSetting Where CaseNo = @CaseNo";
                    setting = Conn.Query<ReceiveSetting>(sql, new { CaseNo = casesetting.CaseNo }).ToList();
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Receive資訊查案場" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// AI即時資訊比較
        /// </summary>
        /// <param name="setting"> AI設定資訊</param>
        /// <returns></returns>
        public bool AI64Load(ReceiveSetting setting)
        {
            bool Flag = false;
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT TIMESTAMPDIFF(MINUTE,(SELECT ttimen FROM Ai64 Where CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo),@Datetime)";
                    var value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, ReceiveNo = setting.ReceiveNo, Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                    if (value != null)
                    {
                        if (value >= setting.MTimeoutSpan)
                        {
                            Flag = true;
                        }
                        else
                        {
                            Flag = false;
                        }
                    }
                    else
                    {
                        Flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI即時資訊比較" + "錯誤(ChungHsin)");
            }
            return Flag;
        }
        /// <summary>
        /// 更新Receive點位最後上傳時間
        /// </summary>
        /// <param name="setting"></param>
        public void UpdataReceive_Time(ReceiveSetting setting, bool TimeFlag)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE ReceiveSetting SET SendTime = @SendTime WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo";
                    if (TimeFlag)
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, ReceiveNo = setting.ReceiveNo });
                    }
                    else
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, ReceiveNo = setting.ReceiveNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新Receive最後上傳時間" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// 查詢AI點位上傳時間
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public DateTime? AI_LastTime(ReceiveSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT ttimen FROM Ai64 Where CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo";
                    var value = Conn.QuerySingle<DateTime>(sql, new { CaseNo = setting.CaseNo, ReceiveNo = setting.ReceiveNo });
                    return value;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新AI點位最後上傳時間" + "錯誤(ChungHsin)");
                return null;
            }
        }
        /// <summary>
        /// Receive上傳間隔時間
        /// </summary>
        /// <returns></returns>
        public int? Receive_Time(ReceiveSetting setting)
        {
            int? value = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT TIMESTAMPDIFF(HOUR,(SELECT SendTime FROM ReceiveSetting Where CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo),@Datetime)";
                    value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, ReceiveNo = setting.ReceiveNo, Datetime = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receive上傳間隔時間" + "錯誤(ChungHsin)");
            }
            return value;
        }
        #endregion

        #region AiConfig(AI點位資訊)
        /// <summary>
        /// 載入AI點位資訊
        /// </summary>
        /// <returns></returns>
        public List<AiConfig> AiConfigLoad()
        {
            List<AiConfig> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiConfig";
                    setting = Conn.Query<AiConfig>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入AI點位資訊" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// 載入AI點位資訊(指定設備)
        /// </summary>
        /// <returns></returns>
        public List<AiConfig> AiConfigLoad(int DeviceTypeEnum)
        {
            List<AiConfig> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiConfig WHERE DeviceTypeEnum = @DeviceTypeEnum";
                    setting = Conn.Query<AiConfig>(sql, new { DeviceTypeEnum }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入AI點位資訊(指定設備)" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// AiConfig更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_AiConfig(AiConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE AiConfig SET " +
                        "AIName = @AIName," +
                        "Aiunit = @Aiunit," +
                        "CharAddress = @CharAddress," +
                        "Ratio = @Ratio," +
                        "AnalysisMothed = @AnalysisMothed," +
                        "Enum_Array = @Enum_Array" +
                        " WHERE AINo = @AINo AND DeviceTypeEnum = @DeviceTypeEnum";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// AiConfig刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="AINo"></param>
        public void Delete_AiConfig(string CaseNo, int AINo, int DeviceTypeEnum)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM AiConfig  WHERE  AINo = @AINo AND DeviceTypeEnum = @DeviceTypeEnum ";
                    Conn.Execute(sql, new { CaseNo, AINo, DeviceTypeEnum });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig刪除" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        ///  AiConfig新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_AiConfig(AiConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO AiConfig (DeviceTypeEnum,AINo,CharAddress,AIName,Aiunit,Ratio,AnalysisMothed,Enum_Array)" +
                       " VALUES(@DeviceTypeEnum,@AINo,@CharAddress,@AIName,@Aiunit,@Ratio,@AnalysisMothed,@Enum_Array) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig新增" + "錯誤(ChungHsin)");
            }
        }
        #endregion

        #region AISetting
        /// <summary>
        /// 載入AI資訊
        /// </summary>
        /// <returns></returns>
        public List<AiSetting> AiSettingLoad()
        {
            List<AiSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiSetting";
                    setting = Conn.Query<AiSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入AI資訊" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// 載入AI資訊(指定設備)
        /// </summary>
        /// <returns></returns>
        public List<AiSetting> AiSettingLoad(string CaseNo, int ReciveNo)
        {
            List<AiSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiSetting WHERE CaseNo=@CaseNo AND ReciveNo=@ReciveNo";
                    setting = Conn.Query<AiSetting>(sql, new { CaseNo, ReciveNo }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入AI資訊(指定設備)" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// AiSettingg更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_AiSetting(AiSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE AiSetting SET " +
                       "CompareFlag = @CompareFlag," +
                       "Max = @Max," +
                       "Min = @Min," +
                       "CompareType = @CompareType," +
                       "EnumFlag = @EnumFlag," +
                       "EnumType = @EnumType" +
                       " WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo AND AINo = @AINo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiSetting更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// AiSetting新增
        /// </summary>
        /// <param name="settings"></param>
        public void Insert_AiSetting(List<AiSetting> settings)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    foreach (var setting in settings)
                    {
                        string sql = "INSERT IGNORE INTO AiSetting (ReceiveNo,CaseNo,AiNo)" +
                           " VALUES(@ReceiveNo,@CaseNo,@AiNo)";
                        Conn.Execute(sql, setting);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiSetting新增" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// AiSetting刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="ReceiveNo"></param>
        public void Delete_AiSetting(string CaseNo,int ReceiveNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM AiSetting  WHERE  CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo ";
                    Conn.Execute(sql, new { CaseNo, ReceiveNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiSetting刪除" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// AI點為設定查詢是否需要比較
        /// </summary>
        /// <returns></returns>
        public List<AiSetting> AiConfig_Compare_Load()
        {
            List<AiSetting> aiSettings = null;
            try
            {
                using (var conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiSetting WHERE CompareFlag = true";
                    aiSettings = conn.Query<AiSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI點為設定查詢是否需要比較" + "錯誤(Ewatch)");
            }
            return aiSettings;
        }
        /// <summary>
        /// 讀取AI64即時資訊(Web)
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public AI64 Ai64web(AiSetting setting)
        {
            try
            {
                using (var conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT * FROM Ai64 WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo";
                    var data = conn.QuerySingle<AI64>(sql, new { setting.CaseNo, setting.ReceiveNo });
                    return data;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "讀取AI64即時資訊(Web)" + "錯誤(Ewatch)");
                return null;
            }
        }
        #endregion

        #region StateConfig(狀態點位資訊)
        /// <summary>
        /// 載入狀態點位資訊(指定設備)
        /// </summary>
        /// <returns></returns>
        public List<StateConfig> StateConfigLoad()
        {
            List<StateConfig> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM StateConfig";
                    setting = Conn.Query<StateConfig>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入狀態點位資訊(指定設備)" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// 載入狀態點位資訊(指定設備)
        /// </summary>
        /// <returns></returns>
        public List<StateConfig> StateConfigLoad(int DeviceTypeEnum)
        {
            List<StateConfig> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM StateConfig WHERE DeviceTypeEnum = @DeviceTypeEnum";
                    setting = Conn.Query<StateConfig>(sql, new { DeviceTypeEnum }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入狀態點位資訊(指定設備)" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// StateConfig更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_StateConfig(StateConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE StateConfig SET " +
                        "StateName = @StateName," +
                        "CharAddress = @CharAddress," +
                        "StateBitAddress = @StateBitAddress," +
                        "StateFlag = @StateFlag," +
                        "StateHigh = @StateHigh," +
                        "StateLow = @StateLow" +
                        " WHERE StateNo = @StateNo AND DeviceTypeEnum = @DeviceTypeEnum";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateConfig更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// StateConfig刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="AINo"></param>
        public void Delete_StateConfig( int StateNo, int DeviceTypeEnum)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM StateConfig  WHERE  StateNo = @StateNo AND DeviceTypeEnum = @DeviceTypeEnum ";
                    Conn.Execute(sql, new { StateNo, DeviceTypeEnum });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateConfig刪除" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        ///  StateConfig新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_StateConfig(StateConfig setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO StateConfig (DeviceTypeEnum,StateNo,CharAddress,StateName,StateBitAddress,StateFalg,StateHigh,StateLow)" +
                       " VALUES(@DeviceTypeEnum,@StateNo,@CharAddress,@StateName,@StateBitAddress,@StateFalg,@StateHigh,@StateLow) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateConfig新增" + "錯誤(ChungHsin)");
            }
        }
        #endregion

        #region StateSetting
        /// <summary>
        /// 載入State資訊
        /// </summary>
        /// <returns></returns>
        public List<StateSetting> StateSettingLoad()
        {
            List<StateSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM StateSetting";
                    setting = Conn.Query<StateSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入AI資訊" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// 載入State資訊(指定設備)
        /// </summary>
        /// <returns></returns>
        public List<StateSetting> StateSettingLoad(string CaseNo, int ReciveNo)
        {
            List<StateSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiSetting WHERE CaseNo=@CaseNo AND ReciveNo=@ReciveNo";
                    setting = Conn.Query<StateSetting>(sql, new { CaseNo, ReciveNo }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "載入State資訊(指定設備)" + "錯誤(ChungHsin)");
            }
            return setting;
        }
        /// <summary>
        /// StateSettingg更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_StateSetting(StateSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE StateSetting SET " +
                       "NotifyFlag = @NotifyFlag" +
                       " WHERE CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo AND StateNo = @StateNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateSetting更新" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// StateSetting新增
        /// </summary>
        /// <param name="settings"></param>
        public void Insert_StateSetting(List<StateSetting> settings)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    foreach (var setting in settings)
                    {
                        string sql = "INSERT IGNORE INTO StateSetting (ReceiveNo,CaseNo,StateNo)" +
                           " VALUES(@ReceiveNo,@CaseNo,@StateNo)";
                        Conn.Execute(sql, setting);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateSetting新增" + "錯誤(ChungHsin)");
            }
        }
        /// <summary>
        /// StateSetting刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="ReceiveNo"></param>
        public void Delete_StateSetting(string CaseNo, int ReceiveNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM StateSetting  WHERE  CaseNo = @CaseNo AND ReceiveNo = @ReceiveNo ";
                    Conn.Execute(sql, new { CaseNo, ReceiveNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StateSetting刪除" + "錯誤(ChungHsin)");
            }
        }     
        #endregion
    }
}
