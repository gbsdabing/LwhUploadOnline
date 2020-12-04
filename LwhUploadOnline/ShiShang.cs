using System;
using System.Data;
using System.Web;
using System.Xml;



namespace LwhUploadOnline
{
    public class ShiShang
    {
        public bool JK_Status { get { return jk_status; } }
        private bool jk_status = false;
        private string line_id = "";
        private SSService outlineservice = null;

        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="lineID">检测线号</param>
        public ShiShang(string url, string lineID)
        {
            try
            {
                line_id = lineID;
                outlineservice = new SSService(url);
                jk_status = true;
            }
            catch (Exception er)
            {
                jk_status = false;
                IOControl.saveXmlLogInf("南京新仕尚联网接口初始化失败，错误信息：\r\n" + er.Message);
            }
        }

        /// <summary>
        /// 发送照片
        /// </summary>
        /// <param name="zpzl">照片种类（0360-外廓尺寸自动测量前照片、0361-外廓尺寸自动测量侧面照片、0362-准备质量前45度照片、0361-准备质量后45度照片）</param>
        /// <param name="jylsh">检验流水号</param>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <param name="hphm">号牌号码</param>
        /// <param name="hpzl">号牌种类</param>
        /// <param name="vin">车架号码</param>
        /// <returns>是否成功（成功为1、失败为0）</returns>
        public bool Capture(string zpzl, string jylsh, string jcbh, int jccs, string hphm, string hpzl, string vin)
        {
            if (jk_status == false)
                return false;
            try
            {
                string hpzl_temp = hpzl.Split('(')[1].Split(')')[0];
                IOControl.saveXmlLogInf("南京新仕尚联网，hphm:" + hphm + "|hpzl:" + hpzl_temp + "|vin:" + vin + "|jylsh:" + jylsh + "|jcbh:" + jcbh + "|jccs:" + jccs.ToString() + "，开始发送照片（" + zpzl + "）");
                string result = outlineservice.Capture(zpzl, line_id, jylsh, jcbh, jccs, hphm, hpzl_temp, vin);
                IOControl.saveXmlLogInf("Received:号牌号码：" + hphm + " | 流水号：" + jylsh + " | 检测次数：" + jccs.ToString() + " | 照片种类：" + zpzl + "，发送结果：" + result);
                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("南京新仕尚联网发送照片（" + zpzl + "）失败，错误信息：\r\n" + er.Message);
                return false;
            }
        }

        /// <summary>
        /// 发送项目录像开始命令
        /// </summary>
        /// <param name="xmmc">检测项目名称代码（M1-外廓尺寸自动测量、M2-外廓尺寸自动测量侧面）</param>
        /// <param name="jylsh">检验流水号</param>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <param name="hphm">号牌号码</param>
        /// <param name="hpzl">号牌种类</param>
        /// <param name="vin">车架号码</param>
        /// <returns>是否成功（成功为1、失败为0）</returns>
        public bool StartVideo(string xmmc, string jylsh, string jcbh, int jccs, string hphm, string hpzl, string vin)
        {
            if (jk_status == false)
                return false;
            try
            {
                string hpzl_temp = hpzl.Split('(')[1].Split(')')[0];
                IOControl.saveXmlLogInf("南京新仕尚联网，号牌号码：" + hphm + "|流水号：" + jylsh + "|检测次数：" + jccs.ToString() + "，项目（" + xmmc + "）录像开始");
                string result = outlineservice.StartVideo(xmmc, line_id, jylsh, jcbh, jccs, hphm, hpzl_temp, vin);
                IOControl.saveXmlLogInf("Received:号牌号码：" + hphm + " | 流水号：" + jylsh + " | 检测次数：" + jccs.ToString() + " | 项目：" + xmmc + "，发送结果：" + result);
                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("南京新仕尚联网发送录制视频开始失败，错误信息：\r\n" + er.Message);
                return false;
            }
        }

        /// <summary>
        /// 发送项目录像结束命令
        /// </summary>
        /// <param name="xmmc">检测项目名称代码（M1-外廓尺寸自动测量、M2-外廓尺寸自动测量侧面）</param>
        /// <param name="jylsh">检验流水号</param>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <param name="hphm">号牌号码</param>
        /// <param name="hpzl">号牌种类</param>
        /// <param name="vin">车架号码</param>
        /// <returns>是否成功（成功为1、失败为0）</returns>
        public bool StopVideo(string xmmc, string jylsh, string jcbh, int jccs, string hphm, string hpzl, string vin)
        {
            if (jk_status == false)
                return false;
            try
            {
                string hpzl_temp = hpzl.Split('(')[1].Split(')')[0];
                IOControl.saveXmlLogInf("南京新仕尚联网，号牌号码：" + hphm + "|流水号：" + jylsh + "|检测次数：" + jccs.ToString() + "，项目（" + xmmc + "）录像结束");
                string result = outlineservice.StopVideo(xmmc, line_id, jylsh, jcbh, jccs, hphm, hpzl_temp, vin);
                IOControl.saveXmlLogInf("Received:号牌号码：" + hphm + " | 流水号：" + jylsh + " | 检测次数：" + jccs.ToString() + " | 项目：" + xmmc + "，发送结果：" + result);
                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception er)
            {
                IOControl.saveXmlLogInf("南京新仕尚联网发送录制视频结束失败，错误信息：\r\n" + er.Message);
                return false;
            }
        }
    }
}
