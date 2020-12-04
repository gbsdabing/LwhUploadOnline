using System;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    public class WeiKe
    {
        string Xtlb = "";
        string Jkxlh = "";
        WKService outlineservice = null;

        public WeiKe(string url, string xtlb, string jkxlh)
        {
            if (url != "")
                outlineservice = new WKService(url);
            else
                outlineservice = new WKService();
            Xtlb = xtlb;
            Jkxlh = jkxlh;

        }
        public DataTable GetVehicleInf(string hphm, string hpzl, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  HPHM:" + hphm + "|HPZL:" + hpzl + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.getclxx(hphm, hpzl));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                return ReadVehicleInfDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleInf_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeStart(wkStartAndFinish model, out string code, out string message)
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
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe110 = xmldoc.CreateElement("sj");//创建一个<Node>节点 
                xe110.InnerText = model.sj;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe110);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeStart_Send:\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.wkcc(model.jylsh, send_xml, "0"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeStart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeFinish(wkStartAndFinish model, out string code, out string message)
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
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe110 = xmldoc.CreateElement("sj");//创建一个<Node>节点 
                xe110.InnerText = model.sj;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe110);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeFinish_Send:\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.wkcc(model.jylsh, send_xml, "2"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeFinish_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeVideoStart(wkVedioStartAndFinish model, out string code, out string message)
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
                XmlElement xe104 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe104.InnerText = model.jyjgbh;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("tongdao");//创建一个<Node>节点 
                xe107.InnerText = model.tongdao;
                XmlElement xe110 = xmldoc.CreateElement("gongwei");//创建一个<Node>节点 
                xe110.InnerText = model.gongwei;
                XmlElement xe111 = xmldoc.CreateElement("xuhao");//创建一个<Node>节点 
                xe111.InnerText = model.xuhao;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeVideoStart_Send:  9991\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "9991", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeVideoStart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeVideoFinish(wkVedioStartAndFinish model, out string code, out string message)
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
                XmlElement xe104 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe104.InnerText = model.jyjgbh;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("tongdao");//创建一个<Node>节点 
                xe107.InnerText = model.tongdao;
                XmlElement xe110 = xmldoc.CreateElement("gongwei");//创建一个<Node>节点 
                xe110.InnerText = model.gongwei;
                XmlElement xe111 = xmldoc.CreateElement("xuhao");//创建一个<Node>节点 
                xe111.InnerText = model.xuhao;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeVideoFinish_Send:  9992\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "9992", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeVideoFinish_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18C63(wk18C63 model, out string code, out string message)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writetestDetailResult(wktestDetailResult model, out string code, out string message)
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
                XmlElement xe105 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe105.InnerText = model.jycs;


                XmlElement xe111 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe111.InnerText = model.cwkc;
                XmlElement xe112 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe112.InnerText = model.cwkk;
                XmlElement xe113 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe113.InnerText = model.cwkg;
                XmlElement xe114 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe114.InnerText = model.clwkccpd;
                XmlElement xe115 = xmldoc.CreateElement("cwkcsx");//创建一个<Node>节点 
                xe115.InnerText = model.cwkcsx;
                XmlElement xe116 = xmldoc.CreateElement("cwkcxx");//创建一个<Node>节点 
                xe116.InnerText = model.cwkcxx;
                XmlElement xe117 = xmldoc.CreateElement("cwkcpd");//创建一个<Node>节点 
                xe117.InnerText = model.cwkcpd;
                XmlElement xe118 = xmldoc.CreateElement("cwkksx");//创建一个<Node>节点 
                xe118.InnerText = model.cwkksx;
                XmlElement xe119 = xmldoc.CreateElement("cwkkxx");//创建一个<Node>节点 
                xe119.InnerText = model.cwkkxx;
                XmlElement xe120 = xmldoc.CreateElement("cwkkpd");//创建一个<Node>节点 
                xe120.InnerText = model.cwkkpd;
                XmlElement xe121 = xmldoc.CreateElement("cwkgsx");//创建一个<Node>节点 
                xe121.InnerText = model.cwkgsx;
                XmlElement xe122 = xmldoc.CreateElement("cwkgxx");//创建一个<Node>节点 
                xe122.InnerText = model.cwkgxx;
                XmlElement xe123 = xmldoc.CreateElement("cwkgpd");//创建一个<Node>节点 
                xe123.InnerText = model.cwkgpd;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe105);
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
                xe1.AppendChild(xe121);
                xe1.AppendChild(xe122);
                xe1.AppendChild(xe123);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writetestDetailResult_Send:\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.wkcc(model.jylsh, send_xml, "1"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writetestDetailResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        #region XML解析
        public DataTable ReadVehicleInfDatatable(string xmlstring, out string code, out string message)
        {
            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(xmlstring);
            code = ds.Tables[0].Rows[0]["code"].ToString();
            message = "";
            if (code != "1")
                message = ds.Tables[0].Rows[0]["message"].ToString();
            Console.Write(code + "\r\n");
            Console.Write(message + "\r\n");
            if (code == "1")
            {
                if (ds.Tables.Contains("vehispara"))
                    return ds.Tables["vehispara"];
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }

    public class wkStartAndFinish
    {
        public string jylsh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string sj;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jycs"></param>
        /// <param name="hpzl"></param>
        /// <param name="hphm"></param>
        /// <param name="clsbdh"></param>
        /// <param name="sj"></param>
        public wkStartAndFinish(string jylsh, string jycs, string hpzl, string hphm, string clsbdh, string sj)
        {
            this.jylsh = jylsh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.sj = sj;
        }
    }

    public class wkVedioStartAndFinish
    {
        public string jylsh;
        public string jyjgbh;
        public string hpzl;
        public string hphm;
        public string tongdao;
        public string gongwei;
        public string xuhao;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jycs"></param>
        /// <param name="hpzl"></param>
        /// <param name="hphm"></param>
        /// <param name="clsbdh"></param>
        /// <param name="sj"></param>
        public wkVedioStartAndFinish(string jylsh, string jyjgbh, string hpzl, string hphm, string tongdao, string gongwei, string xuhao)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.tongdao = tongdao;
            this.gongwei = gongwei;
            this.xuhao = xuhao;
        }
    }

    public class wk18C63
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
        public wk18C63(string jylsh, string jyjgbh, string jcxdh, string jycs, string hphm, string hpzl, string clsbdh, string zp, string pssj, string jyxm, string zpzl)
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

    public class wktestDetailResult
    {
        public string jylsh;
        public string jycs;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string clwkccpd;
        public string cwkcsx;
        public string cwkcxx;
        public string cwkcpd;
        public string cwkksx;
        public string cwkkxx;
        public string cwkkpd;
        public string cwkgsx;
        public string cwkgxx;
        public string cwkgpd;

        public wktestDetailResult(string jylsh, string jycs, string cwkc, string cwkk, string cwkg, string clwkccpd,
            string cwkcsx, string cwkcxx, string cwkcpd, string cwkksx, string cwkkxx, string cwkkpd,
            string cwkgsx, string cwkgxx, string cwkgpd)
        {
            this.jylsh = jylsh;
            this.jycs = jycs;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.clwkccpd = clwkccpd;
            this.cwkcsx = cwkcsx;
            this.cwkcxx = cwkcxx;
            this.cwkcpd = cwkcpd;
            this.cwkksx = cwkksx;
            this.cwkkxx = cwkkxx;
            this.cwkkpd = cwkkpd;
            this.cwkgsx = cwkgsx;
            this.cwkgxx = cwkgxx;
            this.cwkgpd = cwkgpd;
        }
    }
}
