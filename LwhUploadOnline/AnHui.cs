using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;



namespace LwhUploadOnline
{
    public class AnHui
    {
        string Jkxlh = "";
        AHOutService outlineservice = null;

        public AnHui(string url, string jkxlh)
        {
            outlineservice = new AHOutService(url);
            Jkxlh = jkxlh;
        }

        /// <summary>
        /// 获取平台时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="datestring"></param>
        /// <returns></returns>
        public bool GetSystemDatetime(out string code, out string message, out string datestring)
        {
            IOControl.saveXmlLogInf("GetSynchTime_Send: 时间同步接口\r\n参数1-jkxlh:" + Jkxlh + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.getSynchTime(Jkxlh));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            if (code == "1")
            {
                datestring = message;
                return true;
            }
            else
            {
                datestring = "";
                return false;
            }
        }

        /// <summary>
        /// 获取待检车辆信息
        /// </summary>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="clsbdh"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetVehicleInf(string hphm, string hpzl, string clsbdh, out string code, out string message, out DataTable dt)
        {
            if (clsbdh.Length > 4)
                clsbdh = clsbdh.Substring(clsbdh.Length - 4, 4);//车辆识别代号超过4位时传后4位

            IOControl.saveXmlLogInf("QueryVehicleOut_Send:车辆信息查询接口 \r\n参数1-jkxlh:" + Jkxlh + "|参数2-hphm:" + hphm + "|参数3-hpzl:" + hpzl + "|参数4-clsbdh:" + clsbdh + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.queryVehicleOut(Jkxlh, hpzl, hphm, clsbdh));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            if (code == "1")
            {
                dt = ds.Tables["vehinfo"];
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                dt = null;
                return false;
            }
        }

        /// <summary>
        /// 写入项目开始
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeProjectStart(AhprojectStart model, out string code, out string message)
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("reqinfo");//创建一个<Node>节点      
            XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
            xe101.InnerText = model.jylsh;
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
            xe110.InnerText = model.kssj;
            xe1.AppendChild(xe101);
            xe1.AppendChild(xe103);
            xe1.AppendChild(xe104);
            xe1.AppendChild(xe105);
            xe1.AppendChild(xe106);
            xe1.AppendChild(xe107);
            xe1.AppendChild(xe108);
            xe1.AppendChild(xe109);
            xe1.AppendChild(xe110);
            root.AppendChild(xe1);

            string xmlstring = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
            IOControl.saveXmlLogInf("ReqBeginDetectingOut_Send:请求检验项目开始 \r\n参数1-Jkxlh:" + Jkxlh + "|参数2-regXmlDoc:" + "\r\n" + xmlstring + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.reqBeginDetectingOut(Jkxlh, xmlstring));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            return code == "1";
        }

        /// <summary>
        /// 写入项目结束
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeProjectFinish(AhprojectFinish model, out string code, out string message)
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("reqinfo");//创建一个<Node>节点      
            XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
            xe101.InnerText = model.jylsh;
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
            xe110.InnerText = model.jssj;
            xe1.AppendChild(xe101);
            xe1.AppendChild(xe103);
            xe1.AppendChild(xe104);
            xe1.AppendChild(xe105);
            xe1.AppendChild(xe106);
            xe1.AppendChild(xe107);
            xe1.AppendChild(xe108);
            xe1.AppendChild(xe109);
            xe1.AppendChild(xe110);
            root.AppendChild(xe1);

