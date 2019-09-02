using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace RV.LinacRecordPlayer
{
    public partial class FormRecordPlayer : Form
    {
        volatile int mIsStartCount = 0;
        volatile bool mIsPause = false;
        ManualResetEvent mPauseEvent = new ManualResetEvent(true);

        public FormRecordPlayer()
        {
            InitializeComponent();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (fdbSelectPath.ShowDialog() == DialogResult.OK) txtPath.Text = fdbSelectPath.SelectedPath;
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            btnPause.Text = (mIsPause = !mIsPause) ? "Continue" : "Pause";
            if (mIsPause) mPauseEvent.Reset();
            else mPauseEvent.Set();
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            int curCount = ++mIsStartCount;
            mIsPause = false;
            mPauseEvent.Set();
            btnStart.Text = curCount % 2 != 0 ? "Stop" : "Start";
            txtPath.Enabled = btnOpen.Enabled = curCount % 2 == 0;
            lbTime.Text = (trackBarProgress.Value = 0).ToString();
            btnPause.Text = "Pause";
            picBoxMain.Image = null;
            if (curCount % 2 == 0) return;
            try
            {
                List<string> fileList = new DirectoryInfo(txtPath.Text.Trim()).GetFiles("*.bmp").ToList().ConvertAll(f => f.FullName);
                fileList.Sort();
                trackBarProgress.Maximum = Convert.ToInt32(Path.GetFileNameWithoutExtension(fileList[fileList.Count - 1]));
                int currentTime = 0;
                foreach (string file in fileList)
                {
                    try
                    {
                        int timeInfo = Convert.ToInt32(Path.GetFileNameWithoutExtension(file));
                        await Task.Delay(Math.Max(0, timeInfo - currentTime));
                        await Task.Run(delegate { mPauseEvent.WaitOne(); });
                        if (curCount != mIsStartCount) return;
                        lbTime.Text = (trackBarProgress.Value = currentTime = timeInfo).ToString();
                        picBoxMain.Image = new Bitmap(file);
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}
