using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NetSendWaitCar
{
    public partial class FrmMain : Form
    {
        private string global_path = @"D:\外廓数据文件";
        /// <summary>
        /// 联网配置信息
        /// </summary>
        private UploadConfigModel softConfig = new UploadConfigModel();
        /// <summary>
        /// 待检车辆列表
        /// </summary>
        private DataTable dtWaitCarList = null;
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

        /// <summary>
        /// 检测程序用待检车辆信息
        /// </summary>
        private string CarInfoPath = @"C:\jcdatatxt\carinfo.ini";
        private string CarInfoPathTemp = @"C:\jcdatatxt\carinfo_temp.ini";

        #region 委托更新界面信息
        //更新RTB信息
        private delegate void DgUpdateRtb(string msg);
        private void UpdateRtb(string msg)
        {
            try
            {
                BeginInvoke(new DgUpdateRtb(update_rtb), msg);
            }
            catch { }
        }
        private void update_rtb(string msg)
        {
            if (rtbRunLogs.Text.Length > 5000)
                rtbRunLogs.Text = rtbRunLogs.Text.Remove(0, 2000);
            rtbRunLogs.AppendText(msg + "\r\n");
            rtbRunLogs.Select(rtbRunLogs.TextLength, 0);
            rtbRunLogs.ScrollToCaret();
        }

        //更新控件是否Enable状态
        private delegate void DgCCE(Control c, bool is_active);
        private void ChangeControlEnable(Control c, bool is_active)
        {
            BeginInvoke(new DgCCE(change_control_enable), c, is_active);
        }
        private void change_control_enable(Control c, bool is_active)
        {
            try
            {
                c.Enabled = is_active;
            }
            catch { }
        }

        //更新控件显示名称
        private delegate void DgUpdateText(Control c, string txt);
        private void UpdateText(Control c, string txt)
        {
            BeginInvoke(new DgUpdateText(update_text), c, txt);
        }
        private void update_text(Control c, string txt)
        {
            try
            {
                c.Text = txt;
            }
            catch { }
        }
        #endregion

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

        public FrmMain()
        {
            InitializeComponent();

            pSingleCar.Location = new Point(6, 24);
            dgvWaitCarList.Location = new Point(6, 24);
            pInputZBZL.Location = new Point((int)((pMain.Width - pInputZBZL.Width) / 2), (int)((pMain.Height - pInputZBZL.Height) / 2));
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region 获取配置信息
            if (getNetConfig())
            {
                UpdateRtb("已获取联网配置信息");
            }
            else
            {
                MessageBox.Show("联网发车软件初始化时获取联网配置信息失败\r\n请检查配置信息文件是否存在及格式是否正确");
                return;
            }
            #endregion

            #region 初始化联网接口
            DataTable dt_time = null;
            if (softConfig.WaitCarModel == NetWaitCarModel.大雷联网列表)
            {
                #region 大雷待检列表
                dl_interface = new DaLei(softConfig.JkdzWaitCar);
                dt_time = dl_interface.GetSystemDatetime(softConfig.StationID);
                if (dt_time != null && dt_time.Rows.Count > 0)
                {
                    if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                        UpdateRtb("同步待检车辆平台时间（" + dt_time.Rows[0]["sj"].ToString() + "）成功");
                    else
                        UpdateRtb("更新待检车辆平台时间(" + dt_time.Rows[0]["sj"].ToString() + ")失败");
                }
                else
                {
                    UpdateRtb("同步待检车辆平台时间失败");
                }

                btGetWaitInfo.Enabled = true;
                dgvWaitCarList.Visible = true;
                tbHPHM.Enabled = false;
                tbVIN.Enabled = false;
                cbbHPZL.Enabled = false;
                cbbJYLB.Enabled = false;
                #endregion
            }
            else if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
            {
                #region 华燕待检列表
                hy_interface = new HuaYan(softConfig.JkdzWaitCar, softConfig.Xtlb, softConfig.JkxlhWaitCar);
                dt_time = hy_interface.GetSystemDatetime(softConfig.StationID);
                if (dt_time != null && dt_time.Rows.Count > 0)
                {
                    if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                        UpdateRtb("同步待检车辆平台时间（" + dt_time.Rows[0]["sj"].ToString() + "）成功");
                    else
                        UpdateRtb("更新待检车辆平台时间(" + dt_time.Rows[0]["sj"].ToString() + ")失败");
                }
                else
                {
                    UpdateRtb("同步待检车辆平台时间失败");
                    return;
                }

                btGetWaitInfo.Enabled = true;
                dgvWaitCarList.Visible = true;
                tbHPHM.Enabled = false;
                tbVIN.Enabled = false;
                cbbHPZL.Enabled = false;
                cbbJYLB.Enabled = false;
                #endregion
            }
            else
            {
                #region 联网查询
                btGetWaitInfo.Text = "查询待检车辆";
                switch (softConfig.NetModel)
                {
                    case NetUploadModel.安车:
                        #region ac
                        ac_interface = new AnChe(softConfig.StationID, softConfig.Jkdz1, softConfig.Xtlb, softConfig.Jkxlh, softConfig.CJBH, softConfig.DWJGDM, softConfig.DWMC, softConfig.YHBS, softConfig.YHXM, softConfig.ZDBS);
                        dt_time = ac_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                UpdateRtb("同步并更新上传平台时间（" + dt_time.Rows[0]["sj"].ToString() + "）成功");
                            else
                                UpdateRtb("更新上传平台时间(" + dt_time.Rows[0]["sj"].ToString() + ")失败");
                        }
                        else
                        {
                            UpdateRtb("同步上传平台时间失败");
                            return;
                        }
                        #endregion
                        break;
                    case NetUploadModel.安徽:
                        break;
                    case NetUploadModel.宝辉:
                        break;
                    case NetUploadModel.大雷:
                        #region dl
                        if (softConfig.WaitCarModel != NetWaitCarModel.大雷联网列表)
                        {
                            dl_interface = new DaLei(softConfig.Jkdz1);
                            dt_time = dl_interface.GetSystemDatetime(softConfig.StationID);
                            if (dt_time != null && dt_time.Rows.Count > 0)
                            {
                                if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                    UpdateRtb("同步并更新上传平台时间（" + dt_time.Rows[0]["sj"].ToString() + "）成功");
                                else
                                    UpdateRtb("更新时间(" + dt_time.Rows[0]["sj"].ToString() + ")失败");
                            }
                            else
                            {
                                UpdateRtb("同步上传平台时间失败");
                            }
                        }
                        #endregion
                        break;
                    case NetUploadModel.广西:
                        break;
                    case NetUploadModel.海城新疆:
                        break;
                    case NetUploadModel.海城四川:
                        break;
                    case NetUploadModel.华燕:
                        #region hy
                        hy_interface = new HuaYan(softConfig.Jkdz1, softConfig.Xtlb, softConfig.Jkxlh);
                        dt_time = hy_interface.GetSystemDatetime(softConfig.StationID);
                        if (dt_time != null && dt_time.Rows.Count > 0)
                        {
                            if (SetSysTime.SetLocalTimeByStr(DateTime.Parse(dt_time.Rows[0]["sj"].ToString())))
                                UpdateRtb("同步上传平台时间（" + dt_time.Rows[0]["sj"].ToString() + "）成功");
                            else
                                UpdateRtb("更新上传平台时间(" + dt_time.Rows[0]["sj"].ToString() + ")失败");
                        }
                        else
                        {
                            UpdateRtb("同步上传台时间失败");
                            return;
                        }
                        #endregion
                        break;
                    case NetUploadModel.湖北:
                        break;
                    case NetUploadModel.康士柏:
                        break;
                    case NetUploadModel.欧润特:
                        break;
                    case NetUploadModel.上饶:
                        break;
                    case NetUploadModel.南京新仕尚:
                        break;
                    case NetUploadModel.万国:
                        break;
                    case NetUploadModel.维科:
                        break;
                    case NetUploadModel.新盾:
                        break;
                    case NetUploadModel.新力源:
                        break;
                    case NetUploadModel.益中祥:
                        break;
                    case NetUploadModel.中航:
                        break;
                    default:
                        break;
                }

                btGetWaitInfo.Enabled = true;
                pSingleCar.Visible = true;
                ckQGLC.Enabled = false;
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// 刷新待检车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGetWaitInfo_Click(object sender, EventArgs e)
        {
            waitCarInfoZhu = null;
            waitCarInfoGua = null;
            dtWaitCarList = null;

            if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
            {
                #region 华燕待检列表方式
                Init_Dgv();

                //获取待检车辆列表
                dtWaitCarList = hy_interface.GetVehicleList();

                //更新待检车辆列表到dgv
                if (dtWaitCarList != null && dtWaitCarList.Rows.Count > 0)
                {
                    dgvWaitCarList.DataSource = dtWaitCarList;//设置数据源

                    #region 车辆类型为挂车时将车牌号添加到挂车选择列表中
                    for (int i = 0; i < dtWaitCarList.Rows.Count; i++)
                    {
                        if (dtWaitCarList.Rows[i]["hpzlid"].ToString() == "15" || dtWaitCarList.Rows[i]["hpzl"].ToString().Contains("挂车") || dtWaitCarList.Rows[i]["hpzl"].ToString().Contains("15"))
                        {
                            cbbGCHP.Items.Add(dtWaitCarList.Rows[i]["cph"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["jylsh"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["jycs"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["M1"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["Z1"].ToString());
                        }
                    }
                    #endregion

                    dgvWaitCarList.Rows[0].Cells[1].Selected = true;
                }
                else
                {
                    UpdateRtb("未查到待检车辆或查询失败");
                    return;
                }
                #endregion
            }
            else if (softConfig.WaitCarModel == NetWaitCarModel.大雷联网列表)
            {
                #region 大雷待检列表方式
                Init_Dgv();

                //获取待检车辆列表
                dtWaitCarList = dl_interface.GetVehicleList();
                //更新待检车辆列表到dgv
                if (dtWaitCarList != null && dtWaitCarList.Rows.Count > 0)
                {
                    dgvWaitCarList.DataSource = dtWaitCarList;//设置数据源

                    #region 车辆类型为挂车时将车牌号添加到挂车选择列表中
                    for (int i = 0; i < dtWaitCarList.Rows.Count; i++)
                    {
                        if (dtWaitCarList.Rows[i]["cllx"].ToString().StartsWith("B"))
                            cbbGCHP.Items.Add(dtWaitCarList.Rows[i]["hphm"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["jylsh"].ToString() + "_" +
                                              dtWaitCarList.Rows[i]["jycs"].ToString());
                    }
                    #endregion

                    #region 调整显示顺序
                    dgvWaitCarList.Columns["jylsh"].DisplayIndex = 0;
                    dgvWaitCarList.Columns["jycs"].DisplayIndex = 1;
                    dgvWaitCarList.Columns["hphm"].DisplayIndex = 2;
                    dgvWaitCarList.Columns["hpzl"].DisplayIndex = 3;
                    dgvWaitCarList.Columns["clsbdh"].DisplayIndex = 4;
                    dgvWaitCarList.Columns["cwkc"].DisplayIndex = 5;
                    dgvWaitCarList.Columns["cwkk"].DisplayIndex = 6;
                    dgvWaitCarList.Columns["cwkg"].DisplayIndex = 7;
                    for (int i = 8; i < dgvWaitCarList.Columns.Count - 1; i++)
                    {
                        dgvWaitCarList.Columns[i].Visible = false;
                    }
                    #endregion

                    dgvWaitCarList.Rows[0].Cells[1].Selected = true;
                }
                else
                {
                    UpdateRtb("未查到待检车辆或查询失败");
                    return;
                }
                #endregion
            }
            else
            {
                #region 联网查询方式
                Init_pSingle();

                string code, msg;
                switch (softConfig.NetModel)
                {
                    case NetUploadModel.安车:
                        dtWaitCarList = ac_interface.GetVehicleInf(tbHPHM.Text, cbbHPZL.Text, tbVIN.Text, out code, out msg);
                        if (code == "0" || dtWaitCarList == null || dtWaitCarList.Rows.Count < 1)
                        {
                            UpdateRtb("待检车辆查询失败或未查到有待检车辆：" + msg);
                            return;
                        }
                        break;
                    case NetUploadModel.安徽:
                        break;
                    case NetUploadModel.宝辉:
                        break;
                    case NetUploadModel.大雷:
                        break;
                    case NetUploadModel.广西:
                        break;
                    case NetUploadModel.海城新疆:
                        break;
                    case NetUploadModel.海城四川:
                        break;
                    case NetUploadModel.华燕:
                        break;
                    case NetUploadModel.湖北:
                        break;
                    case NetUploadModel.康士柏:
                        break;
                    case NetUploadModel.欧润特:
                        break;
                    case NetUploadModel.上饶:
                        break;
                    case NetUploadModel.南京新仕尚:
                        break;
                    case NetUploadModel.万国:
                        break;
                    case NetUploadModel.维科:
                        break;
                    case NetUploadModel.新盾:
                        break;
                    case NetUploadModel.新力源:
                        break;
                    case NetUploadModel.益中祥:
                        break;
                    case NetUploadModel.中航:
                        break;
                    default:
                        UpdateRtb("暂不支持待检车辆查询方式");
                        return;
                }

                waitCarInfoZhu = UpdateWaitCarInfo(dtWaitCarList.Rows[0]);//更新待检车辆信息
                btSendToTest.Enabled = true;
                #endregion
            }
        }

        /// <summary>
        /// 发车上线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendToTest_Click(object sender, EventArgs e)
        {
            try
            {
                //校验待检车辆信息是否完整
                if (waitCarInfoZhu != null && waitCarInfoZhu.IsReadyToTest)
                {
                    waitCarInfoZhu.SFJCCKG = ckLWH.Checked ? "Y" : "N";
                    waitCarInfoZhu.SFJCLBGD = ckLB.Checked ? "Y" : "N";
                    waitCarInfoZhu.SFJCHX = ckHX.Checked ? "Y" : "N";
                    waitCarInfoZhu.SFJCZJ = ckZJ.Checked ? "Y" : "N";
                    waitCarInfoZhu.SFJCZBZL = ckZBZL.Checked ? "Y" : "N";
                }
                else
                {
                    //主车信息为空时，不能开始检测
                    MessageBox.Show("待检车辆信息为空或不全，不能开始检测！");
                    return;
                }

                if (ckQGLC.Checked && waitCarInfoGua != null && waitCarInfoGua.IsReadyToTest)
                {
                    waitCarInfoGua.SFJCCKG = ckLWH_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCLBGD = ckLB_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCHX = ckHX_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCZJ = ckZJ_g.Checked ? "Y" : "N";
                    waitCarInfoGua.SFJCZBZL = ckZBZL_g.Checked ? "Y" : "N";
                }
                else
                {
                    //牵挂联测时挂车信息为空，不能开始检测
                    MessageBox.Show("挂车辆信息为空或不全，不能进行牵挂联测！");
                    return;
                }

                //生成待检车辆信息
                if (CreateWaitCarInfoFile())
                {
                    MessageBox.Show("发车上线成功！");
                    btSendToTest.Enabled = false;
                }
                else
                    UpdateRtb("发车生成待检车辆文件失败！");
            }
            catch (Exception er)
            {
                UpdateRtb("检测启动过程出错：" + er.Message);
            }
        }

        /// <summary>
        /// 联网列表方式更换被选待检车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWaitCarList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvWaitCarList.CurrentRow.Index > -1)
                {
                    if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
                    {
                        #region hy
                        DataTable dt_temp = null;//查询得到的待检车辆信息表
                        string code = "", msg = "";
                        string jylsh = dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString();
                        string jycs = dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString();
                        if (dgvWaitCarList.CurrentRow.Cells["M1"].Value.ToString() == "1")
                        {
                            dt_temp = hy_interface.GetVehicleInf(jylsh, jycs, "M1", out code, out msg);
                        }
                        else
                        {
                            dt_temp = hy_interface.GetVehicleInf(jylsh, jycs, "Z1", out code, out msg);
                        }

                        if (dt_temp != null && dt_temp.Rows.Count > 0)
                        {
                            //车辆信息查询结果新增两列，用于标识是否检外廓、整备质量
                            dt_temp.Columns.Add("M1"); 
                            dt_temp.Columns.Add("Z1");

                            #region 如果查询的是挂车，则校验是否查到的结果是牵挂联测的结果
                            DataRow dr_waitcar = null;
                            if (dt_temp.Rows.Count == 1)
                                dr_waitcar = dt_temp.Rows[0];
                            else
                            {
                                foreach (DataRow dr in dt_temp.Rows)
                                {
                                    if (jylsh == dr["jylsh"].ToString() && jycs == dr["jycs"].ToString())
                                    {
                                        dr_waitcar = dr;
                                        break;
                                    }
                                }
                            }

                            if (dr_waitcar != null)
                            {
                                if (dt_temp.Columns.Contains("gcjylsh") && dr_waitcar["gcjyls"].ToString() != "")
                                    tbInputZbzl.Text = dr_waitcar["gcjyls"].ToString()+"&"+dr_waitcar["zbzl"].ToString();
                            }
                            else
                            {
                                UpdateRtb("执行查询成功但结果无对应车辆信息");
                                return;
                            }
                            #endregion
                            dr_waitcar["M1"] = dgvWaitCarList.CurrentRow.Cells["M1"].Value.ToString();
                            dr_waitcar["Z1"] = dgvWaitCarList.CurrentRow.Cells["Z1"].Value.ToString();

                            waitCarInfoZhu = UpdateWaitCarInfo(dr_waitcar);

                            if (waitCarInfoZhu != null)
                            {
                                btSendToTest.Enabled = true;
                                waitCarInfoZhu.IsReadyToTest = true;//待检车辆信息更新完成，可以进行检测
                                UpdateRtb("待检车辆信息(主车)已获取且准备好下发检测软件");
                            }
                            else
                            {
                                UpdateRtb("初始化待下发待检车辆信息(主车)模板失败");
                                return;
                            }
                        }
                        else
                        {
                            waitCarInfoZhu = null;
                            UpdateRtb("查询待检车辆（" + dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString() + "|" + dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString() + "）信息失败");
                            return;
                        }
                        #endregion
                    }
                    else if (softConfig.WaitCarModel == NetWaitCarModel.大雷联网列表)
                    {
                        #region dl
                        DataRow[] drs_temp = dtWaitCarList.Select("jylsh ='" + dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString() + "' and jycs ='" + dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString() + "'");

                        if (drs_temp != null && drs_temp.Length > 0)
                        {
                            if (dtWaitCarList.Columns.Contains("jcfs") && drs_temp[0]["jcfs"].ToString().Trim() != "")
                                daleijcfs = drs_temp[0]["jcfs"].ToString().Trim();
                            else
                                daleijcfs = "1";

                            waitCarInfoZhu = UpdateWaitCarInfo(drs_temp[0]);

                            if (waitCarInfoZhu != null)
                            {
                                btSendToTest.Enabled = true;
                                waitCarInfoZhu.IsReadyToTest = true;//待检车辆信息更新完成，可以进行检测
                                UpdateRtb("待检车辆信息已获取且准备好下发检测软件");
                            }
                            else
                            {
                                UpdateRtb("初始化待下发待检车辆信息(主车)模板失败");
                                return;
                            }
                        }
                        else
                        {
                            waitCarInfoZhu = null;
                            UpdateRtb("查询待检车辆（" + dgvWaitCarList.CurrentRow.Cells["jylsh"].Value.ToString() + "|" + dgvWaitCarList.CurrentRow.Cells["jycs"].Value.ToString() + "）信息失败");
                            return;
                        }
                        #endregion}
                    }
                    else
                        return;
                }
            }
            catch (Exception er)
            {
                waitCarInfoZhu = null;
                UpdateRtb("获取车辆信息出错：" + er.Message);
            }
        }

        /// <summary>
        /// 勾选牵挂联测时初始化挂车检测选项信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckQGLC_CheckedChanged(object sender, EventArgs e)
        {
            cbbGCHP.Text = "";

            if (ckQGLC.Checked)//当前只考虑有待检列表的牵挂联测
            {
                if (ckZBZL.Checked)
                {
                    ckZBZL.Checked = false;
                    UpdateRtb("牵挂联测时不能同时检测牵引车整备质量，故将牵引车整备质量项目置为不检");
                }
                ckZBZL.Enabled = false;
                
                //初始化挂车待检车辆信息状态，允许选择挂车
                gpGuaChe.Enabled = true;
                ckZJ_g.Checked = false;
                ckLB_g.Checked = false;
                ckHX_g.Checked = false;
            }
            else
            {
                ckZBZL.Enabled = true;
                waitCarInfoGua = null;
            }
        }

        /// <summary>
        /// 要执行牵挂联测选着挂车信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbGCHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbGCHP.Text == "" || cbbGCHP.SelectedIndex < 0)
                return;

            try
            {
                waitCarInfoGua = new WaitCarModel();
                string[] jcxx = cbbGCHP.Text.Split('_');//根据不同的联网方式，挂车号牌的组成各不相同
                DataRow[] drs_waitcar = null;
                DataRow dr_waitcar = null;
                string code = "", msg = "";

                if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
                {
                    #region hy
                    DataTable dt_temp_hy = hy_interface.GetVehicleInf(jcxx[1], jcxx[2], jcxx[3] == "1" ? "M1" : "Z1", out code, out msg);

                    if (dt_temp_hy != null && dt_temp_hy.Rows.Count > 0)
                    {
                        if (dt_temp_hy.Rows.Count == 1)
                            dr_waitcar = dt_temp_hy.Rows[0];
                        else
                        {
                            foreach (DataRow dr in dt_temp_hy.Rows)
                            {
                                if (jcxx[1] == dr["jylsh"].ToString() && jcxx[2] == dr["jycs"].ToString())
                                {
                                    dr_waitcar = dr;
                                    break;
                                }
                            }
                        }
                        if (dr_waitcar == null)
                        {
                            UpdateRtb("执行查询成功但无挂车车辆信息");
                            ckQGLC.Checked = false;
                            return;
                        }
                        #region 获取整备质量信息
                        if (jcxx[4] == "1")
                        {
                            string[] temp = null;
                            if (tbInputZbzl.Text.Contains("&"))
                            {
                                temp = tbInputZbzl.Text.Split('&');
                                if (temp[0] == dr_waitcar["jylsh"].ToString())
                                {
                                    waitCarInfoGua.QYCHP = temp[1];
                                    ckZBZL_g.Checked = true;
                                }
                                else
                                {
                                    DialogResult dr = MessageBox.Show("牵挂联测模式下检测挂车整备质量但未获取到牵引车整备质量\r\n是否继续检测？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                    if (dr == DialogResult.OK)
                                    {
                                        //继续检测择弹出提示整备质量输入提示框
                                        pInputZBZL.BringToFront();
                                        pInputZBZL.Visible = true;
                                    }
                                    else
                                    {
                                        //其他（点取消按钮）执行代码
                                        UpdateRtb("牵挂联测时检测挂车整备质量且无牵引车整备质量，退出检测！");
                                        ckQGLC.Checked = false;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                DialogResult dr = MessageBox.Show("牵挂联测模式下检测挂车整备质量但未获取到牵引车整备质量\r\n是否继续检测？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (dr == DialogResult.OK)
                                {
                                    //继续检测择弹出提示整备质量输入提示框
                                    pInputZBZL.BringToFront();
                                    pInputZBZL.Visible = true;
                                }
                                else
                                {
                                    //其他（点取消按钮）执行代码
                                    UpdateRtb("牵挂联测时检测挂车整备质量且无牵引车整备质量，退出检测！");
                                    ckQGLC.Checked = false;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            ckZBZL.Checked = false;
                            ckZBZL_g.Checked = false;
                        }
                        #endregion

                        waitCarInfoGua.WGJYH = dr_waitcar["jylsh"].ToString();
                        waitCarInfoGua.JCCS = dr_waitcar["jycs"].ToString();
                        waitCarInfoGua.CLPH = dr_waitcar["cph"].ToString();
                        waitCarInfoGua.JCLX = waitCarInfoGua.CLPH == "" ? "1" : "0";
                        waitCarInfoGua.HPYS = "";
                        waitCarInfoGua.HPZL = dr_waitcar["hpzlid"].ToString();
                        waitCarInfoGua.FDJHM = dr_waitcar["fdjh"].ToString();
                        waitCarInfoGua.PPXH = dr_waitcar["clpp"].ToString();
                        waitCarInfoGua.VIN = dr_waitcar["clsbdh"].ToString();
                        waitCarInfoGua.CLLX = dr_waitcar["cllx"].ToString() + dr_waitcar["cllxstr"].ToString();
                        waitCarInfoGua.CZ = dr_waitcar["syr"].ToString();
                        waitCarInfoGua.CD = int.Parse(dr_waitcar["cwkc"].ToString());
                        waitCarInfoGua.KD = int.Parse(dr_waitcar["cwkk"].ToString());
                        waitCarInfoGua.GD = int.Parse(dr_waitcar["cwkg"].ToString());
                        waitCarInfoGua.HXCD = 0;
                        waitCarInfoGua.HXKD = 0;
                        waitCarInfoGua.HXGD = 0;
                        waitCarInfoGua.LBGD = 0;
                        waitCarInfoGua.ZJ1 = 0;
                        waitCarInfoGua.ZJ2 = 0;
                        waitCarInfoGua.ZJ3 = 0;
                        waitCarInfoGua.ZJ4 = 0;
                        waitCarInfoGua.ZBZL = int.Parse(dr_waitcar["zbzl"].ToString());
                        waitCarInfoGua.SCZBZL = 0;
                        waitCarInfoGua.ZDZZL = dr_waitcar["zczl"].ToString() == "" ? 0 : int.Parse(dr_waitcar["zczl"].ToString());
                        ckLWH_g.Checked = true;
                        
                        //显示选中车辆信息
                        string hy_waitcar_info = "挂车待检车辆信息查询结果如下：\r\n车牌号：" + waitCarInfoGua.CLPH + "|检验流水号：" + waitCarInfoGua.WGJYH + "|检验次数：" + waitCarInfoGua.JCCS + "|检验类别：" + waitCarInfoGua.JCLX == "0" ? "在用车检验" : "新车检验" +
                                                "\r\n外廓尺寸标准数据：" + waitCarInfoGua.CD.ToString() + "x" + waitCarInfoGua.KD.ToString() + "x" + waitCarInfoGua.GD.ToString();
                        UpdateRtb(hy_waitcar_info);
                    }
                    else
                    {
                        UpdateRtb("获取挂车车辆信息失败：" + msg);
                        ckQGLC.Checked = false;
                        return;
                    }
                    #endregion
                }
                else if (softConfig.WaitCarModel == NetWaitCarModel.大雷联网列表)
                {
                    #region dl
                    drs_waitcar = dtWaitCarList.Select("jylsh = '" + jcxx[1] + "' and jycs = '" + jcxx[2] + "'");
                    if (drs_waitcar != null && drs_waitcar.Length > 0)
                    {
                        dr_waitcar = drs_waitcar[0];

                        waitCarInfoGua.WGJYH = dr_waitcar["jylsh"].ToString();
                        waitCarInfoGua.JCCS = dr_waitcar["jycs"].ToString();
                        waitCarInfoGua.CLPH = dr_waitcar["cph"].ToString();
                        waitCarInfoGua.JCLX = waitCarInfoGua.CLPH == "" ? "1" : "0";
                        waitCarInfoGua.HPYS = "";
                        waitCarInfoGua.HPZL = dr_waitcar["hpzlid"].ToString();
                        waitCarInfoGua.FDJHM = dr_waitcar["fdjh"].ToString();
                        waitCarInfoGua.PPXH = dr_waitcar["clpp"].ToString();
                        waitCarInfoGua.VIN = dr_waitcar["clsbdh"].ToString();
                        waitCarInfoGua.CLLX = dr_waitcar["cllx"].ToString() + dr_waitcar["cllxstr"].ToString();
                        waitCarInfoGua.CZ = dr_waitcar["syr"].ToString();
                        waitCarInfoGua.CD = int.Parse(dr_waitcar["cwkc"].ToString());
                        waitCarInfoGua.KD = int.Parse(dr_waitcar["cwkk"].ToString());
                        waitCarInfoGua.GD = int.Parse(dr_waitcar["cwkg"].ToString());
                        waitCarInfoGua.HXCD = 0;
                        waitCarInfoGua.HXKD = 0;
                        waitCarInfoGua.HXGD = 0;
                        waitCarInfoGua.LBGD = 0;
                        waitCarInfoGua.ZJ1 = 0;
                        waitCarInfoGua.ZJ2 = 0;
                        waitCarInfoGua.ZJ3 = 0;
                        waitCarInfoGua.ZJ4 = 0;
                        waitCarInfoGua.ZBZL = int.Parse(dr_waitcar["zbzl"].ToString());
                        waitCarInfoGua.SCZBZL = 0;
                        waitCarInfoGua.ZDZZL = dr_waitcar["zczl"].ToString() == "" ? 0 : int.Parse(dr_waitcar["zczl"].ToString());

                        #region 确定检测项目
                        ckLWH_g.Checked = (dr_waitcar["clwkbz"].ToString() == "1");
                        ckZJ_g.Checked = (dr_waitcar["zjbz"].ToString() == "1");
                        ckLB_g.Checked = (dr_waitcar["lbjcbz"].ToString() == "1");
                        ckHX_g.Checked = false;
                        ckZBZL_g.Checked = (dr_waitcar["zbzlbz"].ToString() == "1");
                        if (ckZBZL_g.Checked)
                        {
                            //牵挂联测要检挂车整备质量时，先确认牵引车是否检整备质量，在检查是否下发牵引车整备质量，若无弹出对话框输入或取消检测
                            if (ckZBZL.Checked)
                            {
                                DialogResult dr = MessageBox.Show("牵挂联测时不能检测牵引车整备质量\r\n当前检测项目已勾选牵引车整备质量\r\n若要继续检测将自动取消检测牵引车整备质量\r\n是否继续进行检测？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (dr == DialogResult.OK)
                                {
                                    //确认执行代码
                                    ckZBZL.Checked = false;
                                    ckZBZL.Enabled = false;
                                }
                                else
                                {
                                    //其他（点取消按钮）执行代码
                                    UpdateRtb("牵挂联测时不能检测牵引车整备质量，请单检牵引车整备质量或牵挂联测挂车整备质量！");
                                    ckQGLC.Checked = false;
                                    return;
                                }
                            }

                            if (dtWaitCarList.Columns.Contains("qyczbzl"))
                            {
                                int zbzltemp = 0;
                                if (dr_waitcar["qyczbzl"].ToString() != "" && int.TryParse(dr_waitcar["qyczbzl"].ToString(), out zbzltemp) && zbzltemp > 0)
                                {
                                    waitCarInfoGua.QYCHP = zbzltemp.ToString();
                                }
                                else
                                {
                                    DialogResult dr = MessageBox.Show("牵挂联测模式下检测挂车整备质量但下发牵引车整备质量(" + dr_waitcar["qyczbzl"].ToString() + ")数据异常\r\n是否继续检测？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                    if (dr == DialogResult.OK)
                                    {
                                        //继续检测择弹出提示整备质量输入提示框
                                        pInputZBZL.BringToFront();
                                        pInputZBZL.Visible = true;
                                    }
                                    else
                                    {
                                        //其他（点取消按钮）执行代码
                                        UpdateRtb("牵挂联测时检测挂车整备质量且无牵引车整备质量，退出检测！");
                                        ckQGLC.Checked = false;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                DialogResult dr = MessageBox.Show("牵挂联测模式下检测挂车整备质量但未下发牵引车整备质量\r\n是否继续检测？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (dr == DialogResult.OK)
                                {
                                    //继续检测择弹出提示整备质量输入提示框
                                    pInputZBZL.BringToFront();
                                    pInputZBZL.Visible = true;
                                }
                                else
                                {
                                    //其他（点取消按钮）执行代码
                                    UpdateRtb("牵挂联测时检测挂车整备质量且无牵引车整备质量，退出检测！");
                                    ckQGLC.Checked = false;
                                    return;
                                }
                            }
                        }
                        #endregion

                        //显示选中车辆信息
                        string dl_waitcar_info = "挂车待检车辆信息查询结果如下：\r\n车牌号：" + waitCarInfoGua.CLPH + "|检验流水号：" + waitCarInfoGua.WGJYH + "|检验次数：" + waitCarInfoGua.JCCS + "|检验类别：" + waitCarInfoGua.JCLX == "0" ? "在用车检验" : "新车检验" +
                                                "\r\n外廓尺寸标准数据：" + waitCarInfoGua.CD.ToString() + "x" + waitCarInfoGua.KD.ToString() + "x" + waitCarInfoGua.GD.ToString();
                        UpdateRtb(dl_waitcar_info);
                    }
                    else
                    {
                        UpdateRtb("获取挂车车辆信息失败：" + msg);
                        ckQGLC.Checked = false;
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 联网查询方式
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            break;
                        case NetUploadModel.安徽:
                            break;
                        case NetUploadModel.宝辉:
                            break;
                        case NetUploadModel.大雷:
                            break;
                        case NetUploadModel.广西:
                            break;
                        case NetUploadModel.海城新疆:
                            break;
                        case NetUploadModel.海城四川:
                            break;
                        case NetUploadModel.华燕:
                            break;
                        case NetUploadModel.湖北:
                            break;
                        case NetUploadModel.康士柏:
                            break;
                        case NetUploadModel.欧润特:
                            break;
                        case NetUploadModel.上饶:
                            break;
                        case NetUploadModel.南京新仕尚:
                            break;
                        case NetUploadModel.万国:
                            break;
                        case NetUploadModel.维科:
                            break;
                        case NetUploadModel.新盾:
                            break;
                        case NetUploadModel.新力源:
                            break;
                        case NetUploadModel.益中祥:
                            break;
                        case NetUploadModel.中航:
                            break;
                        default:
                            break;
                    }

                    return;
                    #endregion
                }

                waitCarInfoGua.IsReadyToTest = true;
            }
            catch (Exception er)
            {
                ckQGLC.Checked = false;
                UpdateRtb("获取挂车信息出错：\r\n" + er.Message);
            }
        }

        #region 功能函数
        /// <summary>
        /// 获取联网配置信息
        /// </summary>
        /// <returns></returns>
        public bool getNetConfig()
        {
            try
            {
                UploadConfigModel config = new UploadConfigModel();

                string config_path = global_path + "\\uploadConfig.ini";
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                int i = 0;

                IOControl.GetPrivateProfileString("联网配置", "待检车辆来源", "0", temp, 2048, config_path);
                if (int.TryParse(temp.ToString().Trim(), out i))
                    config.WaitCarModel = (NetWaitCarModel)i;
                else
                    config.WaitCarModel = NetWaitCarModel.联网查询;

                IOControl.GetPrivateProfileString("联网配置", "待检车辆接口地址", "", temp, 2048, config_path);
                config.JkdzWaitCar = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "待检车辆接口序列号", "", temp, 2048, config_path);
                config.JkxlhWaitCar = temp.ToString().Trim();


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

                IOControl.GetPrivateProfileString("联网配置", "接口地址一", "", temp, 2048, config_path);
                config.Jkdz1 = temp.ToString().Trim();

                IOControl.GetPrivateProfileString("联网配置", "接口地址二", "", temp, 2048, config_path);
                config.Jkdz2 = temp.ToString().Trim();

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

                softConfig = config;

                return true;
            }
            catch (Exception er)
            {
                IOControl.WriteLogs("获取联网配置出错：" + er.Message);
                return false;
            }
        }

        /// <summary>
        /// 初始化待检列表显示
        /// </summary>
        private void Init_Dgv()
        {
            try
            {
                //清空dgv待检列表
                if (dgvWaitCarList.Rows.Count > 0)
                    dgvWaitCarList.Rows.Clear();

                //清空挂车待检车辆
                if (cbbGCHP.Items.Count > 0)
                    cbbGCHP.Items.Clear();

                //初始化检测项目
                ckLWH.Checked = false;
                ckLWH_g.Checked = false;
                ckZJ.Checked = false;
                ckZJ_g.Checked = false;
                ckLB.Checked = false;
                ckLB_g.Checked = false;
                ckHX.Checked = false;
                ckHX_g.Checked = false;
                ckZBZL.Checked = false;
                ckZBZL_g.Checked = false;

                ckQGLC.Checked = false;
            }
            catch (Exception er)
            {
                
            }
        }

        /// <summary>
        /// 初始化单车待检车辆信息显示
        /// </summary>
        private void Init_pSingle()
        {
            try
            {
                //清空查询信息
                tbHPHM.Text = "";
                tbVIN.Text = "";
                cbbHPZL.Text = "";
                cbbJYLB.Text = "";
                cbbGCHP.Text = "";

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
                ckLWH.Checked = false;
                ckLWH_g.Checked = false;
                ckZJ.Checked = false;
                ckZJ_g.Checked = false;
                ckLB.Checked = false;
                ckLB_g.Checked = false;
                ckHX.Checked = false;
                ckHX_g.Checked = false;
                ckZBZL.Checked = false;
                ckZBZL_g.Checked = false;
                ckQGLC.Checked = false;
            }
            catch (Exception er)
            {

            }
        }

        /// <summary>
        /// 根据从平台查询到的待检车辆信息更新显示信息及待检车辆缓存模板（单车模式查询后直接更新，待检列表模式选择待检车辆的时候更新）
        /// </summary>
        /// <param name="dr_waitcar">查到的待检车辆</param>
        /// <returns></returns>
        private WaitCarModel UpdateWaitCarInfo(DataRow drWaitCar)
        {
            try
            {
                WaitCarModel waitCar_temp = new WaitCarModel();

                if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
                {
                    #region hy
                    waitCar_temp.WGJYH = drWaitCar["jylsh"].ToString();
                    waitCar_temp.JCCS = drWaitCar["jycs"].ToString();
                    waitCar_temp.CLPH = drWaitCar["cph"].ToString();
                    waitCar_temp.JCLX = waitCar_temp.CLPH == "" ? "1" : "0";
                    waitCar_temp.HPYS = "";
                    waitCar_temp.HPZL = drWaitCar["hpzlid"].ToString();
                    waitCar_temp.FDJHM = drWaitCar["fdjh"].ToString();
                    waitCar_temp.PPXH = drWaitCar["clpp"].ToString();
                    waitCar_temp.VIN = drWaitCar["clsbdh"].ToString();
                    waitCar_temp.CLLX = drWaitCar["cllx"].ToString() + drWaitCar["cllxstr"].ToString();
                    waitCar_temp.CZ = drWaitCar["syr"].ToString();
                    waitCar_temp.CD = int.Parse(drWaitCar["cwkc"].ToString());
                    waitCar_temp.KD = int.Parse(drWaitCar["cwkk"].ToString());
                    waitCar_temp.GD = int.Parse(drWaitCar["cwkg"].ToString());
                    waitCar_temp.HXCD = 0;
                    waitCar_temp.HXKD = 0;
                    waitCar_temp.HXGD = 0;
                    waitCar_temp.LBGD = 0;
                    waitCar_temp.ZJ1 = 0;
                    waitCar_temp.ZJ2 = 0;
                    waitCar_temp.ZJ3 = 0;
                    waitCar_temp.ZJ4 = 0;
                    waitCar_temp.ZBZL = int.Parse(drWaitCar["zbzl"].ToString());
                    waitCar_temp.SCZBZL = 0;
                    waitCar_temp.ZDZZL = drWaitCar["zczl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zczl"].ToString());

                    #region 确定检测项目
                    ckLWH.Checked = (drWaitCar["M1"].ToString() == "1");
                    ckZBZL.Checked = (drWaitCar["Z1"].ToString() == "1");
                    ckZJ.Checked = false;
                    ckLB.Checked = false;
                    ckHX.Checked = false;
                    #endregion

                    //显示选中车辆信息
                    UpdateRtb("主车待检车辆信息查询结果如下：\r\n车牌号：" + waitCar_temp.CLPH + "|检验流水号：" + waitCar_temp.WGJYH + "|检验次数：" + waitCar_temp.JCCS + "|检验类别：" + waitCar_temp.JCLX == "0" ? "在用车检验" : "新车检验" +
                                "\r\n|外廓尺寸标准数据：" + waitCar_temp.CD.ToString() + "x" + waitCar_temp.KD.ToString() + "x" + waitCar_temp.GD.ToString() + "|整备质量：" + waitCar_temp.ZBZL);
                    #endregion
                }
                else if (softConfig.WaitCarModel == NetWaitCarModel.华燕联网列表)
                {
                    #region dl
                    waitCar_temp.WGJYH = drWaitCar["jylsh"].ToString();
                    waitCar_temp.JCCS = drWaitCar["jycs"].ToString();
                    waitCar_temp.CLPH = drWaitCar["hphm"].ToString();
                    waitCar_temp.JCLX = drWaitCar["jylb"].ToString() == "00" ? "1" : "0";
                    waitCar_temp.HPYS = "";
                    waitCar_temp.HPZL = drWaitCar["hpzl"].ToString();
                    waitCar_temp.FDJHM = drWaitCar["fdjh"].ToString();
                    waitCar_temp.PPXH = drWaitCar["clpp"].ToString();
                    waitCar_temp.VIN = drWaitCar["clsbdh"].ToString();
                    waitCar_temp.CLLX = drWaitCar["cllx"].ToString();
                    waitCar_temp.CZ = drWaitCar["syr"].ToString();
                    waitCar_temp.CD = int.Parse(drWaitCar["cwkc"].ToString());
                    waitCar_temp.KD = int.Parse(drWaitCar["cwkk"].ToString());
                    waitCar_temp.GD = int.Parse(drWaitCar["cwkg"].ToString());
                    waitCar_temp.HXCD = drWaitCar["hxcd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxcd"].ToString());
                    waitCar_temp.HXKD = drWaitCar["hxkd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxkd"].ToString());
                    waitCar_temp.HXGD = drWaitCar["hxgd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxgd"].ToString());
                    waitCar_temp.LBGD = drWaitCar["lbgd"].ToString() == "" ? 0 : int.Parse(drWaitCar["lbgd"].ToString());
                    waitCar_temp.ZJ1 = drWaitCar["zj1"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj1"].ToString());
                    waitCar_temp.ZJ2 = drWaitCar["zj2"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj2"].ToString());
                    waitCar_temp.ZJ3 = drWaitCar["zj3"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj3"].ToString());
                    waitCar_temp.ZJ4 = drWaitCar["zj4"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj4"].ToString());
                    waitCar_temp.ZBZL = drWaitCar["zbzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zbzl"].ToString());
                    waitCar_temp.SCZBZL = 0;
                    waitCar_temp.ZDZZL = drWaitCar["zzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zzl"].ToString());

                    #region 确定检测项目
                    ckLWH.Checked = (drWaitCar["clwkbz"].ToString() == "1");
                    ckZJ.Checked = (drWaitCar["zjbz"].ToString() == "1");
                    ckLB.Checked = (drWaitCar["lbjcbz"].ToString() == "1");
                    ckHX.Checked = false;
                    ckZBZL.Checked = (drWaitCar["zbzlbz"].ToString() == "1");
                    #endregion

                    //显示选中车辆信息
                    UpdateRtb("主车待检车辆信息查询结果如下：\r\n车牌号：" + waitCar_temp.CLPH + "|检验流水号：" + waitCar_temp.WGJYH + "|检验次数：" + waitCar_temp.JCCS + "|检验类别：" + waitCar_temp.JCLX == "0" ? "在用车检验" : "新车检验" +
                                "\r\n|外廓尺寸标准数据：" + waitCar_temp.CD.ToString() + "x" + waitCar_temp.KD.ToString() + "x" + waitCar_temp.GD.ToString() + "|整备质量：" + waitCar_temp.ZBZL);
                    #endregion
                }
                else
                {
                    #region 联网查询
                    switch (softConfig.NetModel)
                    {
                        case NetUploadModel.安车:
                            #region ac
                            waitCar_temp.WGJYH = drWaitCar["jylsh"].ToString();
                            waitCar_temp.JCCS = drWaitCar["jycs"].ToString();
                            waitCar_temp.CLPH = drWaitCar["hphm"].ToString();
                            waitCar_temp.JCLX = drWaitCar["jylb"].ToString() == "00" ? "1" : "0";
                            waitCar_temp.HPYS = "";
                            waitCar_temp.HPZL = drWaitCar["hpzl"].ToString();
                            waitCar_temp.FDJHM = drWaitCar["fdjh"].ToString();
                            waitCar_temp.PPXH = drWaitCar["clpp1"].ToString();
                            waitCar_temp.VIN = drWaitCar["clsbdh"].ToString();
                            waitCar_temp.CLLX = drWaitCar["cllx"].ToString();
                            waitCar_temp.CZ = drWaitCar["syr"].ToString();
                            waitCar_temp.CD = int.Parse(drWaitCar["cwkc"].ToString());
                            waitCar_temp.KD = int.Parse(drWaitCar["cwkk"].ToString());
                            waitCar_temp.GD = int.Parse(drWaitCar["cwkg"].ToString());
                            waitCar_temp.HXCD = 0;
                            waitCar_temp.HXKD = 0;
                            waitCar_temp.HXGD = 0;
                            waitCar_temp.LBGD = 0;
                            waitCar_temp.ZJ1 = drWaitCar["zj"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj"].ToString());
                            waitCar_temp.ZJ2 = 0;
                            waitCar_temp.ZJ3 = 0;
                            waitCar_temp.ZJ4 = 0;
                            waitCar_temp.ZBZL = drWaitCar["zbzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zbzl"].ToString());
                            waitCar_temp.SCZBZL = 0;
                            waitCar_temp.ZDZZL = drWaitCar["zzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zzl"].ToString());

                            //显示选中车辆信息
                            UpdateRtb("主车待检车辆信息查询结果如下：\r\n车牌号：" + waitCar_temp.CLPH + "|检验流水号：" + waitCar_temp.WGJYH + "|检验次数：" + waitCar_temp.JCCS + "|检验类别：" + waitCar_temp.JCLX == "0" ? "在用车检验" : "新车检验" +
                                        "\r\n|外廓尺寸标准数据：" + waitCar_temp.CD.ToString() + "x" + waitCar_temp.KD.ToString() + "x" + waitCar_temp.GD.ToString() + "|整备质量：" + waitCar_temp.ZBZL);
                            #endregion
                            break;
                        case NetUploadModel.安徽:
                            break;
                        case NetUploadModel.宝辉:
                            break;
                        case NetUploadModel.大雷:
                            #region dl
                            waitCar_temp.WGJYH = drWaitCar["jylsh"].ToString();
                            waitCar_temp.JCCS = drWaitCar["jycs"].ToString();
                            waitCar_temp.CLPH = drWaitCar["hphm"].ToString();
                            waitCar_temp.JCLX = drWaitCar["jylb"].ToString() == "00" ? "1" : "0";
                            waitCar_temp.HPYS = "";
                            waitCar_temp.HPZL = drWaitCar["hpzl"].ToString();
                            waitCar_temp.FDJHM = drWaitCar["fdjh"].ToString();
                            waitCar_temp.PPXH = drWaitCar["clpp"].ToString();
                            waitCar_temp.VIN = drWaitCar["clsbdh"].ToString();
                            waitCar_temp.CLLX = drWaitCar["cllx"].ToString();
                            waitCar_temp.CZ = drWaitCar["syr"].ToString();
                            waitCar_temp.CD = int.Parse(drWaitCar["cwkc"].ToString());
                            waitCar_temp.KD = int.Parse(drWaitCar["cwkk"].ToString());
                            waitCar_temp.GD = int.Parse(drWaitCar["cwkg"].ToString());
                            waitCar_temp.HXCD = drWaitCar["hxcd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxcd"].ToString());
                            waitCar_temp.HXKD = drWaitCar["hxkd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxkd"].ToString());
                            waitCar_temp.HXGD = drWaitCar["hxgd"].ToString() == "" ? 0 : int.Parse(drWaitCar["hxgd"].ToString());
                            waitCar_temp.LBGD = drWaitCar["lbgd"].ToString() == "" ? 0 : int.Parse(drWaitCar["lbgd"].ToString());
                            waitCar_temp.ZJ1 = drWaitCar["zj1"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj1"].ToString());
                            waitCar_temp.ZJ2 = drWaitCar["zj2"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj2"].ToString());
                            waitCar_temp.ZJ3 = drWaitCar["zj3"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj3"].ToString());
                            waitCar_temp.ZJ4 = drWaitCar["zj4"].ToString() == "" ? 0 : int.Parse(drWaitCar["zj4"].ToString());
                            waitCar_temp.ZBZL = drWaitCar["zbzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zbzl"].ToString());
                            waitCar_temp.SCZBZL = 0;
                            waitCar_temp.ZDZZL = drWaitCar["zzl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zzl"].ToString());

                            #region 确定检测项目
                            ckLWH.Checked = (drWaitCar["clwkbz"].ToString() == "1");
                            ckZJ.Checked = (drWaitCar["zjbz"].ToString() == "1");
                            ckLB.Checked = (drWaitCar["lbjcbz"].ToString() == "1");
                            ckHX.Checked = false;
                            ckZBZL.Checked = (drWaitCar["zbzlbz"].ToString() == "1");
                            #endregion

                            //显示选中车辆信息
                            UpdateRtb("主车待检车辆信息查询结果如下：\r\n车牌号：" + waitCar_temp.CLPH + "|检验流水号：" + waitCar_temp.WGJYH + "|检验次数：" + waitCar_temp.JCCS + "|检验类别：" + waitCar_temp.JCLX == "0" ? "在用车检验" : "新车检验" +
                                        "\r\n|外廓尺寸标准数据：" + waitCar_temp.CD.ToString() + "x" + waitCar_temp.KD.ToString() + "x" + waitCar_temp.GD.ToString() + "|整备质量：" + waitCar_temp.ZBZL);
                            #endregion
                            break;
                        case NetUploadModel.广西:
                            break;
                        case NetUploadModel.海城新疆:
                            break;
                        case NetUploadModel.海城四川:
                            break;
                        case NetUploadModel.华燕:
                            #region hy
                            waitCar_temp.WGJYH = drWaitCar["jylsh"].ToString();
                            waitCar_temp.JCCS = drWaitCar["jycs"].ToString();
                            waitCar_temp.CLPH = drWaitCar["cph"].ToString();
                            waitCar_temp.JCLX = waitCar_temp.CLPH == "" ? "1" : "0";
                            waitCar_temp.HPYS = "";
                            waitCar_temp.HPZL = drWaitCar["hpzlid"].ToString();
                            waitCar_temp.FDJHM = drWaitCar["fdjh"].ToString();
                            waitCar_temp.PPXH = drWaitCar["clpp"].ToString();
                            waitCar_temp.VIN = drWaitCar["clsbdh"].ToString();
                            waitCar_temp.CLLX = drWaitCar["cllx"].ToString() + drWaitCar["cllxstr"].ToString();
                            waitCar_temp.CZ = drWaitCar["syr"].ToString();
                            waitCar_temp.CD = int.Parse(drWaitCar["cwkc"].ToString());
                            waitCar_temp.KD = int.Parse(drWaitCar["cwkk"].ToString());
                            waitCar_temp.GD = int.Parse(drWaitCar["cwkg"].ToString());
                            waitCar_temp.HXCD = 0;
                            waitCar_temp.HXKD = 0;
                            waitCar_temp.HXGD = 0;
                            waitCar_temp.LBGD = 0;
                            waitCar_temp.ZJ1 = 0;
                            waitCar_temp.ZJ2 = 0;
                            waitCar_temp.ZJ3 = 0;
                            waitCar_temp.ZJ4 = 0;
                            waitCar_temp.ZBZL = int.Parse(drWaitCar["zbzl"].ToString());
                            waitCar_temp.SCZBZL = 0;
                            waitCar_temp.ZDZZL = drWaitCar["zczl"].ToString() == "" ? 0 : int.Parse(drWaitCar["zczl"].ToString());

                            #region 确定检测项目
                            ckLWH.Checked = (drWaitCar["M1"].ToString() == "1");
                            ckZBZL.Checked = (drWaitCar["Z1"].ToString() == "1");
                            ckZJ.Checked = false;
                            ckLB.Checked = false;
                            ckHX.Checked = false;
                            #endregion

                            //显示选中车辆信息
                            UpdateRtb("主车待检车辆信息查询结果如下：\r\n车牌号：" + waitCar_temp.CLPH + "|检验流水号：" + waitCar_temp.WGJYH + "|检验次数：" + waitCar_temp.JCCS + "|检验类别：" + waitCar_temp.JCLX == "0" ? "在用车检验" : "新车检验" +
                                        "\r\n|外廓尺寸标准数据：" + waitCar_temp.CD.ToString() + "x" + waitCar_temp.KD.ToString() + "x" + waitCar_temp.GD.ToString() + "|整备质量：" + waitCar_temp.ZBZL);
                            #endregion
                            break;
                        case NetUploadModel.湖北:
                            break;
                        case NetUploadModel.康士柏:
                            break;
                        case NetUploadModel.欧润特:
                            break;
                        case NetUploadModel.上饶:
                            break;
                        case NetUploadModel.南京新仕尚:
                            break;
                        case NetUploadModel.万国:
                            break;
                        case NetUploadModel.维科:
                            break;
                        case NetUploadModel.新盾:
                            break;
                        case NetUploadModel.新力源:
                            break;
                        case NetUploadModel.益中祥:
                            break;
                        case NetUploadModel.中航:
                            break;
                        default:
                            break;
                    }
                    #endregion
                }

                #region 界面显示
                lbJYLSH.Text = waitCar_temp.WGJYH;
                lbJYLB.Text = waitCar_temp.JCLX == "0" ? "" : "";
                lbCPHM.Text = waitCar_temp.CLPH;
                lbHPZL.Text = waitCar_temp.HPZL;
                lbVIN.Text = waitCar_temp.VIN;
                lbCLLX.Text = waitCar_temp.CLLX;
                lbLWHBZ.Text = waitCar_temp.CD.ToString() + " x " + waitCar_temp.KD.ToString() + " x " + waitCar_temp.GD.ToString();
                lbHXBZ.Text = waitCar_temp.HXCD.ToString() + " x " + waitCar_temp.HXKD.ToString() + " x " + waitCar_temp.HXGD.ToString();
                lbZJBZ.Text = waitCar_temp.ZJ1 + " | " + waitCar_temp.ZJ2 + " | " + waitCar_temp.ZJ3 + " | " + waitCar_temp.ZJ4;
                lbLBBZ.Text = waitCar_temp.LBGD.ToString();
                lbZBZLBZ.Text = waitCar_temp.ZBZL.ToString();
                #endregion

                return waitCar_temp;
            }
            catch (Exception er)
            {
                IOControl.WriteLogs("更新查询到的待检车辆信息到发车待检车辆信息模板出错：\r\n" + er.Message);
                return null;
            }
        }
        
        /// <summary>
        /// 输入整备质量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btInputZBZL_Click(object sender, EventArgs e)
        {
            int zbzltemp = 0;
            if (tbInputZbzl.Text != "" && int.TryParse(tbInputZbzl.Text, out zbzltemp) && zbzltemp > 0)
            {
                waitCarInfoGua.QYCHP = zbzltemp.ToString();
                pInputZBZL.Visible = false;
                tbInputZbzl.Text = "";
            }
            else
                MessageBox.Show("请输入正确格式的整备质量数据！");
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
                    #region 写主车信息（同时先将挂车信息置为空）
                    IOControl.WritePrivateProfileString("检测信息", "外观检验号", waitCarInfoZhu.WGJYH, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "是否检测长宽高", waitCarInfoZhu.SFJCCKG, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "是否检测栏板高度", waitCarInfoZhu.SFJCLBGD, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "是否检测轴距", waitCarInfoZhu.SFJCZJ, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "是否检测整备质量", waitCarInfoZhu.SFJCZBZL, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "是否检测货箱", waitCarInfoZhu.SFJCHX, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "车辆牌号", waitCarInfoZhu.CLPH, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "检测类型", waitCarInfoZhu.JCLX, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "号牌颜色", waitCarInfoZhu.HPYS, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "号牌种类", waitCarInfoZhu.HPZL, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "发动机号码", waitCarInfoZhu.FDJHM, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "品牌型号", waitCarInfoZhu.PPXH, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "检测次数", waitCarInfoZhu.JCCS, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "VIN", waitCarInfoZhu.VIN, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "车辆类型", waitCarInfoZhu.CLLX, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "车主", waitCarInfoZhu.CZ, CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "长度", waitCarInfoZhu.CD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "宽度", waitCarInfoZhu.KD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "高度", waitCarInfoZhu.GD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "货箱长度", waitCarInfoZhu.HXCD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "货箱宽度", waitCarInfoZhu.HXKD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "货箱高度", waitCarInfoZhu.HXGD.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "轴距1", waitCarInfoZhu.ZJ1.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "轴距2", waitCarInfoZhu.ZJ2.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "轴距3", waitCarInfoZhu.ZJ3.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "轴距4", waitCarInfoZhu.ZJ4.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "整备质量", waitCarInfoZhu.ZBZL.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "实测整备质量", waitCarInfoZhu.SCZBZL.ToString(), CarInfoPathTemp);
                    IOControl.WritePrivateProfileString("检测信息", "最大总质量", waitCarInfoZhu.ZDZZL.ToString(), CarInfoPathTemp);

                    IOControl.WritePrivateProfileString("检测信息", "DaLeiJCFS", daleijcfs, CarInfoPathTemp);
                    #endregion

                    #region 再写挂车信息
                    if (ckQGLC.Checked)
                    {
                        if (waitCarInfoGua != null && waitCarInfoGua.IsReadyToTest)
                        {
                            #region 写挂车信息
                            IOControl.WritePrivateProfileString("检测信息", "主挂联测", "3", CarInfoPathTemp);

                            IOControl.WritePrivateProfileString("检测信息", "挂车外观检验号", waitCarInfoGua.WGJYH, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "是否检测挂车外廓", waitCarInfoGua.SFJCCKG, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "是否检测挂车整备质量", waitCarInfoGua.SFJCZBZL, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "是否检测挂车栏板", waitCarInfoGua.SFJCLBGD, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "是否检测挂车轴距", waitCarInfoGua.SFJCZJ, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "是否检测挂车货箱", waitCarInfoGua.SFJCHX, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车号牌", waitCarInfoGua.CLPH, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车长", waitCarInfoGua.CD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车宽", waitCarInfoGua.KD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车高", waitCarInfoGua.GD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车轴距1", waitCarInfoGua.ZJ1.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车轴距2", waitCarInfoGua.ZJ2.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车轴距3", waitCarInfoGua.ZJ3.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车轴距4", waitCarInfoGua.ZJ4.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车检测类型", waitCarInfoGua.JCLX, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车号牌种类", waitCarInfoGua.HPZL, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车车辆类型", waitCarInfoGua.CLLX, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车检测次数", waitCarInfoGua.JCCS, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车厂牌型号", waitCarInfoGua.PPXH, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车发动机号码", waitCarInfoGua.FDJHM, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车车主", waitCarInfoGua.CZ, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车VIN", waitCarInfoGua.VIN, CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车整备质量", waitCarInfoGua.ZBZL.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车实测整备质量", waitCarInfoGua.SCZBZL.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车栏板高度", waitCarInfoGua.LBGD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车货箱长", waitCarInfoGua.HXCD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车货箱宽", waitCarInfoGua.HXKD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车货箱高", waitCarInfoGua.HXGD.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "挂车总质量", waitCarInfoGua.ZDZZL.ToString(), CarInfoPathTemp);
                            IOControl.WritePrivateProfileString("检测信息", "牵引车号牌", waitCarInfoGua.QYCHP, CarInfoPathTemp);
                            #endregion
                        }
                        else
                        {
                            UpdateRtb("牵挂联测时待下发挂车信息为空或异常，无法发车");
                            return false;
                        }
                    }
                    else
                    {
                        IOControl.WritePrivateProfileString("检测信息", "主挂联测", "1", CarInfoPathTemp);

                        IOControl.WritePrivateProfileString("检测信息", "挂车外观检验号", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "是否检测挂车外廓", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "是否检测挂车整备质量", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "是否检测挂车栏板", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "是否检测挂车轴距", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "是否检测挂车货箱", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车号牌", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车长", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车宽", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车高", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车轴距1", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车轴距2", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车轴距3", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车轴距4", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车检测类型", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车号牌种类", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车车辆类型", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车检测次数", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车厂牌型号", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车发动机号码", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车车主", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车VIN", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车整备质量", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车实测整备质量", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车栏板高度", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车货箱长", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车货箱宽", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车货箱高", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "挂车总质量", "", CarInfoPathTemp);
                        IOControl.WritePrivateProfileString("检测信息", "牵引车号牌", "", CarInfoPathTemp);
                    }
                    #endregion
                }
                else
                {
                    UpdateRtb("待检车辆主车信息为空或异常，无法发车");
                    return false;
                }
                
                File.Copy(CarInfoPathTemp, CarInfoPath, true);//复制临时待检车辆信息到待检车辆信息
                File.Delete(CarInfoPathTemp);

                return true;
            }
            catch (Exception er)
            {
                UpdateRtb("待检车辆信息下发检测程序出错：\r\n" + er.Message);
                return false;
            }
        }
        #endregion
    }
}
