﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Web;

// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.1。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="AccessServiceSoap", Namespace="http://tempuri.org/")]
public partial class hcAccessService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback QueryServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback WriteServiceOperationCompleted;
    
    private System.Threading.SendOrPostCallback webEncodeUTF8OperationCompleted;
    
    private System.Threading.SendOrPostCallback webDecodeUTF8OperationCompleted;
    
    private System.Threading.SendOrPostCallback GetBase64OperationCompleted;
    
    /// <remarks/>
    public hcAccessService() {
        this.Url = "http://11.1.1.4:94/Services/AccessService.asmx";
    }
    public hcAccessService(string url)
    {
        this.Url = url;
    }

    /// <remarks/>
    public event QueryServiceCompletedEventHandler QueryServiceCompleted;
    
    /// <remarks/>
    public event WriteServiceCompletedEventHandler WriteServiceCompleted;
    
    /// <remarks/>
    public event webEncodeUTF8CompletedEventHandler webEncodeUTF8Completed;
    
    /// <remarks/>
    public event webDecodeUTF8CompletedEventHandler webDecodeUTF8Completed;
    
    /// <remarks/>
    public event GetBase64CompletedEventHandler GetBase64Completed;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/QueryService", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string QueryService(string jkxlh, string jkid, string xmlDoc) {
        object[] results = this.Invoke("QueryService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginQueryService(string jkxlh, string jkid, string xmlDoc, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("QueryService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndQueryService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void QueryServiceAsync(string jkxlh, string jkid, string xmlDoc) {
        this.QueryServiceAsync(jkxlh, jkid, xmlDoc, null);
    }
    
    /// <remarks/>
    public void QueryServiceAsync(string jkxlh, string jkid, string xmlDoc, object userState) {
        if ((this.QueryServiceOperationCompleted == null)) {
            this.QueryServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnQueryServiceOperationCompleted);
        }
        this.InvokeAsync("QueryService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc}, this.QueryServiceOperationCompleted, userState);
    }
    
    private void OnQueryServiceOperationCompleted(object arg) {
        if ((this.QueryServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.QueryServiceCompleted(this, new QueryServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WriteService", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string WriteService(string jkxlh, string jkid, string xmlDoc) {
        object[] results = this.Invoke("WriteService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginWriteService(string jkxlh, string jkid, string xmlDoc, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("WriteService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndWriteService(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void WriteServiceAsync(string jkxlh, string jkid, string xmlDoc) {
        this.WriteServiceAsync(jkxlh, jkid, xmlDoc, null);
    }
    
    /// <remarks/>
    public void WriteServiceAsync(string jkxlh, string jkid, string xmlDoc, object userState) {
        if ((this.WriteServiceOperationCompleted == null)) {
            this.WriteServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWriteServiceOperationCompleted);
        }
        this.InvokeAsync("WriteService", new object[] {
                    jkxlh,
                    jkid,
                    xmlDoc}, this.WriteServiceOperationCompleted, userState);
    }
    
    private void OnWriteServiceOperationCompleted(object arg) {
        if ((this.WriteServiceCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.WriteServiceCompleted(this, new WriteServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/webEncodeUTF8", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string webEncodeUTF8(string str) {
        object[] results = this.Invoke("webEncodeUTF8", new object[] {
                    str});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginwebEncodeUTF8(string str, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("webEncodeUTF8", new object[] {
                    str}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndwebEncodeUTF8(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void webEncodeUTF8Async(string str) {
        this.webEncodeUTF8Async(str, null);
    }
    
    /// <remarks/>
    public void webEncodeUTF8Async(string str, object userState) {
        if ((this.webEncodeUTF8OperationCompleted == null)) {
            this.webEncodeUTF8OperationCompleted = new System.Threading.SendOrPostCallback(this.OnwebEncodeUTF8OperationCompleted);
        }
        this.InvokeAsync("webEncodeUTF8", new object[] {
                    str}, this.webEncodeUTF8OperationCompleted, userState);
    }
    
    private void OnwebEncodeUTF8OperationCompleted(object arg) {
        if ((this.webEncodeUTF8Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.webEncodeUTF8Completed(this, new webEncodeUTF8CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/webDecodeUTF8", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string webDecodeUTF8(string str) {
        object[] results = this.Invoke("webDecodeUTF8", new object[] {
                    str});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginwebDecodeUTF8(string str, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("webDecodeUTF8", new object[] {
                    str}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndwebDecodeUTF8(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void webDecodeUTF8Async(string str) {
        this.webDecodeUTF8Async(str, null);
    }
    
    /// <remarks/>
    public void webDecodeUTF8Async(string str, object userState) {
        if ((this.webDecodeUTF8OperationCompleted == null)) {
            this.webDecodeUTF8OperationCompleted = new System.Threading.SendOrPostCallback(this.OnwebDecodeUTF8OperationCompleted);
        }
        this.InvokeAsync("webDecodeUTF8", new object[] {
                    str}, this.webDecodeUTF8OperationCompleted, userState);
    }
    
    private void OnwebDecodeUTF8OperationCompleted(object arg) {
        if ((this.webDecodeUTF8Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.webDecodeUTF8Completed(this, new webDecodeUTF8CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetBase64", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string GetBase64([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] byt) {
        object[] results = this.Invoke("GetBase64", new object[] {
                    byt});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetBase64(byte[] byt, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetBase64", new object[] {
                    byt}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndGetBase64(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void GetBase64Async(byte[] byt) {
        this.GetBase64Async(byt, null);
    }
    
    /// <remarks/>
    public void GetBase64Async(byte[] byt, object userState) {
        if ((this.GetBase64OperationCompleted == null)) {
            this.GetBase64OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBase64OperationCompleted);
        }
        this.InvokeAsync("GetBase64", new object[] {
                    byt}, this.GetBase64OperationCompleted, userState);
    }
    
    private void OnGetBase64OperationCompleted(object arg) {
        if ((this.GetBase64Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetBase64Completed(this, new GetBase64CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void QueryServiceCompletedEventHandler(object sender, QueryServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class QueryServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal QueryServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void WriteServiceCompletedEventHandler(object sender, WriteServiceCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class WriteServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal WriteServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void webEncodeUTF8CompletedEventHandler(object sender, webEncodeUTF8CompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class webEncodeUTF8CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal webEncodeUTF8CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void webDecodeUTF8CompletedEventHandler(object sender, webDecodeUTF8CompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class webDecodeUTF8CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal webDecodeUTF8CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void GetBase64CompletedEventHandler(object sender, GetBase64CompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetBase64CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetBase64CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
