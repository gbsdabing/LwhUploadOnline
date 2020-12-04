using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    /// <summary>
    /// 海城新的联网
    /// </summary>
    public class HaiCheng
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        private string Cjsqbh = "";
        private string Dwjgdm = "";
        private string Dwmc = "";
        private string Yhbz = "";
        private string Yhxm = "";
        private string Zdbs = "";
        private HCTmriOutNewAccess outlineservice = null;

        public HaiCheng(string url, string xtlb, string jkxlh, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs)
        {
            outlineservice = new HCTmriOutNewAccess(url);
            Xtlb = xtlb;
            Jkxlh = jkxlh;
            Cjsqbh = cjsqbh;
            Dwjgdm = dwjgdm;
            Dwmc = dwmc;
            Yhbz = yhbz;
            Yhxm = yhxm;
            Zdbs = zdbs;
        }

        //public bool GetSysTime()
        //{

        //}
    }

    /// <summary>
    /// 海城老的联网
    /// </summary>
    public class HaiChengOld
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        hcAccessService outlineservice = null;
        public HaiChengOld(string url, string xtlb, string jkxlh)
        {
            outlineservice = new hcAccessService(url);
            Xtlb = xtlb;
            Jkxlh = jkxlh;
        }
    }

    #region 联网数据
    public class CHK02_new
    {
        public string jclsh;
        public string clpp1;
        public string cllx;
        public string clsbdh;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string zj;
        public string zbzl;
        public string bz;
        public string clfs;
        public string pdfs;
        public string jdcxh;
        public string syr;
        public string cgslh;
        public string clr;
        public string clsj;
        public string hpzl;
        public string hphm;
        public string jczbh;
        public string ywzl;
        public string czy;
        public string sfzmhm;
        public CHK02_new(string jclsh, string clpp1, string cllx, string clsbdh, string cwkc, string cwkk, string cwkg, string zj, string zbzl, string bz, string clfs, string pdfs, string jdcxh, string syr, string cgslh, string clr, string clsj, string hpzl, string hphm, string jczbh, string ywzl, string czy, string sfzmhm)
        {
            this.jclsh = jclsh;
            this.clpp1 = clpp1;
            this.cllx = cllx;
            this.clsbdh = clsbdh;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.zj = zj;
            this.zbzl = zbzl;
            this.bz = bz;
            this.clfs = clfs;
            this.pdfs = pdfs;
            this.jdcxh = jdcxh;
            this.syr = syr;
            this.cgslh = cgslh;
            this.clr = clr;
            this.clsj = clsj;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.jczbh = jczbh;
            this.ywzl = ywzl;
            this.czy = czy;
            this.sfzmhm = sfzmhm;
        }
    }
    public class CHK03_new
    {
        public string jclsh;
        public string zplx;
        public string jdcxh;
        public string czy;
        public string picbase64;
        public CHK03_new(string jclsh, string zplx, string jdcxh, string czy, string picbase64)
        {
            this.jclsh = jclsh;
            this.zplx = zplx;
            this.jdcxh = jdcxh;
            this.czy = czy;
            this.picbase64 = picbase64;
        }
    }
    public class CHK04_new
    {
        public string jclsh;
        public string cglsh;
        public string jdcxh;
        public string clsbdh;
        public string hpzl;
        public string hphm;
        public string zbzl;
        public string jczbh;
        public string sbbh;
        public string clsj;
        public CHK04_new(string jclsh, string cglsh, string jdcxh, string clsbdh, string hpzl, string hphm, string zbzl, string jczbh, string sbbh, string clsj)
        {
            this.jclsh = jclsh;
            this.cglsh = cglsh;
            this.jdcxh = jdcxh;
            this.clsbdh = clsbdh;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.zbzl = zbzl;
            this.jczbh = jczbh;
            this.sbbh = sbbh;
            this.clsj = clsj;
        }
    }
    public class HCCHK10_New
    {

        public string jclsh;
        public string jczbh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string kssj;
        public HCCHK10_New(string jclsh, string jczbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string kssj)
        {
            this.jclsh = jclsh;
            this.jczbh = jczbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (this.hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.kssj = kssj;
        }
    }
    public class HCCHK11_New
    {

        public string jclsh;
        public string jczbh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string jssj;
        public HCCHK11_New(string jclsh, string jczbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string jssj)
        {
            this.jclsh = jclsh;
            this.jczbh = jczbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (this.hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.jssj = jssj;
        }
    }
    #endregion
}
