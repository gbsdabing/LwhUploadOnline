using System;
using System.Data;
using System.Web;
using System.Xml;



namespace LwhUploadOnline
{
    public class HuaYan
    {
        private string Xtlb = "";
        private string Jkxlh = "";
        private HYCarTestHandleService1 outlineservice = null;

        public HuaYan(string url, string xtlb, string jkxlh)
        {
            try
            {
                outlineservice = new HYCarTestHandleService1(url);
                Xtlb = xtlb;
                Jkxlh = jkxlh;
            }
            catch { }
        }

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
                IOControl.saveXmlLogInf("GetSystemDatetime_Send:   XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "JKID:18Q50\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18Q50", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetSystemDatetime_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        public DataTable GetVehicleList()
        {
            try
            {
                DataTable dt_m1 = new DataTable();
                DataTable dt_z1 = new DataTable();
                dt_m1 = GetVehicleList("M1");
                //dt_z1 = GetVehicleList("Z1");
                dt_z1 = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("jylsh");
                dt.Columns.Add("jycs");
                dt.Columns.Add("jcsx");
                dt.Columns.Add("cph");
                dt.Columns.Add("hpzlid");
                dt.Columns.Add("hpzl");
                dt.Columns.Add("jclb");
                dt.Columns.Add("jczt");
                dt.Columns.Add("regdate");
                dt.Columns.Add("Z1");
                dt.Columns.Add("M1");
                if (dt_m1 != null)
                {
                    if (dt_z1 != null)//有检整备质量的车
                    {
                        for (int i = 0; i < dt_m1.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            string jylsh = dt_m1.Rows[i]["jylsh"].ToString();
                            string jycs = dt_m1.Rows[i]["jycs"].ToString();
                            dr["jylsh"] = jylsh;// dt_m1.Rows[i]["jylsh"];
                            dr["jycs"] = jycs;// dt_m1.Rows[i]["jycs"];
                            dr["jcsx"] = dt_m1.Rows[i]["jcsx"];
                            dr["cph"] = dt_m1.Rows[i]["cph"];
                            dr["hpzlid"] = dt_m1.Rows[i]["hpzlid"];
                            dr["hpzl"] = dt_m1.Rows[i]["hpzl"];
                            dr["jclb"] = dt_m1.Rows[i]["jclb"];
                            dr["jczt"] = dt_m1.Rows[i]["jczt"];
                            dr["regdate"] = dt_m1.Rows[i]["regdate"];
                            dr["M1"] = "1";
                            dr["Z1"] = dt_z1.Select("jylsh ='" + jylsh + "' and jycs='" + jycs + "'").Length > 0 ? "1" : "0";
                            dt.Rows.Add(dr);
                        }
                        for (int i = 0; i < dt_z1.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            string jylsh = dt_z1.Rows[i]["jylsh"].ToString();
                            string jycs = dt_z1.Rows[i]["jycs"].ToString();
                            if (dt_m1.Select("jylsh ='" + jylsh + "' and jycs='" + jycs + "'").Length > 0)//如果外廓列表有则不用添加，在上一步遍历外廓时已经添加
                            {
                                continue;
                            }
                            else//单独添加整备质量
                            {
                                dr["jylsh"] = jylsh;// dt_m1.Rows[i]["jylsh"];
                                dr["jycs"] = jycs;// dt_m1.Rows[i]["jycs"];
                                dr["jcsx"] = dt_z1.Rows[i]["jcsx"];
                                dr["cph"] = dt_z1.Rows[i]["cph"];
                                dr["hpzlid"] = dt_z1.Rows[i]["hpzlid"];
                                dr["hpzl"] = dt_z1.Rows[i]["hpzl"];
                                dr["jclb"] = dt_z1.Rows[i]["jclb"];
                                dr["jczt"] = dt_z1.Rows[i]["jczt"];
                                dr["regdate"] = dt_z1.Rows[i]["regdate"];
                                dr["M1"] = "0";
                                dr["Z1"] = "1";
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    else//没有检整备质量的车
                    {
                        for (int i = 0; i < dt_m1.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            string jylsh = dt_m1.Rows[i]["jylsh"].ToString();
                            string jycs = dt_m1.Rows[i]["jycs"].ToString();
                            dr["jylsh"] = jylsh;// dt_m1.Rows[i]["jylsh"];
                            dr["jycs"] = jycs;// dt_m1.Rows[i]["jycs"];
                            dr["jcsx"] = dt_m1.Rows[i]["jcsx"];
                            dr["cph"] = dt_m1.Rows[i]["cph"];
                            dr["hpzlid"] = dt_m1.Rows[i]["hpzlid"];
                            dr["hpzl"] = dt_m1.Rows[i]["hpzl"];
                            dr["jclb"] = dt_m1.Rows[i]["jclb"];
                            dr["jczt"] = dt_m1.Rows[i]["jczt"];
                            dr["regdate"] = dt_m1.Rows[i]["regdate"];
                            dr["M1"] = "1";
                            dr["Z1"] = "0";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else if (dt_z1 != null)
                {
                    for (int i = 0; i < dt_z1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        string jylsh = dt_z1.Rows[i]["jylsh"].ToString();
                        string jycs = dt_z1.Rows[i]["jycs"].ToString();
                        dr["jylsh"] = jylsh;// dt_m1.Rows[i]["jylsh"];
                        dr["jycs"] = jycs;// dt_m1.Rows[i]["jycs"];
                        dr["jcsx"] = dt_z1.Rows[i]["jcsx"];
                        dr["cph"] = dt_z1.Rows[i]["cph"];
                        dr["hpzlid"] = dt_z1.Rows[i]["hpzlid"];
                        dr["hpzl"] = dt_z1.Rows[i]["hpzl"];
                        dr["jclb"] = dt_z1.Rows[i]["jclb"];
                        dr["jczt"] = dt_z1.Rows[i]["jczt"];
                        dr["regdate"] = dt_z1.Rows[i]["regdate"];
                        dr["M1"] = "0";
                        dr["Z1"] = "1";
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    dt = null;
                }
                return dt;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("解析待检列表有异常：" + er.Message);
                return null;
            }
        }

        public DataTable GetVehicleList(string jyxm)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlelem = xmldoc.CreateElement("", "root", "");
                xmldoc.AppendChild(xmlelem);
                XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
                XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
                XmlElement xe2 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe2.InnerText = jyxm;
                XmlElement xe3 = xmldoc.CreateElement("jczt");//创建一个<Node>节点 
                xe3.InnerText = "1";
                XmlElement xe4 = xmldoc.CreateElement("regdate");//创建一个<Node>节点 
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleList_Send:   XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "JKID:18Q18\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18Q18", send_xml));
                IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");

                string code, message;
                return XmlOperation.ReadXmlToDatatable(receiveXml, out code, out message);
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("GetVehicleList_Send_Error: \r\n" + er.Message + "\r\n");
                return null;
            }
        }

        public DataTable GetVehicleInf(string jylsh, string jccs, string jyxm,string pdyj, out string code, out string message)
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
                XmlElement xe2 = xmldoc.CreateElement("jylsh");//创建一个<Node>节点 
                xe2.InnerText = jylsh;
                XmlElement xe3 = xmldoc.CreateElement("jycs");//创建一个<Node>节点 
                xe3.InnerText = jccs;
                XmlElement xe4 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe4.InnerText = jyxm;
                XmlElement xe5 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe5.InnerText = pdyj;
                xe1.AppendChild(xe2);
                xe1.AppendChild(xe3);
                xe1.AppendChild(xe4);
                xe1.AppendChild(xe5);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("GetVehicleInf_Send:  XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "|JKID:18Q19\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.queryObjectOut(Xtlb, Jkxlh, "18Q19", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectStart(HyProjectStart model, out string code, out string message)
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
                XmlElement xe111 = xmldoc.CreateElement("sflx");//创建一个<Node>节点 
                xe111.InnerText = "1";
                XmlElement xe112 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe112.InnerText = model.pdyj;
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
                IOControl.saveXmlLogInf("writeProjectStart_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18C55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18C55", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeProjectFinish(HyProjectFinish model, out string code, out string message)
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
                XmlElement xe111 = xmldoc.CreateElement("sflx");//创建一个<Node>节点 
                xe111.InnerText = "1";
                XmlElement xe112 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe112.InnerText = model.pdyj;

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
                IOControl.saveXmlLogInf("writeProjectFinish_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18C58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18C58", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeVideoStart(HyVideoStart model, out string code, out string message)
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
                XmlElement xe111 = xmldoc.CreateElement("lxxm");//创建一个<Node>节点 
                xe111.InnerText = model.lxxm;
                XmlElement xe112 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe112.InnerText = "1";
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
                IOControl.saveXmlLogInf("writeVideoStart_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18W55\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18W55", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeVideoStop(HyVideoStop model, out string code, out string message)
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
                XmlElement xe111 = xmldoc.CreateElement("lxxm");//创建一个<Node>节点 
                xe111.InnerText = model.lxxm;
                XmlElement xe112 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe112.InnerText = "1";
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
                IOControl.saveXmlLogInf("writeVideoStart_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18W58\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18W58", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writetestDetailResult(HyTestDetailResult model, out string code, out string message)
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
                XmlElement cdevl = xmldoc.CreateElement("cdevl");//创建一个<Node>节点 
                cdevl.InnerText = model.cdevl;
                XmlElement kdevl = xmldoc.CreateElement("kdevl");//创建一个<Node>节点 
                kdevl.InnerText = model.kdevl;
                XmlElement gdevl = xmldoc.CreateElement("gdevl");//创建一个<Node>节点 
                gdevl.InnerText = model.gdevl;
                XmlElement xlbgd = xmldoc.CreateElement("xlbgd");//创建一个<Node>节点 
                xlbgd.InnerText = model.xlbgd;
                XmlElement xlbgdpd = xmldoc.CreateElement("xlbgdpd");//创建一个<Node>节点 
                xlbgdpd.InnerText = model.xlbgdpd;
                XmlElement pdyj = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                pdyj.InnerText = model.xlbgdpd;


                XmlElement xe115 = xmldoc.CreateElement("cwkcLimit");//创建一个<Node>节点 
                xe115.InnerText = model.cwkcLimit;
                XmlElement xe116 = xmldoc.CreateElement("cwkkLimit");//创建一个<Node>节点 
                xe116.InnerText = model.clwkccpd;
                XmlElement xe117 = xmldoc.CreateElement("cwkgLimit");//创建一个<Node>节点 
                xe117.InnerText = model.cwkkLimit;
                XmlElement xe118 = xmldoc.CreateElement("L_StdLimit1589");//创建一个<Node>节点 
                xe118.InnerText = model.L_StdLimit1589;
                XmlElement xe119 = xmldoc.CreateElement("W_StdLimit1589");//创建一个<Node>节点 
                xe119.InnerText = model.W_StdLimit1589;
                XmlElement xe120 = xmldoc.CreateElement("H_StdLimit1589");//创建一个<Node>节点 
                xe120.InnerText = model.H_StdLimit1589;
                XmlElement xe121 = xmldoc.CreateElement("sfxz");//创建一个<Node>节点 
                //xe121.InnerText = model.sfxz;
                XmlElement xe122 = xmldoc.CreateElement("scc");//创建一个<Node>节点 
                // xe122.InnerText = model.scc;
                XmlElement xe123 = xmldoc.CreateElement("sck");//创建一个<Node>节点 
                //xe123.InnerText = model.sck;
                XmlElement xe124 = xmldoc.CreateElement("scg");//创建一个<Node>节点 
                //xe124.InnerText = model.scg;
                XmlElement xe125 = xmldoc.CreateElement("scpj");//创建一个<Node>节点 
                //xe125.InnerText = model.scpj;
                XmlElement xe126 = xmldoc.CreateElement("ms");//创建一个<Node>节点 
                //xe126.InnerText = model.ms;

                string temp = model.Stdbfb;
                XmlElement xe127 = xmldoc.CreateElement("Stdbfb");//创建一个<Node>节点 
                XmlElement xe128 = xmldoc.CreateElement("Stdjdz");//创建一个<Node>节点 
                if (temp != "" || temp.Contains("或"))
                {
                    xe127.InnerText = temp.Replace("±", "").Split('或')[1].Replace("%", "");
                    xe128.InnerText = temp.Replace("±", "").Split('或')[0];
                }
                else
                {
                    xe127.InnerText = "2";
                    xe128.InnerText = "100";
                }
                
                XmlElement xe129 = xmldoc.CreateElement("Wkzj");//创建一个<Node>节点 
                XmlElement xe130 = xmldoc.CreateElement("Zjpj");//创建一个<Node>节点 
                XmlElement xe131 = xmldoc.CreateElement("Stdjdz_zj");//创建一个<Node>节点 
                XmlElement xe132 = xmldoc.CreateElement("Stdbfb_zj");//创建一个<Node>节点 
                XmlElement xe133 = xmldoc.CreateElement("cwzjLimit");//创建一个<Node>节点 
                if (model.Wkzj.Length > 0 && model.Wkzj != "0")
                {
                    xe129.InnerText = model.Wkzj;
                    xe130.InnerText = model.Zjpj == "不合格" ? "2" : "1";
                    if (model.Stdjdz_zj != "" && model.Stdjdz_zj.Contains("或"))
                    {
                        xe131.InnerText = model.Stdjdz_zj.Replace("±", "").Split('或')[0];
                        xe132.InnerText = model.Stdjdz_zj.Replace("±", "").Split('或')[1].Replace("%","");
                    }
                    else
                    {
                        xe131.InnerText = "100";
                        xe132.InnerText = "2";
                    }
                    if (model.cwzjLimit.Length > 0 && model.cwzjLimit != "0")
                    {
                        int zjbzz = 0, jdwc = 0, xdwc = 0;
                        zjbzz = int.Parse(model.cwzjLimit);
                        jdwc = int.Parse(xe131.InnerText);
                        xdwc = (int)(int.Parse(xe132.InnerText) * zjbzz * 0.02d);
                        if (jdwc > xdwc)
                            xe133.InnerText = (zjbzz - jdwc).ToString() + "-" + (zjbzz + jdwc).ToString();
                        else
                            xe133.InnerText = (zjbzz - xdwc).ToString() + "-" + (zjbzz + xdwc).ToString();
                    }
                    else
                        xe133.InnerText = "";
                }
                else
                {
                    //xe129.InnerText = "";
                    //xe130.InnerText = "";
                    //xe131.InnerText = "";
                    //xe132.InnerText = "";
                    //xe133.InnerText = "";
                }

                XmlElement xe134 = xmldoc.CreateElement("xlb_StdLimit1589");//创建一个<Node>节点 
                //xe134.InnerText = model.clwkccpd;
                XmlElement xe135 = xmldoc.CreateElement("Stdjdz_xlb");//创建一个<Node>节点 
                //xe135.InnerText = model.clwkccpd;
                XmlElement xe136 = xmldoc.CreateElement("cwxlbLimit");//创建一个<Node>节点 
                //xe136.InnerText = model.clwkccpd;


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

                xe1.AppendChild(cdevl);
                xe1.AppendChild(kdevl);
                xe1.AppendChild(gdevl);
                xe1.AppendChild(xlbgd);
                xe1.AppendChild(xlbgdpd);
                xe1.AppendChild(pdyj);

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

                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writetestDetailResult_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18C81\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18C81", send_xml));
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool writeZbzlTestDetailResult(HyZbzlDetailResult model, out string code, out string message)
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
                IOControl.saveXmlLogInf("writeZbzlTestDetailResult_Send:   XTLB:" + Xtlb + "|JKXLH:" + Jkxlh + "JKID:18C81\r\n" + send_xml + "\r\n");

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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code">1:成功 小于0:失败</param>
        /// <param name="message"></param>
        public bool writeOutlineCapturePicture(HyCapturePicture model, out string code, out string message)
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
                XmlElement zp = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                zp.InnerText = "";
                XmlElement xe109 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe109.InnerText = model.pzcfsj;
                XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe110.InnerText = model.jyxm;
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
                XmlElement spdm = xmldoc.CreateElement("spdm");//创建一个<Node>节点 
                spdm.InnerText = model.spdm;
                XmlElement pdyj = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                pdyj.InnerText = "1";
                xe1.AppendChild(xe101);
                xe1.AppendChild(xe102);
                xe1.AppendChild(xe103);
                xe1.AppendChild(xe104);
                xe1.AppendChild(xe105);
                xe1.AppendChild(xe106);
                xe1.AppendChild(xe107);
                xe1.AppendChild(zp);
                xe1.AppendChild(xe109);
                xe1.AppendChild(xe110);
                xe1.AppendChild(xe111);
                xe1.AppendChild(spdm);
                xe1.AppendChild(pdyj);
                root.AppendChild(xe1);
                
                string send_xml = XmlOperation.ConvertXmlToStringUTF8(xmldoc);
                IOControl.saveXmlLogInf("writeOutlineCapturePicture_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18N63\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18N63", send_xml));
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

        public bool writePhoto(HyPhoto model, out string code, out string message)
        {
            code = "";
            message = "";
            return true;
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
                XmlElement xe108 = xmldoc.CreateElement("zp");//创建一个<Node>节点 
                xe108.InnerText = XmlOperation.PushTxt(model.zp);
                XmlElement xe109 = xmldoc.CreateElement("pssj");//创建一个<Node>节点 
                xe109.InnerText = model.pssj;
                XmlElement xe110 = xmldoc.CreateElement("jyxm");//创建一个<Node>节点 
                xe110.InnerText = model.jyxm;
                XmlElement xe111 = xmldoc.CreateElement("zpzl");//创建一个<Node>节点 
                xe111.InnerText = model.zpzl;
                XmlElement xe112 = xmldoc.CreateElement("pdyj");//创建一个<Node>节点 
                xe112.InnerText = "1";
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
                IOControl.saveXmlLogInf("writeOutlineCapturePicture_Send:   XTLB:01|JKXLH:" + Jkxlh + "JKID:18C63\r\n" + send_xml + "\r\n");

                string receiveXml = HttpUtility.UrlDecode(outlineservice.writeObjectOut("01", Jkxlh, "18C63", send_xml));
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
    
    /// <summary>
    /// 项目开始
    /// </summary>
    public class HyProjectStart
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
        public string pdyj; 

        public HyProjectStart(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj,string pdyj)
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
            this.pdyj = pdyj;
        }
    }

    /// <summary>
    /// 项目结束
    /// </summary>
    public class HyProjectFinish
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
        public string pdyj;

        public HyProjectFinish(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj, string pdyj)
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
            this.pdyj = pdyj;
        }
    }

    /// <summary>
    /// 录像结束
    /// </summary>
    public class HyVideoStart
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
        public string lxxm;

        public HyVideoStart(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string kssj, string lxxm)
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
            this.lxxm = lxxm;
        }
    }

    /// <summary>
    /// 录像结束
    /// </summary>
    public class HyVideoStop
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
        public string lxxm;

        public HyVideoStop(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string jyxm, string jssj, string lxxm)
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
            this.lxxm = lxxm;
        }
    }

    /// <summary>
    /// 拍照命令
    /// </summary>
    public class HyCapturePicture
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
        public string spdm;
        public HyCapturePicture(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pzcfsj, string jyxm, string zpzl, string spdm)
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
            this.spdm = spdm;
        }
    }

