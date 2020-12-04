using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;



namespace LwhUploadOnline
{
    public class GuangXi
    {
        string Xtlb = "";
        string Yhdh = "";
        string Jkxh = "";

        public GuangXi(string xtlb,string yhdh,string jkxh)
        {
            Xtlb = xtlb;
            Yhdh = yhdh;
            Jkxh = jkxh;
            gxDll.init();
        }
        
        public DataTable GetVehicleInf(string hphm, string hpzl, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe2.InnerText = XmlOperation.encodeGBK(hphm);
                XmlElement xe3 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe3.InnerText = hpzl;
                XmlElement xe4 = xmldoc.CreateElement("bm");//创建一个<Node>节点 
                xe4.InnerText = "gbk";
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringGBK(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  Xtlb:" + Xtlb + "|Jkid:18C91|Yhdh:" + Yhdh + "|Jkxh:" + Jkxh + "\r\n" + send_xml + "\r\n");

                StringBuilder receiveXml = new StringBuilder();
                receiveXml.Length = 10240;
                gxDll.queryObjectOut(Xtlb, "18C91", send_xml, Yhdh, Jkxh, receiveXml);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml.ToString() + "\r\n");

                return XmlOperation.ReadXmlToDatatable(receiveXml.ToString(), out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleInf_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        public bool writeProjectStart(gxprojectStart model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jcbh");//创建一个<Node>节点 
                xe101.InnerText = model.jcbh;
                XmlElement xe102 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe102.InnerText = model.jyxm;
                XmlElement xe103 = xmldoc.CreateElement("zxh");//创建一个<Node>节点 
                xe103.InnerText = model.zxh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("cbh");//创建一个<Node>节点 
                xe105.InnerText = model.cbh;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeProjectStart_Send:  Xtlb:" + Xtlb + "|Jkid:18C55|Yhdh:" + Yhdh + "|Jkxh:" + Jkxh + "\r\n" + send_xml + "\r\n");

                StringBuilder receiveXml = new StringBuilder();
                receiveXml.Length = 10240;
                gxDll.writeObjectOut(Xtlb, "18C55", send_xml, Yhdh, Jkxh, receiveXml);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml.ToString() + "\r\n");

                XmlOperation.ReadACKString(receiveXml.ToString(), out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeProjectStart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeProjectFinish(gxprojectFinish model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jcbh");//创建一个<Node>节点 
                xe101.InnerText = model.jcbh;
                XmlElement xe102 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe102.InnerText = model.jyxm;
                XmlElement xe103 = xmldoc.CreateElement("zxh");//创建一个<Node>节点 
                xe103.InnerText = model.zxh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("cbh");//创建一个<Node>节点 
                xe105.InnerText = model.cbh;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeProjectFinish_Send:  Xtlb:" + Xtlb + "|Jkid:18C58|Yhdh:" + Yhdh + "|Jkxh:" + Jkxh + "\r\n" + send_xml + "\r\n");

                StringBuilder receiveXml = new StringBuilder();
                receiveXml.Length = 10240;
                gxDll.writeObjectOut(Xtlb, "18C58", send_xml, Yhdh, Jkxh, receiveXml);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml.ToString() + "\r\n");

                XmlOperation.ReadACKString(receiveXml.ToString(), out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeProjectFinish_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发外廓结果
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeTestDetails(gxtestDetailResult model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jcbh");//创建一个<Node>节点 
                xe101.InnerText = model.jcbh;
                XmlElement xe102 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe102.InnerText = model.jyxm;
                XmlElement xe103 = xmldoc.CreateElement("zxh");//创建一个<Node>节点 
                xe103.InnerText = model.zxh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe105.InnerText = model.cwkc;
                XmlElement xe106 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe106.InnerText = model.cwkk;
                XmlElement xe107 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe107.InnerText = model.cwkg;
                XmlElement xe108 = xmldoc.CreateElement("zj");//创建一个<Node>节点 
                xe108.InnerText = model.zj;
                XmlElement xe109 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe109.InnerText = model.clwkccpd;
                XmlElement xe110 = xmldoc.CreateElement("cbmc");//创建一个<Node>节点 
                xe110.InnerText = XmlOperation.encodeGBK(model.cbmc);
                XmlElement xe111 = xmldoc.CreateElement("ip");//创建一个<Node>节点 
                xe111.InnerText = model.ip;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                xe1.AppendChild(xe109);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeTestDetails_Send:  Xtlb:" + Xtlb + "|Jkid:18C81|Yhdh:" + Yhdh + "|Jkxh:" + Jkxh + "\r\n" + send_xml + "\r\n");

                StringBuilder receiveXml = new StringBuilder();
                receiveXml.Length = 10240;
                gxDll.writeObjectOut(Xtlb, "18C81", send_xml, Yhdh, Jkxh, receiveXml);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml.ToString() + "\r\n");

                XmlOperation.ReadACKString(receiveXml.ToString(), out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeTestDetails_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发整备质量结果
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeZbzlDetails(gxZbzlDetailResult model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jcbh");//创建一个<Node>节点 
                xe101.InnerText = model.jcbh;
                XmlElement xe102 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe102.InnerText = "PB";
                XmlElement xe103 = xmldoc.CreateElement("zxh");//创建一个<Node>节点 
                xe103.InnerText = model.zxh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe105.InnerText = model.zbzl;
                XmlElement xe106 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                xe106.InnerText = model.zbzlpd;
                XmlElement xe107 = xmldoc.CreateElement("cbmc");//创建一个<Node>节点 
                xe107.InnerText = XmlOperation.encodeGBK(model.cbmc);
                XmlElement xe108 = xmldoc.CreateElement("ip");//创建一个<Node>节点 
                xe108.InnerText = model.ip;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeZbzlDetails_Send:  Xtlb:" + Xtlb + "|Jkid:18C81|Yhdh:" + Yhdh + "|Jkxh:" + Jkxh + "\r\n" + send_xml + "\r\n");

                StringBuilder receiveXml = new StringBuilder();
                receiveXml.Length = 10240;
                gxDll.writeObjectOut(Xtlb, "18C81", send_xml, Yhdh, Jkxh, receiveXml);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml.ToString() + "\r\n");

                XmlOperation.ReadACKString(receiveXml.ToString(), out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeZbzlDetails_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }

    public static class gxDll
    {
        [DllImport("ZJDLL.dll", EntryPoint = "init", CallingConvention = CallingConvention.Winapi)]
        public static extern bool init();

        [DllImport("ZJDLL.dll", EntryPoint = "queryObjectOut", CallingConvention = CallingConvention.Winapi)]
        public static extern int queryObjectOut(string xtlb, string jkid, string UTF8XmlDoc, string yhdh, string jkxh, StringBuilder rexml);

        [DllImport("ZJDLL.dll", EntryPoint = "writeObjectOut", CallingConvention = CallingConvention.Winapi)]
        public static extern int writeObjectOut(string xtlb, string jkid, string UTF8XmlDoc, string yhdh, string jkxh, StringBuilder rexml);
    }

    public class gxprojectStart
    {

        public string jcbh;
        public string zxh;
        public string jycs;
        public string cbh;
        public string jyxm;

        public gxprojectStart(string jcbh, string zxh, string jycs, string cbh, string jyxm)
        {
            this.jcbh = jcbh;
            this.zxh = zxh;
            this.jycs = jycs;
            this.cbh = cbh;
            this.jyxm = jyxm;

        }
    }

    public class gxprojectFinish
    {
        public string jcbh;
        public string zxh;
        public string jycs;
        public string cbh;
        public string jyxm;

        public gxprojectFinish(string jcbh, string zxh, string jycs, string cbh, string jyxm)
        {
            this.jcbh = jcbh;
            this.zxh = zxh;
            this.jycs = jycs;
            this.cbh = cbh;
            this.jyxm = jyxm;

        }
    }

    public class gxcapturePicture
    {
        public string jylsh;
        public string jyjgbh;
        public string jycs;
        public string jcxdh;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string jyxm;
        public string zpzl;
        public string pzcfsj;
        public gxcapturePicture(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pzcfsj, string jyxm, string zpzl)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.pzcfsj = pzcfsj;
            this.jyxm = jyxm;
            this.zpzl = zpzl;
        }
    }

    public class gxtestDetailResult
    {
        public string jcbh;
        public string zxh;
        public string jyxm;
        public string jycs;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string zj;
        public string clwkccpd;
        public string cbmc;
        public string ip;
        public gxtestDetailResult(string jcbh, string zxh, string jyxm, string jycs,
            string cwkc, string cwkk, string cwkg, string zj, string clwkccpd,
            string cbmc, string ip)
        {
            this.jcbh = jcbh;
            this.zxh = zxh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.zj = zj;
            this.clwkccpd = clwkccpd;
            this.cbmc = cbmc;
            this.ip = ip;
        }
    }

    public class gxZbzlDetailResult
    {
        public string jcbh;
        public string zxh;
        public string jyxm;
        public string jycs;
        public string zbzl;
        public string cbmc;
        public string zbzlpd;
        public string ip;
        public gxZbzlDetailResult(string jcbh, string zxh, string jyxm, string jycs,
            string zbzl, string zbzlpd, string cbmc, string ip)
        {
            this.jcbh = jcbh;
            this.zxh = zxh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.zbzl = zbzl;
            this.zbzlpd = zbzlpd;
            this.cbmc = cbmc;
            this.ip = ip;
        }
    }
}
