using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Xml.Serialization;
using System.Web.Services.Description;
using Microsoft.CSharp;
using System.Reflection;



namespace NetSendWaitCar
{
    public class YiZhongXiang
    {
        private Type interface_type = null;
        private object interface_obj = null;
        private string dllpath = System.Environment.CurrentDirectory + "\\interface_yzx.dll";
        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <param name="url">接口地址（不含?wsdl）</param>
        /// <param name="error_info">错误信息</param>
        /// <returns>是否成功</returns>
        public bool InitWebService(string url, out string error_info)
        {
            error_info = "";

            #region 校验输入接口地址
            if (url.EndsWith("?wsdl") || url.EndsWith("?WSDL"))
                url = url.Substring(0, url.LastIndexOf("?"));
            if (url.EndsWith(".asmx") == false)
            {
                error_info = "接口地址不对，请检查后重试！";
                return false;
            }
            #endregion

            #region 删除启动目录已存在的dll文件
            if (File.Exists(dllpath) == true)
            {
                try
                {
                    File.Delete(dllpath);
                }
                catch
                {
                    error_info = "删除之前的代理类文件失败，请手动删除后重试！";
                    return false;
                }
            }
            #endregion

            #region 生成接口代理类的dll文件
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            try
            {
                //获取服务描述语言(WSDL)
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");

                //创建及格式化wsdl文档
                ServiceDescription description = ServiceDescription.Read(stream);

                //注意classname一定要赋值获取 
                string classname = description.Services[0].Name;

                //创建客户端代理类
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap";
                importer.Style = ServiceDescriptionImportStyle.Client;
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

                //添加wsdl文档
                importer.AddServiceDescription(description, "", "");

                //为代理类添加命名空间，缺省为全局空间
                CodeNamespace nmspace = new CodeNamespace(@namespace);

                //生成客户端代理类代码
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);

                //设定编译器的参数    
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = "interface_yzx.dll";
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类    
                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
                if (result.Errors.HasErrors == true)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in result.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //初始化代理客户端
                Assembly asm = Assembly.LoadFrom("interface_yzx.dll");
                interface_type = asm.GetType("EnterpriseServerBase.WebService.DynamicWebCalling." + classname, true, true);
                interface_obj = Activator.CreateInstance(interface_type);
                return true;
            }
            catch (Exception ex)
            {
                error_info = ex.Message.ToString();
                return false;
            }
            #endregion
        }

        public string SendInfo(string method_name, string[] argsXml, out string error_info)
        {
            error_info = "";

            if (File.Exists(dllpath) == false)
            {
                error_info = "代理类客户端不存在，请检查后重试！";
                return "null";
            }
            else
            {
                try
                {
                    string sendXml = "";
                    for (int i = 0; i < argsXml.Length; i++)
                    {
                        sendXml = sendXml + argsXml[i] + "\r\n";
                    }
                    IOControl.saveXmlLogInf(method_name + "_Send:\r\n" + sendXml + "\r\n");

                    MethodInfo method = interface_type.GetMethod(method_name);
                    string receiveXml = method.Invoke(interface_obj, argsXml).ToString();
                    IOControl.saveXmlLogInf("Received:\r\n" + receiveXml + "\r\n");
                    return receiveXml;
                }
                catch (Exception ex)
                {
                    IOControl.saveXmlLogInf(method_name + "_Send_Error:\r\n" + ex.Message + "\r\n");
                    error_info = "接口调用发生错误：" + ex.Message;
                    return "null";
                }
            }
        }
    }
}
