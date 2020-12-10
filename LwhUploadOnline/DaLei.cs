using System;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;


namespace LwhUploadOnline
{
    public class DaLei
    {
        DLOutlineService outlineservice = null;
        public DaLei(string url)
        {
            outlineservice = new DLOutlineService(url);
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
                IOControl.saveXmlLogInf("GetSystemDatetime_Send: 18C50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut("18C50", send_xml), Encoding.UTF8);
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
                XmlElement xe2 = xmldoc.CreateElement("hphm");//创建一个<Node>节点           
                XmlElement xe3 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                XmlElement xe4 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点  
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleList_Send: 18R03\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut("18R03", send_xml), Encoding.UTF8);
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
        /// 获取待检车辆信息
        /// </summary>
        /// <param name="hphm"></param>
        /// <param name="hpzl"></param>
        /// <param name="jylsh"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public DataTable GetVehicleInf(string hphm, string hpzl, string jylsh, out string code, out string message)
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
                XmlElement xe4 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe4.InnerText = jylsh;
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send: 18R03\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut("18R03", send_xml), Encoding.UTF8);
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
        /// 发送检测结果(18W02)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeTestResult(TestRecordModel model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                #region xml
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("vehispara");//创建一个<Node>节点
                XmlElement xe101 = xmldoc.CreateElement("jylsh");
                xe101.InnerText = model.LSH;
                XmlElement xe102 = xmldoc.CreateElement("hphm");
                xe102.InnerText = model.CLHP;
                XmlElement xe103 = xmldoc.CreateElement("hpzl");
                if (model.HPZL.Contains("("))
                    xe103.InnerText = model.HPZL.Split('(')[1].Split(')')[0];
                else
                    xe103.InnerText = model.HPZL;
                XmlElement xe104 = xmldoc.CreateElement("clsbdh");
                xe104.InnerText = model.VIN;
                XmlElement xe105 = xmldoc.CreateElement("cwkc");
                xe105.InnerText = model.LENGTHBZZ.ToString();
                XmlElement xe106 = xmldoc.CreateElement("cwkk");
                xe106.InnerText = model.WIDTHBZZ.ToString();
                XmlElement xe107 = xmldoc.CreateElement("cwkg");
                xe107.InnerText = model.HEIGHTBZZ.ToString();
                XmlElement xe108 = xmldoc.CreateElement("clwkc");
                xe108.InnerText = model.LENGTHCLZ.ToString();
                XmlElement xe109 = xmldoc.CreateElement("clwkk");
                xe109.InnerText = model.WIDTHCLZ.ToString();
                XmlElement xe110 = xmldoc.CreateElement("clwkg");
                xe110.InnerText = model.HEIGHTCLZ.ToString();
                XmlElement xe111 = xmldoc.CreateElement("zlbgd");
                xe111.InnerText = model.LBHEIGHTCLZ == 0 ? "" : model.LBHEIGHTCLZ.ToString();
                XmlElement xe112 = xmldoc.CreateElement("ylbgd");
                xe112.InnerText = model.LBHEIGHTCLZ == 0 ? "" : model.LBHEIGHTCLZ.ToString();
                XmlElement xe113 = xmldoc.CreateElement("hxcd");
                xe113.InnerText = "";
                XmlElement xe114 = xmldoc.CreateElement("hxkd");
                xe114.InnerText = "";
                XmlElement xe115 = xmldoc.CreateElement("hxgd");
                xe115.InnerText = "";
                XmlElement xe116 = xmldoc.CreateElement("zbzl");
                xe116.InnerText = model.SCZBZL == 0 ? "" : model.SCZBZL.ToString();
                XmlElement xe117 = xmldoc.CreateElement("zj1");
                xe117.InnerText = model.ZJ1CLZ == 0 ? "" : model.ZJ1CLZ.ToString();
                XmlElement xe118 = xmldoc.CreateElement("zj2");
                xe118.InnerText = model.ZJ2CLZ == 0 ? "" : model.ZJ2CLZ.ToString();
                XmlElement xe119 = xmldoc.CreateElement("zj3");
                xe119.InnerText = model.ZJ3CLZ == 0 ? "" : model.ZJ3CLZ.ToString();
                XmlElement xe120 = xmldoc.CreateElement("zj4");
                xe120.InnerText = model.ZJ4CLZ == 0 ? "" : model.ZJ4CLZ.ToString();
                XmlElement xe121 = xmldoc.CreateElement("zjpd");
                if (model.ZJBJ)
                    xe121.InnerText = model.ZZJPD == "不合格" ? "2" : "1";
                else
                    xe121.InnerText = "0";
                XmlElement xe122 = xmldoc.CreateElement("lbgdpd");
                if (model.LBHEIGHTBJ)
                    xe122.InnerText = model.LBHEIGHTPD == "不合格" ? "2" : "1";
                else
                    xe122.InnerText = "0";

