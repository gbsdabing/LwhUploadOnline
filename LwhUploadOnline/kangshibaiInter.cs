﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl 自动生成, Version=4.6.1055.0。
// 
namespace LwhUploadOnline
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "safeSoap", Namespace = "http://tempuri.org/")]
    public partial class KSBsafe : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback UploadObjectOutOperationCompleted;

        /// <remarks/>
        public KSBsafe()
        {
            this.Url = "http://192.168.6.56:8081/safe.asmx";
        }

        public KSBsafe(string url)
        {
            this.Url = url;
        }

        /// <remarks/>
        public event UploadObjectOutCompletedEventHandler UploadObjectOutCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UploadObjectOut", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UploadObjectOut(int nSignal, string strXml, string strIP)
        {
            IOControl.saveXmlLogInf("\r\n" + "Send:" + " InterfaceNo:" + nSignal.ToString() + "\r\n" + strXml);
            object[] results = this.Invoke("UploadObjectOut", new object[] {
                        nSignal,
                        strXml,
                        strIP});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUploadObjectOut(int nSignal, string strXml, string strIP, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UploadObjectOut", new object[] {
                        nSignal,
                        strXml,
                        strIP}, callback, asyncState);
        }

        /// <remarks/>
        public string EndUploadObjectOut(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void UploadObjectOutAsync(int nSignal, string strXml, string strIP)
        {
            this.UploadObjectOutAsync(nSignal, strXml, strIP, null);
        }

        /// <remarks/>
        public void UploadObjectOutAsync(int nSignal, string strXml, string strIP, object userState)
        {
            if ((this.UploadObjectOutOperationCompleted == null))
            {
                this.UploadObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadObjectOutOperationCompleted);
            }
            this.InvokeAsync("UploadObjectOut", new object[] {
                        nSignal,
                        strXml,
                        strIP}, this.UploadObjectOutOperationCompleted, userState);
        }

        private void OnUploadObjectOutOperationCompleted(object arg)
        {
            if ((this.UploadObjectOutCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadObjectOutCompleted(this, new UploadObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void UploadObjectOutCompletedEventHandler(object sender, UploadObjectOutCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
