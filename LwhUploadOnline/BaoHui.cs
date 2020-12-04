using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;



namespace LwhUploadOnline
{
    public class BaoHui
    {

        private string jkxlh = "";
        private string lineid = "";
        private BHService outlineservice = null;

        public BaoHui(string jkxlh, string lineid, string url)
        {
            this.jkxlh = jkxlh;
            this.lineid = lineid;
            outlineservice = new BHService(url);
        }

        /// <summary>
        /// 获取平台时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="systemTime"></param>
        /// <returns></returns>
        public bool getSystemTime(out int code, out string message, out DateTime systemTime)
        {
            code = 0;
            message = "";
            systemTime = DateTime.Now;
            try
            {
                IOControl.saveXmlLogInf("getSystemTime_Send: 18C50\r\n");
                ReturnInfo receiveInf = outlineservice.queryObjectOut(jkxlh, "18C50", "");
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received: Code:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                if (code == 1)
                {
                    systemTime = System.DateTime.Parse(XmlOperation.CXmlToDatatTable(HttpUtility.UrlDecode(receiveInf.infoXML)).Rows[0]["SystemTime"].ToString());
                    return true;
                }
                else
                    return false;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("getSystemTime_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发送项目开始
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jyxm"></param>
        /// <param name="tgbz"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool writeProjectstart(string jylsh, string jyxm, string tgbz, out int code, out string message)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点
                XmlElement xe11 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe11.InnerText = jylsh;
                XmlElement xe12 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe12.InnerText = jyxm;
                XmlElement xe13 = xmldoc.CreateElement("tdh");//创建一个<Node>节点 
                xe13.InnerText = lineid;
                XmlElement xe14 = xmldoc.CreateElement("tgbz");//创建一个<Node>节点 
                xe14.InnerText = XmlOperation.encodeUTF8(tgbz);
                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeProjectstart_Send:  18C55\r\n" + send_xml + "\r\n");

                ReturnInfo receiveInf = outlineservice.writeObjectOut(jkxlh, "18C55", send_xml);
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received:\r\nCode:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                return true;
            }
            catch (Exception er)
            {
                code = 0;
                message = "";
                IOControl.saveXmlLogInf("writeProjectstart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发送外廓尺寸结果数据
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jyxm"></param>
        /// <param name="jycs"></param>
        /// <param name="cwkc"></param>
        /// <param name="cwkk"></param>
        /// <param name="cwkg"></param>
        /// <param name="cwkbz"></param>
        /// <param name="cwkhg"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool writeM1Result(string jylsh, string jyxm, string jycs, string cwkc, string cwkk, string cwkg, string cwkbz, string cwkhg, out int code, out string message)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点
                XmlElement xe11 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe11.InnerText = jylsh;
                XmlElement xe12 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe12.InnerText = jyxm;
                XmlElement xe13 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe13.InnerText = jycs;
                XmlElement xe14 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe14.InnerText = cwkc;
                XmlElement xe15 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe15.InnerText = cwkk;
                XmlElement xe16 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe16.InnerText = cwkg;
                XmlElement xe17 = xmldoc.CreateElement("cwkbz");//创建一个<Node>节点 
                xe17.InnerText = XmlOperation.encodeUTF8(cwkbz);
                XmlElement xe18 = xmldoc.CreateElement("cwkhg");//创建一个<Node>节点 
                xe18.InnerText = cwkhg == "合格" ? "O" : "X";
                XmlElement xe19 = xmldoc.CreateElement("tdh");//创建一个<Node>节点 
                xe19.InnerText = lineid;
                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                xe1.AppendChild(xe18);
                xe1.AppendChild(xe19);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeM1Result_Send:  18C81\r\n" + send_xml + "\r\n");

                ReturnInfo receiveInf = outlineservice.writeObjectOut(jkxlh, "18C81", send_xml);
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received:\r\nCode:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                return true;
            }
            catch (Exception er)
            {
                code = 0;
                message = "";
                IOControl.saveXmlLogInf("writeM1Result_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发送整备质量结果数据
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jyxm"></param>
        /// <param name="jycs"></param>
        /// <param name="zbzl"></param>
        /// <param name="zbzlbz"></param>
        /// <param name="zbzlhg"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeZ1Result(string jylsh, string jyxm, string jycs, string zbzl, string zbzlbz, string zbzlhg, out int code, out string message)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点
                XmlElement xe11 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe11.InnerText = jylsh;
                XmlElement xe12 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe12.InnerText = jyxm;
                XmlElement xe13 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe13.InnerText = jycs;
                XmlElement xe14 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe14.InnerText = zbzl;
                XmlElement xe15 = xmldoc.CreateElement("zbzlbz");//创建一个<Node>节点 
                xe15.InnerText = XmlOperation.encodeUTF8(zbzlbz);
                XmlElement xe16 = xmldoc.CreateElement("zbzlhg");//创建一个<Node>节点 
                xe16.InnerText = zbzlhg == "合格" ? "O" : "X";
                XmlElement xe17 = xmldoc.CreateElement("tdh");//创建一个<Node>节点 
                xe17.InnerText = lineid;
                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeZ1Result_Send:  18C81\r\n" + send_xml + "\r\n");

                ReturnInfo receiveInf = outlineservice.writeObjectOut(jkxlh, "18C81", send_xml);
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received:\r\nCode:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                return true;
            }
            catch (Exception er)
            {
                code = 0;
                message = "";
                IOControl.saveXmlLogInf("writeZ1Result_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发送照片
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="jycs"></param>
        /// <param name="zpzl"></param>
        /// <param name="zp_path"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writePicture(string jylsh, string jycs, string zpzl, string zp_path, out int code, out string message)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点

                XmlElement xe11 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe11.InnerText = jylsh;
                XmlElement xe12 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe12.InnerText = jycs;
                XmlElement xe13 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe13.InnerText = zpzl;
                XmlElement xe14 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                if (zp_path != "")
                    xe14.InnerText = XmlOperation.encodeUTF8(XmlOperation.PushTxt(zp_path));
                else
                    xe14.InnerText = "";
                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writePicture_Send:  00C63\r\n" + XmlOperation.removePictureInfo(send_xml, new string[4] { @"<zp>", @"<zp1>", @"<zp2>", @"<zp3>" }, new string[4] { @"</zp>", @"</zp1>", @"</zp2>", @"</zp3>" }) + "\r\n");

                ReturnInfo receiveInf = outlineservice.writeObjectOut(jkxlh, "00C63", send_xml);
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received:\r\nCode:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                return true;
            }
            catch (Exception er)
            {
                code = 0;
                message = "";
                IOControl.saveXmlLogInf("writePicture_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeProjectfinish(string jylsh, string jyxm, string tgbz, out int code, out string message)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点
                XmlElement xe11 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe11.InnerText = jylsh;
                XmlElement xe12 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe12.InnerText = jyxm;
                XmlElement xe13 = xmldoc.CreateElement("tdh");//创建一个<Node>节点 
                xe13.InnerText = lineid;
                XmlElement xe14 = xmldoc.CreateElement("tgbz");//创建一个<Node>节点 
                xe14.InnerText = XmlOperation.encodeUTF8(tgbz);
                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeProjectstart_Send:  18C58\r\n" + send_xml + "\r\n");

                ReturnInfo receiveInf = outlineservice.writeObjectOut(jkxlh, "18C58", send_xml);
                code = receiveInf.code;
                message = receiveInf.message;
                IOControl.saveXmlLogInf("Received:\r\nCode:" + code + "|Message:" + message + "|InfoXML:\r\n" + receiveInf.infoXML + "\r\n");

                return true;
            }
            catch (Exception er)
            {
                code = 0;
                message = "";
                IOControl.saveXmlLogInf("writeProjectstart_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }        
    }
}
