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
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;


// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.1。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="TmriOutAccessSoap", Namespace="http://www.hyjtkj.com/")]
public partial class SRTmriOutAccess : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback GetVerOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetLoginParmOperationCompleted;
    
    private System.Threading.SendOrPostCallback queryObjectOutOperationCompleted;
    
    private System.Threading.SendOrPostCallback writeObjectOutOperationCompleted;
    
    /// <remarks/>
    public SRTmriOutAccess() {
        this.Url = "http://192.168.55.100:8090/jyjk/Tmrioutaccess.asmx";
    }
    public SRTmriOutAccess(string url)
    {
        this.Url = url;
    }

    /// <remarks/>
    public event GetVerCompletedEventHandler GetVerCompleted;
    
    /// <remarks/>
    public event GetLoginParmCompletedEventHandler GetLoginParmCompleted;
    
    /// <remarks/>
    public event SRqueryObjectOutCompletedEventHandler queryObjectOutCompleted;
    
    /// <remarks/>
    public event SRwriteObjectOutCompletedEventHandler writeObjectOutCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.hyjtkj.com/GetVer", RequestNamespace="http://www.hyjtkj.com/", ResponseNamespace="http://www.hyjtkj.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string GetVer(string str) {
        object[] results = this.Invoke("GetVer", new object[] {
                    str});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetVer(string str, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetVer", new object[] {
                    str}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndGetVer(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void GetVerAsync(string str) {
        this.GetVerAsync(str, null);
    }
    
    /// <remarks/>
    public void GetVerAsync(string str, object userState) {
        if ((this.GetVerOperationCompleted == null)) {
            this.GetVerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVerOperationCompleted);
        }
        this.InvokeAsync("GetVer", new object[] {
                    str}, this.GetVerOperationCompleted, userState);
    }
    
    private void OnGetVerOperationCompleted(object arg) {
        if ((this.GetVerCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetVerCompleted(this, new GetVerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.hyjtkj.com/GetLoginParm", RequestNamespace="http://www.hyjtkj.com/", ResponseNamespace="http://www.hyjtkj.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string GetLoginParm(string _CYYID, string mac, string imie, string ip) {
        object[] results = this.Invoke("GetLoginParm", new object[] {
                    _CYYID,
                    mac,
                    imie,
                    ip});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginGetLoginParm(string _CYYID, string mac, string imie, string ip, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetLoginParm", new object[] {
                    _CYYID,
                    mac,
                    imie,
                    ip}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndGetLoginParm(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void GetLoginParmAsync(string _CYYID, string mac, string imie, string ip) {
        this.GetLoginParmAsync(_CYYID, mac, imie, ip, null);
    }
    
    /// <remarks/>
    public void GetLoginParmAsync(string _CYYID, string mac, string imie, string ip, object userState) {
        if ((this.GetLoginParmOperationCompleted == null)) {
            this.GetLoginParmOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLoginParmOperationCompleted);
        }
        this.InvokeAsync("GetLoginParm", new object[] {
                    _CYYID,
                    mac,
                    imie,
                    ip}, this.GetLoginParmOperationCompleted, userState);
    }
    
    private void OnGetLoginParmOperationCompleted(object arg) {
        if ((this.GetLoginParmCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetLoginParmCompleted(this, new GetLoginParmCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.hyjtkj.com/queryObjectOut", RequestNamespace="http://www.hyjtkj.com/", ResponseNamespace="http://www.hyjtkj.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string queryObjectOut(string xtlb, string jkxlh, string jkid, string QueryUTF8XmlDoc)
    {
        object[] results = this.Invoke("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    QueryUTF8XmlDoc});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginqueryObjectOut(string xtlb, string jkxlh, string jkid, string QueryUTF8XmlDoc, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    QueryUTF8XmlDoc}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndqueryObjectOut(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string QueryUTF8XmlDoc) {
        this.queryObjectOutAsync(xtlb, jkxlh, jkid, QueryUTF8XmlDoc, null);
    }
    
    /// <remarks/>
    public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string QueryUTF8XmlDoc, object userState) {
        if ((this.queryObjectOutOperationCompleted == null)) {
            this.queryObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryObjectOutOperationCompleted);
        }
        this.InvokeAsync("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    QueryUTF8XmlDoc}, this.queryObjectOutOperationCompleted, userState);
    }
    
    private void OnqueryObjectOutOperationCompleted(object arg) {
        if ((this.queryObjectOutCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.queryObjectOutCompleted(this, new SRqueryObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.hyjtkj.com/writeObjectOut", RequestNamespace="http://www.hyjtkj.com/", ResponseNamespace="http://www.hyjtkj.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string writeObjectOut(string xtlb, string jkxlh, string jkid, string writeUTF8XmlDoc)
    {
        object[] results = this.Invoke("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    writeUTF8XmlDoc});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginwriteObjectOut(string xtlb, string jkxlh, string jkid, string writeUTF8XmlDoc, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    writeUTF8XmlDoc}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndwriteObjectOut(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string writeUTF8XmlDoc) {
        this.writeObjectOutAsync(xtlb, jkxlh, jkid, writeUTF8XmlDoc, null);
    }
    
    /// <remarks/>
    public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string writeUTF8XmlDoc, object userState) {
        if ((this.writeObjectOutOperationCompleted == null)) {
            this.writeObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwriteObjectOutOperationCompleted);
        }
        this.InvokeAsync("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    writeUTF8XmlDoc}, this.writeObjectOutOperationCompleted, userState);
    }
    
    private void OnwriteObjectOutOperationCompleted(object arg) {
        if ((this.writeObjectOutCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.writeObjectOutCompleted(this, new SRwriteObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void GetVerCompletedEventHandler(object sender, GetVerCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetVerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetVerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
public delegate void GetLoginParmCompletedEventHandler(object sender, GetLoginParmCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class GetLoginParmCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetLoginParmCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
public delegate void SRqueryObjectOutCompletedEventHandler(object sender, SRqueryObjectOutCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SRqueryObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal SRqueryObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
public delegate void SRwriteObjectOutCompletedEventHandler(object sender, SRwriteObjectOutCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SRwriteObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal SRwriteObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
