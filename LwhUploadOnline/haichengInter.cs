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
namespace LwhUploadOnline {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="TmriOutNewAccessSoapBinding", Namespace="http://localhost:1044/")]
    public partial class HCTmriOutNewAccess : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback writeObjectOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback queryObjectOutNewOperationCompleted;
        
        private System.Threading.SendOrPostCallback queryObjectOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback writeObjectOutNewOperationCompleted;
        
        /// <remarks/>
        public HCTmriOutNewAccess() {
            this.Url = "http://11.121.35.23:8080/StationService/trffweb/services/TmriOutNewAccess";
        }
        public HCTmriOutNewAccess(string url)
        {
            this.Url = url;
        }
        /// <remarks/>
        public event writeObjectOutCompletedEventHandler writeObjectOutCompleted;
        
        /// <remarks/>
        public event queryObjectOutNewCompletedEventHandler queryObjectOutNewCompleted;
        
        /// <remarks/>
        public event queryObjectOutCompletedEventHandler queryObjectOutCompleted;
        
        /// <remarks/>
        public event writeObjectOutNewCompletedEventHandler writeObjectOutNewCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://localhost:1044/", ResponseNamespace="http://localhost:1044/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string writeObjectOut([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xtlb, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkxlh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkid, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string cjsqbh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwjgdm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwmc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhbz, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhxm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string zdbs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xmlDoc) {
            object[] results = this.Invoke("writeObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginwriteObjectOut(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("writeObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndwriteObjectOut(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc) {
            this.writeObjectOutAsync(xtlb, jkxlh, jkid, cjsqbh, dwjgdm, dwmc, yhbz, yhxm, zdbs, xmlDoc, null);
        }
        
        /// <remarks/>
        public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, object userState) {
            if ((this.writeObjectOutOperationCompleted == null)) {
                this.writeObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwriteObjectOutOperationCompleted);
            }
            this.InvokeAsync("writeObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, this.writeObjectOutOperationCompleted, userState);
        }
        
        private void OnwriteObjectOutOperationCompleted(object arg) {
            if ((this.writeObjectOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.writeObjectOutCompleted(this, new writeObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://localhost:1044/", ResponseNamespace="http://localhost:1044/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string queryObjectOutNew([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xtlb, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkxlh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkid, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string cjsqbh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwjgdm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwmc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhbz, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhxm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string zdbs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xmlDoc) {
            IOControl.saveXmlLogInf("SEND:" + xtlb + "|" + jkxlh + "|" + jkid + "|" + cjsqbh + "|" + dwjgdm + "|" + dwmc + "|" + yhbz + "|" + yhxm + "|" + zdbs + "\r\n" + xmlDoc);
            object[] results = this.Invoke("queryObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginqueryObjectOutNew(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("queryObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndqueryObjectOutNew(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void queryObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc) {
            this.queryObjectOutNewAsync(xtlb, jkxlh, jkid, cjsqbh, dwjgdm, dwmc, yhbz, yhxm, zdbs, xmlDoc, null);
        }
        
        /// <remarks/>
        public void queryObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, object userState) {
            if ((this.queryObjectOutNewOperationCompleted == null)) {
                this.queryObjectOutNewOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryObjectOutNewOperationCompleted);
            }
            this.InvokeAsync("queryObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, this.queryObjectOutNewOperationCompleted, userState);
        }
        
        private void OnqueryObjectOutNewOperationCompleted(object arg) {
            if ((this.queryObjectOutNewCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryObjectOutNewCompleted(this, new queryObjectOutNewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://localhost:1044/", ResponseNamespace="http://localhost:1044/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string queryObjectOut([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xtlb, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkxlh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkid, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string cjsqbh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwjgdm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwmc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhbz, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhxm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string zdbs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xmlDoc) {
            object[] results = this.Invoke("queryObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginqueryObjectOut(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("queryObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndqueryObjectOut(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc) {
            this.queryObjectOutAsync(xtlb, jkxlh, jkid, cjsqbh, dwjgdm, dwmc, yhbz, yhxm, zdbs, xmlDoc, null);
        }
        
        /// <remarks/>
        public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, object userState) {
            if ((this.queryObjectOutOperationCompleted == null)) {
                this.queryObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryObjectOutOperationCompleted);
            }
            this.InvokeAsync("queryObjectOut", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, this.queryObjectOutOperationCompleted, userState);
        }
        
        private void OnqueryObjectOutOperationCompleted(object arg) {
            if ((this.queryObjectOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryObjectOutCompleted(this, new queryObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://localhost:1044/", ResponseNamespace="http://localhost:1044/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string writeObjectOutNew([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xtlb, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkxlh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string jkid, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string cjsqbh, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwjgdm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string dwmc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhbz, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string yhxm, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string zdbs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string xmlDoc) {
            IOControl.saveXmlLogInf("SEND:" + xtlb + "|" + jkxlh + "|" + jkid + "|" + cjsqbh + "|" + dwjgdm + "|" + dwmc + "|" + yhbz + "|" + yhxm + "|" + zdbs + "\r\n" + ((jkid == "CHK03") ? xmlDoc.Remove(xmlDoc.IndexOf("<picbase64>"), xmlDoc.IndexOf("</picbase64>") - xmlDoc.IndexOf("<picbase64>") + 12) : xmlDoc));
            object[] results = this.Invoke("writeObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginwriteObjectOutNew(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("writeObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndwriteObjectOutNew(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void writeObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc) {
            this.writeObjectOutNewAsync(xtlb, jkxlh, jkid, cjsqbh, dwjgdm, dwmc, yhbz, yhxm, zdbs, xmlDoc, null);
        }
        
        /// <remarks/>
        public void writeObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string xmlDoc, object userState) {
            if ((this.writeObjectOutNewOperationCompleted == null)) {
                this.writeObjectOutNewOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwriteObjectOutNewOperationCompleted);
            }
            this.InvokeAsync("writeObjectOutNew", new object[] {
                        xtlb,
                        jkxlh,
                        jkid,
                        cjsqbh,
                        dwjgdm,
                        dwmc,
                        yhbz,
                        yhxm,
                        zdbs,
                        xmlDoc}, this.writeObjectOutNewOperationCompleted, userState);
        }
        
        private void OnwriteObjectOutNewOperationCompleted(object arg) {
            if ((this.writeObjectOutNewCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.writeObjectOutNewCompleted(this, new writeObjectOutNewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void writeObjectOutCompletedEventHandler(object sender, writeObjectOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class writeObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal writeObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void queryObjectOutNewCompletedEventHandler(object sender, queryObjectOutNewCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryObjectOutNewCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal queryObjectOutNewCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void queryObjectOutCompletedEventHandler(object sender, queryObjectOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal queryObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    public delegate void writeObjectOutNewCompletedEventHandler(object sender, writeObjectOutNewCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class writeObjectOutNewCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal writeObjectOutNewCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}