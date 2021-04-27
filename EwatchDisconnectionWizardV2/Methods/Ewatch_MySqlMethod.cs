using Dapper;
using EwatchDisconnectionWizardV2.Configuration;
using EwatchDisconnectionWizardV2.Enums;
using EwatchDisconnectionWizardV2.Modules.Ewatch_Modules;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchDisconnectionWizardV2.Methods
{
    public class Ewatch_MySqlMethod
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
        public Ewatch_MySqlMethod(MySqlSetting mySqlSetting)
        {
            if (mySqlSetting != null)
            {
                scsb = new MySqlConnectionStringBuilder()
                {
                    Database = "Ewatchdb",
                    Server = mySqlSetting.DataSource,
                    UserID = mySqlSetting.UserID,
                    Password = mySqlSetting.Password,
                    CharacterSet = "utf8"
                };
                Logscsb = new MySqlConnectionStringBuilder()
                {
                    Database = "EwatchLog",
                    Server = mySqlSetting.DataSource,
                    UserID = mySqlSetting.UserID,
                    Password = mySqlSetting.Password,
                    CharacterSet = "utf8"
                };
                Webscsb = new MySqlConnectionStringBuilder()
                {
                    Database = "EwatchWeb",
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
                Log.Error(ex, "案場資訊" + "錯誤(Ewatch)");
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
                        " WHERE CaseNo = @CaseNo AND AiNo = @AiNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "案場修改" + "錯誤(Ewatch)");
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
                Log.Error(ex, "案場新增" + "錯誤(Ewatch)");
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
                Log.Error(ex, "案場刪除" + "錯誤(Ewatch)");
            }
        }
        #endregion

        #region AI
        /// <summary>
        /// AI資訊
        /// </summary>
        /// <returns></returns>
        public List<AiSetting> AiLoad()
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
                Log.Error(ex, "AI資訊" + "錯誤(Ewatch)");
            }
            return setting;
        }
        /// <summary>
        /// Ai更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_AiSetting(AiSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE AiSetting SET " +
                        "AiName = @AiName," +
                        "NotifyFlag = @NotifyFlag," +
                        "TimeoutSpan = @TimeoutSpan," +
                        "MTimeoutSpan = @MTimeoutSpan" +
                        " WHERE CaseNo = @CaseNo AND AiNo = @AiNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ai更新" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// Ai新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_AiSetting(AiSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO AiSetting (CaseNo,AiNo,AiName,NotifyFlag,TimeoutSpan,MTimeoutSpan)" +
                       " VALUES(@CaseNo,@AiNo,@AiName,@NotifyFlag,@TimeoutSpan,@MTimeoutSpan) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ai新增" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// Ai刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="AiNo"></param>
        public void Delete_AiSetting(string CaseNo, int AiNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM AiSetting  WHERE CaseNo = @CaseNo AND AiNo = @AiNo ";
                    Conn.Execute(sql, new { CaseNo, AiNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ai刪除" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// AI資訊查案場
        /// </summary>
        /// <returns></returns>
        public List<AiSetting> AiLoad(CaseSetting casesetting)
        {
            List<AiSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiSetting Where CaseNo = @CaseNo";
                    setting = Conn.Query<AiSetting>(sql, new { CaseNo = casesetting.CaseNo }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI資訊查案場" + "錯誤(Ewatch)");
            }
            return setting;
        }
        /// <summary>
        /// AI即時資訊比較
        /// </summary>
        /// <param name="setting"> AI設定資訊</param>
        /// <returns></returns>
        public bool AI64Load(AiSetting setting)
        {
            bool Flag = false;
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT TIMESTAMPDIFF(MINUTE,(SELECT ttimen FROM Ai64 Where CaseNo = @CaseNo AND AiNo = @AiNo),@Datetime)";
                    var value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, AiNo = setting.AiNo, Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
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
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI即時資訊比較" + "錯誤(Ewatch)");
            }
            return Flag;
        }
        /// <summary>
        /// 更新AI點位最後上傳時間
        /// </summary>
        /// <param name="setting"></param>
        public void UpdataAi_Time(AiSetting setting, bool TimeFlag)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE AiSetting SET SendTime = @SendTime WHERE CaseNo = @CaseNo AND AiNo = @AiNo";
                    if (TimeFlag)
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, AiNo = setting.AiNo });
                    }
                    else
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, AiNo = setting.AiNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新AI點位最後上傳時間" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 查詢AI點位上傳時間
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public DateTime? AI_LastTime(AiSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT ttimen FROM Ai64 Where CaseNo = @CaseNo AND AiNo = @AiNo";
                    var value = Conn.QuerySingle<DateTime>(sql, new { CaseNo = setting.CaseNo, AiNo = setting.AiNo });
                    return value;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新AI點位最後上傳時間" + "錯誤(Ewatch)");
                return null;
            }
        }
        /// <summary>
        /// AI上傳間隔時間
        /// </summary>
        /// <returns></returns>
        public int? Ai_Time(AiSetting setting)
        {
            int? value = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT TIMESTAMPDIFF(HOUR,(SELECT SendTime FROM AiSetting Where CaseNo = @CaseNo AND AiNo = @AiNo),@Datetime)";
                    value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, AiNo = setting.AiNo, Datetime = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI上傳間隔時間" + "錯誤(Ewatch)");
            }
            return value;
        }
        #endregion

        #region AI點位資訊
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
                Log.Error(ex, "載入AI點位資訊" + "錯誤(Ewatch)");
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
                        "AiName = @AiName," +
                        "Aiunit = @Aiunit," +
                        "AicalculateFlag = @AicalculateFlag," +
                        "CompareFlag = @CompareFlag," +
                        "AiMax = @AiMax," +
                        "AiMin = @AiMin," +
                        "EnumFlag = @EnumFlag," +
                        "Enum_Array = @Enum_Array" +
                        " WHERE CaseNo = @CaseNo AND AiNo = @AiNo AND Ai = @Ai";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig更新" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// AiConfig刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="AiNo"></param>
        public void Delete_AiConfig(string CaseNo, int AiNo, string Ai)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM AiConfig  WHERE CaseNo = @CaseNo AND AiNo = @AiNo AND Ai = @Ai ";
                    Conn.Execute(sql, new { CaseNo, AiNo, Ai });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig刪除" + "錯誤(Ewatch)");
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
                    string sql = "INSERT IGNORE INTO AiConfig (CaseNo,AiNo,Ai,AiName,Aiunit,AicalculateFlag,CompareFlag,AiMax,AiMin,EnumFlag,Enum_Array)" +
                       " VALUES(@CaseNo,@AiNo,@Ai,@AiName,@Aiunit,@AicalculateFlag,@CompareFlag,@AiMax,@AiMin,@EnumFlag,@Enum_Array) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AiConfig新增" + "錯誤(Ewatch)");
            }
        }
        #region AI 點位上下限比較
        /// <summary>
        /// AI點為設定查詢是否需要比較
        /// </summary>
        /// <returns></returns>
        public List<AiConfig> AiConfig_Compare_Load()
        {
            List<AiConfig> aiConfigs = null;
            try
            {
                using (var conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM AiConfig WHERE CompareFlag = true";
                    aiConfigs = conn.Query<AiConfig>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AI點為設定查詢是否需要比較" + "錯誤(Ewatch)");
            }
            return aiConfigs;
        }
        /// <summary>
        /// 讀取AI64即時資訊(Web)
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public AI64 Ai64web(AiConfig config)
        {
            try
            {
                using (var conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = "SELECT * FROM Ai64 WHERE CaseNo = @CaseNo AND AiNo = @AiNo";
                    var data = conn.QuerySingle<AI64>(sql, new { config.CaseNo, config.AiNo });
                    return data;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "讀取AI64即時資訊(Web)" + "錯誤(Ewatch)");
                return null;
            }
        }
        /// <summary>
        /// 上下限比較紀錄
        /// </summary>
        /// <param name="config">AI點為設定資訊</param>
        public decimal Alarm_Procedure(AiConfig config, AI64 aI64)
        {
            try
            {
                decimal[] value = new decimal[64];
                using (var conn = new MySqlConnection(Logscsb.ConnectionString))
                {
                    string Procedure = "AiAlarmProcedure";
                    int i = 0;
                    value[i] = aI64.Ai1; i++;
                    value[i] = aI64.Ai2; i++;
                    value[i] = aI64.Ai3; i++;
                    value[i] = aI64.Ai4; i++;
                    value[i] = aI64.Ai5; i++;
                    value[i] = aI64.Ai6; i++;
                    value[i] = aI64.Ai7; i++;
                    value[i] = aI64.Ai8; i++;
                    value[i] = aI64.Ai9; i++;
                    value[i] = aI64.Ai10; i++;
                    value[i] = aI64.Ai11; i++;
                    value[i] = aI64.Ai12; i++;
                    value[i] = aI64.Ai13; i++;
                    value[i] = aI64.Ai14; i++;
                    value[i] = aI64.Ai15; i++;
                    value[i] = aI64.Ai16; i++;
                    value[i] = aI64.Ai17; i++;
                    value[i] = aI64.Ai18; i++;
                    value[i] = aI64.Ai19; i++;
                    value[i] = aI64.Ai20; i++;
                    value[i] = aI64.Ai21; i++;
                    value[i] = aI64.Ai22; i++;
                    value[i] = aI64.Ai23; i++;
                    value[i] = aI64.Ai24; i++;
                    value[i] = aI64.Ai25; i++;
                    value[i] = aI64.Ai26; i++;
                    value[i] = aI64.Ai27; i++;
                    value[i] = aI64.Ai28; i++;
                    value[i] = aI64.Ai29; i++;
                    value[i] = aI64.Ai30; i++;
                    value[i] = aI64.Ai31; i++;
                    value[i] = aI64.Ai32; i++;
                    value[i] = aI64.Ai33; i++;
                    value[i] = aI64.Ai34; i++;
                    value[i] = aI64.Ai35; i++;
                    value[i] = aI64.Ai36; i++;
                    value[i] = aI64.Ai37; i++;
                    value[i] = aI64.Ai38; i++;
                    value[i] = aI64.Ai39; i++;
                    value[i] = aI64.Ai40; i++;
                    value[i] = aI64.Ai41; i++;
                    value[i] = aI64.Ai42; i++;
                    value[i] = aI64.Ai43; i++;
                    value[i] = aI64.Ai44; i++;
                    value[i] = aI64.Ai45; i++;
                    value[i] = aI64.Ai46; i++;
                    value[i] = aI64.Ai47; i++;
                    value[i] = aI64.Ai48; i++;
                    value[i] = aI64.Ai49; i++;
                    value[i] = aI64.Ai50; i++;
                    value[i] = aI64.Ai51; i++;
                    value[i] = aI64.Ai52; i++;
                    value[i] = aI64.Ai53; i++;
                    value[i] = aI64.Ai54; i++;
                    value[i] = aI64.Ai55; i++;
                    value[i] = aI64.Ai56; i++;
                    value[i] = aI64.Ai57; i++;
                    value[i] = aI64.Ai58; i++;
                    value[i] = aI64.Ai59; i++;
                    value[i] = aI64.Ai60; i++;
                    value[i] = aI64.Ai61; i++;
                    value[i] = aI64.Ai62; i++;
                    value[i] = aI64.Ai63; i++;
                    value[i] = aI64.Ai64;
                    conn.Execute(Procedure, new { nowTime = aI64.ttime, CaseNo1 = config.CaseNo, AiNo1 = config.AiNo, Ai1 = config.Ai, NowData = value[Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(config.Ai, @"[^0-9]+", "")) - 1] }, commandType: System.Data.CommandType.StoredProcedure);
                }
                return value[Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(config.Ai, @"[^0-9]+", "")) - 1];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "上下限比較紀錄" + "錯誤(Ewatch)");
                return 0;
            }
        }
        #endregion


        #endregion

        #region 電表
        /// <summary>
        /// 電表資訊
        /// </summary>
        /// <returns></returns>
        public List<ElectricSetting> ElectricLoad()
        {
            List<ElectricSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM ElectricSetting ";
                    setting = Conn.Query<ElectricSetting>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表資訊" + "錯誤(Ewatch)");
            }
            return setting;
        }
        /// <summary>
        /// 電表更新
        /// </summary>
        /// <param name="electricSetting"></param>
        public void Update_ElectricSetting(ElectricSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE ElectricSetting SET " +
                        "ElectricName = @ElectricName," +
                        "PhaseTypeEnum = @PhaseTypeEnum," +
                        "NotifyFlag = @NotifyFlag," +
                        "TimeoutSpan = @TimeoutSpan," +
                        "MTimeoutSpan = @MTimeoutSpan" +
                        " WHERE CaseNo = @CaseNo AND ElectricNo = @ElectricNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表更新" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 電表新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_ElectricSetting(ElectricSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO ElectricSetting (CaseNo,ElectricNo,ElectricName,PhaseTypeEnum,NotifyFlag,TimeoutSpan,MTimeoutSpan)" +
                       " VALUES(@CaseNo,@ElectricNo,@ElectricName,@PhaseTypeEnum,@NotifyFlag,@TimeoutSpan,@MTimeoutSpan) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表新增" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 電表刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="AiNo"></param>
        public void Delete_ElectricSetting(string CaseNo, int ElectricNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM ElectricSetting  WHERE CaseNo = @CaseNo AND ElectricNo = @ElectricNo ";
                    Conn.Execute(sql, new { CaseNo, ElectricNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表刪除" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 電表資訊查詢案場
        /// </summary>
        /// <returns></returns>
        public List<ElectricSetting> ElectricLoad(CaseSetting casesetting)
        {
            List<ElectricSetting> setting = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT * FROM ElectricSetting Where CaseNo = @CaseNo";
                    setting = Conn.Query<ElectricSetting>(sql, new { CaseNo = casesetting.CaseNo }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表資訊查詢案場" + "錯誤(Ewatch)");
            }
            return setting;
        }
        /// <summary>
        /// 電表即時資訊比較
        /// </summary>
        /// <param name="setting">電表設定資訊</param>
        /// <returns></returns>
        public bool ElectricMeterLoad(ElectricSetting setting)
        {
            bool Flag = false;
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {
                    string sql = string.Empty;
                    ElectricPhaseTypeEnum phaseTypeEnum = (ElectricPhaseTypeEnum)setting.PhaseTypeEnum;
                    switch (phaseTypeEnum)
                    {
                        case ElectricPhaseTypeEnum.Three:
                            {
                                sql = "SELECT TIMESTAMPDIFF(MINUTE,(SELECT ttimen FROM ThreePhaseElectricMeter Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo),@Datetime)";
                            }
                            break;
                        case ElectricPhaseTypeEnum.Single:
                            {
                                sql = "SELECT TIMESTAMPDIFF(MINUTE,(SELECT ttimen FROM SinglePhaseElectricMeter Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo),@Datetime)";
                            }
                            break;
                    }
                    var value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, ElectricNo = setting.ElectricNo, Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
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
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表即時資訊比較" + "錯誤(Ewatch)");
            }
            return Flag;
        }
        /// <summary>
        /// 更新電表點位最後上傳時間
        /// </summary>
        /// <param name="setting"></param>
        public void UpdataElectricMeter_Time(ElectricSetting setting, bool TimeFlag)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE ElectricSetting SET SendTime = @SendTime Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo";
                    if (TimeFlag)
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, ElectricNo = setting.ElectricNo });
                    }
                    else
                    {
                        var value = Conn.Execute(sql, new { SendTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss"), CaseNo = setting.CaseNo, ElectricNo = setting.ElectricNo });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新電表點位最後上傳時間" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 查詢電表點位上傳時間
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public DateTime? ElectricMeter_LastTime(ElectricSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(Webscsb.ConnectionString))
                {

                    string sql = string.Empty;
                    ElectricPhaseTypeEnum phaseTypeEnum = (ElectricPhaseTypeEnum)setting.PhaseTypeEnum;
                    switch (phaseTypeEnum)
                    {
                        case ElectricPhaseTypeEnum.Three:
                            {
                                sql = "SELECT ttimen FROM ThreePhaseElectricMeter Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo";
                            }
                            break;
                        case ElectricPhaseTypeEnum.Single:
                            {
                                sql = "SELECT ttimen FROM SinglePhaseElectricMeter Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo";
                            }
                            break;
                    }
                    var value = Conn.QuerySingle<DateTime>(sql, new { CaseNo = setting.CaseNo, ElectricNo = setting.ElectricNo });
                    return value;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "查詢電表點位上傳時間" + "錯誤(Ewatch)");
                return null;
            }
        }
        /// <summary>
        /// 電表上傳間隔時間
        /// </summary>
        /// <returns></returns>
        public int? ElectricMeter_Time(ElectricSetting setting)
        {
            int? value = null;
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "SELECT TIMESTAMPDIFF(HOUR,(SELECT SendTime FROM ElectricSetting Where CaseNo = @CaseNo AND ElectricNo = @ElectricNo),@Datetime)";

                    value = Conn.QuerySingle<int?>(sql, new { CaseNo = setting.CaseNo, ElectricNo = setting.ElectricNo, Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "電表上傳間隔時間" + "錯誤(Ewatch)");
            }
            return value;
        }
        #endregion

        #region 狀態
        /// <summary>
        /// 狀態資訊
        /// </summary>
        /// <returns></returns>
        public List<StateSetting> StateLoad()
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
                Log.Error(ex, "狀態資訊" + "錯誤(Ewatch)");
            }
            return setting;
        }
        /// <summary>
        /// 狀態更新
        /// </summary>
        /// <param name="setting"></param>
        public void Update_StateSetting(StateSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "UPDATE StateSetting SET " +
                        "StateName = @StateName," +
                        "StateFlag = @StateFlag," +
                        "NotifyFlag = @NotifyFlag," +
                        "StateHigh = @StateHigh," +
                        "StateLow = @StateLow" +
                        " WHERE CaseNo = @CaseNo AND StateNo = @StateNo";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "狀態更新" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 狀態新增
        /// </summary>
        /// <param name="setting"></param>
        public void Insert_StateSetting(StateSetting setting)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "INSERT IGNORE INTO StateSetting (CaseNo,StateNo,StateName,StateFlag,NotifyFlag,StateHigh,StateLow)" +
                       " VALUES(@CaseNo,@StateNo,@StateName,@StateFlag,@NotifyFlag,@StateHigh,@StateLow) ";
                    Conn.Execute(sql, setting);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "狀態新增" + "錯誤(Ewatch)");
            }
        }
        /// <summary>
        /// 狀態刪除
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <param name="StateNo"></param>
        public void Delete_StateSetting(string CaseNo, int StateNo)
        {
            try
            {
                using (var Conn = new MySqlConnection(scsb.ConnectionString))
                {
                    string sql = "Delete FROM StateSetting  WHERE CaseNo = @CaseNo AND StateNo = @StateNo ";
                    Conn.Execute(sql, new { CaseNo, StateNo });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "狀態刪除" + "錯誤(Ewatch)");
            }
        }
        #endregion
    }
}
