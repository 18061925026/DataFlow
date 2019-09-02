using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ThreadTest
{
    public partial class FormInterrupt : Form
    {
        Thread mWorkThread;          //工作线程
        bool mIsWorking = true;     //线程是否退出
        bool mIsRunning = false;     //是否暂停
        EventWaitHandle mIsWorkingEvent = new AutoResetEvent(true);      //中断信号事件

        public FormInterrupt()
        {
            InitializeComponent();
            mWorkThread = new Thread(o => ThreadRun());
            mWorkThread.Start();
            mIsWorkingEvent.Set();
        }

        private void ThreadRun()
        {
            DateTime time = new DateTime();
            while (mIsWorking)
            {
                if (mIsRunning)
                {
                    time = time.AddMilliseconds(1000);
                    string timeShow = time.ToString("HH:mm:ss.ff");
                    this.BeginInvoke(new Action(delegate 
                    {
                        lbTime.Text = timeShow;
                    }));
                }
                mIsWorkingEvent.WaitOne(1000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放线程资源
            mIsWorking = false;
            mIsWorkingEvent.Set();    //取消等待
            if (!mWorkThread.Join(500))  //线程是否能够正常退出
            {
                if (null != mWorkThread && mWorkThread.IsAlive)     //非正常退出时，强制关闭(不会进入)
                {
                    mWorkThread.Abort();
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            mIsRunning = !mIsRunning;
            btnRun.Text = mIsRunning ? "Stop" : "Start";
            mIsWorkingEvent.Set();   //取消等待
        }
    }
}