            string xmlstring = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
            IOControl.saveXmlLogInf("ReqEndDetectingOut_Send:请求检验项目结束 \r\n参数1-Jkxlh:" + Jkxlh + "|参数2-regXmlDoc:" + "\r\n" + xmlstring + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.reqEndDetectingOut(Jkxlh, xmlstring));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            return code == "1";
        }

        /// <summary>
        /// 写入外廓结果数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writetestDetailResult(AhtestDetailResult model, out string code, out string message)
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("reqinfo");//创建一个<Node>节点      
            XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
            xe101.InnerText = model.jylsh;
            XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
            xe103.InnerText = model.jcxdh;
            XmlElement xe104 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
            xe104.InnerText = model.jyxm;
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
            xe1.AppendChild(xe101);
            xe1.AppendChild(xe103);
            xe1.AppendChild(xe104);
            xe1.AppendChild(xe105);
            xe1.AppendChild(xe111);
            xe1.AppendChild(xe112);
            xe1.AppendChild(xe113);
            xe1.AppendChild(xe114);
            root.AppendChild(xe1);

            string xmlstring = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
            IOControl.saveXmlLogInf("ReqWriteCheckDataOut_Send:写检验结果详细数据 \r\n参数1-Jkxlh:" + Jkxlh + "|参数2-Jyxm:" + model.jyxm + "|参数3-regXmlDoc:" + "\r\n" + xmlstring + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.reqWriteCheckDataOut(Jkxlh, model.jyxm, xmlstring));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            return code == "1";
        }

        /// <summary>
        /// 写入整备质量结果数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeZbzlTestDetailResult(ahZbzlDetailResult model, out string code, out string message)
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("reqinfo");//创建一个<Node>节点      
            XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
            xe101.InnerText = model.jylsh;
            XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
            xe103.InnerText = model.jcxdh;
            XmlElement xe104 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
            xe104.InnerText = model.jyxm;
            XmlElement xe105 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
            xe105.InnerText = model.jycs;
            XmlElement xe111 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
            xe111.InnerText = model.zbzl;
            XmlElement xe112 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
            xe112.InnerText = model.zbzlpd;
            xe1.AppendChild(xe101);
            xe1.AppendChild(xe103);
            xe1.AppendChild(xe104);
            xe1.AppendChild(xe105);
            xe1.AppendChild(xe111);
            xe1.AppendChild(xe112);
            root.AppendChild(xe1);

            string xmlstring = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
            IOControl.saveXmlLogInf("ReqWriteCheckDataOut_Send:写检验结果详细数据 \r\n参数1-Jkxlh:" + Jkxlh + "|参数2-Jyxm:" + model.jyxm + "|参数3-regXmlDoc:" + "\r\n" + xmlstring + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.reqWriteCheckDataOut(Jkxlh, model.jyxm, xmlstring));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            return code == "1";
        }

        /// <summary>
        /// 发送拍照命令
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeCapturePicture(AhcapturePicture model, out string code, out string message)
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("reqinfo");//创建一个<Node>节点      
            XmlElement xe101 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
            xe101.InnerText = model.jylsh;
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
            XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
            xe110.InnerText = model.jyxm;
            XmlElement xe111 = xmldoc.CreateElement("isFrontLeft");//创建一个<Node>节点 
            xe111.InnerText = model.isFrontLeft;
            xe1.AppendChild(xe101);
            xe1.AppendChild(xe103);
            xe1.AppendChild(xe104);
            xe1.AppendChild(xe105);
            xe1.AppendChild(xe106);
            xe1.AppendChild(xe107);
            xe1.AppendChild(xe110);
            xe1.AppendChild(xe111);
            root.AppendChild(xe1);

            string xmlstring = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
            IOControl.saveXmlLogInf("ReqCaptureOut_Send:请求抓拍照片 \r\n参数1-Jkxlh:" + Jkxlh + "|参数2-regXmlDoc:" + "\r\n" + xmlstring + "\r\n");
            string receiveXml = HttpUtility.UrlDecode(outlineservice.reqCaptureOut(Jkxlh, xmlstring));
            IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

            DataSet ds = new DataSet();
            ds = XmlOperation.CXmlToDataSet(receiveXml);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = ds.Tables["head"].Rows[0]["message"].ToString();
            return code == "1";
        }
    }

    #region 数据结构定义
    public class AhprojectStart
    {

        public string jylsh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jyxm;
        public string kssj;

        public AhprojectStart(string jylsh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj)
        {
            this.jylsh = jylsh;
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
    public class AhprojectFinish
    {
        public string jylsh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jyxm;
        public string jssj;
        public AhprojectFinish(string jylsh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj)
        {
            this.jylsh = jylsh;
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
    public class AhcapturePicture
    {
        public string jylsh;
        public string jycs;
        public string jcxdh;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string jyxm;
        public string isFrontLeft;
        public AhcapturePicture(string jylsh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string jyxm, string isFrontLeft)
        {
            this.jylsh = jylsh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            if (hpzl.Contains("("))
                this.hpzl = hpzl.Split('(')[1].Split(')')[0];
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.isFrontLeft = isFrontLeft;
            this.jyxm = jyxm;
        }
    }
    public class AhtestDetailResult
    {
        public string jylsh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string clwkccpd;
        public AhtestDetailResult(string jylsh, string jcxdh, string jyxm, string jycs, string cwkc,
            string cwkk, string cwkg, string clwkccpd)
        {
            this.jylsh = jylsh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.clwkccpd = clwkccpd;
        }
    }
    public class ahZbzlDetailResult
    {
        public string jylsh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string zbzl;
        public string zbzlpd;
        public ahZbzlDetailResult(string jylsh, string jcxdh, string jyxm, string jycs, string zbzl,
            string zbzlpd)
        {
            this.jylsh = jylsh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.zbzl = zbzl;
            this.zbzlpd = zbzlpd;
        }
    }
    #endregion
}
