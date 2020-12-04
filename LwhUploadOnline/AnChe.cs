using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;
using System.Collections;

namespace LwhUploadOnline
{
    public class AnChe
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        private string Cjbh = "";
        private string Dwjgdm = "";
        private string Dwmc = "";
        private string Yhbz = "";
        private string Yhxm = "";
        private string Zdbs = "";
        private string Jyjgbh = "";

        private ACTmriJaxRpcOutAccessService outlineservice = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jyjgbh">检验机构编号</param>
        /// <param name="url">接口地址</param>
        /// <param name="xtlb">系统类别</param>
        /// <param name="jkxlh">接口序列号</param>
        /// <param name="cjbh">场景编号</param>
        /// <param name="dwjgdm">单位机构代码</param>
        /// <param name="dwmc">单位名称</param>
        /// <param name="yhbz">用户标识</param>
        /// <param name="yhxm">用户姓名</param>
        /// <param name="zdbs">终端标识</param>
        public AnChe(string jyjgbh, string url, string xtlb, string jkxlh, string cjbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs)
        {
            try
            {
                Jyjgbh = jyjgbh;
                Xtlb = xtlb;
                Jkxlh = jkxlh;
                Cjbh = cjbh;
                Dwjgdm = dwjgdm;
                Dwmc = dwmc;
                Yhbz = yhbz;
                Yhxm = yhxm;
                Zdbs = zdbs;
                outlineservice = new ACTmriJaxRpcOutAccessService(url);

                #region 初始化号牌种类名称代码对应关系
                
                #endregion
            }
            catch { }
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
                IOControl.saveXmlLogInf("GetSystemDatetime_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18C50", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, msg;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out msg);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetSystemDatetime_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
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
        /// <returns></returns>
        public DataTable GetVehicleInf(string hphm, string hpzl, string clsbdh, out string code, out string message)
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
                xe5.InnerText = Jyjgbh;
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                xe1.AppendChild(xe5);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18S02\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18S02", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
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
        /// 发送项目开始
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeProjectStart(AcprojectStart model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectStart_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C55", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
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
        /// 发送项目结束
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectFinish(AcprojectFinish model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeProjectFinish_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C58", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
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
        /// 发送外廓尺寸结果数据
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
                IOControl.saveXmlLogInf("writetestDetailResult_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C81", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
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
        /// 发送整备质量结果数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeZbzlTestDetailResult(acZbzlDetailResult model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeZbzlTestDetailResult_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18C81", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
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
        /// 发送照片
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool writeOutlineCapturePicture(AccapturePicture model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeOutlineCapturePicture_Send: XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18S04\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut(Xtlb, Jkxlh, "18S04", Cjbh, Dwjgdm, Dwmc, Yhbz, Yhxm, Zdbs, send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);
                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeOutlineCapturePicture_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
    }

    public class AcprojectStart
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

        public AcprojectStart(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj)
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
    public class AcprojectFinish
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
        public AcprojectFinish(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj)
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
    public class AccapturePicture
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
        public AccapturePicture(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pzcfsj, string jyxm, string zpzl)
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
    public class ActestDetailResult
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
        public ActestDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string cwkc,
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
    public class acZbzlDetailResult
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
        public acZbzlDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string zbzl,
            string zbzlpd)
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