    /// <summary>
    /// 拍照命令
    /// </summary>
    public class HyPhoto
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
        public HyPhoto(string jylsh, string jyjgbh, string jcxdh, string jycs, string hpzl, string hphm, string clsbdh, string pssj, string jyxm, string zpzl, string zp)
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
            this.pssj = pssj;
            this.jyxm = jyxm;
            this.zpzl = zpzl;
            this.zp = zp;
        }
    }

    /// <summary>
    /// 外廓结果
    /// </summary>
    public class HyTestDetailResult
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
        public string cdevl;
        public string kdevl;
        public string gdevl;
        public string xlbgd;
        public string xlbgdpd;

        public string cwkcLimit;
        public string cwkkLimit;
        public string cwkgLimit;
        public string L_StdLimit1589;
        public string W_StdLimit1589;
        public string H_StdLimit1589;
        public string sfxz;
        public string scc;
        public string sck;
        public string scg;
        public string scpj;
        public string ms;
        public string Stdbfb;
        public string Stdjdz;
        public string Wkzj;
        public string Zjpj;
        public string Stdjdz_zj;
        public string Stdbfb_zj;
        public string cwzjLimit;
        public string xlb_StdLimit1589;
        public string Stdjdz_xlb;
        public string cwxlbLimit;

        public HyTestDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string cwkc,
            string cwkk, string cwkg, string clwkccpd, string cdevl, string kdevl, string gdevl, string xlbgd, string xlbgdpd, string cwkcLimit, string cwkkLimit, 
            string cwkgLimit, string L_StdLimit1589, string W_StdLimit1589, string H_StdLimit1589, string sfxz, string scc, string sck, string scg, string scpj, 
            string ms, string Stdbfb, string Stdjdz, string Wkzj, string Zjpj, string Stdjdz_zj, string Stdbfb_zj, string cwzjLimit, string xlb_StdLimit1589, string Stdjdz_xlb, string cwxlbLimit)
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
            this.cdevl = cdevl;
            this.kdevl = kdevl;
            this.gdevl = gdevl;
            this.xlbgd = xlbgd;
            this.xlbgdpd = xlbgdpd;

            this.cwkcLimit = cwkcLimit;
            this.cwkkLimit = cwkkLimit;
            this.cwkgLimit = cwkgLimit;
            this.L_StdLimit1589 = L_StdLimit1589;
            this.W_StdLimit1589 = W_StdLimit1589;
            this.H_StdLimit1589 = H_StdLimit1589;
            this.sfxz = sfxz;
            this.scc = scc;
            this.sck = sck;
            this.scg = scg;
            this.scpj = scpj;
            this.ms = ms;
            this.Stdbfb = Stdbfb;
            this.Stdjdz = Stdjdz;
            this.Wkzj = Wkzj;
            this.Zjpj = Zjpj;
            this.Stdjdz_zj = Stdjdz_zj;
            this.Stdbfb_zj = Stdbfb_zj;
            this.cwzjLimit = cwzjLimit;
            this.xlb_StdLimit1589 = xlb_StdLimit1589;
            this.Stdjdz_xlb = Stdjdz_xlb;
            this.cwxlbLimit = cwxlbLimit;
        }
    }

    /// <summary>
    /// 整备质量结果
    /// </summary>
    public class HyZbzlDetailResult
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
        public HyZbzlDetailResult(string jylsh, string jyjgbh, string jcxdh, string jyxm, string jycs, string hpzl, string hphm, string clsbdh, string gwjysbbh, string zbzl,
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
