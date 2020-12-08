using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSendWaitCar
{
    
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
            cd = "0";
            kd = "0";
            gd = "0";
            hxkd = "0";
            hxgd = "0";
            lbgd = "0";
            zs = "2";
            zj1 = "0";
            zj2 = "0";
            zj3 = "0";
            zj4 = "0";
            zbzl = "0";
            sczbzl = "0";
            zdzzl = "0";
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
        public string CD { get { return cd; } set { cd = value; } }

        /// <summary>
        /// 宽度
        /// </summary>
        public string KD { get { return kd; } set { kd = value; } }

        /// <summary>
        /// 高度
        /// </summary>
        public string GD { get { return gd; } set { gd = value; } }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        public string HXCD { get { return hxcd; } set { hxcd = value; } }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        public string HXKD { get { return hxkd; } set { hxkd = value; } }

        /// <summary>
        /// 货箱高度
        /// </summary>
        public string HXGD { get { return hxgd; } set { hxgd = value; } }

        /// <summary>
        /// 轴距1
        /// </summary>
        public string LBGD { get { return lbgd; } set { lbgd = value; } }

        /// <summary>
        /// 轴数
        /// </summary>
        public string ZS { get { return zs; } set { zs = value; } }

        /// <summary>
        /// 轴距1
        /// </summary>
        public string ZJ1 { get { return zj1; } set { zj1 = value; } }

        /// <summary>
        /// 轴距2
        /// </summary>
        public string ZJ2 { get { return zj2; } set { zj2 = value; } }

        /// <summary>
        /// 轴距3
        /// </summary>
        public string ZJ3 { get { return zj3; } set { zj3 = value; } }

        /// <summary>
        /// 轴距4
        /// </summary>
        public string ZJ4 { get { return zj4; } set { zj4 = value; } }

        /// <summary>
        /// 整备质量
        /// </summary>
        public string ZBZL { get { return zbzl; } set { zbzl = value; } }

        /// <summary>
        /// 实测整备质量
        /// </summary>
        public string SCZBZL { get { return sczbzl; } set { sczbzl = value; } }

        /// <summary>
        /// 最大总质量
        /// </summary>
        public string ZDZZL { get { return zdzzl; } set { zdzzl = value; } }

        /// <summary>
        /// 牵引车号牌（当前录入整备质量）
        /// </summary>
        public string QYCHP { get { return qychp; } set { qychp = value; } }

        /// <summary>
        /// 是否安装尾板
        /// </summary>
        public string SFAZWB { get { return sfazwb; } set { sfazwb = value; } }

        /// <summary>
        /// 尾板质量
        /// </summary>
        public string WBZL { get { return wbzl; } set { wbzl = value; } }

        /// <summary>
        /// 是否有其他加装部件
        /// </summary>
        public string SFAZQTBJ { get { return sfyqtjzbj; } set { sfyqtjzbj = value; } }

        /// <summary>
        /// 其他加装部件质量
        /// </summary>
        public string QTBJZL { get { return qtjzbjzl; } set { qtjzbjzl = value; } }

        /// <summary>
        /// 其他部件说明
        /// </summary>
        public string QTBJSM { get { return qtbjsm; } set { qtbjsm = value; } }
        #endregion

        private bool is_ready_to_test;        
        private string zglc;
        private string sfjcckg;
        private string sfjclbgd;
        private string sfjchx;
        private string sfjczj;
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
        private string cd;
        private string kd;
        private string gd;
        private string hxcd;
        private string hxkd;
        private string hxgd;
        private string lbgd;
        private string zs;
        private string zj1;
        private string zj2;
        private string zj3;
        private string zj4;
        private string zbzl;
        private string sczbzl;
        private string zdzzl;
        private string qychp;

        private string sfazwb;
        private string wbzl;
        private string sfyqtjzbj;
        private string qtjzbjzl;
        private string qtbjsm;
    }
}
