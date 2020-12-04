using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Web;

namespace LwhUploadOnline
{
    public static class XmlOperation
    {
        public static DataTable ReadXmlToDatatable(string xmlstring, out string code, out string message)
        {
            code = "";
            message = "";
            try
            {
                DataSet ds = CXmlToDataSet(xmlstring);
                if (ds != null)
                {
                    code = ds.Tables[0].Rows[0]["code"].ToString();
                    message = ds.Tables[0].Rows[0]["message"].ToString();
                    if (code == "1")
                        return CXmlToDatatTable(GetbodyInfo(xmlstring));
                    else
                        return null;
                }
                else
                {
                    code = "";
                    message = "ReadXmlToDatatable_Failed: 解析xml为table时为空";
                    return null;
                }
            }
            catch (Exception er)
            {
                message = "ReadXmlToDatatable_Error:" + er.Message;
                return null;
            }
        }

        public static bool ReadACKString(string xmlstring, out string result, out string info)
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

                return true;
            }
            catch (Exception er)
            {
                result = "";
                info = "ReadACKString_Error: " + er.Message;

                return false;
            }
        }

        /// <summary>
        /// 获取字符串UTF8编码
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static String encodeUTF8(string xmlDoc)
        {
            string newstring = "";
            try
            {
                newstring = HttpUtility.UrlEncode(xmlDoc, Encoding.UTF8);
            }
            catch
            {
                newstring = "";
            }

            return newstring;
        }

        /// <summary>
        /// 获取字符串GBK编码
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static String encodeGBK(string xmlDoc)
        {
            string newstring = "";
            try
            {
                newstring = System.Web.HttpUtility.UrlEncode(xmlDoc, Encoding.GetEncoding("GBK"));
            }
            catch
            {
                newstring = "";
            }

            return newstring;
        }

        /// <summary>  
        /// 将XmlDocument转化为string(UTF8编码)
        /// </summary>  
        /// <param name="xmlDoc"></param>  
        /// <returns></returns>  
        public static string ConvertXmlToStringUTF8(XmlDocument xmlDoc)
        {
            string newstring = "";
            try
            {
                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                stream.Position = 0;
                string xmlString = sr.ReadToEnd();
                sr.Close();
                stream.Close();
                newstring = xmlString.Replace("xml version=\"1.0\"", "xml version=\"1.0\" encoding=\"utf-8\"");
                newstring = Regex.Replace(newstring.Replace("\r\n", ""), @">\s+<", "><");//去除节点之间所有的空格，回车及其他符号
            }
            catch
            {
                newstring = "";
            }

            return newstring;
        }

        /// <summary>
        /// 将XmlDocument转化为string(GBK编码)
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string ConvertXmlToStringGBK(XmlDocument xmlDoc)
        {
            string newstring = "";
            try
            {
                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);
                StreamReader sr = new StreamReader(stream);
                stream.Position = 0;
                string xmlString = sr.ReadToEnd();
                sr.Close();
                stream.Close();
                newstring = xmlString.Replace("xml version=\"1.0\"", "xml version=\"1.0\" encoding=\"GBK\"");
                newstring = Regex.Replace(newstring.Replace("\r\n", ""), @">\s+<", "><");//去除节点之间所有的空格，回车及其他符号
            }
            catch
            {
                newstring = "";
            }

            return newstring;
        }

        /// <summary>
        /// 根据图像路径获取其64位编码字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PushTxt(string image_path)
        {
            using (FileStream fs = new FileStream(image_path, FileMode.Open, FileAccess.Read))
            {
                int filelength = (int)fs.Length;
                if (filelength <= 0)
                    return "";
                try
                {
                    Byte[] FileByteArray = new Byte[filelength]; //图象文件临时储存Byte数组  
                    System.IO.BinaryReader strread = new System.IO.BinaryReader(fs); //建立数据流对像                     
                    strread.Read(FileByteArray, 0, filelength);//读取图象文件数据，FileByteArray为数据储存体，0为数据指针位置、FileLnegth为数据长度 
                    return Convert.ToBase64String(FileByteArray);
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 移除xml字符串中指定开头结尾标签的照片信息
        /// </summary>
        /// <param name="xmlinf"></param>
        /// <param name="xmlnodestart"></param>
        /// <param name="xmlnodeend"></param>
        /// <returns></returns>
        public static string removePictureInfo(string xmlinf, string[] xmlnodestart, string[] xmlnodeend)
        {
            try
            {
                string temp = string.Copy(xmlinf);

                for (int i = 0; i < xmlnodestart.Length; i++)
                {
                    int iBegin = temp.ToString().IndexOf(xmlnodestart[i]);
                    int iEnd = temp.ToString().IndexOf(xmlnodeend[i]);
                    if (iEnd > 0 && iBegin > 0)
                    {
                        temp = temp.Remove(iBegin + xmlnodestart[i].Length, iEnd - iBegin - xmlnodestart[i].Length);
                    }
                }

                return temp;
            }
            catch
            {
                return xmlinf;
            }
        }

        #region XML处理内部函数
        /// <summary>
        /// 获取XML字符串中body节点数据
        /// </summary>
        /// <param name="xmlinf"></param>
        /// <returns></returns>
        public static string GetbodyInfo(string xmlinf)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine(xmlinf);
                int iBegin = str.ToString().IndexOf(@"<body>");
                int iEnd = str.ToString().IndexOf(@"</body>");
                return str.ToString().Substring(iBegin, iEnd - iBegin + 7);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param >Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param >Xml字符串</param>
        /// <param >Table表索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
        {
            return CXmlToDataSet(xmlStr).Tables[tableIndex];
        }

        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param >Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            return CXmlToDataSet(xmlStr).Tables[0];
        }
        #endregion
    }
}