                XmlElement xe123 = xmldoc.CreateElement("zbzlpd");
                XmlElement xe133 = xmldoc.CreateElement("zbzlabswcpd");
                XmlElement xe134 = xmldoc.CreateElement("zbzlrelwcpd");
                if (model.ZBZLBJ)
                {
                    xe123.InnerText = model.ZBZLPD == "不合格" ? "2" : "1";

                    if (model.BY2 != "" && model.BY2.Contains("|"))
                    {
                        string[] temp = model.BY2.Split('|');
                        if (temp[0] != "")
                            xe133.InnerText = temp[0] == "不合格" ? "2" : "1";
                        else
                            xe133.InnerText = "0";

                        if (temp[1] != "")
                            xe134.InnerText = temp[1] == "不合格" ? "2" : "1";
                        else
                            xe134.InnerText = "0";
                    }
                    else
                    {
                        xe133.InnerText = "0";
                        xe134.InnerText = "0";
                    }
                }
                else
                {
                    xe123.InnerText = "0";

                    xe133.InnerText = "0";
                    xe134.InnerText = "0";
                }

                XmlElement xe124 = xmldoc.CreateElement("clwkccpd");
                XmlElement xe130 = xmldoc.CreateElement("cwkcpd");
                XmlElement xe131 = xmldoc.CreateElement("cwkkpd");
                XmlElement xe132 = xmldoc.CreateElement("cwkgpd");
                if (model.LWHBJ)
                {
                    xe124.InnerText = (model.LENGTHPD == "不合格" || model.WIDTHPD == "不合格" || model.HEIGHTPD == "不合格") ? "2" : "1";

                    xe130.InnerText = model.LENGTHPD == "不合格" ? "2" : "1";
                    xe131.InnerText = model.WIDTHPD == "不合格" ? "2" : "1";
                    xe132.InnerText = model.HEIGHTPD == "不合格" ? "2" : "1";
                }
                else
                {
                    xe124.InnerText = "0";

                    xe130.InnerText = "0";
                    xe131.InnerText = "0";
                    xe132.InnerText = "0";
                }
                XmlElement xe125 = xmldoc.CreateElement("clsj");
                xe125.InnerText = model.JCSJ.ToString("yyyy-MM-dd HH:mm:ss");

                XmlElement xe126 = xmldoc.CreateElement("cwkcwc");
                xe126.InnerText = model.LENGTHWC;
                XmlElement xe127 = xmldoc.CreateElement("cwkkwc");
                xe127.InnerText = model.WIDTHWC;
                XmlElement xe128 = xmldoc.CreateElement("cwkgwc");
                xe128.InnerText = model.HEIGHTWC;
                XmlElement xe129 = xmldoc.CreateElement("zbzlwc");
                xe129.InnerText = model.ZBZLWC;

                XmlElement xe135 = xmldoc.CreateElement("cwkccfw");
                xe135.InnerText = model.BY1;
                XmlElement xe136 = xmldoc.CreateElement("cwkxz");
                xe136.InnerText = model.LENGTHXZ;
                XmlElement xe137 = xmldoc.CreateElement("zbzlxz");
                xe137.InnerText = model.ZBZLXZ;

