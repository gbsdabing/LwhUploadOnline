using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;



namespace LwhUploadOnline
{
    public class ShangRao
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        private SRTmriOutAccess outlineservice = null;

        public ShangRao(string url, string xtlb, string jkxlh)
        {
            outlineservice = new SRTmriOutAccess(url);
            Xtlb = xtlb;
            Jkxlh = jkxlh;
        }

        /// <summary>
        /// 获取平台时间
        /// </summary>
        /// <param name="babh"></param>
        /// <returns></returns>
        public DataTable GetSystemDatetime(string babh)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("babh");//创建一个<Node>节点 
                xe2.InnerText = babh;
                xe1.AppendChild(xe2);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetSystemDatetime_Send:  18C50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18C50", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetSystemDatetime_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 获取待检列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetVehicleList()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点           
                XmlElement xe3 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                XmlElement xe4 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                XmlElement xe5 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点   
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                xe1.AppendChild(xe5);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleList_Send:  18R47\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18R47", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, msg;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out msg);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleList_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18W55(sr18W55 model, out string code, out string message)
        {
            code = "0";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = model.jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe105.InnerText = model.hpzl;
                }
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe108.InnerText = model.gwjysbbh;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = model.jyxm;
                XmlElement xe110 = xmldoc.CreateElement("kssj");//创建一个<Node>节点 
                xe110.InnerText = model.kssj;
                XmlElement xe111 = xmldoc.CreateElement("lxxm");//创建一个<Node>节点 
                xe111.InnerText = model.lxxm;
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
                IOControl.saveXmlLogInf("write18W55_Send:  18W55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18W55", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18W55_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18W58(sr18W58 model, out string code, out string message)
        {
            code = "0";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = model.jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe105.InnerText = model.hpzl;
                }
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe108.InnerText = model.gwjysbbh;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = model.jyxm;
                XmlElement xe110 = xmldoc.CreateElement("jssj");//创建一个<Node>节点 
                xe110.InnerText = model.jssj;
                XmlElement xe111 = xmldoc.CreateElement("lxxm");//创建一个<Node>节点 
                xe111.InnerText = model.lxxm;
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
                IOControl.saveXmlLogInf("write18W58_Send:  18W58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18W58", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18W58_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18N63(sr18N63 model, out string code, out string message)
        {
            code = "0";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      
                XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = model.jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe105.InnerText = model.hpzl;
                }
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe109 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe109.InnerText = model.pssj;
                XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe110.InnerText = model.jyxm;
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
                XmlElement xe112 = xmldoc.CreateElement("spdm");//创建一个<Node>节点 
                xe112.InnerText = model.spdm;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe109);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(xe112);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("write18N63_Send:  18N63\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18N63", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18N63_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="clsbdh"></param>
        /// <param name="dly"></param>
        /// <param name="dlysfzh"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public DataTable GetVehicleInfBy18J52(string hphm, string hpzl, string clsbdh, string dly, string dlysfzh, out string code, out string message)
        {
            code = "0";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe2.InnerText = hphm;
                XmlElement xe3 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe3.InnerText = hpzl;
                XmlElement xe4 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe4.InnerText = clsbdh;
                XmlElement xe5 = xmldoc.CreateElement("dly");//创建一个<Node>节点  
                xe5.InnerText = dly;
                XmlElement xe6 = xmldoc.CreateElement("dlysfzh");//创建一个<Node>节点  
                xe6.InnerText = dlysfzh;
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                xe1.AppendChild(xe5);
                xe1.AppendChild(xe6);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInfBy18J52_Send:  18J52\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18J52", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleInfBy18J52_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public DataTable GetTestResultBy18J17(string jylsh, out string code, out string message)
        {
            code = "0";
            message = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe2.InnerText = jylsh;
                xe1.AppendChild(xe2);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetTestResultBy18J17_Send:  18J17\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18J17", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetTestResultBy18J17_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="clsbdh"></param>
        /// <param name="filePath"></param>
        /// <param name="zplx"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool writeTestImage(string jylsh, string hphm, string hpzl, string clsbdh, string filePath, string zplx, out string code, out string message)
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
                XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe101.InnerText = jylsh;
                XmlElement xe102 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe102.InnerText = XmlOperation.encodeUTF8(hphm);
                XmlElement xe103 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe103.InnerText = hpzl;
                XmlElement xe104 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe104.InnerText = clsbdh;
                XmlElement xe105 = xmldoc.CreateElement("zplx");//创建一个<Node>节点 
                xe105.InnerText = zplx;
                XmlElement xe106 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.PushTxt(filePath);
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeTestImage_Send:  18W03\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18W03", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeTestImage_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }

    public class sr18W55
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jyxm;
        public string kssj;
        public string lxxm;
        public sr18W55(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj, string lxxm)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.gwjysbbh = gwjysbbh;
            this.jyxm = jyxm;
            this.kssj = kssj;
            this.lxxm = lxxm;
        }
    }

    public class sr18W58
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jyxm;
        public string jssj;
        public string lxxm;
        public sr18W58(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj, string lxxm)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.gwjysbbh = gwjysbbh;
            this.jyxm = jyxm;
            this.jssj = jssj;
            this.lxxm = lxxm;
        }
    }

    public class sr18N63
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string pssj;
        public string jyxm;
        public string zpzl;
        public string spdm;
        public sr18N63(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pssj, string jyxm, string zpzl, string spdm)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hphm = hphm;
            this.hpzl = hpzl;
            this.clsbdh = clsbdh;
            this.pssj = pssj;
            this.jyxm = jyxm;
            this.zpzl = zpzl;
            this.spdm = spdm;
        }


    }
}
