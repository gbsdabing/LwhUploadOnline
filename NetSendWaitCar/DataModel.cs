using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSendWaitCar
{
    public class DataModel
    {
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
            Jkdz1 = "";
            Jkdz2 = "";
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
        /// 联网接口地址（待检车辆）
        /// </summary>
        public string JkdzWaitCar { get; set; }

        /// <summary>
        /// 接口序列号（待检车辆）
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
        /// 联网上传接口地址1
        /// </summary>
        public string Jkdz1 { get; set; }

        /// <summary>
        /// 联网上传接口地址2
        /// </summary>
        public string Jkdz2 { get; set; }

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
    }

    /// <summary>
    /// 检测程序用待检车辆信息模板
    /// </summary>
    public class WaitCarModel
    {
        public WaitCarModel()
        {
            is_ready_to_test = false;

            zglc = "1";
            sfjcckg = "Y";
            sfjclbgd = "N";
            sfjczj = "Y";
            sfjczbzl = "Y";

            wgjyh = "";
            clph = "";
            jclx = "";
            hpys = "";
            hpzl = "";
            fdjhm = "";
            ppxh = "";
            jccs = "";
            vin = "";
            cllx = "";
            cz = "";
            cd = 0;
            kd = 0;
            gd = 0;
            hxkd = 0;
            hxgd = 0;
            lbgd = 0;
            zj1 = 0;
            zj2 = 0;
            zj3 = 0;
            zj4 = 0;
            zbzl = 0;
            sczbzl = 0;
            zdzzl = 0;
            qychp = "0";
        }

        /// <summary>
        /// 数据是否准备好可以进入检测
        /// </summary>
        public bool IsReadyToTest { get { return is_ready_to_test; } set { is_ready_to_test = value; } }

        #region 检测定义部分
        /// <summary>
        /// 主挂联测（1:主车 2:挂车 3:主挂）
        /// </summary>
        public string ZGLC { get { return zglc; } set { zglc = value; } }

        /// <summary>
        /// 是否检测长宽高（Y:检测；N:不检测）
        /// </summary>
        public string SFJCCKG { get { return sfjcckg; } set { sfjcckg = value; } }

        /// <summary>
        /// 是否检测栏板高度（Y:检测；N:不检测）
        /// </summary>
        public string SFJCLBGD { get { return sfjclbgd; } set { sfjclbgd = value; } }

        /// <summary>
        /// 是否检测栏板高度（Y:检测；N:不检测）
        /// </summary>
        public string SFJCHX { get { return sfjchx; } set { sfjchx = value; } }

        /// <summary>
        /// 是否检测轴距（Y:检测；N:不检测）
        /// </summary>
        public string SFJCZJ { get { return sfjczj; } set { sfjczj = value; } }

        /// <summary>
        /// 是否检测整备质量（Y:检测；N:不检测）
        /// </summary>
        public string SFJCZBZL { get { return sfjczbzl; } set { sfjczbzl = value; } }
        #endregion

        #region 车辆信息
        /// <summary>
        /// 外观检验号
        /// </summary>
        public string WGJYH { get { return wgjyh; } set { wgjyh = value; } }

        /// <summary>
        /// 车辆牌号
        /// </summary>
        public string CLPH { get { return clph; } set { clph = value; } }

        /// <summary>
        /// 检测类型（只传代码）
        /// </summary>
        public string JCLX { get { return jclx; } set { jclx = value; } }

        /// <summary>
        /// 号牌颜色（只传代码）
        /// </summary>
        public string HPYS { get { return hpys; } set { hpys = value; } }

        /// <summary>
        /// 号牌种类
        /// </summary>
        public string HPZL { get { return hpzl; } set { hpzl = value; } }

        /// <summary>
        /// 发动机号码
        /// </summary>
        public string FDJHM { get { return fdjhm; } set { fdjhm = value; } }

        /// <summary>
        /// 品牌型号
        /// </summary>
        public string PPXH { get { return ppxh; } set { ppxh = value; } }

        /// <summary>
        /// 检测次数
        /// </summary>
        public string JCCS { get { return jccs; } set { jccs = value; } }

        /// <summary>
        /// VIN
        /// </summary>
        public string VIN { get { return vin; } set { vin = value; } }

        /// <summary>
        /// 车辆类型（只传代码）
        /// </summary>
        public string CLLX { get { return cllx; } set { cllx = value; } }

        /// <summary>
        /// 车主
        /// </summary>
        public string CZ { get { return cz; } set { cz = value; } }

        /// <summary>
        /// 长度
        /// </summary>
        public int CD { get { return cd; } set { cd = value; } }

        /// <summary>
        /// 宽度
        /// </summary>
        public int KD { get { return kd; } set { kd = value; } }

        /// <summary>
        /// 高度
        /// </summary>
        public int GD { get { return gd; } set { gd = value; } }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        public int HXCD { get { return hxcd; } set { hxcd = value; } }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        public int HXKD { get { return hxkd; } set { hxkd = value; } }

        /// <summary>
        /// 货箱高度
        /// </summary>
        public int HXGD { get { return hxgd; } set { hxgd = value; } }

        /// <summary>
        /// 轴距1
        /// </summary>
        public int LBGD { get { return lbgd; } set { lbgd = value; } }

        /// <summary>
        /// 轴距1
        /// </summary>
        public int ZJ1 { get { return zj1; } set { zj1 = value; } }

        /// <summary>
        /// 轴距2
        /// </summary>
        public int ZJ2 { get { return zj2; } set { zj2 = value; } }

        /// <summary>
        /// 轴距3
        /// </summary>
        public int ZJ3 { get { return zj3; } set { zj3 = value; } }

        /// <summary>
        /// 轴距4
        /// </summary>
        public int ZJ4 { get { return zj4; } set { zj4 = value; } }

        /// <summary>
        /// 整备质量
        /// </summary>
        public int ZBZL { get { return zbzl; } set { zbzl = value; } }

        /// <summary>
        /// 实测整备质量
        /// </summary>
        public int SCZBZL { get { return sczbzl; } set { sczbzl = value; } }

        /// <summary>
        /// 最大总质量
        /// </summary>
        public int ZDZZL { get { return zdzzl; } set { zdzzl = value; } }

        /// <summary>
        /// 牵引车号牌（当前录入整备质量）
        /// </summary>
        public string QYCHP { get { return qychp; } set { qychp = value; } }
        #endregion

        private bool is_ready_to_test;

        /// <summary>
        /// 主挂联测（1:主车 2:挂车 3:主挂）
        /// </summary>
        private string zglc;

        /// <summary>
        /// 是否检测长宽高（Y:检测；N:不检测）
        /// </summary>
        private string sfjcckg;

        /// <summary>
        /// 是否检测栏板高度（Y:检测；N:不检测）
        /// </summary>
        private string sfjclbgd;

        /// <summary>
        /// 是否检测货箱（Y:检测；N:不检测）
        /// </summary>
        private string sfjchx;

        /// <summary>
        /// 是否检测轴距（Y:检测；N:不检测）
        /// </summary>
        private string sfjczj;

        /// <summary>
        /// 是否检测整备质量（Y:检测；N:不检测）
        /// </summary>
        private string sfjczbzl;

        private string wgjyh;
        private string clph;
        private string jclx;
        private string hpys;
        private string hpzl;
        private string fdjhm;
        private string ppxh;
        private string jccs;
        private string vin;
        private string cllx;
        private string cz;
        private int cd;
        private int kd;
        private int gd;
        private int hxcd;
        private int hxkd;
        private int hxgd;
        private int lbgd;
        private int zj1;
        private int zj2;
        private int zj3;
        private int zj4;
        private int zbzl;
        private int sczbzl;
        private int zdzzl;
        private string qychp;
    }
}
