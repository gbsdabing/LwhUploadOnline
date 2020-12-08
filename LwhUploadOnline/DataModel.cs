using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LwhUploadOnline
{
     
    /// <summary>
    /// 结果实体类
    /// </summary>
    public class TestRecordModel
    {        
        /*
         * Json转实体类调用举例
           using Newtonsoft.Json;
           using Newtonsoft.Json.Linq;
           string JsonStr = "";
           TestRecordModel test = JsonConvert.DeserializeObject<TestRecordModel>(JsonStr);
         */
        public TestRecordModel()
        {
            RecordId = "";
            LSH = "";
            JCCS = "";
            CLHP = "";
            CLLX = "";
            HPZL = "";
            CPYS = "";
            CZ = "";
            CCRQ = DateTime.Now;
            ZCRQ = DateTime.Now;
            JCSJ = DateTime.Now;
            JCKSSJ = DateTime.Now;
            JCJSSJ = DateTime.Now;
            VIN = "";
            CLPP = "";
            ZZL = "";
            FDJH = "";
            CLZS = "";
            JCLX = "0";
            LENGTHBZZ = 0;
            WIDTHBZZ = 0;
            HEIGHTBZZ = 0;
            MAXLENGTH = 0;
            MAXWIDTH = 0;
            MAXHEIGHT = 0;
            LBHEIGHTBZZ = 0;
            ZJ1BZZ = 0;
            ZJ2BZZ = 0;
            ZJ3BZZ = 0;
            ZJ4BZZ = 0;
            ZJ5BZZ = 0;
            ZZJBZZ = 0;
            HXLENGTHBZZ = 0;
            HXWIDTHBZZ = 0;
            HXHEIGHTBZZ = 0;
            ZBZLBZZ = 0;
            LENGTHCLZ = 0;
            WIDTHCLZ = 0;
            HEIGHTCLZ = 0;
            ZJ1CLZ = 0;
            ZJ2CLZ = 0;
            ZJ3CLZ = 0;
            ZJ4CLZ = 0;
            ZJ5CLZ = 0;
            ZZJCLZ = 0;
            LBHEIGHTCLZ = 0;
            HXLENGTHCLZ = 0;
            HXWIDTHCLZ = 0;
            HXHEIGHTCLZ = 0;
            ZBZLCLZ = 0;
            LENGTHXZ = "";
            WIDTHXZ = "";
            HEIGHTXZ = "";
            LBHEIGHTXZ = "";
            ZJXZ = "";
            ZBZLXZ = "";
            HXXZ = "";
            LENGTHWC = "";
            WIDTHWC = "";
            HEIGHTWC = "";
            LBHEIGHTWC = "";
            ZJ1WC = "";
            ZJ2WC = "";
            ZJ3WC = "";
            ZJ4WC = "";
            ZJ5WC = "";
            ZZJWC = "";
            HXLENGTHWC = "";
            HXWIDTHWC = "";
            HXHEIGHTWC = "";
            LENGTHPD = "";
            WIDTHPD = "";
            HEIGHTPD = "";
            LBHEIGHTPD = "";
            ZJ1PD = "";
            ZJ2PD = "";
            ZJ3PD = "";
            ZJ4PD = "";
            ZJ5PD = "";
            ZZJPD = "";
            ZBZLPD = "";
            ZBZLABSPD = "";
            ZBZLRELPD = "";
            ZBZLWC = "";
            ZBZLCLFS = 0;
            QYCDJZBZL = 0;
            KCZL = 0;
            QYCZBZL = 0;
            SCZBZL = 0;
            JSYWEIGHT = 0;
            GB1589ISUSE = true;
            ZBZLA1 = "";
            ZBZLA2 = "";
            ZBZLA3 = "";
            ZBZLA4 = "";
            ZBZLA5 = "";
            ZBZLA6 = "";
            ZBZLARRAYCOUNT = 0;
            CLZS = "";
            JSY = "";
            ZHPD = "";
            FILEPATH = "";
            LWHBJ = false;
            ZBZLBJ = false;
            ZJBJ = false;
            HXBJ = false;
            LBHEIGHTBJ = false;
            BY1 = "";
            BY2 = "";
            BY3 = "";
            BY4 = "";
            BY5 = "";
            lengthMin = 0;
            lengthMax = 0;
            widthMin = 0;
            widthMax = 0;
            heightMin = 0;
            heightMax = 0;
            WBBJ = false;
            WBZL = 0;
            QTBJBJ = false;
            QTBJZL = 0;
            QTBJSM = "";
            CLZL = 0;
        }

        public string RecordId { set; get; }
        public string LSH { set; get; }
        public string JCCS { set; get; }
        public string CLHP { set; get; }
        public string CLLX { set; get; }
        public string HPZL { set; get; }
        public string CPYS { set; get; }
        public string CZ { set; get; }
        public DateTime CCRQ { set; get; }
        public DateTime ZCRQ { set; get; }
        /// <summary>
        /// 检测开始时间
        /// </summary>
        public DateTime JCSJ { set; get; }
        /// <summary>
        /// 联网发送外廓结束时间
        /// </summary>
        public DateTime JCKSSJ { set; get; }
        /// <summary>
        /// 联网发送整备质量结束时间
        /// </summary>
        public DateTime JCJSSJ { set; get; }
        public string VIN { set; get; }
        public string CLPP { set; get; }
        public string ZZL { set; get; }
        public string FDJH { set; get; }
        public string CLZS { set; get; }
        public string JCLX { set; get; }
        public int LENGTHBZZ { set; get; }
        public int WIDTHBZZ { set; get; }
        public int HEIGHTBZZ { set; get; }
        public int MAXLENGTH { set; get; }
        public int MAXWIDTH { set; get; }
        public int MAXHEIGHT { set; get; }
        public int LBHEIGHTBZZ { set; get; }
        public int ZJ1BZZ { set; get; }
        public int ZJ2BZZ { set; get; }
        public int ZJ3BZZ { set; get; }
        public int ZJ4BZZ { set; get; }
        public int ZJ5BZZ { set; get; }
        public int ZZJBZZ { set; get; }
        public int HXLENGTHBZZ { set; get; }
        public int HXWIDTHBZZ { set; get; }
        public int HXHEIGHTBZZ { set; get; }
        public int ZBZLBZZ { set; get; }
        public int LENGTHCLZ { set; get; }
        public int WIDTHCLZ { set; get; }
        public int HEIGHTCLZ { set; get; }
        public int ZJ1CLZ { set; get; }
        public int ZJ2CLZ { set; get; }
        public int ZJ3CLZ { set; get; }
        public int ZJ4CLZ { set; get; }
        public int ZJ5CLZ { set; get; }
        public int ZZJCLZ { set; get; }
        public int LBHEIGHTCLZ { set; get; }
        public int HXLENGTHCLZ { set; get; }
        public int HXWIDTHCLZ { set; get; }
        public int HXHEIGHTCLZ { set; get; }
        public int ZBZLCLZ { set; get; }
        public string LENGTHXZ { set; get; }
        public string WIDTHXZ { set; get; }
        public string HEIGHTXZ { set; get; }
        public string LBHEIGHTXZ { set; get; }
        public string ZJXZ { set; get; }
        public string ZBZLXZ { set; get; }
        public string HXXZ { set; get; }
        public string LENGTHWC { set; get; }
        public string WIDTHWC { set; get; }
        public string HEIGHTWC { set; get; }
        public string LBHEIGHTWC { set; get; }
        public string ZJ1WC { set; get; }
        public string ZJ2WC { set; get; }
        public string ZJ3WC { set; get; }
        public string ZJ4WC { set; get; }
        public string ZJ5WC { set; get; }
        public string ZZJWC { set; get; }
        public string HXLENGTHWC { set; get; }
        public string HXWIDTHWC { set; get; }
        public string HXHEIGHTWC { set; get; }
        public string LENGTHPD { set; get; }
        public string WIDTHPD { set; get; }
        public string HEIGHTPD { set; get; }
        public string LBHEIGHTPD { set; get; }
        public string ZJ1PD { set; get; }
        public string ZJ2PD { set; get; }
        public string ZJ3PD { set; get; }
        public string ZJ4PD { set; get; }
        public string ZJ5PD { set; get; }
        public string ZZJPD { set; get; }
        public string ZBZLPD { set; get; }
        public string ZBZLRELPD { set; get; }
        public string ZBZLABSPD { set; get; }
        /// <summary>
        /// 起重尾板标记，Y:有 N:无
        /// </summary>
        public bool WBBJ { set; get; }
        /// <summary>
        /// 车用起重尾板质量
        /// </summary>
        public int WBZL { set; get; }
        public int WBCD { set; get; }
        /// <summary>
        /// 其他部件标记，Y:有 N:无
        /// </summary>
        public bool QTBJBJ { set; get; }
        /// <summary>
        /// 其他部件质量
        /// </summary>
        public int QTBJZL { set; get; }
        /// <summary>
        /// 其他部件说明
        /// </summary>
        public string QTBJSM { set; get; }
        /// <summary>
        /// 称重测量质量，对厢式货车、厢式挂车安装车用起重尾板的，该项含车用起重尾板质量
        /// </summary>
        public int CLZL { set; get; }
        public string ZBZLWC { set; get; }
        /// <summary>
        /// 事备质量测量方式 0-地磅 1-轮重仪
        /// </summary>
        public int ZBZLCLFS { set; get; }
        /// <summary>
        /// 牵引车实测整备质量，由上层系统传入，计算挂车整备质量时用
        /// </summary>
        public int QYCZBZL { set; get; }
        /// <summary>
        /// 牵引车登记整备质量，由上层系统传入，计算挂车空车质量时用
        /// </summary>
        public int QYCDJZBZL { set; get; }
        /// <summary>
        /// 空车质量
        /// </summary>
        public int KCZL { set; get; }
        /// <summary>
        /// 实测整备质量，即测量完后总的重量，未减速牵引车或引车员重量
        /// </summary>
        public int SCZBZL { set; get; }
        /// <summary>
        /// 驾驶员重量
        /// </summary>
        public int JSYWEIGHT { set; get; }
        public bool GB1589ISUSE { set; get; }
        public string ZBZLA1 { set; get; }
        public string ZBZLA2 { set; get; }
        public string ZBZLA3 { set; get; }
        public string ZBZLA4 { set; get; }
        public string ZBZLA5 { set; get; }
        public string ZBZLA6 { set; get; }
        public int ZBZLARRAYCOUNT { set; get; }
        public string JSY { set; get; }
        public string ZHPD { set; get; }
        public string FILEPATH { set; get; }
        public bool LWHBJ { set; get; }
        public bool ZBZLBJ { set; get; }
        public bool ZJBJ { set; get; }
        public bool HXBJ { set; get; }
        public bool LBHEIGHTBJ { set; get; }
        /// <summary>
        /// 长宽高范围
        /// </summary>
        public string BY1 { set; get; }
        /// <summary>
        /// 整备质量详细判定结果 绝对误差判定结果|相对误差判定结果 ，如果对应的为空表示未判定
        /// </summary>
        public string BY2 { set; get; }
        /// <summary>
        /// WBBJ|WBZL|WBCD|QTBJBJ|QTBJZL|QTBJSM|CLZL,如：N|0|0|N|0||0
        /// </summary>
        public string BY3 { set; get; }
        public string BY4 { set; get; }
        public string BY5 { set; get; }
        public bool UseXz { set; get; }
        public int lengthWcXz { set; get; }
        public int heightWcXz { set; get; }
        public int widthWcXz { set; get; }
        public int zbzlWcXz { set; get; }
        public int lbWcXz { set; get; }
        public int lengthMin { set; get; }
        public int lengthMax { set; get; }

        public int widthMin { set; get; }
        public int widthMax { set; get; }
        public int heightMin { set; get; }
        public int heightMax { set; get; }
    }

    public class recordModelControl
    {
        public static int getIntFromDb(DataRow dr, string columnName)
        {
            int temp = 0;
            if (dr[columnName].ToString() == "")
                return 0;
            else if (int.TryParse(dr[columnName].ToString(), out temp))
                return temp;
            else
                return 0;
        }
        public static bool getBoolFromDb(DataRow dr, string columnName)
        {
            if (dr[columnName].ToString() == "")
                return false;
            else if (dr[columnName].ToString() == "Y")
                return true;
            else
                return false;
        }
        public static string IntToString(int value, string s = "")
        {
            if (value == 0)
                return s;
            else
                return value.ToString();
        }
        public static int StringToInt(string value, int s = 0, string name = "")
        {
            try
            {
                if (value == "")
                    return s;
                else
                    return int.Parse(value);
            }
            catch
            {
                throw (new Exception(name + "输入格式不正确"));
            }
        }
        public static string StringToString(string value, string s = "")
        {
            if (value == "")
                return s;
            else
                return value;
        }
        public static string BoolToString(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }
    }
}