                #region MyRegion
                XmlElement xe138 = xmldoc.CreateElement("yzzlz");//创建一个<Node>节点 
                xe138.InnerText = "";
                XmlElement xe139 = xmldoc.CreateElement("yzylz");//创建一个<Node>节点 
                xe139.InnerText = "";
                XmlElement xe140 = xmldoc.CreateElement("yzlz");//创建一个<Node>节点 
                xe140.InnerText = "";
                XmlElement xe141 = xmldoc.CreateElement("ezzlz");//创建一个<Node>节点 
                xe141.InnerText = "";
                XmlElement xe142 = xmldoc.CreateElement("ezylz");//创建一个<Node>节点 
                xe142.InnerText = "";
                XmlElement xe143 = xmldoc.CreateElement("ezlz");//创建一个<Node>节点 
                xe143.InnerText = "";
                XmlElement xe144 = xmldoc.CreateElement("sanzzlz");//创建一个<Node>节点 
                xe144.InnerText = "";
                XmlElement xe145 = xmldoc.CreateElement("sanzylz");//创建一个<Node>节点 
                xe145.InnerText = "";
                XmlElement xe146 = xmldoc.CreateElement("sanzlz");//创建一个<Node>节点 
                xe146.InnerText = "";
                XmlElement xe147 = xmldoc.CreateElement("sizzlz");//创建一个<Node>节点 
                xe147.InnerText = "";
                XmlElement xe148 = xmldoc.CreateElement("sizylz");//创建一个<Node>节点 
                xe148.InnerText = "";
                XmlElement xe149 = xmldoc.CreateElement("sizlz");//创建一个<Node>节点 
                xe149.InnerText = "";
                XmlElement xe150 = xmldoc.CreateElement("wuzzlz");//创建一个<Node>节点 
                xe150.InnerText = "";
                XmlElement xe151 = xmldoc.CreateElement("wuzylz");//创建一个<Node>节点 
                xe151.InnerText = "";
                XmlElement xe152 = xmldoc.CreateElement("wuzlz");//创建一个<Node>节点 
                xe152.InnerText = "";
                XmlElement xe153 = xmldoc.CreateElement("lzzlz");//创建一个<Node>节点 
                xe153.InnerText = "";
                XmlElement xe154 = xmldoc.CreateElement("lzylz");//创建一个<Node>节点 
                xe154.InnerText = "";
                XmlElement xe155 = xmldoc.CreateElement("lzlz");//创建一个<Node>节点 
                xe155.InnerText = "";
                #endregion

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
                xe1.AppendChild(xe121);
                xe1.AppendChild(xe122);
                xe1.AppendChild(xe123);
                xe1.AppendChild(xe124);
                xe1.AppendChild(xe125);
                xe1.AppendChild(xe126);
                xe1.AppendChild(xe127);
                xe1.AppendChild(xe128);
                xe1.AppendChild(xe129);
                xe1.AppendChild(xe130);
                xe1.AppendChild(xe131);
                xe1.AppendChild(xe132);
                xe1.AppendChild(xe133);
                xe1.AppendChild(xe134);
                xe1.AppendChild(xe135);
                xe1.AppendChild(xe136);
                xe1.AppendChild(xe137);
                xe1.AppendChild(xe138);
                xe1.AppendChild(xe139);
                xe1.AppendChild(xe140);
                xe1.AppendChild(xe141);
                xe1.AppendChild(xe142);
                xe1.AppendChild(xe143);
                xe1.AppendChild(xe144);
                xe1.AppendChild(xe145);
                xe1.AppendChild(xe146);
                xe1.AppendChild(xe147);
                xe1.AppendChild(xe148);
                xe1.AppendChild(xe149);
                xe1.AppendChild(xe150);
                xe1.AppendChild(xe151);
                xe1.AppendChild(xe152);
                xe1.AppendChild(xe153);
                xe1.AppendChild(xe154);
                xe1.AppendChild(xe155);

