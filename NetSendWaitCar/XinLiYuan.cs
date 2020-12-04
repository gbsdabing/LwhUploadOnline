using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;



namespace NetSendWaitCar
{
    public class XinLiYuan
    {
        private XLYTmriOutAccess outlineservice = null;
        private string Xtlb = "";
        private string Jkxlh = "";
        public XinLiYuan(string url, string xtlb, string jkxlh)
        {
            Xtlb = xtlb;
            Jkxlh = jkxlh;
            outlineservice = new XLYTmriOutAccess(url);
        }

        /// <summary>
        /// 18C50 同步系统时间
        /// </summary>
        /// <param name="babh">备案编号</param>
        /// <param name="sys_time">系统时间</param>
        /// <returns>是否成功</returns>
        public bool GetSystemDatetime(string babh, ref DateTime sys_time)
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
                IOControl.saveXmlLogInf("GetSystemDatetime_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18C50", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                DataTable dt = XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
                if (dt.Rows.Count > 0)
                {
                    sys_time = DateTime.Parse(dt.Rows[0]["sj"].ToString());
                    return true;
                }
                else
                {
                    IOControl.saveXmlLogInf("GetSystemDatetime_Send_Failed: \r\n" + message + "\r\n");
                    return false;
                }
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetSystemDatetime_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 获取待检车辆信息
        /// </summary>
        /// <param name="hphm">号牌号码</param>
        /// <param name="hpzl">号牌种类</param>
        /// <param name="clsbdh">车辆识别代号</param>
        /// <param name="jyjgbh">检验机构编号</param>
        /// <param name="code">查询结果代码</param>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
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
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C49\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18C49", send_xml));
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
        /// 项目开始18C55
        /// </summary>
        /// <param name="model">项目开始信息</param>
        /// <param name="code">查询结果代码</param>
        /// <param name="message">错误信息</param>
        public bool writeProjectStart(XlyprojectStart model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectStart_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C55", send_xml));
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
        /// 项目结束18C58
        /// </summary>
        /// <param name="model">项目结束信息</param>
        /// <param name="code">查询结果代码</param>
        /// <param name="message">错误信息</param>
        public bool writeProjectFinish(XlyprojectFinish model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectFinish_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C58", send_xml));
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
        /// 外廓检测结果18C81_M1
        /// </summary>
        /// <param name="model">结果信息</param>
        /// <param name="code">写入返回代码</param>
        /// <param name="message">错误信息</param>
        public bool writetestDetailResult(XlytestDetailResult model, out string code, out string message)
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
                XmlElement xe107 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe107.InnerText = model.cwkk;
                XmlElement xe108 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe108.InnerText = model.cwkg;
                XmlElement xe109 = xmldoc.CreateElement("clwkccpd");//创建一个<Node>节点 
                xe109.InnerText = model.clwkccpd;
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
                IOControl.saveXmlLogInf("writetestDetailResult_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C81", send_xml));
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

        /// <summary>
        /// 外廓检测结果18C81_Z1
        /// </summary>
        /// <param name="model">结果信息</param>
        /// <param name="code">写入返回代码</param>
        /// <param name="message">错误信息</param>
        public bool writeZbzlTestDetailResult(XlyZbzlDetailResult model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeZbzlTestDetailResult_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C81", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeZbzlTestDetailResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 触发拍照18S04
        /// </summary>
        /// <param name="model">拍照信息</param>
        /// <param name="code">写入返回代码</param>
        /// <param name="message">错误信息</param>
        public bool writeCapturePicture(XlycapturePicture model, out string code, out string message)
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
                XmlElement xe109 = xmldoc.CreateElement("pzcfsj");//创建一个<Node>节点 
                xe109.InnerText = model.pzcfsj;
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
                xe1.AppendChild(xe109);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeCapturePicture_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18S04\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18S04", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeCapturePicture_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        public bool writeTestImage(string jylsh, string jyjgbh, string jcxdh, string jycs, string hphm, string hpzl, string clsbdh, string filePath, string pssj, string jyxm, string zpzl, out string code, out string message)
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
                XmlElement xe102 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点 
                xe102.InnerText = jylsh;
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = jylsh;
                XmlElement xe104 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe104.InnerText = jylsh;
                XmlElement xe105 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe105.InnerText = XmlOperation.encodeUTF8(hphm);
                XmlElement xe106 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                xe106.InnerText = hpzl;
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe108.InnerText = jylsh;
                XmlElement xe109 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe109.InnerText = jylsh;
                XmlElement xe110 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe110.InnerText = jylsh;
                XmlElement xe111 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                xe111.InnerText = XmlOperation.PushTxt(filePath);
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

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C63\r\n" + XmlOperation.removePictureInfo(send_xml, new string[1] { @"<zp>" }, new string[1] { @"</zp>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C63", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleInf_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }

    /// <summary>
    /// 项目开始18C55
    /// </summary>
    public class XlyprojectStart
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

        public XlyprojectStart(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj)
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

    /// <summary>
    /// 项目结束18C58
    /// </summary>
    public class XlyprojectFinish
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string jyxm;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jssj;
        public XlyprojectFinish(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj)
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

    /// <summary>
    /// 触发拍照18S04
    /// </summary>
    public class XlycapturePicture
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
        public XlycapturePicture(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pzcfsj, string jyxm, string zpzl)
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

    /// <summary>
    /// 外廓尺寸检测结果18C81_M1
    /// </summary>
    public class XlytestDetailResult
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string clwkccpd;
        public XlytestDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string cwkc, string cwkk, string cwkg, string clwkccpd)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.clwkccpd = clwkccpd;
        }
    }

    /// <summary>
    /// 整备质量检测结果18C81_Z1
    /// </summary>
    public class XlyZbzlDetailResult
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
        public string zbzl;
        public string zbzlpd;
        public XlyZbzlDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string zbzl, string zbzlpd)
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
            this.zbzl = zbzl;
            this.zbzlpd = zbzlpd;
        }
    }
}
