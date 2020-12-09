using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSendWaitCar
{
    public partial class FrmMain : Form
    {
        #region 联网接口定义
        private LwhUploadOnline.AnChe ac_interface = null;
        private LwhUploadOnline.BaoHui bh_interface = null;
        private LwhUploadOnline.DaLei dl_interface = null;
        private LwhUploadOnline.GongAn ga_interface = null;
        private LwhUploadOnline.HaiCheng hc_interface = null;
        private LwhUploadOnline.HaiChengOld hc_interface_old = null;
        private LwhUploadOnline.GuangXi gx_interface = null;
        private LwhUploadOnline.HuaYan hy_interface = null;
        private LwhUploadOnline.HuBei hb_interface = null;
        private LwhUploadOnline.KangShiBai ksb_interface = null;
        private LwhUploadOnline.ShangRao ss_interface = null;
        private LwhUploadOnline.ShiShang njss_interface = null;
        private LwhUploadOnline.WanGuo wg_interface = null;
        private LwhUploadOnline.WeiKe wk_interface = null;
        private LwhUploadOnline.XinDun xd_interface = null;
        private LwhUploadOnline.YiZhongXiang yzx_interface = null;
        private LwhUploadOnline.XinLiYuan xly_interface = null;
        private LwhUploadOnline.ZhongHang zh_interface = null;
        #endregion

        private string global_path = @"D:\外廓数据文件";
        /// <summary>
        /// 联网配置信息
        /// </summary>
        private LwhUploadOnline.UploadConfigModel softConfig = new LwhUploadOnline.UploadConfigModel();
        /// <summary>
        /// 待检车辆列表
        /// </summary>
        private List<WaitCarModel> WaitCarList = new List<WaitCarModel>();
        /// <summary>
        /// 主车待检车辆信息
        /// </summary>
        private WaitCarModel waitCarInfoZhu = null;
        /// <summary>
        /// 挂车待检车辆信息
        /// </summary>
        private WaitCarModel waitCarInfoGua = null;
        /// <summary>
        /// 大雷联网检测方式（用于确定是否要上传服务站）
        /// </summary>
        private string daleijcfs = "1";

        private DataTable dtDgvSource = null;

        /// <summary>
        /// 检测程序用待检车辆信息
        /// </summary>
        private string CarInfoPath = @"C:\jcdatatxt\carinfo.ini";
        private string CarInfoPathTemp = @"C:\jcdatatxt\carinfo_temp.ini";


        public FrmMain()
        {
            InitializeComponent();
            pCarList.Location = new Point(2, 116);
            pCarSingle.Location = new Point(2, 116);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmMain1_Load(object sender, EventArgs e)
        {
            #region 获取配置信息
            LwhUploadOnline.UploadConfigModel config_temp = LwhUploadOnline.configControl.getUploadConfig();
            if (config_temp != null)
                softConfig = config_temp;
            else
            {
                MessageBox.Show("获取配置信息失败，请检测配置文件是否存在并重启软件重新配置！");
                return;
            }
            #endregion

            #region 初始化联网列表数据源
            dtDgvSource = new DataTable();
            dtDgvSource.Columns.Add("检验流水号");
            dtDgvSource.Columns.Add("检验次数");
            dtDgvSource.Columns.Add("车辆号牌");
            dtDgvSource.Columns.Add("号牌种类");
            dtDgvSource.Columns.Add("外廓");
            dtDgvSource.Columns.Add("称重");
            dtDgvSource.Columns.Add("长");
            dtDgvSource.Columns.Add("宽");
            dtDgvSource.Columns.Add("高");
            dtDgvSource.Columns.Add("整备质量");
            dtDgvSource.Columns.Add("轴距");
            #endregion

            #region 初始化联网接口
            DataTable dt_time = null;
            if (softConfig.WaitCarModel == LwhUploadOnline.NetWaitCarModel.大雷联网列表)
            {
                #region 大雷联网列表
                dl_interface = new LwhUploadOnline.DaLei(softConfig.JkdzWaitCar);
                dt_time = dl_interface.GetSystemDatetime(softConfig.StationID);
                if (dt_time != null && dt_time.Rows.Count > 0)
                {
                    if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())) == false)
                        MessageBox.Show("同步平台时间失败！");
                }
                else
                    MessageBox.Show("获取平台时间失败！");

                pCarList.Visible = true;
                #endregion
            }
            else if (softConfig.WaitCarModel == LwhUploadOnline.NetWaitCarModel.华燕联网列表)
            {
                #region 华燕联网列表
                hy_interface = new LwhUploadOnline.HuaYan(softConfig.JkdzWaitCar, softConfig.Xtlb, softConfig.JkxlhWaitCar);
                dt_time = hy_interface.GetSystemDatetime(softConfig.StationID);
                if (dt_time != null && dt_time.Rows.Count > 0)
                {
                    if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())) == false)
                        MessageBox.Show("同步平台时间失败！");
                }
                else
                    MessageBox.Show("获取平台时间失败！");

                pCarList.Visible = true;
                #endregion
            }
            else
            {
                #region 联网查询方式
                switch (softConfig.NetModel)
                {
                    case LwhUploadOnline.NetUploadModel.安车:
                        #region ac
                        ac_interface = new LwhUploadOnline.AnChe(softConfig.StationID, softConfig.Jkdz, softConfig.Xtlb, softConfig.Jkxlh, softConfig.CJBH, softConfig.DWJGDM, softConfig.DWMC, softConfig.YHBS, softConfig.YHXM, softConfig.ZDBS);
                        dt_time = ac_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())) == false)
                                MessageBox.Show("同步平台时间失败！");
                        }
                        else
                            MessageBox.Show("获取平台时间失败！");
                        #endregion
                        break;
                    case LwhUploadOnline.NetUploadModel.安徽:
                        break;
                    case LwhUploadOnline.NetUploadModel.宝辉:
                        break;
                    case LwhUploadOnline.NetUploadModel.大雷:
                        #region dl
                        if (softConfig.WaitCarModel != LwhUploadOnline.NetWaitCarModel.大雷联网列表)
                        {
                            dl_interface = new LwhUploadOnline.DaLei(softConfig.Jkdz);
                            dt_time = dl_interface.GetSystemDatetime(softConfig.StationID);
                            if (dt_time != null && dt_time.Rows.Count > 0)
                            {
                                if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())) == false)
                                    MessageBox.Show("同步平台时间失败！");
                            }
                            else
                                MessageBox.Show("获取平台时间失败！");
                        }
                        #endregion
                        break;
                    case LwhUploadOnline.NetUploadModel.广西:
                        break;
                    case LwhUploadOnline.NetUploadModel.海城新疆:
                        break;
                    case LwhUploadOnline.NetUploadModel.海城四川:
                        break;
                    case LwhUploadOnline.NetUploadModel.华燕:
                        #region hy
                        hy_interface = new LwhUploadOnline.HuaYan(softConfig.Jkdz, softConfig.Xtlb, softConfig.Jkxlh);
                        dt_time = hy_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())) == false)
                                MessageBox.Show("同步平台时间失败！");
                        }
                        else
                            MessageBox.Show("获取平台时间失败！");
                        #endregion
                        break;
                    case LwhUploadOnline.NetUploadModel.湖北:
                        break;
                    case LwhUploadOnline.NetUploadModel.康士柏:
                        break;
                    case LwhUploadOnline.NetUploadModel.欧润特:
                        break;
                    case LwhUploadOnline.NetUploadModel.上饶:
                        break;
                    case LwhUploadOnline.NetUploadModel.南京新仕尚:
                        break;
                    case LwhUploadOnline.NetUploadModel.万国:
                        break;
                    case LwhUploadOnline.NetUploadModel.维科:
                        break;
                    case LwhUploadOnline.NetUploadModel.新盾:
                        break;
                    case LwhUploadOnline.NetUploadModel.新力源:
                        break;
                    case LwhUploadOnline.NetUploadModel.益中祥:
                        break;
                    case LwhUploadOnline.NetUploadModel.中航:
                        break;
                    default:
                        MessageBox.Show("不支持的联网方式！");
                        return;
                }

                pCarSingle.Visible = true;
                #endregion
            }
            #endregion

            btSeach.Enabled = true;
        }

        /// <summary>
        /// 待检车辆查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSeach_Click(object sender, EventArgs e)
        {
            if (pCarList.Visible)
            {
                #region 待检列表查询
                if (WaitCarList != null && WaitCarList.Count > 0 && (tbJYLSH.Text != "" || tbHPHM.Text != "" || cbbHPZL.Text != "" || tbVIN.Text != "" || cbbJYLB.Text != ""))
                {
                    #region dgv有数据且有查询条件时，模糊查询lvWaitCarList中对应条件的待检车辆信息
                    List<WaitCarModel> waitcar_temp = new List<WaitCarModel>();
                    for (int i = 0; i < WaitCarList.Count; i++)
                    {
                        WaitCarModel temp = WaitCarList[i];
                        if ((tbJYLSH.Text != "" && temp.WGJYH.Contains(tbJYLSH.Text)) ||
                             tbHPHM.Text != "" && temp.CLPH.Contains(tbHPHM.Text) ||
                             cbbHPZL.Text != "" && cbbHPZL.Text.Contains(temp.HPZL) ||
                             tbVIN.Text != "" && temp.VIN.Contains(tbVIN.Text) ||
                             ((cbbJYLB.SelectedIndex == 0 && temp.JCLX == "1")||(cbbJYLB.SelectedIndex > 0 && temp.JCLX == "0")))
                            waitcar_temp.Add(temp);
                    }

                    ShowWaitCarList(waitcar_temp);//刷新待检列表
                    #endregion
                }
                else
                {
                    #region dgv无数据、有数据但无查询条件时查询平台数据，从平台查询待检车辆信息
                    刷新待检列表ToolStripMenuItem.PerformClick();
                    #endregion
                }
                #endregion
            }
            else
            {
                #region 单车待检车辆信息查询，从平台查询对应条件的待检车辆信息
                #region 初始化查询显示结果
                //清空待检车辆信息
                lbJYLSH.Text = "-";
                lbJYLB.Text = "-";
                lbCPHM.Text = "-";
                lbHPZL.Text = "-";
                lbVIN.Text = "-";
                lbCLLX.Text = "-";
                lbLWHBZ.Text = "-";
                lbHXBZ.Text = "-";
                lbZJBZ.Text = "-";
                lbLBBZ.Text = "-";
                lbZBZLBZ.Text = "-";

                //初始化检测项目
                ckLWH_s.Checked = false;
                ckZJ_s.Checked = false;
                ckLB_s.Checked = false;
                ckHX_s.Checked = false;
                ckZBZL_s.Checked = false;
                #endregion

                WaitCarModel waitcar_single = new WaitCarModel();
                string code, msg;
                DataTable dt_WaitCar = null;
                try
                {
                    switch (softConfig.NetModel)
                    {
                        case LwhUploadOnline.NetUploadModel.安车:
                            #region ac
                            dt_WaitCar = ac_interface.GetVehicleInf(tbHPHM.Text, cbbHPZL.Text, tbVIN.Text, out code, out msg);
                            if (dt_WaitCar != null && dt_WaitCar.Rows.Count > 0)
                            {
                                DataRow drWaitCar = dt_WaitCar.Rows[0];
                                waitcar_single.WGJYH = drWaitCar["jylsh"].ToString();
                                waitcar_single.JCCS = drWaitCar["jycs"].ToString();
                                waitcar_single.CLPH = drWaitCar["hphm"].ToString();
                                waitcar_single.JCLX = drWaitCar["jylb"].ToString() == "00" ? "1" : "0";
                                waitcar_single.HPYS = "";
                                waitcar_single.HPZL = drWaitCar["hpzl"].ToString();
                                waitcar_single.FDJHM = drWaitCar["fdjh"].ToString();
                                waitcar_single.PPXH = drWaitCar["clpp1"].ToString();
                                waitcar_single.VIN = drWaitCar["clsbdh"].ToString();
                                waitcar_single.CLLX = drWaitCar["cllx"].ToString();
                                waitcar_single.CZ = drWaitCar["syr"].ToString();
                                waitcar_single.CD = drWaitCar["cwkc"].ToString();
                                waitcar_single.KD = drWaitCar["cwkk"].ToString();
                                waitcar_single.GD = drWaitCar["cwkg"].ToString();
                                waitcar_single.HXCD = "0";
                                waitcar_single.HXKD = "0";
                                waitcar_single.HXGD = "0";
                                waitcar_single.LBGD = "0";
                                waitcar_single.ZS = drWaitCar["zs"].ToString() == "" ? "0" : drWaitCar["zs"].ToString();
                                waitcar_single.ZJ1 = drWaitCar["zj"].ToString() == "" ? "0" : drWaitCar["zj"].ToString();
                                waitcar_single.ZJ2 = "0";
                                waitcar_single.ZJ3 = "0";
                                waitcar_single.ZJ4 = "0";
                                waitcar_single.ZBZL = drWaitCar["zbzl"].ToString() == "" ? "0" : drWaitCar["zbzl"].ToString();
                                waitcar_single.SCZBZL = "0";
                                waitcar_single.ZDZZL = drWaitCar["zzl"].ToString() == "" ? "0" : drWaitCar["zzl"].ToString();

                                waitcar_single.QYCHP = "";
                                waitcar_single.SFAZWB = "";
                                waitcar_single.WBZL = "";
                                waitcar_single.SFAZQTBJ = "";
                                waitcar_single.QTBJZL = "";
                                waitcar_single.QTBJSM = "";

                                waitcar_single.IsReadyToTest = true;
                            }
                            else
                            {
                                MessageBox.Show("查询待检车辆信息失败：" + msg);
                                return;
                            }
                            #endregion
                            break;
                        case LwhUploadOnline.NetUploadModel.安徽:
                            break;
                        case LwhUploadOnline.NetUploadModel.宝辉:
                            break;
                        case LwhUploadOnline.NetUploadModel.大雷:
                            #region dl
                            dt_WaitCar = dl_interface.GetVehicleInf(tbHPHM.Text, cbbHPZL.Text, tbJYLSH.Text, out code, out msg);
                            if (dt_WaitCar != null && dt_WaitCar.Rows.Count > 0)
                            {
                                DataRow dr_temp = dt_WaitCar.Rows[0];
                                waitcar_single.WGJYH = dr_temp["jylsh"].ToString();
                                waitcar_single.JCCS = dr_temp["hphm"].ToString();
                                waitcar_single.CLPH = dr_temp["cph"].ToString();
                                waitcar_single.JCLX = dr_temp["jylb"].ToString() == "00" ? "1" : "0";
                                waitcar_single.HPYS = "";
                                waitcar_single.HPZL = dr_temp["hpzl"].ToString();
                                waitcar_single.FDJHM = dr_temp["fdjh"].ToString();
                                waitcar_single.PPXH = dr_temp["clpp"].ToString();
                                waitcar_single.VIN = dr_temp["clsbdh"].ToString();
                                waitcar_single.CLLX = dr_temp["cllx"].ToString();
                                waitcar_single.CZ = dr_temp["syr"].ToString();
                                waitcar_single.CD = dr_temp["cwkc"].ToString();
                                waitcar_single.KD = dr_temp["cwkk"].ToString();
                                waitcar_single.GD = dr_temp["cwkg"].ToString();
                                waitcar_single.HXCD = dr_temp["hxcd"].ToString() == "" ? "0" : dr_temp["hxcd"].ToString();
                                waitcar_single.HXKD = dr_temp["hxkd"].ToString() == "" ? "0" : dr_temp["hxkd"].ToString();
                                waitcar_single.HXGD = dr_temp["hxgd"].ToString() == "" ? "0" : dr_temp["hxgd"].ToString();
                                waitcar_single.LBGD = dr_temp["lbgd"].ToString() == "" ? "0" : dr_temp["lbgd"].ToString();
                                waitcar_single.ZS = dr_temp["czs"].ToString() == "" ? "0" : dr_temp["czs"].ToString();
                                waitcar_single.ZJ1 = dr_temp["zj1"].ToString() == "" ? "0" : dr_temp["zj1"].ToString();
                                waitcar_single.ZJ2 = dr_temp["zj2"].ToString() == "" ? "0" : dr_temp["zj2"].ToString();
                                waitcar_single.ZJ3 = dr_temp["zj3"].ToString() == "" ? "0" : dr_temp["zj3"].ToString();
                                waitcar_single.ZJ4 = dr_temp["zj4"].ToString() == "" ? "0" : dr_temp["zj4"].ToString();
                                waitcar_single.ZBZL = dr_temp["zbzl"].ToString() == "" ? "0" : dr_temp["zbzl"].ToString();
                                waitcar_single.SCZBZL = "0";
                                waitcar_single.ZDZZL = dr_temp["zzl"].ToString() == "" ? "0" : dr_temp["zzl"].ToString();

                                if (dt_WaitCar.Columns.Contains("qychp"))
                                    waitcar_single.QYCHP = dr_temp["qychp"].ToString();
                                else
                                    waitcar_single.QYCHP = "";

                                waitcar_single.SFAZWB = "";
                                waitcar_single.WBZL = "";
                                waitcar_single.SFAZQTBJ = "";
                                waitcar_single.QTBJZL = "";
                                waitcar_single.QTBJSM = "";

                                waitcar_single.SFJCCKG = (dr_temp["clwkbz"].ToString() == "1" ? "Y" : "N");
                                waitcar_single.SFJCZJ = (dr_temp["zjbz"].ToString() == "1" ? "Y" : "N");
                                waitcar_single.SFJCLBGD = (dr_temp["lbjcbz"].ToString() == "1" ? "Y" : "N");
                                waitcar_single.SFJCHX = "N";
                                waitcar_single.SFJCZBZL = (dr_temp["zbzlbz"].ToString() == "1" ? "Y" : "N");

                                waitcar_single.IsReadyToTest = true;
                            }
                            else
                            {
                                MessageBox.Show("待检列表查询失败或未查到待检车辆");
                                return;
                            }
                            #endregion
                            break;
                        case LwhUploadOnline.NetUploadModel.广西:
                            break;
                        case LwhUploadOnline.NetUploadModel.海城新疆:
                            break;
                        case LwhUploadOnline.NetUploadModel.海城四川:
                            break;
                        case LwhUploadOnline.NetUploadModel.华燕:
                            #region hy
                            dt_WaitCar = hy_interface.GetVehicleInf(tbJYLSH.Text, tbJYCS.Text, "M1", out code, out msg);
                            if (dt_WaitCar != null && dt_WaitCar.Rows.Count > 0)
                            {
                                DataRow drWaitCar = dt_WaitCar.Rows[0];                                
                                waitcar_single.WGJYH = drWaitCar["jylsh"].ToString();
                                waitcar_single.JCCS = drWaitCar["jycs"].ToString();
                                waitcar_single.CLPH = drWaitCar["cph"].ToString();
                                waitcar_single.JCLX = waitcar_single.CLPH == "" ? "1" : "0";
                                waitcar_single.HPYS = "";
                                waitcar_single.HPZL = drWaitCar["hpzlid"].ToString() + "_" + drWaitCar["hpzl"].ToString();
                                waitcar_single.FDJHM = drWaitCar["fdjh"].ToString();
                                waitcar_single.PPXH = drWaitCar["clpp"].ToString();
                                waitcar_single.VIN = drWaitCar["clsbdh"].ToString();
                                waitcar_single.CLLX = drWaitCar["cllx"].ToString() + drWaitCar["cllxstr"].ToString();
                                waitcar_single.CZ = drWaitCar["syr"].ToString();
                                waitcar_single.CD = drWaitCar["cwkc"].ToString();
                                waitcar_single.KD = drWaitCar["cwkk"].ToString();
                                waitcar_single.GD = drWaitCar["cwkg"].ToString();
                                waitcar_single.HXCD = "0";
                                waitcar_single.HXKD = "0";
                                waitcar_single.HXGD = "0";
                                waitcar_single.LBGD = "0";
                                waitcar_single.ZJ1 = "0";
                                waitcar_single.ZJ2 = "0";
                                waitcar_single.ZJ3 = "0";
                                waitcar_single.ZJ4 = "0";
                                waitcar_single.ZBZL = drWaitCar["zbzl"].ToString();
                                waitcar_single.SCZBZL = "0";
                                waitcar_single.ZDZZL = drWaitCar["zczl"].ToString() == "" ? "0" : drWaitCar["zczl"].ToString();

                                waitcar_single.QYCHP = "";
                                waitcar_single.SFAZWB = "";
                                waitcar_single.WBZL = "";
                                waitcar_single.SFAZQTBJ = "";
                                waitcar_single.QTBJZL = "";
                                waitcar_single.QTBJSM = "";

                                waitcar_single.SFJCCKG = "Y";
                                waitcar_single.SFJCZJ = "N";
                                waitcar_single.SFJCLBGD = "N";
                                waitcar_single.SFJCHX = "N";
                                waitcar_single.SFJCZBZL = "N";

                                waitcar_single.IsReadyToTest = true;
                            }
                            else
                            {
                                MessageBox.Show("查询待检车辆信息失败：" + msg);
                                return;
                            }
                            #endregion
                            break;
                        case LwhUploadOnline.NetUploadModel.湖北:
                            break;
                        case LwhUploadOnline.NetUploadModel.康士柏:
                            break;
                        case LwhUploadOnline.NetUploadModel.欧润特:
                            break;
                        case LwhUploadOnline.NetUploadModel.上饶:
                            break;
                        case LwhUploadOnline.NetUploadModel.南京新仕尚:
                            break;
                        case LwhUploadOnline.NetUploadModel.万国:
                            break;
                        case LwhUploadOnline.NetUploadModel.维科:
                            break;
                        case LwhUploadOnline.NetUploadModel.新盾:
                            break;
                        case LwhUploadOnline.NetUploadModel.新力源:
                            break;
                        case LwhUploadOnline.NetUploadModel.益中祥:
                            break;
                        case LwhUploadOnline.NetUploadModel.中航:
                            break;
                        default:
                            MessageBox.Show("未知联网方式！");
                            return;
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("从平台获取待检车辆信息出错：" + er.Message);
                    return;
                }

                if (waitcar_single != null && waitcar_single.IsReadyToTest)
                {
                    waitCarInfoZhu = waitcar_single;

                    //清空待检车辆信息
                    lbJYLSH.Text = waitCarInfoZhu.WGJYH;
                    lbJYLB.Text = waitCarInfoZhu.JCLX == "0" ? "车辆年检" : "新车上牌";
                    lbCPHM.Text = waitCarInfoZhu.CLPH;
                    lbHPZL.Text = waitCarInfoZhu.HPZL;
                    lbVIN.Text = waitCarInfoZhu.VIN;
                    lbCLLX.Text = waitCarInfoZhu.CLLX;
                    lbLWHBZ.Text = waitCarInfoZhu.CD;
                    lbHXBZ.Text = waitCarInfoZhu.HXCD + "x" + waitCarInfoZhu.HXKD + "x" + waitCarInfoZhu.HXGD;
                    lbZJBZ.Text = waitCarInfoZhu.ZJ1+"|"+ waitCarInfoZhu.ZJ2 + "|" + waitCarInfoZhu.ZJ3 + "|" + waitCarInfoZhu.ZJ4;
                    lbLBBZ.Text = waitCarInfoZhu.LBGD;
                    lbZBZLBZ.Text = waitCarInfoZhu.ZBZL;

                    //初始化检测项目
                    ckLWH_s.Checked = waitCarInfoZhu.SFJCCKG == "Y";
                    ckZJ_s.Checked = waitCarInfoZhu.SFJCZJ == "Y";
                    ckLB_s.Checked = waitCarInfoZhu.SFJCLBGD == "Y";
                    ckHX_s.Checked = waitCarInfoZhu.SFJCHX == "Y";
                    ckZBZL_s.Checked = waitCarInfoZhu.SFJCZBZL == "Y";
                }
                #endregion
            }
        }

        #region 联网待检列表右键功能
        private void 单车发车上线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWaitCarList.CurrentRow.Index > -1)
                {
                    string jylsh = dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString();
                    string jycs = dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString();

                    for (int i = 0; i < WaitCarList.Count; i++)
                    {
                        if (WaitCarList[i].WGJYH == jylsh && WaitCarList[i].JCCS == jycs)
                        {
                            waitCarInfoZhu = WaitCarList[i];
                            ckQGLC.Checked = false;

                            if (CreateWaitCarInfoFile())
                                MessageBox.Show("发车上线成功");

                            return;
                        }
                    }

                    MessageBox.Show("待检车辆列表已更新，请刷新后重新发车");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("单车发车上线检测出错：" + er.Message);
            }
        }

        private void 设为待检主车ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWaitCarList.CurrentRow.Index > -1)
                {
                    string jylsh = dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString();
                    string jycs = dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString();

                    for (int i = 0; i < WaitCarList.Count; i++)
                    {
                        if (WaitCarList[i].WGJYH == jylsh && WaitCarList[i].JCCS == jycs)
                        {
                            waitCarInfoZhu = WaitCarList[i];

                            lbZhuCheHPHM.Text = waitCarInfoZhu.CLPH;
                            lbZhuCheHPZL.Text = waitCarInfoZhu.HPZL;

                            ckLWH_z.Checked = waitCarInfoZhu.SFJCCKG == "Y";
                            ckZJ_z.Checked = waitCarInfoZhu.SFJCZJ == "Y";
                            ckLB_z.Checked = waitCarInfoZhu.SFJCLBGD == "Y";
                            ckHX_z.Checked = waitCarInfoZhu.SFJCHX == "Y";
                            ckZBZL_z.Checked = waitCarInfoZhu.SFJCZBZL == "Y";

                            return;
                        }
                    }

                    lbZhuCheHPHM.Text = "-";
                    lbZhuCheHPZL.Text = "-";
                    MessageBox.Show("设为主车失败，可能是待检列表已更新，请刷新后重新设置");
                }
            }
            catch (Exception er)
            {
                lbZhuCheHPHM.Text = "-";
                lbZhuCheHPZL.Text = "-";
                MessageBox.Show("设置主车出错：" + er.Message);
            }
        }

        private void 设为待检挂车ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWaitCarList.CurrentRow.Index > -1)
                {
                    string jylsh = dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString();
                    string jycs = dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString();

                    for (int i = 0; i < WaitCarList.Count; i++)
                    {
                        if (WaitCarList[i].WGJYH == jylsh && WaitCarList[i].JCCS == jycs)
                        {
                            waitCarInfoGua = WaitCarList[i];

                            lbGuaCheHPHM.Text = waitCarInfoGua.CLPH;
                            lbGuaCheHPZL.Text = waitCarInfoGua.HPZL;

                            ckLWH_g.Checked = waitCarInfoGua.SFJCCKG == "Y";
                            ckZJ_g.Checked = waitCarInfoGua.SFJCZJ == "Y";
                            ckLB_g.Checked = waitCarInfoGua.SFJCLBGD == "Y";
                            ckHX_g.Checked = waitCarInfoGua.SFJCHX == "Y";
                            ckZBZL_g.Checked = waitCarInfoGua.SFJCZBZL == "Y";

                            return;
                        }
                    }

                    lbGuaCheHPHM.Text = "-";
                    lbGuaCheHPZL.Text = "-";
                    MessageBox.Show("设为主车失败，可能是待检列表已更新，请刷新后重新设置");
                }
            }
            catch (Exception er)
            {
                lbGuaCheHPHM.Text = "-";
                lbGuaCheHPZL.Text = "-";
                MessageBox.Show("设置主车出错：" + er.Message);
            }
        }

        private void 刷新待检列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_WaitCarList = null;
                if (softConfig.WaitCarModel == LwhUploadOnline.NetWaitCarModel.华燕联网列表)
                {
                    #region hy
                    dt_WaitCarList = hy_interface.GetVehicleList();
                    if (dt_WaitCarList != null && dt_WaitCarList.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_WaitCarList.Rows.Count; i++)
                        {
                            DataTable dt_carinfo_hy = null;
                            string code = "", msg = "";
                            if (dt_WaitCarList.Rows[i]["M1"].ToString() == "1")
                                dt_carinfo_hy = hy_interface.GetVehicleInf(dt_WaitCarList.Rows[i]["jylsh"].ToString(), dt_WaitCarList.Rows[i]["jycs"].ToString(), "M1", out code, out msg);
                            else
                                dt_carinfo_hy = hy_interface.GetVehicleInf(dt_WaitCarList.Rows[i]["jylsh"].ToString(), dt_WaitCarList.Rows[i]["jycs"].ToString(), "M1", out code, out msg);

                            if (code == "1" && dt_carinfo_hy != null && dt_carinfo_hy.Rows.Count > 0)
                            {
                                DataRow dr_temp = dt_carinfo_hy.Rows[0];
                                WaitCarModel wait_temp = new WaitCarModel();
                                wait_temp.WGJYH = dr_temp["jylsh"].ToString();
                                wait_temp.JCCS = dr_temp["jycs"].ToString();
                                wait_temp.CLPH = dr_temp["cph"].ToString();
                                wait_temp.JCLX = wait_temp.CLPH == "" ? "1" : "0";
                                wait_temp.HPYS = "";
                                wait_temp.HPZL = dr_temp["hpzlid"].ToString() + "_" + dr_temp["hpzl"].ToString();
                                wait_temp.FDJHM = dr_temp["fdjh"].ToString();
                                wait_temp.PPXH = dr_temp["clpp"].ToString();
                                wait_temp.VIN = dr_temp["clsbdh"].ToString();
                                wait_temp.CLLX = dr_temp["cllx"].ToString() + dr_temp["cllxstr"].ToString();
                                wait_temp.CZ = dr_temp["syr"].ToString();
                                wait_temp.CD = dr_temp["cwkc"].ToString();
                                wait_temp.KD = dr_temp["cwkk"].ToString();
                                wait_temp.GD = dr_temp["cwkg"].ToString();
                                wait_temp.HXCD = "0";
                                wait_temp.HXKD = "0";
                                wait_temp.HXGD = "0";
                                wait_temp.LBGD = "0";
                                wait_temp.ZJ1 = "0";
                                wait_temp.ZJ2 = "0";
                                wait_temp.ZJ3 = "0";
                                wait_temp.ZJ4 = "0";
                                wait_temp.ZBZL = dr_temp["zbzl"].ToString();
                                wait_temp.SCZBZL = "0";
                                wait_temp.ZDZZL = dr_temp["zczl"].ToString() == "" ? "0" : dr_temp["zczl"].ToString();

                                wait_temp.QYCHP = "";
                                wait_temp.SFAZWB = "";
                                wait_temp.WBZL = "";
                                wait_temp.SFAZQTBJ = "";
                                wait_temp.QTBJZL = "";
                                wait_temp.QTBJSM = "";

                                wait_temp.SFJCCKG = (dt_WaitCarList.Rows[i]["M1"].ToString() == "1" ? "Y" : "N");
                                wait_temp.SFJCZJ = "N";
                                wait_temp.SFJCLBGD = "N";
                                wait_temp.SFJCHX = "N";
                                wait_temp.SFJCZBZL = (dt_WaitCarList.Rows[i]["Z1"].ToString() == "1" ? "Y" : "N");

                                WaitCarList.Add(wait_temp);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("待检列表查询失败或未查到待检车辆");
                        return;
                    }
                    #endregion
                }
                else if (softConfig.WaitCarModel == LwhUploadOnline.NetWaitCarModel.大雷联网列表)
                {
                    #region dl
                    dt_WaitCarList = dl_interface.GetVehicleList();
                    if (dt_WaitCarList != null && dt_WaitCarList.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_WaitCarList.Rows.Count; i++)
                        {
                            DataRow dr_temp = dt_WaitCarList.Rows[i];
                            WaitCarModel wait_temp = new WaitCarModel();
                            wait_temp.WGJYH = dr_temp["jylsh"].ToString();
                            wait_temp.JCCS = dr_temp["hphm"].ToString();
                            wait_temp.CLPH = dr_temp["cph"].ToString();
                            wait_temp.JCLX = dr_temp["jylb"].ToString() == "00" ? "1" : "0";
                            wait_temp.HPYS = "";
                            wait_temp.HPZL = dr_temp["hpzl"].ToString();
                            wait_temp.FDJHM = dr_temp["fdjh"].ToString();
                            wait_temp.PPXH = dr_temp["clpp"].ToString();
                            wait_temp.VIN = dr_temp["clsbdh"].ToString();
                            wait_temp.CLLX = dr_temp["cllx"].ToString();
                            wait_temp.CZ = dr_temp["syr"].ToString();
                            wait_temp.CD = dr_temp["cwkc"].ToString();
                            wait_temp.KD = dr_temp["cwkk"].ToString();
                            wait_temp.GD = dr_temp["cwkg"].ToString();
                            wait_temp.HXCD = dr_temp["hxcd"].ToString() == "" ? "0" : dr_temp["hxcd"].ToString();
                            wait_temp.HXKD = dr_temp["hxkd"].ToString() == "" ? "0" : dr_temp["hxkd"].ToString();
                            wait_temp.HXGD = dr_temp["hxgd"].ToString() == "" ? "0" : dr_temp["hxgd"].ToString();
                            wait_temp.LBGD = dr_temp["lbgd"].ToString() == "" ? "0" : dr_temp["lbgd"].ToString();
                            wait_temp.ZS = dr_temp["czs"].ToString() == "" ? "0" : dr_temp["czs"].ToString();
                            wait_temp.ZJ1 = dr_temp["zj1"].ToString() == "" ? "0" : dr_temp["zj1"].ToString();
                            wait_temp.ZJ2 = dr_temp["zj2"].ToString() == "" ? "0" : dr_temp["zj2"].ToString();
                            wait_temp.ZJ3 = dr_temp["zj3"].ToString() == "" ? "0" : dr_temp["zj3"].ToString();
                            wait_temp.ZJ4 = dr_temp["zj4"].ToString() == "" ? "0" : dr_temp["zj4"].ToString();
                            wait_temp.ZBZL = dr_temp["zbzl"].ToString() == "" ? "0" : dr_temp["zbzl"].ToString();
                            wait_temp.SCZBZL = "0";
                            wait_temp.ZDZZL = dr_temp["zzl"].ToString() == "" ? "0" : dr_temp["zzl"].ToString();

                            if (dt_WaitCarList.Columns.Contains("qychp"))
                                wait_temp.QYCHP = dr_temp["qychp"].ToString();
                            else
                                wait_temp.QYCHP = "";

                            wait_temp.SFAZWB = "";
                            wait_temp.WBZL = "";
                            wait_temp.SFAZQTBJ = "";
                            wait_temp.QTBJZL = "";
                            wait_temp.QTBJSM = "";

                            wait_temp.SFJCCKG = (dr_temp["clwkbz"].ToString() == "1" ? "Y" : "N");
                            wait_temp.SFJCZJ = (dr_temp["zjbz"].ToString() == "1" ? "Y" : "N");
                            wait_temp.SFJCLBGD = (dr_temp["lbjcbz"].ToString() == "1" ? "Y" : "N");
                            wait_temp.SFJCHX = "N";
                            wait_temp.SFJCZBZL = (dr_temp["zbzlbz"].ToString() == "1" ? "Y" : "N");

                            WaitCarList.Add(wait_temp);
                        }
                    }
                    else
                    {
                        MessageBox.Show("待检列表查询失败或未查到待检车辆");
                        return;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("未知联网列表模式");
                    return;
                }

                ShowWaitCarList(WaitCarList);//刷新待检列表
            }
            catch (Exception er)
            {
                MessageBox.Show("获取待检列表出错：" + er.Message);
            }
        }
        #endregion

        /// <summary>
        /// 联网待检列表方式发车上线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendWaitListCarToTest_Click(object sender, EventArgs e)
        {
            if (waitCarInfoZhu != null && waitCarInfoZhu.IsReadyToTest)
            {
                waitCarInfoZhu.SFJCCKG = ckLWH_z.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCLBGD = ckLB_z.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCHX = ckHX_z.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCZJ = ckZJ_z.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCZBZL = ckZBZL_z.Checked ? "Y" : "N";
            }
            else
            {
                //主车信息为空时，不能开始检测
                MessageBox.Show("待检车辆主车信息为空或不全，不能开始检测！");
                return;
            }

            if (ckQGLC.Checked)
            {
                #region 牵挂联测，校验挂车信息是否齐全
                if (waitCarInfoGua != null && waitCarInfoGua.IsReadyToTest)
                {
                    waitCarInfoGua.SFJCCKG = ckLWH_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCLBGD = ckLB_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCHX = ckHX_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCZJ = ckZJ_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCZBZL = ckZBZL_g.Checked ? "Y" : "N";
                }
                else
                {
                    //牵挂联测挂车车信息为空时，不能开始检测
                    MessageBox.Show("待检挂车信息为空或不全，不能开始检测！");
                    return;
                }
                #endregion
            }

            if (CreateWaitCarInfoFile())
                MessageBox.Show("发车上线成功");
        }

        #region 联网待检列表刷新功能函数
        /// <summary>
        /// 更新待检列表显示
        /// </summary>
        /// <param name="wait_car_list"></param>
        private void ShowWaitCarList(List<WaitCarModel> wait_car_list)
        {
            try
            {
                if (wait_car_list != null && wait_car_list.Count > 0)
                {
                    #region 更新待检列表显示
                    for (int i = 0; i < wait_car_list.Count; i++)
                    {
                        WaitCarModel waicar = wait_car_list[i];
                        DataRow dr_temp = dtDgvSource.NewRow();
                        dr_temp["检验流水号"] = waicar.WGJYH;
                        dr_temp["检验次数"] = waicar.JCCS;
                        dr_temp["车辆号牌"] = waicar.CLPH;
                        dr_temp["号牌种类"] = waicar.HPZL;
                        dr_temp["外廓"] = waicar.SFJCCKG;
                        dr_temp["称重"] = waicar.SFJCZBZL;
                        dr_temp["长"] = waicar.CD;
                        dr_temp["宽"] = waicar.KD;
                        dr_temp["高"] = waicar.GD;
                        dr_temp["整备质量"] = waicar.ZBZL;
                        dr_temp["轴距"] = waicar.ZJ1+"/"+ waicar.ZJ2 + "/" + waicar.ZJ3 + "/" + waicar.ZJ4;
                        
                        dtDgvSource.Rows.Add(dr_temp);
                    }
                    #endregion

                    dgvWaitCarList.DataSource = dtDgvSource;
                }
                else
                    dtDgvSource.Rows.Clear();
            }
            catch (Exception er)
            {
                MessageBox.Show("刷新待检列表出错：" + er.Message);
            }
        }
        #endregion

        /// <summary>
        /// 单车方式发车上线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendSingleCarToTest_Click(object sender, EventArgs e)
        {
            if (waitCarInfoZhu != null && waitCarInfoZhu.IsReadyToTest)
            {
                waitCarInfoZhu.SFJCCKG = ckLWH_s.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCLBGD = ckLB_s.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCHX = ckHX_s.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCZJ = ckZJ_s.Checked ? "Y" : "N";
                waitCarInfoZhu.SFJCZBZL = ckZBZL_s.Checked ? "Y" : "N";
            }
            else
            {
                //主车信息为空时，不能开始检测
                MessageBox.Show("待检车辆主车信息为空或不全，不能开始检测！");
                return;
            }

            //发车上线
            if (CreateWaitCarInfoFile())
                MessageBox.Show("发车上线成功");
        }
        
        /// <summary>
        /// 生成检测程序用待检车辆信息
        /// </summary>
        /// <returns></returns>
        private bool CreateWaitCarInfoFile()
        {
            try
            {
                //先校验是否为牵挂联测，当前版本只允许在待检列表中选择挂车
                if (waitCarInfoZhu != null && waitCarInfoZhu.IsReadyToTest)
                {
                    #region 写主车信息
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "外观检验号", waitCarInfoZhu.WGJYH, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测长宽高", waitCarInfoZhu.SFJCCKG, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测栏板高度", waitCarInfoZhu.SFJCLBGD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测轴距", waitCarInfoZhu.SFJCZJ, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测整备质量", waitCarInfoZhu.SFJCZBZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测货箱", waitCarInfoZhu.SFJCHX, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "车辆牌号", waitCarInfoZhu.CLPH, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "检测类型", waitCarInfoZhu.JCLX, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "号牌颜色", waitCarInfoZhu.HPYS, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "号牌种类", waitCarInfoZhu.HPZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "发动机号码", waitCarInfoZhu.FDJHM, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "品牌型号", waitCarInfoZhu.PPXH, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "检测次数", waitCarInfoZhu.JCCS, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "VIN", waitCarInfoZhu.VIN, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "车辆类型", waitCarInfoZhu.CLLX, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "车主", waitCarInfoZhu.CZ, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "长度", waitCarInfoZhu.CD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "宽度", waitCarInfoZhu.KD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "高度", waitCarInfoZhu.GD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "货箱长度", waitCarInfoZhu.HXCD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "货箱宽度", waitCarInfoZhu.HXKD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "货箱高度", waitCarInfoZhu.HXGD, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "轴数", waitCarInfoZhu.ZS, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "轴距1", waitCarInfoZhu.ZJ1, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "轴距2", waitCarInfoZhu.ZJ2, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "轴距3", waitCarInfoZhu.ZJ3, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "轴距4", waitCarInfoZhu.ZJ4, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "整备质量", waitCarInfoZhu.ZBZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "实测整备质量", waitCarInfoZhu.SCZBZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "最大总质量", waitCarInfoZhu.ZDZZL, CarInfoPathTemp);

                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否安装尾板", waitCarInfoZhu.SFAZWB, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "尾板质量", waitCarInfoZhu.WBZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否有其他加装部件", waitCarInfoZhu.SFAZQTBJ, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "其他加装部件质量", waitCarInfoZhu.QTBJZL, CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "其他部件说明", waitCarInfoZhu.QTBJSM, CarInfoPathTemp);

                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "DaLeiJCFS", daleijcfs, CarInfoPathTemp);
                    #endregion
                }
                else
                {
                    MessageBox.Show("待检车辆主车信息为空或异常，无法生成待检车辆信息");
                    return false;
                }

                #region 再写挂车信息
                if (pCarList.Visible && ckQGLC.Checked)
                {
                    if (waitCarInfoGua != null && waitCarInfoGua.IsReadyToTest)
                    {
                        #region 写挂车信息
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "主挂联测", "3", CarInfoPathTemp);

                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车外观检验号", waitCarInfoGua.WGJYH, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车外廓", waitCarInfoGua.SFJCCKG, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车整备质量", waitCarInfoGua.SFJCZBZL, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车栏板", waitCarInfoGua.SFJCLBGD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车轴距", waitCarInfoGua.SFJCZJ, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车货箱", waitCarInfoGua.SFJCHX, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车号牌", waitCarInfoGua.CLPH, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车长", waitCarInfoGua.CD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车宽", waitCarInfoGua.KD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车高", waitCarInfoGua.GD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距1", waitCarInfoGua.ZJ1, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距2", waitCarInfoGua.ZJ2, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距3", waitCarInfoGua.ZJ3, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距4", waitCarInfoGua.ZJ4, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车检测类型", waitCarInfoGua.JCLX, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车号牌种类", waitCarInfoGua.HPZL, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车车辆类型", waitCarInfoGua.CLLX, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车检测次数", waitCarInfoGua.JCCS, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车厂牌型号", waitCarInfoGua.PPXH, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车发动机号码", waitCarInfoGua.FDJHM, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车车主", waitCarInfoGua.CZ, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车VIN", waitCarInfoGua.VIN, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车整备质量", waitCarInfoGua.ZBZL, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车实测整备质量", waitCarInfoGua.SCZBZL, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车栏板高度", waitCarInfoGua.LBGD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱长", waitCarInfoGua.HXCD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱宽", waitCarInfoGua.HXKD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱高", waitCarInfoGua.HXGD, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车总质量", waitCarInfoGua.ZDZZL, CarInfoPathTemp);
                        LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "牵引车号牌", waitCarInfoGua.QYCHP, CarInfoPathTemp);
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("牵挂联测时待下发挂车信息为空或异常，无法生成挂车待检车辆信息");
                        return false;
                    }
                }
                else
                {
                    #region 不检挂车时写入空白数据
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "主挂联测", "1", CarInfoPathTemp);

                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车外观检验号", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车外廓", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车整备质量", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车栏板", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车轴距", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "是否检测挂车货箱", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车号牌", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车长", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车宽", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车高", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴数", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距1", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距2", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距3", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车轴距4", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车检测类型", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车号牌种类", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车车辆类型", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车检测次数", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车厂牌型号", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车发动机号码", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车车主", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车VIN", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车整备质量", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车实测整备质量", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车栏板高度", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱长", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱宽", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车货箱高", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "挂车总质量", "", CarInfoPathTemp);
                    LwhUploadOnline.IOControl.WritePrivateProfileString("检测信息", "牵引车号牌", "", CarInfoPathTemp);
                    #endregion
                }
                #endregion

                File.Copy(CarInfoPathTemp, CarInfoPath, true);//复制临时待检车辆信息到待检车辆信息
                File.Delete(CarInfoPathTemp);

                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show("生成待检车辆文件出错：\r\n" + er.Message);
                return false;
            }
        }
    }
}
