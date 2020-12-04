using System;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    public class XinDun
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        private xdTmriOutAccess outlineservice = null;
        public XinDun(string url, string xtlb, string jkxlh)
        {
            outlineservice = new xdTmriOutAccess(url);
            Xtlb = xtlb;
            Jkxlh = jkxlh;
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
                xe2.InnerText = hphm;
                XmlElement xe3 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (hpzl.Contains("("))
                {
                    xe3.InnerText = hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe3.InnerText = hpzl;
                }
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
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18J11(xd18J11 model, out string code, out string message)
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
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe102.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe103 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe103.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe103.InnerText = model.hpzl;
                }
                XmlElement xe104 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe104.InnerText = model.clsbdh;
                XmlElement xe105 = xmldoc.CreateElement("gwxm");//创建一个<Node>节点 
                xe105.InnerText = model.gwxm;
                XmlElement xe106 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe106.InnerText = model.jcxdh;
                XmlElement xe107 = xmldoc.CreateElement("lx");//创建一个<Node>节点 
                xe107.InnerText = model.lx;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("write18J11_Send:  18J11\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18J11", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18J11_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18J12(xd18J12 model, out string code, out string message)
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
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe102.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe103 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe103.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe103.InnerText = model.hpzl;
                }
                XmlElement xe104 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe104.InnerText = model.clsbdh;
                XmlElement xe105 = xmldoc.CreateElement("gwxm");//创建一个<Node>节点 
                xe105.InnerText = model.gwxm;
                XmlElement xe106 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe106.InnerText = model.jcxdh;
                XmlElement xe107 = xmldoc.CreateElement("lx");//创建一个<Node>节点 
                xe107.InnerText = model.lx;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("write18J12_Send:  18J12\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18J12", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18J12_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18J16(xd18J16 model, out string code, out string message)
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
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = model.jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe104.InnerText = model.jyxm;
                XmlElement xe105 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe105.InnerText = model.jycs;
                XmlElement xe106 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe106.InnerText = model.cwkc;
                XmlElement xe107 = xmldoc.CreateElement("cwkcpd");//创建一个<Node>节点 
                xe107.InnerText = model.cwkcpd;
                XmlElement xe108 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe108.InnerText = model.cwkk;
                XmlElement xe109 = xmldoc.CreateElement("cwkkpd");//创建一个<Node>节点 
                xe109.InnerText = model.cwkkpd;
                XmlElement xe110 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe110.InnerText = model.cwkg;
                XmlElement xe111 = xmldoc.CreateElement("cwkgpd");//创建一个<Node>节点 
                xe111.InnerText = model.cwkgpd;
                XmlElement xe112 = xmldoc.CreateElement("hxnbcd");//创建一个<Node>节点 

                XmlElement xe113 = xmldoc.CreateElement("hxnbcdpd");//创建一个<Node>节点 
                xe113.InnerText = model.hxnbcdpd;
                XmlElement xe114 = xmldoc.CreateElement("hxnbkd");//创建一个<Node>节点 

                XmlElement xe115 = xmldoc.CreateElement("hxnbkdpd");//创建一个<Node>节点 
                xe115.InnerText = model.hxnbkdpd;
                XmlElement xe116 = xmldoc.CreateElement("hxnbgd");//创建一个<Node>节点 

                XmlElement xe117 = xmldoc.CreateElement("hxnbgdpd");//创建一个<Node>节点 
                xe117.InnerText = model.hxnbgdpd;
                XmlElement xe118 = xmldoc.CreateElement("zj");//创建一个<Node>节点 

                XmlElement xe119 = xmldoc.CreateElement("zjpd");//创建一个<Node>节点 
                xe119.InnerText = model.zjpd;
                XmlElement xe120 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe120.InnerText = model.clwkccpd;
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
                xe1.AppendChild(xe112);
                xe1.AppendChild(xe113);
                xe1.AppendChild(xe114);
                xe1.AppendChild(xe115);
                xe1.AppendChild(xe116);
                xe1.AppendChild(xe117);
                xe1.AppendChild(xe118);
                xe1.AppendChild(xe119);
                xe1.AppendChild(xe120);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleList_Send:  18J16\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18J16", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleList_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18C63(xd18C63 model, out string code, out string message)
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
                xe101.InnerText = model.jylsh;
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = model.jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe105.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe106 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                {
                    xe106.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                }
                else
                {
                    xe106.InnerText = model.hpzl;
                }
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                if (model.zp != "")
                {
                    xe108.InnerText = XmlOperation.PushTxt(model.zp);
                }
                else
                {
                    xe108.InnerText = "";
                }
                XmlElement xe109 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe109.InnerText = model.pssj;
                XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe110.InnerText = model.jyxm;
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
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
                IOControl.saveXmlLogInf("write18C63_Send:  18C63\r\n" + XmlOperation.removePictureInfo(send_xml, new string[1] { @"<zp>" }, new string[1] { @"</zp>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C63", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18C63_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }

    public class xd18J11
    {
        public string jylsh;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string gwxm;
        public string jcxdh;
        public string lx;
        public xd18J11(string jylsh, string hphm, string hpzl, string clsbdh, string gwxm, string jcxdh, string lx)
        {
            this.jylsh = jylsh;
            this.hphm = hphm;
            this.hpzl = hpzl;
            this.clsbdh = clsbdh;
            this.gwxm = gwxm;
            this.jcxdh = jcxdh;
            this.lx = lx;
        }
    }

    public class xd18J12
    {
        public string jylsh;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string gwxm;
        public string jcxdh;
        public string lx;
        public xd18J12(string jylsh, string hphm, string hpzl, string clsbdh, string gwxm, string jcxdh, string lx)
        {
            this.jylsh = jylsh;
            this.hphm = hphm;
            this.hpzl = hpzl;
            this.clsbdh = clsbdh;
            this.gwxm = gwxm;
            this.jcxdh = jcxdh;
            this.lx = lx;
        }
    }

    public class xd18C63
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string zp;
        public string pssj;
        public string jyxm;
        public string zpzl;

        public xd18C63(string jylsh, string jyjgbh, string jcxdh, string jycs, string hphm, string hpzl, string clsbdh, string zp, string pssj, string jyxm, string zpzl)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hphm = hphm;
            this.hpzl = hpzl;
            this.clsbdh = clsbdh;
            this.zp = zp;
            this.pssj = pssj;
            this.jyxm = jyxm;
            this.zpzl = zpzl;
        }
    }

    public class xd18J16
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string cwkc;
        public string cwkcpd;
        public string cwkk;
        public string cwkkpd;
        public string cwkg;
        public string cwkgpd;
        public string hxnbcd;
        public string hxnbcdpd;
        public string hxnbkd;
        public string hxnbkdpd;
        public string hxnbgd;
        public string hxnbgdpd;
        public string zj;
        public string zjpd;
        public string clwkccpd;

        public xd18J16(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs,
            string cwkc, string cwkcpd, string cwkk, string cwkkpd, string cwkg, string cwkgpd,
            string hxnbcd, string hxnbcdpd, string hxnbkd, string hxnbkdpd, string hxnbgd, string hxnbgdpd,
            string zj, string zjpd, string clwkccpd)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.cwkc = cwkc;
            this.cwkcpd = cwkcpd;
            this.cwkk = cwkk;
            this.cwkkpd = cwkkpd;
            this.cwkg = cwkg;
            this.cwkgpd = cwkgpd;
            this.hxnbcd = hxnbcd;
            this.hxnbcdpd = hxnbcdpd;
            this.hxnbkd = hxnbkd;
            this.hxnbkdpd = hxnbkdpd;
            this.hxnbgd = hxnbgd;
            this.hxnbgdpd = hxnbgdpd;
            this.zj = zj;
            this.zjpd = zjpd;
            this.clwkccpd = clwkccpd;
        }
    }
}
