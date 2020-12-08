using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LwhUploadOnline
{
    public class UploadOnline
    {
        #region 变量定义
        #region 联网接口定义
        private AnChe ac_interface = null;
        private BaoHui bh_interface = null;
        private DaLei dl_interface = null;
        private GongAn ga_interface = null;
        private HaiCheng hc_interface = null;
        private HaiChengOld hc_interface_old = null;
        private GuangXi gx_interface = null;
        private HuaYan hy_interface = null;
        private HuBei hb_interface = null;
        private KangShiBai ksb_interface = null;
        private ShangRao ss_interface = null;
        private ShiShang njss_interface = null;
        private WanGuo wg_interface = null;
        private WeiKe wk_interface = null;
        private XinDun xd_interface = null;
        private YiZhongXiang yzx_interface = null;
        private XinLiYuan xly_interface = null;
        private ZhongHang zh_interface = null;
        #endregion

        private bool interface_status = false;//接口状态信息
        private UploadConfigModel softConfig = null;//软件配置信息
        private string GlobalDiretory = @"D:\外廓数据文件";

        #endregion

        #region 对外接口
        /// <summary>
        /// 联网接口dll状态，初始化成功后置为true，以此判定dll是否完成初始化
        /// </summary>
        public bool InterfaceDllStatus { get { return interface_status; } }

        /// <summary>
        /// 初始化联网接口dll（返回code为1时成功，其他为失败，失败时message记录失败信息）
        /// </summary>
        /// <param name="code">接口初始化结果代码</param>
        /// <param name="message">接口初始化结果信息</param>
        public void InitUploadOnline(out string code, out string message)
        {
            code = "0";
            message = "0";
            try
            {
                #region 读取联网上传陪着信息
                if (getUploadConfig(ref softConfig) == false)
                {
                    code = "-2";
                    message = "获取联网配置信息失败";
                    return;
                }
                #endregion

                #region 上传接口初始化
                DataTable dt_time = null;
                switch (softConfig.NetModel)
                {
                    case NetUploadModel.安车:
                        #region ac
                        ac_interface = new AnChe(softConfig.StationID, softConfig.Jkdz, softConfig.Xtlb, softConfig.Jkxlh, softConfig.CJBH, softConfig.DWJGDM, softConfig.DWMC, softConfig.YHBS, softConfig.YHXM, softConfig.ZDBS);
                        dt_time = ac_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            interface_status = true;
                            code = "1";
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                message = "接口初始化成功";
                            else
                                message = "接口初始化成功、同步本地时间失败";
                        }
                        else
                        {
                            code = "-3";
                            message = "接口初始化获取平台时间失败";
                        }
                        #endregion
                        break;
                    case NetUploadModel.安徽://-4
                        break;
                    case NetUploadModel.宝辉://-5
                        break;
                    case NetUploadModel.大雷:
                        #region dl
                        //待检列表未选择大雷联网，继续初始化大雷接口
                        dl_interface = new DaLei(softConfig.Jkdz);
                        dt_time = dl_interface.GetSystemDatetime(softConfig.StationID);
                        interface_status = true;
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            code = "1";
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                message = "接口初始化成功";
                            else
                                message = "接口初始化成功、同步本地时间失败";
                        }
                        else
                        { 
                            code = "-6";
                            message = "接口初始化获取平台时间失败";
                        }
                        #endregion
                        break;
                    case NetUploadModel.广西://-7
                        break;
                    case NetUploadModel.海城新疆://-8
                        break;
                    case NetUploadModel.海城四川://-9
                        break;
                    case NetUploadModel.华燕:
                        #region hy
                        hy_interface = new HuaYan(softConfig.Jkdz, softConfig.Xtlb, softConfig.Jkxlh);
                        dt_time = hy_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            interface_status = true;
                            code = "1";
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                message = "接口初始化成功";
                            else
                                message = "接口初始化成功、同步本地时间失败";
                        }
                        else
                        {
                            code = "-10";
                            message = "接口初始化获取平台时间失败";
                        }
                        #endregion
                        break;
                    case NetUploadModel.湖北://-11
                        break;
                    case NetUploadModel.康士柏://-12
                        break;
                    case NetUploadModel.欧润特://-13
                        break;
                    case NetUploadModel.上饶://-14
                        break;
                    case NetUploadModel.南京新仕尚://-15
                        break;
                    case NetUploadModel.万国://-16
                        break;
                    case NetUploadModel.维科://-17
                        break;
                    case NetUploadModel.新盾://-18
                        break;
                    case NetUploadModel.新力源://-19
                        break;
                    case NetUploadModel.益中祥://-20
                        break;
                    case NetUploadModel.中航://-21
                        break;
                    default:
                        #region 不支持联网方式
                        code = "-22";
                        message = "不支持的联网方式";
                        return;
                        #endregion
                }
                #endregion
            }
            catch (Exception er)
            {
                code = "-1";
                message = "初始化出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送外廓项目开始
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void LwhSendProjectStart(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    string code_temp = "";
                    string msg_temp = "";
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AcprojectStart projectstart_ac = new AcprojectStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            if (ac_interface.writeProjectStart(projectstart_ac, out code, out message) && code == "1")
                                message = "项目开始发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18C55 projectstart_dl = new dalei18C55(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            dl_interface.writeProjectStart(projectstart_dl, out code, out message);
                            if (softConfig.dl_Send18Jxx)
                            {
                                dalei18J11 teststart_dl = new dalei18J11(model.LSH, model.CLHP, model.HPZL, model.VIN, softConfig.WKDH, softConfig.LineID, "0");
                                dl_interface.write18J11(teststart_dl, out code_temp, out msg_temp);
                            }                            
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyProjectStart projectstart_hy = new HyProjectStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            hy_interface.writeProjectStart(projectstart_hy, out code, out message);

                            //再发录像开始  
                            HyVideoStart videostart_hy = new HyVideoStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH);
                            hy_interface.writeVideoStart(videostart_hy, out code_temp, out msg_temp);

                            if (code == "1" && code_temp == "1")
                                message = "发送项目开始/录像开始均成功";
                            else if (code == "1" && code_temp != "1")
                            {
                                code = "2";
                                message = "发送项目开始成功/录像开始失败";
                            }
                            else if (code != "1" && code_temp == "1")
                            {
                                code = "2";
                                message = "发送项目开始失败/录像开始成功";
                            }
                            else
                            {
                                code = "-11";
                                message = "发送项目开始/录像开始均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "LwhSendProjectStart上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送外廓前照
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void LwhSendFrontPic(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AccapturePicture pic_ac = new AccapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.WKDH, softConfig.WkFrontPicBh);
                            if (ac_interface.writeOutlineCapturePicture(pic_ac, out code, out message) && code == "1")
                                message = "外廓前照拍照命令发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkFrontPicBh);
                            dalei18J31 pic_dl2 = new dalei18J31(model.LSH, softConfig.StationID, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.WkFrontPicBh, "");
                            for (int i = 0; i < softConfig.PicSendTimes; i++)
                            {
                                if (softConfig.dl_Send18Jxx)// if (softConfig.NetArea == NetAreaModel.四川)
                                {
                                    if (dl_interface.write18J31(pic_dl2, out code, out message) && code == "1")
                                    {
                                        message = "外廓前照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else if (softConfig.dl_Send18H05)// else if (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "外廓前照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else
                                {
                                    code = "1";
                                    message = "不要求发送外廓前照命令地区，跳过拍前照命令";
                                    return;
                                }
                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                            }
                            /*
                            if (softConfig.dl_Send18H05)// (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                            {
                                dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkFrontPicBh);
                                for (int i = 0; i < softConfig.PicSendTimes; i++)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "外廓前照拍照命令发送成功";
                                        return;
                                    }
                                }

                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                            }
                            else
                            {
                                code = "1";
                                message = "不要求发送外廓前照命令地区，跳过拍前照命令";
                            }*/
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyCapturePicture pic_hy = new HyCapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkFrontPicBh, softConfig.WKDH);
                            if (hy_interface.writeOutlineCapturePicture(pic_hy, out code, out message) && code == "1")
                                message = "外廓前照拍照命令发送成功";
                            else
                                code = "-11";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "LwhSendFrontPic上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送外廓后照
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void LwhSendBackPic(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AccapturePicture pic_ac = new AccapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.WKDH, softConfig.WkBackPicBh);
                            if (ac_interface.writeOutlineCapturePicture(pic_ac, out code, out message) && code == "1")
                                message = "外廓后照拍照命令发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkBackPicBh);
                            dalei18J31 pic_dl2 = new dalei18J31(model.LSH, softConfig.StationID, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.WkBackPicBh, "");
                            for (int i = 0; i < softConfig.PicSendTimes; i++)
                            {
                                if (softConfig.dl_Send18Jxx)// if (softConfig.NetArea == NetAreaModel.四川)
                                {
                                    if (dl_interface.write18J31(pic_dl2, out code, out message) && code == "1")
                                    {
                                        message = "外廓后照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else if (softConfig.dl_Send18H05)// else if (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "外廓后照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else
                                {
                                    code = "1";
                                    message = "不要求发送外廓后照命令地区，跳过拍前照命令";
                                    return;
                                }
                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                            }
                            /*
                            if (softConfig.dl_Send18H05)// if (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                            {
                                dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkBackPicBh);
                                for (int i = 0; i < softConfig.PicSendTimes; i++)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "外廓后照拍照命令发送成功";
                                        return;
                                    }
                                }

                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                            }
                            else
                            {
                                code = "1";
                                message = "不要求发送外廓前照命令地区，跳过拍前照命令";
                            }*/
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyCapturePicture pic_hy = new HyCapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, softConfig.WkBackPicBh, softConfig.WKDH);
                            if (hy_interface.writeOutlineCapturePicture(pic_hy, out code, out message) && code == "1")
                                message = "外廓后照拍照命令发送成功";
                            else
                                code = "-11";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "LwhSendBackPic上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送外廓检测结果
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void LwhSendTestResult(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel result = getTestRecordModel(TestRecordJson);
                if (result != null)
                {
                    string code1 = "", code2 = "", msg1 = "", msg2 = "";
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            ActestDetailResult result_ac = new ActestDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.WKDH, result.JCCS, result.HPZL, result.CLHP, result.VIN, softConfig.WkDeviceID, result.LENGTHCLZ.ToString(), result.WIDTHCLZ.ToString(), result.HEIGHTCLZ.ToString(), (result.LENGTHPD == "不合格" || result.WIDTHPD == "不合格" || result.HEIGHTPD == "不合格") ? "2" : "1");
                            if (ac_interface.writetestDetailResult(result_ac, out code, out message) && code == "1")
                                message = "外廓检测结果发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            if (result.BY4 == "2")
                            {
                                //上传服务站
                                daleifwzTestDetailResult result_dlfwz = new daleifwzTestDetailResult(softConfig.StationID, result.LSH, result.HPZL, result.CLLX, result.CLHP, result.VIN, result.CLPP, "", result.LENGTHBZZ.ToString(), result.WIDTHBZZ.ToString(), result.HEIGHTBZZ.ToString(), result.LENGTHCLZ.ToString(), result.WIDTHCLZ.ToString(), result.HEIGHTCLZ.ToString(), result.LENGTHPD == "不合格" ? "2" : "1", result.WIDTHPD == "不合格" ? "2" : "1", result.HEIGHTPD == "不合格" ? "2" : "1", (result.LENGTHPD == "不合格" || result.WIDTHPD == "不合格" || result.HEIGHTPD == "不合格") ? "2" : "1", GlobalDiretory + "\\" + result.RecordId + "_front.jpg", GlobalDiretory + "\\" + result.RecordId + "_back.jpg", GlobalDiretory + "\\" + result.RecordId + "_frontLaser.jpg", result.ZBZLBZZ.ToString(), result.SCZBZL.ToString(), result.ZBZLPD == "不合格" ? "2" : "1");
                                dl_interface.writeFwztestDetailResult(result_dlfwz, out code2, out msg2);
                            }

                            //上传18W02
                            dl_interface.writeTestResult(result, out code1, out msg1);

                            //上传18C81
                            daleitestDetailResult result_dl = new daleitestDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.WKDH, result.JCCS, result.LENGTHCLZ.ToString(), result.LENGTHPD == "不合格" ? "2" : "1", result.WIDTHCLZ.ToString(), result.WIDTHPD == "不合格" ? "2" : "1", result.HEIGHTCLZ.ToString(), result.HEIGHTPD == "不合格" ? "2" : "1", "0", "0", "0", "0", "0", "0", result.ZZJCLZ.ToString(), result.ZZJPD == "不合格" ? "2" : "1", (result.LENGTHPD == "不合格" || result.WIDTHPD == "不合格" || result.HEIGHTPD == "不合格") ? "2" : "1");
                            dl_interface.writetestDetailResult(result_dl, out code, out message);

                            if (code == "1" && code1 == "1")
                                message = "18C81/18W02均上传成功";
                            else if (code != "1" && code1 == "1")
                            {
                                code = "2";
                                message = "18C81上传失败/18W02上传成功";
                            }
                            else if (code == "1" && code1 != "1")
                            {
                                code = "2";
                                message = "18C81上传成功/18W02上传失败";
                            }
                            else
                                message = "18C81/18W02均上传失败";

                            message += (result.BY4 == "2" ? (code2 == "1" ? "(上传服务站18J51成功)" : "(上传服务站18J51失败)") : "");
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyTestDetailResult result_hy = new HyTestDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.WKDH, result.JCCS, result.HPZL, result.CLHP, result.VIN, softConfig.WkDeviceID, result.LENGTHCLZ.ToString(), result.WIDTHCLZ.ToString(), result.HEIGHTCLZ.ToString(), (result.LENGTHPD == "合格" && result.WIDTHPD == "合格" && result.HEIGHTPD == "合格") ? "1" : "2", result.LENGTHPD == "合格" ? "1" : "2", result.WIDTHPD == "合格" ? "1" : "2", result.HEIGHTPD == "合格" ? "1" : "2", "", "");
                            if (hy_interface.writetestDetailResult(result_hy, out code, out message) && code == "1")
                                message = "外廓检测结果发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "LwhSendTestResult上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送外廓项目结束
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void LwhSendProjectFinish(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    string code_temp = "", msg_temp = "";
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AcprojectFinish projeckfinish = new AcprojectFinish(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            if (ac_interface.writeProjectFinish(projeckfinish, out code, out message) && code == "1")
                                message = "项目开始发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            //发18C58
                            dalei18C58 dlprojectfinish = new dalei18C58(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            dl_interface.writeProjectFinish(dlprojectfinish, out code, out message);

                            if (softConfig.dl_Send18Jxx)
                            {
                                //发18J12
                                dalei18J12 dl18j12 = new dalei18J12(model.LSH, model.CLHP, model.HPZL, model.VIN, softConfig.WKDH, softConfig.LineID, "0");
                                dl_interface.write18J12(dl18j12, out code_temp, out msg_temp);
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyVideoStop hyvideostop = new HyVideoStop(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH);
                            hy_interface.writeVideoStop(hyvideostop, out code_temp, out msg_temp);

                            //再发项目结束                           
                            HyProjectFinish hyprojectfinish = new HyProjectFinish(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.WkDeviceID, softConfig.WKDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            hy_interface.writeProjectFinish(hyprojectfinish, out code, out message);

                            if (code == "1" && code_temp == "1")
                                message = "发送项目结束/录像结束均成功";
                            else if (code == "1" && code_temp != "1")
                            {
                                code = "2";
                                message = "发送项目结束成功/录像结束失败";
                            }
                            else if (code != "1" && code_temp == "1")
                            {
                                code = "2";
                                message = "发送项目结束失败/录像结束成功";
                            }
                            else
                            {
                                code = "-11";
                                message = "发送项目结束/录像结束均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "LwhSendProjectFinish上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送整备质量项目开始
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void ZbzlSendProjectStart(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    string code_temp = "", msg_temp = "";
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AcprojectStart projectstart_ac = new AcprojectStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            if (ac_interface.writeProjectStart(projectstart_ac, out code, out message) && code == "1")
                                message = "整备质量项目开始发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18C55 projectstart_dl = new dalei18C55(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            dl_interface.writeProjectStart(projectstart_dl, out code, out message);
                            if (softConfig.dl_Send18Jxx)
                            {
                                dalei18J11 teststart_dl = new dalei18J11(model.LSH, model.CLHP, model.HPZL, model.VIN, softConfig.ZBZLDH, softConfig.LineID, "0");
                                dl_interface.write18J11(teststart_dl, out code_temp, out msg_temp);
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyProjectStart projectstart_hy = new HyProjectStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            hy_interface.writeProjectStart(projectstart_hy, out code, out message);

                            //再发录像开始  
                            HyVideoStart videostart_hy = new HyVideoStart(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH);
                            hy_interface.writeVideoStart(videostart_hy, out code_temp, out msg_temp);

                            if (code == "1" && code_temp == "1")
                                message = "整备质量发送项目开始/录像开始均成功";
                            else if (code == "1" && code_temp != "1")
                            {
                                code = "2";
                                message = "整备质量发送项目开始成功/录像开始失败";
                            }
                            else if (code != "1" && code_temp == "1")
                            {
                                code = "2";
                                message = "整备质量发送项目开始失败/录像开始成功";
                            }
                            else
                            {
                                code = "-11";
                                message = "整备质量发送项目开始/录像开始均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "ZbzlSendProjectStart上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送整备质量前照
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void ZbzlSendFrontPic(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AccapturePicture pic_ac = new AccapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.ZBZLDH, softConfig.ZbzlFrontPicBh);
                            if (ac_interface.writeOutlineCapturePicture(pic_ac, out code, out message) && code == "1")
                                message = "整备质量前照拍照命令发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, softConfig.ZbzlFrontPicBh);
                            dalei18J31 pic_dl2 = new dalei18J31(model.LSH, softConfig.StationID, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.ZbzlFrontPicBh, "");
                            for (int i = 0; i < softConfig.PicSendTimes; i++)
                            {
                                if (softConfig.dl_Send18Jxx)// if (softConfig.NetArea == NetAreaModel.四川)
                                {
                                    if (dl_interface.write18J31(pic_dl2, out code, out message) && code == "1")
                                    {
                                        message = "整备质量前照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else if (softConfig.dl_Send18H05)// else if (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "整备质量前照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else
                                { 
                                    code = "1";
                                    message = "不要求发送整备质量前照命令地区，跳过拍前照命令";
                                    return;
                                }
                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyCapturePicture pic_hy = new HyCapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, softConfig.ZbzlFrontPicBh, "W1");
                            if (hy_interface.writeOutlineCapturePicture(pic_hy, out code, out message) && code == "1")
                                message = "整备质量前照拍照命令发送成功";
                            else
                                code = "-11";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "ZbzlSendFrontPic上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送整备质量后照
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void ZbzlSendBackPic(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AccapturePicture pic_ac = new AccapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.ZBZLDH, softConfig.ZbzlBackPicBh);
                            if (ac_interface.writeOutlineCapturePicture(pic_ac, out code, out message) && code == "1")
                                message = "整备质量后照拍照命令发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            dalei18H05 pic_dl = new dalei18H05(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, softConfig.ZbzlBackPicBh);
                            dalei18J31 pic_dl2 = new dalei18J31(model.LSH, softConfig.StationID, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), softConfig.ZbzlBackPicBh, "");

                            for (int i = 0; i < softConfig.PicSendTimes; i++)
                            {
                                if(softConfig.dl_Send18Jxx)// (softConfig.NetArea == NetAreaModel.四川)
                                {
                                    if (dl_interface.write18J31(pic_dl2, out code, out message) && code == "1")
                                    {
                                        message = "整备质量后照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else if(softConfig.dl_Send18H05)// (softConfig.NetArea == NetAreaModel.安徽 || softConfig.NetArea == NetAreaModel.湖南 || softConfig.NetArea == NetAreaModel.云南 || softConfig.NetArea == NetAreaModel.重庆)
                                {
                                    if (dl_interface.write18H05(pic_dl, out code, out message) && code == "1")
                                    {
                                        message = "整备质量后照拍照命令发送成功";
                                        return;
                                    }
                                }
                                else
                                {
                                    code = "1";
                                    message = "不要求发送整备质量前照命令地区，跳过拍前照命令";
                                    return;
                                }

                                code = "-7";
                                message = "连续" + softConfig.PicSendTimes.ToString() + "次发送照片命令失败";
                                
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyCapturePicture pic_hy = new HyCapturePicture(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, softConfig.ZbzlBackPicBh, softConfig.ZBZLDH);
                            if (hy_interface.writeOutlineCapturePicture(pic_hy, out code, out message) && code == "1")
                                message = "整备质量后照拍照命令发送成功";
                            else
                                code = "-11";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "ZbzlSendBackPic上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送整备质量检测结果
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void ZbzlSendTestResult(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel result = getTestRecordModel(TestRecordJson);
                if (result != null)
                {
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            acZbzlDetailResult result_ac = new acZbzlDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.ZBZLDH, result.JCCS, result.HPZL, result.CLHP, result.VIN, softConfig.ZbzlDeviceID, result.SCZBZL.ToString(), result.ZBZLPD == "不合格 " ? "2" : "1");
                            if (ac_interface.writeZbzlTestDetailResult(result_ac, out code, out message) && code == "1")
                                message = "整备质量检测结果发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            //上传18C81
                            daleizbzlDetailResult result_dl = new daleizbzlDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.ZBZLDH, result.JCCS, result.SCZBZL.ToString(), result.ZBZLPD == "合格" ? "1" : "2");
                            if (dl_interface.writezbzlDetailResult(result_dl, out code, out message) && code == "1")
                                message = "整备质量检测结果发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyTestDetailResult result_hy = new HyTestDetailResult(result.LSH, softConfig.StationID, softConfig.LineID, softConfig.ZBZLDH, result.JCCS, result.HPZL, result.CLHP, result.VIN, softConfig.ZbzlDeviceID, result.LENGTHCLZ.ToString(), result.WIDTHCLZ.ToString(), result.HEIGHTCLZ.ToString(), (result.LENGTHPD == "合格" && result.WIDTHPD == "合格" && result.HEIGHTPD == "合格") ? "1" : "2", result.LENGTHPD == "合格" ? "1" : "2", result.WIDTHPD == "合格" ? "1" : "2", result.HEIGHTPD == "合格" ? "1" : "2", "", "");
                            if (hy_interface.writetestDetailResult(result_hy, out code, out message) && code == "1")
                                message = "整备质量检测结果发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "ZbzlSendTestResult上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }

        /// <summary>
        /// 发送整备质量项目结束
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void ZbzlSendProjectFinish(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    string code_temp = "", msg_temp = "";
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            AcprojectFinish projeckfinish = new AcprojectFinish(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            if (ac_interface.writeProjectFinish(projeckfinish, out code, out message) && code == "1")
                                message = "项目开始发送成功";
                            else
                                code = "-4";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            
                            //发18C58
                            dalei18C58 dlprojectfinish = new dalei18C58(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            dl_interface.writeProjectFinish(dlprojectfinish, out code, out message);
                            if (softConfig.dl_Send18Jxx)
                            {
                                //发18J12
                                dalei18J12 dl18j12 = new dalei18J12(model.LSH, model.CLHP, model.HPZL, model.VIN, softConfig.ZBZLDH, softConfig.LineID, "0");
                                dl_interface.write18J12(dl18j12, out code_temp, out msg_temp);
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            HyVideoStop hyvideostop = new HyVideoStop(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH);
                            hy_interface.writeVideoStop(hyvideostop, out code_temp, out msg_temp);

                            //再发项目结束                           
                            HyProjectFinish hyprojectfinish = new HyProjectFinish(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, softConfig.ZbzlDeviceID, softConfig.ZBZLDH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            hy_interface.writeProjectFinish(hyprojectfinish, out code, out message);

                            if (code == "1" && code_temp == "1")
                                message = "整备质量发送项目结束/录像结束均成功";
                            else if (code == "1" && code_temp != "1")
                            {
                                code = "2";
                                message = "整备质量发送项目结束成功/录像结束失败";
                            }
                            else if (code != "1" && code_temp == "1")
                            {
                                code = "2";
                                message = "整备质量发送项目结束失败/录像结束成功";
                            }
                            else
                            {
                                code = "-11";
                                message = "整备质量发送项目结束/录像结束均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "ZbzlSendProjectFinish上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }
        
        /// <summary>
        /// 发送照片
        /// </summary>
        /// <param name="TestRecordJson"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void SendPicFile(string TestRecordJson, out string code, out string message)
        {
            code = "0";
            message = "0";
            if (interface_status == false)
            {
                message = "接口未能正常初始化";
                return;
            }

            try
            {
                TestRecordModel model = getTestRecordModel(TestRecordJson);
                if (model != null)
                {
                    string imagePathFront = model.FILEPATH + "\\" + model.RecordId + "_front.jpg";
                    string imagePathBack = model.FILEPATH + "\\" + model.RecordId + "_back.jpg";
                    string imagePathFrontLaser = model.FILEPATH + "\\" + model.RecordId + "_frontLaser.jpg";
                    string imagePathTopLaser = model.FILEPATH + "\\" + model.RecordId + "_backLaser.jpg";
                    string imagePathFrontZbzl = model.FILEPATH + "\\" + model.RecordId + "_frontZbzl.jpg";
                    string imagePathBackZbzl = model.FILEPATH + "\\" + model.RecordId + "_backZbzl.jpg";
                    imagePathFront = GlobalDiretory + "\\" + imagePathFront.Remove(0, 1);
                    imagePathBack = GlobalDiretory + "\\" + imagePathBack.Remove(0, 1);
                    imagePathFrontLaser = GlobalDiretory + "\\" + imagePathFrontLaser.Remove(0, 1);
                    imagePathTopLaser = GlobalDiretory + "\\" + imagePathTopLaser.Remove(0, 1);
                    imagePathFrontZbzl = GlobalDiretory + "\\" + imagePathFrontZbzl.Remove(0, 1);
                    imagePathBackZbzl = GlobalDiretory + "\\" + imagePathBackZbzl.Remove(0, 1);
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            code = "1";
                            message = "安车联网不上传照片文件";
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            #region ah

                            #endregion
                            break;
                        case NetUploadModel.宝辉:
                            #region bh

                            #endregion
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            string dl_code1 = "", dl_code2 = "", dl_code3 = "", dl_code4 = "", dl_code5 = "", dl_code6 = "", dl_msg1 = "", dl_msg2 = "", dl_msg3 = "", dl_msg4 = "", dl_msg5 = "", dl_msg6 = "";
                            dalei18C63 photo_dl = new dalei18C63(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.CLHP, model.HPZL, model.VIN, imagePathFront, model.JCSJ.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, "0360");
                            if (model.LWHBJ)
                            {
                                if (softConfig.dl_Send18Jxx)//(softConfig.NetArea == NetAreaModel.四川 || softConfig.NetArea == NetAreaModel.成都 || softConfig.NetArea == NetAreaModel.自贡 || softConfig.NetArea == NetAreaModel.江西)
                                {
                                    //发0360、0361
                                    dl_interface.write18C63(photo_dl, out dl_code1, out dl_msg1);

                                    photo_dl = new dalei18C63(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.CLHP, model.HPZL, model.VIN, imagePathBack, model.JCKSSJ.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, "0361");
                                    dl_interface.write18C63(photo_dl, out dl_code2, out dl_msg2);
                                }
                                else
                                {
                                    dl_code1 = "1";
                                    dl_code2 = "1";
                                }

                                //发二维图60、62
                                dl_interface.writeTestImage(model.LSH, model.CLHP, model.HPZL, model.VIN, imagePathFrontLaser, "60", out dl_code3, out dl_msg3);
                                dl_interface.writeTestImage(model.LSH, model.CLHP, model.HPZL, model.VIN,imagePathTopLaser, "62", out dl_code4, out dl_msg4);
                            }
                            else
                            {
                                dl_code1 = "1";
                                dl_code2 = "1";
                                dl_code3 = "1";
                                dl_code4 = "1";
                            }

                            if (model.ZBZLBJ)
                            {
                                //发0362、0363
                                if (softConfig.dl_Send18Jxx)//
                                {
                                    photo_dl = new dalei18C63(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.CLHP, model.HPZL, model.VIN, imagePathFrontZbzl, model.JCSJ.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, "0362");
                                    dl_interface.write18C63(photo_dl, out dl_code5, out dl_msg5);

                                    photo_dl = new dalei18C63(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.CLHP, model.HPZL, model.VIN, imagePathBackZbzl, model.JCJSSJ.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.ZBZLDH, "0363");
                                    dl_interface.write18C63(photo_dl, out dl_code6, out dl_msg6);
                                }
                            }
                            else
                            {
                                dl_code5 = "1";
                                dl_code6 = "1";
                            }

                            if (dl_code1 == "1" && dl_code2 == "1" && dl_code3 == "1" && dl_code4 == "1" && dl_code5 == "1" && dl_code6 == "1")
                            {
                                code = "1";
                                message = "照片0360、0361、60、62、0362及0363发送均成功";
                            }
                            else if ((model.LWHBJ && (dl_code1 == "1" || dl_code2 == "1" || dl_code3 == "1" || dl_code4 == "1")) || (model.ZBZLBJ && (dl_code5 == "1" || dl_code6 == "1")))
                            {
                                code = "2";
                                message = "发送照片|" + (model.LWHBJ ? ("0360:" + (dl_code1 == "1" ? "成功" : "失败") + "|0361:" + (dl_code2 == "1" ? "成功" : "失败") + "|60:" + (dl_code3 == "1" ? "成功" : "失败") + "|62:" + (dl_code4 == "1" ? "成功" : "失败")) : "") +
                                                        (model.ZBZLBJ ? ("|0362:" + (dl_code5 == "1" ? "成功" : "失败") + "|0363:" + (dl_code6 == "1" ? "成功" : "失败")) : "");
                            }
                            else
                            {
                                code = "-7";
                                message = "照片" + (model.LWHBJ ? "0360、0361、60、62、" : "") + (model.ZBZLBJ ? "0362、0363" : "") + "发送均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            #region gx

                            #endregion
                            break;
                        case NetUploadModel.海城新疆:
                            #region hc_xj

                            #endregion
                            break;
                        case NetUploadModel.海城四川:
                            #region hc_sc

                            #endregion
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            string hy_code1 = "", hy_code2 = "", hy_code3 = "", hy_code4 = "", hy_msg1 = "", hy_msg2 = "", hy_msg3 = "", hy_msg4 = "";
                            HyPhoto photo = new HyPhoto(model.LSH, softConfig.StationID, softConfig.LineID, model.JCCS, model.HPZL, model.CLHP, model.VIN, model.JCSJ.ToString("yyyy-MM-dd HH:mm:ss"), softConfig.WKDH, "0360", GlobalDiretory + "\\" + model.RecordId + "_front.jpg");
                            if (model.LWHBJ)
                            {
                                //发0360
                                photo.pssj = model.JCKSSJ.ToString("yyyy-MM-dd HH:mm:ss");
                                photo.zpzl = "0360";
                                photo.zp = imagePathFront;
                                hy_interface.writePhoto(photo, out hy_code1, out hy_msg1);

                                //发0361
                                photo.pssj = model.JCKSSJ.ToString("yyyy-MM-dd HH:mm:ss");
                                photo.zpzl = "0361";
                                photo.zp = imagePathBack;
                                hy_interface.writePhoto(photo, out hy_code2, out hy_msg2);
                            }
                            else
                            {
                                hy_code1 = "1";
                                hy_code2 = "1";
                            }

                            if (model.ZBZLBJ)
                            {
                                //发0362
                                photo.jyxm = softConfig.ZBZLDH;
                                photo.pssj = model.JCSJ.ToString("yyyy-MM-dd HH:mm:ss");
                                photo.zpzl = "0362";
                                photo.zp = imagePathFrontZbzl;
                                hy_interface.writePhoto(photo, out hy_code3, out hy_msg3);

                                //发0363
                                photo.pssj = model.JCJSSJ.ToString("yyyy-MM-dd HH:mm:ss");
                                photo.zpzl = "0363";
                                photo.zp = imagePathBackZbzl;
                                hy_interface.writePhoto(photo, out hy_code4, out hy_msg4);
                            }
                            else
                            {
                                hy_code3 = "1";
                                hy_code4 = "1";
                            }

                            if (((model.LWHBJ && hy_code1 == "1" && hy_code2 == "1") || model.LWHBJ == false) &&
                                ((model.ZBZLBJ && hy_code3 == "1" && hy_code4 == "1") || model.ZBZLBJ == false))
                            {
                                code = "1";
                                message = "照片0360、0361、0362及0363发送均成功";
                            }
                            else if ((model.LWHBJ && (hy_code1 == "1" || hy_code2 == "1")) || (model.ZBZLBJ && (hy_code3 == "1" || hy_code4 == "1")))
                            {
                                code = "2";
                                message = "发送照片|" + (model.LWHBJ ? ("0360:" + (hy_code1 == "1" ? "成功" : "失败") + "|0361:" + (hy_code2 == "1" ? "成功" : "失败")) : "") + 
                                                        (model.ZBZLBJ ? ("|0362:" + (hy_code3 == "1" ? "成功" : "失败") + "|0363:" + (hy_code4 == "1" ? "成功" : "失败")) : "");
                            }
                            else
                            {
                                code = "-11";
                                message = "照片" + (model.LWHBJ ? "0360、0361、" : "") + (model.ZBZLBJ ? "0362、0363" : "") + "发送均失败";
                            }
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            #region hb

                            #endregion
                            break;
                        case NetUploadModel.康士柏:
                            #region ac

                            #endregion
                            break;
                        case NetUploadModel.欧润特:
                            #region ort

                            #endregion
                            break;
                        case NetUploadModel.上饶:
                            #region sr

                            #endregion
                            break;
                        case NetUploadModel.南京新仕尚:
                            #region njxss

                            #endregion
                            break;
                        case NetUploadModel.万国:
                            #region wg

                            #endregion
                            break;
                        case NetUploadModel.维科:
                            #region wk

                            #endregion
                            break;
                        case NetUploadModel.新盾:
                            #region xd

                            #endregion
                            break;
                        case NetUploadModel.新力源:
                            #region xly

                            #endregion
                            break;
                        case NetUploadModel.益中祥:
                            #region yzx

                            #endregion
                            break;
                        case NetUploadModel.中航:
                            #region zh

                            #endregion
                            break;
                        default:
                            #region 未知联网方式
                            code = "-3";
                            message = "不支持的联网方式";
                            return;
                            #endregion
                    }
                }
                else
                {
                    code = "-2";
                    message = "转换Json数据出错";
                }
            }
            catch (Exception er)
            {
                code = "-1";
                message = "SendPicFile上传过程出错" + er.Message;
                IOControl.WriteLogs(message);
            }
        }
        #endregion

        #region 内部功能函数
        /// <summary>
        /// 获取联网软件配置信息
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool getUploadConfig(ref UploadConfigModel config)
        {
            try
            {
                config = new UploadConfigModel();

                string config_path = @"D:\外廓数据文件\uploadConfig.ini";
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                int i = 0;
                
                IOControl.GetPrivateProfileString("联网配置", "联网模式", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.NetModel = (NetUploadModel)i;
                else
                    config.NetModel = NetUploadModel.安车;

                IOControl.GetPrivateProfileString("联网配置", "联网地区", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.NetArea = (NetAreaModel)i;
                else
                    config.NetArea = NetAreaModel.四川;
                
                IOControl.GetPrivateProfileString("联网配置", "接口地址", "", temp, 2048, config_path);
                config.Jkdz = temp.ToString().Trim();
                                
                IOControl.GetPrivateProfileString("联网配置", "接口序列号", "", temp, 2048, config_path);
                config.Jkxlh = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "本机IP地址", "", temp, 2048, config_path);
                config.LocalIP = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "系统类别", "", temp, 2048, config_path);
                config.Xtlb = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "检测站编号", "", temp, 2048, config_path);
                config.StationID = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "检测线编号", "", temp, 2048, config_path);
                config.LineID = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "外廓工位号", "", temp, 2048, config_path);
                config.WkDeviceID = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "整备质量工位号", "", temp, 2048, config_path);
                config.ZbzlDeviceID = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "外廓前照编号", "", temp, 2048, config_path);
                config.WkFrontPicBh = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "外廓后照编号", "", temp, 2048, config_path);
                config.WkBackPicBh = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "整备质量前照编号", "", temp, 2048, config_path);
                config.ZbzlFrontPicBh = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "整备质量后照编号", "", temp, 2048, config_path);
                config.ZbzlBackPicBh = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "照片上传次数", "", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i) && i > 0)
                    config.PicSendTimes = i;
                else
                    config.PicSendTimes = 1;

                IOControl.GetPrivateProfileString("联网配置", "场景编号", "", temp, 2048, config_path);
                config.CJBH = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "单位名称", "", temp, 2048, config_path);
                config.DWMC = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "单位机构代码", "", temp, 2048, config_path);
                config.DWJGDM = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "用户标识", "", temp, 2048, config_path);
                config.YHBS = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "用户姓名", "", temp, 2048, config_path);
                config.YHXM = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "终端标识", "", temp, 2048, config_path);
                config.ZDBS = temp.ToString().Trim();

                return true;
            }
            catch (Exception er)
            {
                IOControl.WriteLogs("获取联网配置出错：" + er.Message);
                return false;
            }
        }

        /// <summary>
        /// 解析Json数据为TestRecordModel类
        /// </summary>
        /// <param name="jsonStr">Json数据字符串</param>
        /// <returns></returns>
        private TestRecordModel getTestRecordModel(string jsonStr)
        {
            TestRecordModel model = null;
            try
            {
                model = JsonConvert.DeserializeObject<TestRecordModel>(jsonStr);
                return model;
            }
            catch (Exception er)
            {
                IOControl.WriteLogs("转换Json数据为TestRecordModel类失败：" + er.Message);
                return null;
            }
        }        
        #endregion
    }
}
