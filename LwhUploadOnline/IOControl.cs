using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace LwhUploadOnline
{
    public class IOControl
    {
        private static string start_path = @"D:\外廓数据文件\UploadLogs";
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
                string filepath = start_path + "\\RunLogs\\" + DateTime.Now.ToString("yyMMdd");
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
        /// 保存上传日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static bool saveXmlLogInf(string logs)
        {
            string filepath = start_path + "\\UploadLogs\\" + DateTime.Now.ToString("yyMMdd");
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
    }
}
