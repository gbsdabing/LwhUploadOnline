﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1026
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
[System.Web.Services.WebServiceBindingAttribute(Name = "TmriOutAccessSoapBinding", Namespace = "http://192.1.6.10:8080/vehSupervise/services/TmriOutAccess")]
public partial class ZHTmriOutAccessService : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    private System.Threading.SendOrPostCallback queryObjectOutOperationCompleted;

    private System.Threading.SendOrPostCallback writeObjectOutOperationCompleted;

    /// <remarks/>
    public ZHTmriOutAccessService()
    {
        this.Url = "http://192.1.6.10:8080/vehSupervise/services/TmriOutAccess";
    }

    public ZHTmriOutAccessService(string url)
    {
        this.Url = url;
    }

    /// <remarks/>
    public event ZHqueryObjectOutCompletedEventHandler queryObjectOutCompleted;

    /// <remarks/>
    public event ZHwriteObjectOutCompletedEventHandler writeObjectOutCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://thread.supervise.gren.com", ResponseNamespace = "http://192.1.6.10:8080/vehSupervise/services/TmriOutAccess")]
    [return: System.Xml.Serialization.SoapElementAttribute("queryObjectOutReturn")]
    public string queryObjectOut(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc)
    {
        object[] results = this.Invoke("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginqueryObjectOut(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc}, callback, asyncState);
    }

    /// <remarks/>
    public string EndqueryObjectOut(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    /// <remarks/>
    public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc)
    {
        this.queryObjectOutAsync(xtlb, jkxlh, jkid, UTF8XmlDoc, null);
    }

    /// <remarks/>
    public void queryObjectOutAsync(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc, object userState)
    {
        if ((this.queryObjectOutOperationCompleted == null))
        {
            this.queryObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryObjectOutOperationCompleted);
        }
        this.InvokeAsync("queryObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc}, this.queryObjectOutOperationCompleted, userState);
    }

    private void OnqueryObjectOutOperationCompleted(object arg)
    {
        if ((this.queryObjectOutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.queryObjectOutCompleted(this, new ZHqueryObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://thread.supervise.gren.com", ResponseNamespace = "http://192.1.6.10:8080/vehSupervise/services/TmriOutAccess")]
    [return: System.Xml.Serialization.SoapElementAttribute("writeObjectOutReturn")]
    public string writeObjectOut(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc)
    {
        object[] results = this.Invoke("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginwriteObjectOut(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc}, callback, asyncState);
    }

    /// <remarks/>
    public string EndwriteObjectOut(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }

    /// <remarks/>
    public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc)
    {
        this.writeObjectOutAsync(xtlb, jkxlh, jkid, UTF8XmlDoc, null);
    }

    /// <remarks/>
    public void writeObjectOutAsync(string xtlb, string jkxlh, string jkid, string UTF8XmlDoc, object userState)
    {
        if ((this.writeObjectOutOperationCompleted == null))
        {
            this.writeObjectOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnwriteObjectOutOperationCompleted);
        }
        this.InvokeAsync("writeObjectOut", new object[] {
                    xtlb,
                    jkxlh,
                    jkid,
                    UTF8XmlDoc}, this.writeObjectOutOperationCompleted, userState);
    }

    private void OnwriteObjectOutOperationCompleted(object arg)
    {
        if ((this.writeObjectOutCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.writeObjectOutCompleted(this, new ZHwriteObjectOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void ZHqueryObjectOutCompletedEventHandler(object sender, ZHqueryObjectOutCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ZHqueryObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal ZHqueryObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void ZHwriteObjectOutCompletedEventHandler(object sender, ZHwriteObjectOutCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ZHwriteObjectOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal ZHwriteObjectOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