                root.AppendChild(xe1);
                #endregion

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeTestResult_Send: 18W02\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18W02", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeTestResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发送项目开始命令
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectStart(dalei18C55 model, out string code, out string message)
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
                if (model.hpzl.Contains("("))
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
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
                IOControl.saveXmlLogInf("writeProjectStart_Send: 18C55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18C55", send_xml), Encoding.UTF8);
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
        /// 发18J11
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18J11(dalei18J11 model, out string code, out string message)
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
                    xe103.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
                    xe103.InnerText = model.hpzl;
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
                IOControl.saveXmlLogInf("write18J11_Send: 18J11\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18J11", send_xml), Encoding.UTF8);
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
        /// 发18J12
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18J12(dalei18J12 model, out string code, out string message)
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
                    xe103.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
                    xe103.InnerText = model.hpzl;
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
                IOControl.saveXmlLogInf("write18J12_Send: 18J12\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18J12", send_xml), Encoding.UTF8);
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
        /// 发18C63
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18C63(dalei18C63 model, out string code, out string message)
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
                    xe106.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
                    xe106.InnerText = model.hpzl;

                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                if (model.zp != "")
                    xe108.InnerText = XmlOperation.PushTxt(model.zp);
                else
                    xe108.InnerText = "";

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
                IOControl.saveXmlLogInf("write18C63_Send: 18C63\r\n" + XmlOperation.removePictureInfo(send_xml, new string[1] { @"<zp>" }, new string[1] { @"</zp>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18C63", send_xml), Encoding.UTF8);
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
        /// 发项目结束
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectFinish(dalei18C58 model, out string code, out string message)
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
                if (model.hpzl.Contains("("))
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
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
                IOControl.saveXmlLogInf("writeProjectFinish_Send: 18C58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18C58", send_xml), Encoding.UTF8);
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
        /// 发检测详细结果（江西）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool writetestDetailResult(daleitestDetailResult model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writetestDetailResultJx_Send: 18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18C81", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writetestDetailResultJx_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发整备质量检测结果
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writezbzlDetailResult(daleizbzlDetailResult model, out string code, out string message)
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
                XmlElement xe106 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe106.InnerText = model.zbzl;
                XmlElement xe107 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                xe107.InnerText = model.zbzlpd;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writezbzlDetailResult_Send: 18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18C81", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writezbzlDetailResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeFwztestDetailResult(daleifwzTestDetailResult model, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                #region xml
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("checkresult");//创建一个<Node>节点   
                XmlElement xe101 = xmldoc.CreateElement("jczdm");//创建一个<Node>节点 
                xe101.InnerText = model.jczdm;
                XmlElement xe102 = xmldoc.CreateElement("jclsh");//创建一个<Node>节点 
                xe102.InnerText = model.jylsh;
                XmlElement xe103 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                    xe103.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
                    xe103.InnerText = model.hpzl;
                XmlElement xe104 = xmldoc.CreateElement("cllx");//创建一个<Node>节点 
                xe104.InnerText = model.cllx.Split('_')[0];
                XmlElement xe105 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe105.InnerText = model.hphm;
                XmlElement xe106 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe106.InnerText = model.clsbdh;
                XmlElement xe107 = xmldoc.CreateElement("cpxh");//创建一个<Node>节点 
                xe107.InnerText = model.cpxh;
                XmlElement xe108 = xmldoc.CreateElement("csys");//创建一个<Node>节点 
                xe108.InnerText = model.csys;
                XmlElement xe109 = xmldoc.CreateElement("cwkc");//创建一个<Node>节点 
                xe109.InnerText = model.cwkc;
                XmlElement xe110 = xmldoc.CreateElement("cwkk");//创建一个<Node>节点 
                xe110.InnerText = model.cwkk;
                XmlElement xe111 = xmldoc.CreateElement("cwkg");//创建一个<Node>节点 
                xe111.InnerText = model.cwkg;
                XmlElement xe112 = xmldoc.CreateElement("clcwkc");//创建一个<Node>节点 
                xe112.InnerText = model.clcwkc;
                XmlElement xe113 = xmldoc.CreateElement("clcwkk");//创建一个<Node>节点 
                xe113.InnerText = model.clcwkk;
                XmlElement xe114 = xmldoc.CreateElement("clcwkg");//创建一个<Node>节点 
                xe114.InnerText = model.clcwkg;
                XmlElement xe115 = xmldoc.CreateElement("ccpd");//创建一个<Node>节点 
                xe115.InnerText = model.ccpd;
                XmlElement xe116 = xmldoc.CreateElement("ckpd");//创建一个<Node>节点 
                xe116.InnerText = model.ckpd;
                XmlElement xe117 = xmldoc.CreateElement("cgpd");//创建一个<Node>节点 
                xe117.InnerText = model.cgpd;
                XmlElement xe118 = xmldoc.CreateElement("wkccpd");//创建一个<Node>节点 
                xe118.InnerText = model.wkccpd;
                XmlElement xe119 = xmldoc.CreateElement("zp1");//创建一个<Node>节点 
                xe119.InnerText = XmlOperation.PushTxt(model.zp1);
                XmlElement xe120 = xmldoc.CreateElement("zp2");//创建一个<Node>节点 
                xe120.InnerText = XmlOperation.PushTxt(model.zp2);
                XmlElement xe121 = xmldoc.CreateElement("zp3");//创建一个<Node>节点 
                if (model.zp3 != "")
                    xe121.InnerText = XmlOperation.PushTxt(model.zp3);
                else
                    xe121.InnerText = "";
                XmlElement xe122 = xmldoc.CreateElement("zbzl");//创建一个<Node>节点 
                xe122.InnerText = model.zbzl;
                XmlElement xe123 = xmldoc.CreateElement("clzbzl");//创建一个<Node>节点 
                xe123.InnerText = model.clzbzl;
                XmlElement xe124 = xmldoc.CreateElement("zbzlpd");//创建一个<Node>节点 
                xe124.InnerText = model.zbzlpd;
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
                xe1.AppendChild(xe121);
                xe1.AppendChild(xe122);
                xe1.AppendChild(xe123);
                xe1.AppendChild(xe124);
                root.AppendChild(xe1);
                #endregion

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeFwztestDetailResult_Send: 08J51\r\n" + XmlOperation.removePictureInfo(send_xml, new string[3] { @"<zp1>", @"<zp2>", @"<zp3>" }, new string[3] { @"</zp1>", @"</zp2>", @"</zp3>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("08J51", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                ReadFwzACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeFwztestDetailResult_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 发整18H05照片
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool write18H05(dalei18H05 model, out string code, out string message)
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
                if (model.hpzl.Contains("("))
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
                    xe105.InnerText = model.hpzl;

                XmlElement xe106 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
                xe106.InnerText = XmlOperation.encodeUTF8(model.hphm);
                XmlElement xe107 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
                xe107.InnerText = model.clsbdh;
                XmlElement xe108 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                xe108.InnerText = model.zp;
                XmlElement xe109 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe109.InnerText = model.pssj;
                XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe110.InnerText = model.jyxm;
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
                XmlElement xe112 = xmldoc.CreateElement("isFrontLeft");//创建一个<Node>节点 
                xe112.InnerText = model.zpzl;
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
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeZbzlCapturePicture_Send: 18H05\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18H05", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("writeZbzlCapturePicture_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        /// <summary>
        /// 新盾联网协议里触发拍照指令
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public bool write18J31(dalei18J31 model, out string code, out string message)
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
                XmlElement xe103 = xmldoc.CreateElement("jcxdh");//创建一个<Node>节点 
                xe103.InnerText = model.jcxdh;
                XmlElement xe105 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (model.hpzl.Contains("("))
                    xe105.InnerText = model.hpzl.Split('(')[1].Split(')')[0];
                else
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
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
                XmlElement xe112 = xmldoc.CreateElement("lx");//创建一个<Node>节点 
                xe112.InnerText = model.lx;
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(xe108);
                xe1.AppendChild(xe109);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(xe112);
                root.AppendChild(xe1);

                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("write18J31_Send: 18J31\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18J31", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("write18J31_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
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
                xe102.InnerText = hphm;
                XmlElement xe103 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
                if (hpzl.Contains("("))
                    xe103.InnerText = hpzl.Split('(')[1].Split(')')[0];
                else
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
                IOControl.saveXmlLogInf("_Send: 18W03\r\n" + XmlOperation.removePictureInfo(send_xml, new string[1] { @"<zp>" }, new string[1] { @"</zp>" }) + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("18W03", send_xml), Encoding.UTF8);
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                XmlOperation.ReadACKString(receiveXml, out code, out message);

                return true;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("_Send_Error: \r\n" + er.Message + "\r\n");
                return false;
            }
        }

        #region 解析xml数据        
        private void ReadFwzACKString(string xmlstring, out string result, out string info)
        {
            try
            {
                DataSet ds = new DataSet();
                StringReader stream = new StringReader(xmlstring);
                XmlTextReader reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                DataTable dt1 = ds.Tables["head"];
                result = dt1.Rows[0]["code"].ToString();
                info = dt1.Rows[0]["message"].ToString();
            }
            catch
            {
                result = "1";
                info = "";
            }
        }
        #endregion
    }

    public class dalei18J31
    {
        public string jylsh;
        public string jcxdh;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string gwjysbbh;
        public string jyxm;
        public string kssj;
        public string zpzl;
        public string lx;
        public dalei18J31(string jylsh, string jcxdh, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj, string zpzl, string lx)
        {
            this.jylsh = jylsh;
            this.jcxdh = jcxdh;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.gwjysbbh = gwjysbbh;
            this.jyxm = jyxm;
            this.kssj = kssj;
            this.zpzl = zpzl;
            this.lx = lx;
        }
    }

    public class dalei18C81
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

    }

    public class dalei18C81Z1
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string zbzl;
        public string zbzlpd;

    }

    public class dalei18J11
    {
        public string jylsh;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string gwxm;
        public string jcxdh;
        public string lx;
        public dalei18J11(string jylsh, string hphm, string hpzl, string clsbdh, string gwxm, string jcxdh, string lx)
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

    public class dalei18J12
    {
        public string jylsh;
        public string hphm;
        public string hpzl;
        public string clsbdh;
        public string gwxm;
        public string jcxdh;
        public string lx;
        public dalei18J12(string jylsh, string hphm, string hpzl, string clsbdh, string gwxm, string jcxdh, string lx)
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

    public class dalei18C63
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
        public dalei18C63(string jylsh, string jyjgbh, string jcxdh, string jycs, string hphm, string hpzl, string clsbdh, string zp, string pssj, string jyxm, string zpzl)
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

    public class dalei18C55
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
        public dalei18C55(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj)
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
        }
    }

    public class dalei18C58
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
        public dalei18C58(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj)
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
        }
    }

    public class dalei18H05
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jycs;
        public string hpzl;
        public string hphm;
        public string clsbdh;
        public string zp;
        public string pssj;
        public string jyxm;
        public string zpzl;
        public dalei18H05(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string zp, string pssj, string jyxm, string zpzl)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jycs = jycs;
            this.hpzl = hpzl;
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.zp = zp;
            this.pssj = pssj;
            this.jyxm = jyxm;
            this.zpzl = zpzl;
        }
    }
    
    public class daleitestDetailResult
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
        public daleitestDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs,
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

    public class daleizbzlDetailResult
    {
        public string jylsh;
        public string jyjgbh;
        public string jcxdh;
        public string jyxm;
        public string jycs;
        public string zbzl;
        public string zbzlpd;
        public daleizbzlDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string zbzl, string zbzlpd)
        {
            this.jylsh = jylsh;
            this.jyjgbh = jyjgbh;
            this.jcxdh = jcxdh;
            this.jyxm = jyxm;
            this.jycs = jycs;
            this.zbzl = zbzl;
            this.zbzlpd = zbzlpd;
        }
    }

    public class daleifwzTestDetailResult
    {
        public string jczdm;
        public string jylsh;
        public string hpzl;
        public string cllx;
        public string hphm;
        public string clsbdh;
        public string cpxh;
        public string csys;
        public string cwkc;
        public string cwkk;
        public string cwkg;
        public string clcwkc;
        public string clcwkk;
        public string clcwkg;
        public string ccpd;
        public string ckpd;
        public string cgpd;
        public string wkccpd;
        public string zp1;
        public string zp2;
        public string zp3;
        public string clzbzl;
        public string zbzl;
        public string zbzlpd;
        public daleifwzTestDetailResult(string jczdm, string jylsh, string hpzl, string cllx,
            string hphm, string clsbdh, string cpxh, string csys, string cwkc, string cwkk, string cwkg,
            string clcwkc, string clcwkk, string clcwkg, string ccpd, string ckpd, string cgpd,
            string wkccpd, string zp1, string zp2, string zp3, string clzbzl, string zbzl, string zbzlpd)
        {
            this.jczdm = jczdm;
            this.jylsh = jylsh;
            this.hpzl = hpzl;
            this.cllx = cllx;
            this.hphm = hphm;
            this.clsbdh = clsbdh;
            this.cpxh = cpxh;
            this.csys = csys;
            this.cwkc = cwkc;
            this.cwkk = cwkk;
            this.cwkg = cwkg;
            this.clcwkc = clcwkc;
            this.clcwkk = clcwkk;
            this.clcwkg = clcwkg;
            this.ccpd = ccpd;
            this.ckpd = ckpd;
            this.cgpd = cgpd;
            this.wkccpd = wkccpd;
            this.zp1 = zp1;
            this.zp2 = zp2;
            this.zp3 = zp3;
            this.zbzl = zbzl;
            this.zbzlpd = zbzlpd;
            this.clzbzl = clzbzl;
        }
    }
}
