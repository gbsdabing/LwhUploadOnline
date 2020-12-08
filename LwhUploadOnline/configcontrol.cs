using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LwhUploadOnline
{
    public class configControl
    {
        #region INI读取引用
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="strString">配置值</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileString(string strAppName, string strKeyName, string strString, string strFileName);
        /// <summary>
        /// 读取配置文件值
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="strDefault">返回的默认值</param>
        /// <param name="sbReturnString">返回StringBuilder Cache对象</param>
        /// <param name="nSize">缓冲区大小</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool GetPrivateProfileString(string strAppName, string strKeyName, string strDefault, StringBuilder sbReturnString, int nSize, string strFileName);
        /// <summary>
        /// 读取配置文件中的指定配置节点，并返回整型值
        /// </summary>
        /// <param name="strAppName">配置节点名称</param>
        /// <param name="strKeyName">配置名</param>
        /// <param name="nDefault">返回的默认值</param>
        /// <param name="strFileName">配置文件名</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileInt(string strAppName, string strKeyName, int nDefault, string strFileName);
        #endregion

        private static string global_path = @"D:\外廓数据文件";

        public static UploadConfigModel getUploadConfig()
        {
            try
            {
                UploadConfigModel config = new UploadConfigModel();

                string config_path = global_path + "\\uploadConfig.ini";
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                int i = 0;

                GetPrivateProfileString("联网配置", "待检车辆来源", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.WaitCarModel = (NetWaitCarModel)i;
                else
                    config.WaitCarModel = NetWaitCarModel.联网查询;

                GetPrivateProfileString("联网配置", "待检车辆接口地址", "", temp, 2048, config_path);
                config.JkdzWaitCar = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "待检车辆接口序列号", "", temp, 2048, config_path);
                config.JkxlhWaitCar = temp.ToString().Trim();


                GetPrivateProfileString("联网配置", "联网模式", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.NetModel = (NetUploadModel)i;
                else
                    config.NetModel = NetUploadModel.安车;

                GetPrivateProfileString("联网配置", "联网地区", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.NetArea = (NetAreaModel)i;
                else
                    config.NetArea = NetAreaModel.四川;

                GetPrivateProfileString("联网配置", "接口地址", "", temp, 2048, config_path);
                config.Jkdz = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "接口序列号", "", temp, 2048, config_path);
                config.Jkxlh = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "本机IP地址", "", temp, 2048, config_path);
                config.LocalIP = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "系统类别", "18", temp, 2048, config_path);
                config.Xtlb = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "检测站编号", "", temp, 2048, config_path);
                config.StationID = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "检测线编号", "", temp, 2048, config_path);
                config.LineID = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "外廓工位号", "1", temp, 2048, config_path);
                config.WkDeviceID = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "整备质量工位号", "1", temp, 2048, config_path);
                config.ZbzlDeviceID = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "外廓前照编号", "360", temp, 2048, config_path);
                config.WkFrontPicBh = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "外廓后照编号", "361", temp, 2048, config_path);
                config.WkBackPicBh = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "整备质量前照编号", "362", temp, 2048, config_path);
                config.ZbzlFrontPicBh = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "整备质量后照编号", "363", temp, 2048, config_path);
                config.ZbzlBackPicBh = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "照片上传次数", "1", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i) && i > 0)
                    config.PicSendTimes = i;
                else
                    config.PicSendTimes = 1;

                GetPrivateProfileString("联网配置", "场景编号", "", temp, 2048, config_path);
                config.CJBH = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "单位名称", "", temp, 2048, config_path);
                config.DWMC = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "单位机构代码", "", temp, 2048, config_path);
                config.DWJGDM = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "用户标识", "", temp, 2048, config_path);
                config.YHBS = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "用户姓名", "", temp, 2048, config_path);
                config.YHXM = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "终端标识", "", temp, 2048, config_path);
                config.ZDBS = temp.ToString().Trim();

                GetPrivateProfileString("联网配置", "外廓代号", "M1", temp, 2048, config_path);
                config.WKDH = temp.ToString().Trim();
                GetPrivateProfileString("联网配置", "整备质量代号", "Z1", temp, 2048, config_path);
                config.ZBZLDH = temp.ToString().Trim();
                GetPrivateProfileString("联网配置", "大雷是否发送18JXX", "N", temp, 2048, config_path);
                config.dl_Send18Jxx = temp.ToString().Trim()=="Y";
                GetPrivateProfileString("联网配置", "大雷是否发送18H05", "N", temp, 2048, config_path);
                config.dl_Send18H05 = temp.ToString().Trim()=="Y";
                return config;
            }
            catch (Exception er)
            {
                WriteLogs("获取联网配置出错：" + er.Message);
                return null;
            }
        }

        public static bool WriteBaseConfig(UploadConfigModel config)
        {
            try
            {
                string config_path = global_path + "\\uploadConfig.ini";

                WritePrivateProfileString("联网配置", "检测站编号", config.StationID, config_path);
                WritePrivateProfileString("联网配置", "检测线编号", config.LineID, config_path);
                WritePrivateProfileString("联网配置", "外廓工位号", config.WkDeviceID, config_path);
                WritePrivateProfileString("联网配置", "整备质量工位号", config.ZbzlDeviceID, config_path);
                WritePrivateProfileString("联网配置", "外廓前照编号", config.WkFrontPicBh, config_path);
                WritePrivateProfileString("联网配置", "外廓后照编号", config.WkBackPicBh, config_path);
                WritePrivateProfileString("联网配置", "整备质量前照编号", config.ZbzlFrontPicBh, config_path);
                WritePrivateProfileString("联网配置", "整备质量后照编号", config.ZbzlBackPicBh, config_path);

                WritePrivateProfileString("联网配置", "系统类别", config.Xtlb, config_path);

                WritePrivateProfileString("联网配置", "场景编号", config.CJBH, config_path);
                WritePrivateProfileString("联网配置", "单位名称", config.DWMC, config_path);
                WritePrivateProfileString("联网配置", "单位机构代码", config.DWJGDM, config_path);
                WritePrivateProfileString("联网配置", "用户标识", config.YHBS, config_path);
                WritePrivateProfileString("联网配置", "用户姓名", config.YHXM, config_path);
                WritePrivateProfileString("联网配置", "终端标识", config.ZDBS, config_path);

                WritePrivateProfileString("联网配置", "本机IP地址", config.LocalIP, config_path);
                WritePrivateProfileString("联网配置", "外廓代号", config.WKDH, config_path);
                WritePrivateProfileString("联网配置", "整备质量代号", config.ZBZLDH, config_path);

                return true;
            }
            catch (Exception er)
            {
                WriteLogs("保存联网上传基本配置失败：" + er.Message);
                return false;
            }
        }

        public static bool WriteNetConfig(UploadConfigModel config)
        {
            try
            {
                string config_path = global_path + "\\uploadConfig.ini";
                WritePrivateProfileString("联网配置", "待检车辆来源", ((int)config.WaitCarModel).ToString(), config_path);
                WritePrivateProfileString("联网配置", "待检车辆接口地址", config.JkdzWaitCar, config_path);
                WritePrivateProfileString("联网配置", "待检车辆接口序列号", config.JkxlhWaitCar, config_path);

                WritePrivateProfileString("联网配置", "联网模式", ((int)config.NetModel).ToString(), config_path);
                WritePrivateProfileString("联网配置", "接口序列号", config.Jkxlh, config_path);
                WritePrivateProfileString("联网配置", "接口地址", config.Jkdz, config_path);

                WritePrivateProfileString("联网配置", "大雷是否发送18JXX", config.dl_Send18Jxx?"Y":"N", config_path);
                WritePrivateProfileString("联网配置", "大雷是否发送18H05", config.dl_Send18H05 ? "Y" : "N", config_path);

                WritePrivateProfileString("联网配置", "联网地区", ((int)config.NetArea).ToString(), config_path);

                WritePrivateProfileString("联网配置", "照片上传次数", config.PicSendTimes.ToString(), config_path);

                return true;
            }
            catch (Exception er)
            {
                WriteLogs("保存联网上传联网配置失败：" + er.Message);
                return false;
            }
        }

        /// <summary>
        /// 写上传dll运行日志文件
        /// </summary>
        /// <param name="logs">日志内容</param>
        /// <param name="logs_name">日志文件名</param>
        /// <param name="file_path">日志文件路径</param>
        /// <returns></returns>
        public static bool WriteLogs(string logs)
        {
            try
            {
                string filepath = global_path + "\\SendCarLogs\\RunLogs\\" + DateTime.Now.ToString("yyMMdd");
                string pathname = filepath + "\\" + DateTime.Now.ToString("HH") + "report.log";

                if (Directory.Exists(filepath) == false)
                    Directory.CreateDirectory(filepath);

                using (FileStream fs = new FileStream(pathname, FileMode.Append))
                {
                    using (StreamWriter log = new StreamWriter(fs, Encoding.Default))
                    {
                        log.WriteLine(System.DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n" + logs + " \r\n ");
                        log.Close();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private static bool createDir(string strPath)
        {
            try
            {
                if (Directory.Exists(strPath) == false)
                    Directory.CreateDirectory(strPath);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 联网待检车辆来源
    /// </summary>
    public enum NetWaitCarModel
    {
        联网查询,
        大雷联网列表,
        华燕联网列表
    }

    /// <summary>
    /// 联网上传方式
    /// </summary>
    public enum NetUploadModel
    {
        安车,
        安徽,
        宝辉,
        大雷,
        广西,
        海城新疆,
        海城四川,
        华燕,
        湖北,
        康士柏,
        欧润特,
        上饶,
        南京新仕尚,
        万国,
        维科,
        新盾,
        新力源,
        益中祥,
        中航
    }

    /// <summary>
    /// 联网地区
    /// </summary>
    public enum NetAreaModel
    {
        四川,
        安徽,
        福建,
        贵州,
        湖南,
        江西,
        新疆,
        云南,
        上海,
        重庆,
        成都,
        自贡,
        九江
    }

    /// <summary>
    /// 联网配置信息
    /// </summary>
    public class UploadConfigModel
    {
        public UploadConfigModel()
        {
            WaitCarModel = NetWaitCarModel.联网查询;
            JkdzWaitCar = "";
            JkxlhWaitCar = "";

            NetModel = NetUploadModel.安车;
            NetArea = NetAreaModel.四川;
            Jkdz = "";
            LocalIP = "";
            Xtlb = "18";
            Jkxlh = "";
            StationID = "";
            LineID = "";
            WkDeviceID = "1";
            ZbzlDeviceID = "1";
            WkFrontPicBh = "0360";
            WkBackPicBh = "0361";
            ZbzlFrontPicBh = "0362";
            ZbzlBackPicBh = "0363";
            PicSendTimes = 1;
            CJBH = "";
            DWMC = "";
            DWJGDM = "";
            YHBS = "";
            YHXM = "";
            ZDBS = "";
        }

        /// <summary>
        /// 待检车辆来源
        /// </summary>
        public NetWaitCarModel WaitCarModel { get; set; }

        /// <summary>
        /// 联网上传接口地址1
        /// </summary>
        public string JkdzWaitCar { get; set; }

        /// <summary>
        /// 接口序列号（联网上传）
        /// </summary>
        public string JkxlhWaitCar { get; set; }


        /// <summary>
        /// 联网上传模式
        /// </summary>
        public NetUploadModel NetModel { get; set; }

        /// <summary>
        /// 联网地区
        /// </summary>
        public NetAreaModel NetArea { get; set; }

        /// <summary>
        /// 联网上传接口地址
        /// </summary>
        public string Jkdz { get; set; }

        /// <summary>
        /// 本机IP地址
        /// </summary>
        public string LocalIP { get; set; }

        /// <summary>
        /// 系统类别
        /// </summary>
        public string Xtlb { get; set; }

        /// <summary>
        /// 接口序列号（联网上传）
        /// </summary>
        public string Jkxlh { get; set; }

        /// <summary>
        /// 检测站编号
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 检测线号
        /// </summary>
        public string LineID { get; set; }

        /// <summary>
        /// 外廓工位号
        /// </summary>
        public string WkDeviceID { get; set; }

        /// <summary>
        /// 整备质量工位号
        /// </summary>
        public string ZbzlDeviceID { get; set; }

        /// <summary>
        /// 外廓前照代码
        /// </summary>
        public string WkFrontPicBh { get; set; }

        /// <summary>
        /// 外廓后照代码
        /// </summary>
        public string WkBackPicBh { get; set; }

        /// <summary>
        /// 整备质量前照代码
        /// </summary>
        public string ZbzlFrontPicBh { get; set; }

        /// <summary>
        /// 整备质量后照代码
        /// </summary>
        public string ZbzlBackPicBh { get; set; }

        /// <summary>
        /// 照片发送次数
        /// </summary>
        public int PicSendTimes { get; set; }

        /// <summary>
        /// 场景编号
        /// </summary>
        public string CJBH { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string DWMC { get; set; }

        /// <summary>
        /// 单位机构代码
        /// </summary>
        public string DWJGDM { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string YHBS { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string YHXM { get; set; }

        /// <summary>
        /// 终端标识
        /// </summary>
        public string ZDBS { get; set; }
        public string WKDH { set; get; }
        public string ZBZLDH { set; get; }
        public bool dl_Send18Jxx { set; get; }
        public bool dl_Send18H05 { set; get; }
    }
}
