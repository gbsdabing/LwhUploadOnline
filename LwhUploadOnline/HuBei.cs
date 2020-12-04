using System;
using System.Data;
using System.Web;
using System.Xml;



namespace LwhUploadOnline
{
    public class HuBei
    {
        private HBCarryJcjkServicesService upload_interface = null;
        private string xtlb;
        private string jkxlh;
        private string station_id;
        private string line_id;
        private string local_ip;

        /// <summary>
        /// 湖北联网初始化
        /// </summary>
        /// <param name="station_id">站号</param>
        /// <param name="line_id">线号</param>
        /// <param name="xtlb">系统类别</param>
        /// <param name="jkxlh">接口序列号</param>
        /// <param name="local_ip">申请IP（即本机IP）</param>
        /// <param name="url">接口地址</param>
        public HuBei(string station_id, string line_id, string xtlb, string jkxlh, string local_ip, string url)
        {
            this.station_id = station_id;
            this.line_id = line_id;
            this.xtlb = xtlb;
            this.jkxlh = jkxlh;
            this.local_ip = local_ip;
            upload_interface = new HBCarryJcjkServicesService(url);
        }

        public bool getSystemTime(string babh, out string systime)
        {
            systime = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点
                XmlElement xe2 = xmldoc.CreateElement("babh");//创建一个<Node>节点
                xe2.InnerText = babh;
                XmlElement xe3 = xmldoc.CreateElement("sqip");//创建一个<Node>节点
                xe3.InnerText = local_ip;

                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("getSystemTime_Send:   18C50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.queryObjectOut(xtlb, jkxlh, "18C50", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                DataTable dt = XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
                if (dt != null && code == "1")
                {
                    systime = dt.Rows[0]["sj"].ToString();
                    return true;
                }
                else
                {
                    IOControl.saveXmlLogInf("同步时间失败:" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("getSystemTime_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="vin"></param>
        /// <param name="dt_waitcar_info"></param>
        /// <param name="error_msg"></param>
        /// <returns></returns>
        public bool getVehicleInfo(string jylsh, string hphm, string hpzl, string vin, out DataTable dt_waitcar_info, out string error_msg)
        {
            dt_waitcar_info = new DataTable();
            error_msg = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      

                XmlElement xe11 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe11.InnerText = station_id;
                XmlElement xe12 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe12.InnerText = jylsh;
                XmlElement xe13 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe13.InnerText = XmlOperation.encodeUTF8(hphm);
                XmlElement xe14 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe14.InnerText = hpzl;
                XmlElement xe15 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe15.InnerText = vin;
                XmlElement xe16 = xmldoc.CreateElement("sqip");//创建一个<Node>节点 
                xe16.InnerText = local_ip;

                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                root.AppendChild(xe1);
           
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("getVehicleInfo_Send:   18R13\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.queryObjectOut(xtlb, jkxlh, "18R13", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                dt_waitcar_info = XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
                if (dt_waitcar_info != null && code == "1")
                    return true;
                else
                {
                    IOControl.saveXmlLogInf("获取待检车辆信息失败:" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("getVehicleInfo_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_start"></param>
        /// <param name="jylsh"></param>
        /// <param name="sbbh"></param>
        /// <param name="jycs"></param>
        /// <param name="jyxm"></param>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="vin"></param>
        /// <param name="sj"></param>
        /// <returns></returns>
        public bool SendProjectStartOrStop(bool is_start, string jylsh, string sbbh, string jycs, string jyxm, string hphm, string hpzl, string vin, DateTime sj)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      

                XmlElement xe11 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe11.InnerText = station_id;
                XmlElement xe12 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe12.InnerText = line_id;
                XmlElement xe13 = xmldoc.CreateElement("gwjysbbh");//创建一个<Node>节点 
                xe13.InnerText = sbbh;
                XmlElement xe14 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe14.InnerText = jylsh;
                XmlElement xe15 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe15.InnerText = jycs;
                XmlElement xe16 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe16.InnerText = jyxm;
                XmlElement xe17 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe17.InnerText = XmlOperation.encodeUTF8(hphm);
                XmlElement xe18 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe18.InnerText = hpzl;
                XmlElement xe19 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe19.InnerText = vin;
                XmlElement xe110 = xmldoc.CreateElement(is_start ? "kssj" : "jssj");//创建一个<Node>节点 
                xe110.InnerText = sj.ToString("yyyy-MM-dd HH:mm:ss");
                XmlElement xe111 = xmldoc.CreateElement("sqip");//创建一个<Node>节点 
                xe111.InnerText = local_ip;

                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                xe1.AppendChild(xe18);
                xe1.AppendChild(xe19);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("SendProjectStartOrStop_Send:   "+ (is_start ? "开始：18C55" : "结束：18C58") + "\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.writeObjectOut(xtlb, jkxlh, (is_start ? "开始：18C55" : "结束：18C58"), send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                if(XmlOperation.ReadACKString(receiveXml, out code, out message) && code == "1")
                    return true;
                else
                {
                    IOControl.saveXmlLogInf("写入项目" + (is_start ? "开始" : "结束") + "失败:\r\n" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("SendProjectStartOrStop_Send_Error: " + (is_start ? "开始" : "结束") + "\r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool SendWKResult(string jylsh, string jycs, string jyxm, string length_bz, string width_bz, string height_bz,
            string length, string width, string height,
            string length_pd, string width_pd, string height_pd,
            string zbzlvalue, string clzbzlvalue, string zbzlpdvalue,
            string zhpd, string sqr)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      

                XmlElement xe11 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe11.InnerText = station_id;
                XmlElement xe12 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe12.InnerText = line_id;
                XmlElement xe13 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe13.InnerText = jylsh;
                XmlElement xe14 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe14.InnerText = jycs;
                XmlElement xe15 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe15.InnerText = jyxm;
                XmlElement xe16 = xmldoc.CreateElement("bzcwkc");//创建一个<Node>节点 
                xe16.InnerText = length_bz;
                XmlElement xe17 = xmldoc.CreateElement("bzcwkk");//创建一个<Node>节点 
                xe17.InnerText = width_bz;
                XmlElement xe18 = xmldoc.CreateElement("bzcwkg");//创建一个<Node>节点 
                xe18.InnerText = height_bz;
                XmlElement xe19 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe19.InnerText = length;
                XmlElement xe110 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe110.InnerText = width;
                XmlElement xe111 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe111.InnerText = height;
                XmlElement xe112 = xmldoc.CreateElement("ccpd");//创建一个<Node>节点 
                xe112.InnerText = length_pd;
                XmlElement xe113 = xmldoc.CreateElement("ckpd");//创建一个<Node>节点 
                xe113.InnerText = width_pd;
                XmlElement xe114 = xmldoc.CreateElement("cgpd");//创建一个<Node>节点 
                xe114.InnerText = height_pd;
                XmlElement xe115 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe115.InnerText = zhpd;
                XmlElement xe116 = xmldoc.CreateElement("wjr");//创建一个<Node>节点 
                xe116.InnerText = XmlOperation.encodeUTF8(sqr);
                XmlElement xe117 = xmldoc.CreateElement("sqip");//创建一个<Node>节点 
                xe117.InnerText = local_ip;
                XmlElement zbzl = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                zbzl.InnerText = zbzlvalue;
                XmlElement clzbzl = xmldoc.CreateElement("clzbzl");//创建一个<Node>节点 
                clzbzl.InnerText = clzbzlvalue;
                XmlElement zbzlpd = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                zbzlpd.InnerText = zbzlpdvalue;
                XmlElement xe118 = xmldoc.CreateElement("hxnbcd");//创建一个<Node>节点 
                XmlElement xe119 = xmldoc.CreateElement("hxnbkd");//创建一个<Node>节点 
                XmlElement xe120 = xmldoc.CreateElement("hxnbgd");//创建一个<Node>节点 
                XmlElement xe121 = xmldoc.CreateElement("clzj");//创建一个<Node>节点

                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                xe1.AppendChild(xe18);
                xe1.AppendChild(xe19);
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
                xe1.AppendChild(xe121);
                xe1.AppendChild(zbzl);
                xe1.AppendChild(clzbzl);
                xe1.AppendChild(zbzlpd);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("SendWKResult_Send:   18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.writeObjectOut(xtlb, jkxlh, "18C81", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                if (XmlOperation.ReadACKString(receiveXml, out code, out message) && code == "1")
                    return true;
                else
                {
                    IOControl.saveXmlLogInf("发送外廓检测结果失败:\r\n" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("SendWKResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool SendZBZLResult(string jylsh, string jycs, string jyxm, string zbzl, string zhpd)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      

                XmlElement xe11 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe11.InnerText = station_id;
                XmlElement xe12 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe12.InnerText = line_id;
                XmlElement xe13 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe13.InnerText = jylsh;
                XmlElement xe14 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe14.InnerText = jycs;
                XmlElement xe15 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe15.InnerText = jyxm;
                XmlElement xe16 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe16.InnerText = zbzl;
                XmlElement xe17 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                xe17.InnerText = zhpd;
                XmlElement xe18 = xmldoc.CreateElement("sqip");//创建一个<Node>节点 
                xe18.InnerText = local_ip;

                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                xe1.AppendChild(xe18);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("SendZBZLResult_Send:   18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.writeObjectOut(xtlb, jkxlh, "18C81", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                if (XmlOperation.ReadACKString(receiveXml, out code, out message) && code == "1")
                    return true;
                else
                {
                    IOControl.saveXmlLogInf("发送整备质量检测结果失败:\r\n" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("SendZBZLResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool SendPhoto(string jylsh, string jycs, string jyxm, string hphm, string hpzl, string vin, DateTime pssj, string zpzl, string zp_path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点      

                XmlElement xe11 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe11.InnerText = station_id;
                XmlElement xe12 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe12.InnerText = line_id;
                XmlElement xe13 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe13.InnerText = jylsh;
                XmlElement xe14 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe14.InnerText = jycs;
                XmlElement xe15 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe15.InnerText = jyxm;
                XmlElement xe16 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe16.InnerText = XmlOperation.encodeUTF8(hphm);
                XmlElement xe17 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe17.InnerText = hpzl;
                XmlElement xe18 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe18.InnerText = vin;
                XmlElement xe19 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe19.InnerText = pssj.ToString("yyyy-MM-dd HH:mm:ss");
                XmlElement xe110 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe110.InnerText = zpzl;
                XmlElement xe111 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                xe111.InnerText = XmlOperation.encodeUTF8(XmlOperation.PushTxt(zp_path));
                XmlElement xe112 = xmldoc.CreateElement("sqip");//创建一个<Node>节点 
                xe112.InnerText = local_ip;

                xe1.AppendChild(xe11);
                xe1.AppendChild(xe12);
                xe1.AppendChild(xe13);
                xe1.AppendChild(xe14);
                xe1.AppendChild(xe15);
                xe1.AppendChild(xe16);
                xe1.AppendChild(xe17);
                xe1.AppendChild(xe18);
                xe1.AppendChild(xe19);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(xe112);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("SendPhoto_Send:   18C63 (照片种类（" + zpzl + "）\r\n" + XmlOperation.removePictureInfo(send_xml, new string[1] { @"<zp>" }, new string[1] { @"</zp>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(upload_interface.writeObjectOut(xtlb, jkxlh, "18C63", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                if (XmlOperation.ReadACKString(receiveXml, out code, out message) && code == "1")
                    return true;
                else
                {
                    IOControl.saveXmlLogInf("发送照片（" + zpzl + "）失败:\r\n" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("SendPhoto_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }
}
