using System;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    public class GongAn
    {

        private AccessService access;
        string Jkxlh, Xtlb, Jyjgbh;
        public GongAn(string url, string jkxlh, string xtlb, string jyjgbh)
        {
            access = new AccessService(url);
            Jkxlh = jkxlh;
            Xtlb = xtlb;
            Jyjgbh = jyjgbh;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeGaZbzlTestDetailResult(acZbzlDetailResult model, out string code, out string message)
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
                xe102.InnerText = Jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe104.InnerText = model.jyxm;
                XmlElement xe105 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe105.InnerText = model.jycs;

                XmlElement xe106 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe106.InnerText = model.hpzl;
                XmlElement xe107 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe107.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe108 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe108.InnerText = model.clsbdh;
                XmlElement xe110 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe110.InnerText = model.gwjysbbh;

                XmlElement xe111 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe111.InnerText = model.zbzl;
                XmlElement xe112 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                xe112.InnerText = model.zbzlpd;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(xe112);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeGaZbzlTestDetailResult_Send:  JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(access.WriteService(Jkxlh, "18C81", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeGaZbzlTestDetailResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeGaProjectStart(AcprojectStart model, out string code, out string message)
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
                xe102.InnerText = Jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe108.InnerText = model.gwjysbbh;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = model.jyxm;
                XmlElement xe110 = xmldoc.CreateElement("kssj");//创建一个<Node>节点 
                xe110.InnerText = XmlOperation.encodeUTF8(model.kssj);
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
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeGaProjectStart_Send:  JKXLH:" + Jkxlh + "|JKID:18C55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(access.WriteService(Jkxlh, "18C55", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeGaProjectStart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeGaProjectFinish(AcprojectFinish model, out string code, out string message)
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
                xe102.InnerText = Jyjgbh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = model.jycs;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe108.InnerText = model.gwjysbbh;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = model.jyxm;
                XmlElement xe110 = xmldoc.CreateElement("jssj");//创建一个<Node>节点 
                xe110.InnerText = XmlOperation.encodeUTF8(model.jssj);
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
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeGaProjectFinish_Send:  JKXLH:" + Jkxlh + "|JKID:18C58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(access.WriteService(Jkxlh, "18C58", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeGaProjectFinish_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writetestDetailResult(ActestDetailResult model, out string code, out string message)
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

                XmlElement xe106 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe106.InnerText = model.hpzl;
                XmlElement xe107 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe107.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe108 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe108.InnerText = model.clsbdh;
                XmlElement xe110 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe110.InnerText = model.gwjysbbh;

                XmlElement xe111 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe111.InnerText = model.cwkc;
                XmlElement xe112 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe112.InnerText = model.cwkk;
                XmlElement xe113 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe113.InnerText = model.cwkg;
                XmlElement xe114 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe114.InnerText = model.clwkccpd;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(xe112);
                xe1.AppendChild(xe113);
                xe1.AppendChild(xe114);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writetestDetailResult_Send:  JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(access.WriteService(Jkxlh, "18C81", send_xml));
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
    }
}
