using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    public class KangShiBai
    {
        private KSBsafe outlineservice = null;

        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <param name="url">接口地址</param>
        public KangShiBai(string url)
        {
            outlineservice = new KSBsafe(url);
        }

        /// <summary>
        /// 101101——获取平台时间
        /// </summary>
        /// <param name="InternetTime">平台时间</param>
        /// <returns>是否成功获取</returns>
        public DataTable GetDatetime()
        {
            try
            {
                IOControl.saveXmlLogInf("GetDatetime_Send: 101101\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101101, "null", "null"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetDatetime_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 101201——获取车辆基本信息
        /// </summary>
        /// <param name="hphm">号码</param>
        /// <param name="hpzl">号码种类</param>
        /// <param name="clsbdh">车辆识别码</param>
        /// <param name="jyjgbh">检验机构编号</param>
        /// <param name="code">获取结果</param>
        /// <param name="message">错误信息</param>
        /// <returns>车辆信息</returns>
        public DataTable GetVehicleInf(string hphm, string hpzl, string clsbdh, string jyjgbh, out string code, out string message)
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
                xe3.InnerText = hpzl;
                XmlElement xe4 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe4.InnerText = clsbdh;
                XmlElement xe5 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点  
                xe5.InnerText = jyjgbh;
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                xe1.AppendChild(xe5);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  101205\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101205, send_xml, "null"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");
                
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleInf_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        /// <summary>
        /// 101116——发送拍照命令
        /// </summary>
        /// <param name="jylsh">流水号</param>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="clsbdh"></param>
        /// <param name="filePath"></param>
        /// <param name="zplx"></param>
        /// <param name="code">是否成功代码</param>
        /// <param name="message">返回消息</param>
        public bool writePicture(KsbcapturePicture model, out string code, out string message)
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
                XmlElement xe101 = xmldoc.CreateElement("sbdh");//创建一个<Node>节点 
                xe101.InnerText = model.sbdh;
                XmlElement xe102 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe102.InnerText = model.jylsh;
                XmlElement xe103 = xmldoc.CreateElement("");//创建一个<Node>节点 
                xe103.InnerText = model.jyjgbh;
                XmlElement xe104 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe104.InnerText = model.jcxdh;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe105.InnerText = model.hpzl;
                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe108.InnerText = model.jycs;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = model.jyxm;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                xe1.AppendChild(xe109);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writePicture_Send:  101116\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101116, send_xml, "null"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writePicture_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 101104——项目开始命令
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectStart(KsbprojectStart model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectStart_Send:  101104\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101104, send_xml, "null"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeProjectStart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 101105——项目结束命令
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectFinish(KsbprojectFinish model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectFinish_Send:  101105\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101105, send_xml, "null"));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeProjectFinish_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 101115——上传检测结果详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writetestDetailResult(KsbtestDetailResult model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writetestDetailResult_Send:  101115\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.UploadObjectOut(101115, send_xml, "null"));
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

    public class KsbprojectStart
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

        public KsbprojectStart(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj)
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
            this.gwjysbbh = gwjysbbh;
            this.jyxm = jyxm;
            this.kssj = kssj;
        }
    }

    public class KsbprojectFinish
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

        public KsbprojectFinish(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj)
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
            this.gwjysbbh = gwjysbbh;
            this.jyxm = jyxm;
            this.jssj = jssj;
        }
    }

    public class KsbcapturePicture
    {
        public string sbdh;
        public string jylsh;
        public string jyjgbh;
        public string jycs;
        public string jcxdh;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string jyxm;

        public KsbcapturePicture(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string jyxm)
        {
            this.sbdh = "1";
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
        }
    }

    public class KsbtestDetailResult
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string clwkccpd;

        public KsbtestDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string cwkc,
            string cwkk, string cwkg, string clwkccpd)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.gwjysbbh = gwjysbbh;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.clwkccpd = clwkccpd;
        }
    }
}
