using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLink
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private LwhUploadOnline.UploadConfigModel softConfig = new LwhUploadOnline.UploadConfigModel();

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LwhUploadOnline.UploadConfigModel temp = LwhUploadOnline.configControl.getUploadConfig();
            if (temp != null)
            {
                softConfig = temp;
                InitShow();
            }
            else
                MessageBox.Show("获取配置信息失败，请检测配置文件是否存在并重启软件重新配置！");
        }

        private void btSave1_Click(object sender, EventArgs e)
        {
            softConfig.StationID = tbJczbh.Text;
            softConfig.LineID = tbJcxbh.Text;
            softConfig.WkDeviceID = tbwkgwh.Text;
            softConfig.ZbzlDeviceID = tbzbzlgwh.Text;
            softConfig.WkFrontPicBh = tbwkqz.Text;
            softConfig.WkBackPicBh = tbwkhz.Text;
            softConfig.ZbzlFrontPicBh = tbzbzlqz.Text;
            softConfig.ZbzlBackPicBh = tbzbzlhz.Text;
            softConfig.Xtlb = tbxtlb.Text;
            softConfig.CJBH = tbcjbh.Text;
            softConfig.DWMC = tbdwmc.Text;
            softConfig.DWJGDM = tbdwjgdm.Text;
            softConfig.YHXM = tbyhxm.Text;
            softConfig.YHBS = tbyhbs.Text;
            softConfig.ZDBS = tbzdbs.Text;
            softConfig.LocalIP = cbbLoaclIP.Text;
            softConfig.WKDH = textBoxWkdh.Text;
            softConfig.ZBZLDH = textBoxZBZLDH.Text;

            if (LwhUploadOnline.configControl.WriteBaseConfig(softConfig))
                MessageBox.Show("保存成功");
            else
                MessageBox.Show("保存失败");
        }

        private void btSave2_Click(object sender, EventArgs e)
        {
            softConfig.WaitCarModel = (LwhUploadOnline.NetWaitCarModel)cbbWaitCarModel.SelectedIndex;
            softConfig.JkxlhWaitCar = tbjkxlhWaitCar.Text;
            softConfig.JkdzWaitCar = tbjkdzWaitCar.Text;
            softConfig.NetModel = (LwhUploadOnline.NetUploadModel)cbbUploadModel.SelectedIndex;
            softConfig.Jkxlh = tbjkxlh.Text;
            softConfig.Jkdz = tbjkdz1.Text;
            softConfig.NetArea = (LwhUploadOnline.NetAreaModel)cbblwdq.SelectedIndex;
            softConfig.PicSendTimes = (int)nudPicUploadTimes.Value;
            softConfig.dl_Send18Jxx = checkBoxDL_send18Jxx.Checked;
            softConfig.dl_Send18H05 = checkBoxDL_send18H05.Checked;

            if (LwhUploadOnline.configControl.WriteNetConfig(softConfig))
                MessageBox.Show("保存成功");
            else
                MessageBox.Show("保存失败");
        }

        private void InitShow()
        {
            try
            {
                tbJczbh.Text = softConfig.StationID;
                tbJcxbh.Text = softConfig.LineID;
                tbwkgwh.Text = softConfig.WkDeviceID;
                tbzbzlgwh.Text = softConfig.ZbzlDeviceID;
                tbwkqz.Text = softConfig.WkFrontPicBh;
                tbwkhz.Text = softConfig.WkBackPicBh;
                tbzbzlqz.Text = softConfig.ZbzlFrontPicBh;
                tbzbzlhz.Text = softConfig.ZbzlBackPicBh;
                tbxtlb.Text = softConfig.Xtlb;
                tbcjbh.Text = softConfig.CJBH;
                tbdwmc.Text = softConfig.DWMC;
                tbdwjgdm.Text = softConfig.DWJGDM;
                tbyhxm.Text = softConfig.YHXM;
                tbyhbs.Text = softConfig.YHBS;
                tbzdbs.Text = softConfig.ZDBS;
                cbbLoaclIP.Text = softConfig.LocalIP;
                textBoxWkdh.Text = softConfig.WKDH;
                textBoxZBZLDH.Text = softConfig.ZBZLDH;
                cbbWaitCarModel.SelectedIndex = (int)softConfig.WaitCarModel;
                tbjkxlhWaitCar.Text = softConfig.JkxlhWaitCar;
                tbjkdzWaitCar.Text = softConfig.JkdzWaitCar;
                cbbUploadModel.SelectedIndex = (int)softConfig.NetModel;
                tbjkxlh.Text = softConfig.Jkxlh;
                tbjkdz1.Text = softConfig.Jkdz;
                cbblwdq.SelectedIndex = (int)softConfig.NetArea;
                nudPicUploadTimes.Value = (decimal)softConfig.PicSendTimes;

                checkBoxDL_send18Jxx.Checked= softConfig.dl_Send18Jxx;
                checkBoxDL_send18H05.Checked= softConfig.dl_Send18H05;
            }
            catch { }
        }
    }
}
