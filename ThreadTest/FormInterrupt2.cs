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
    public partial class FormInterrupt2 : Form
    {
        Thread mWorkThread;          //工作线程
        AutoResetEvent mTickEvent = new AutoResetEvent(false);      //定时器
        ManualResetEvent mRunningEvent = new ManualResetEvent(false);      //中断器
        bool mIsRunning = false;     //是否暂停
        volatile bool mIsWorking = true;     //线程是否退出


        public FormInterrupt2()
        {
            InitializeComponent();
            mWorkThread = new Thread(o => ThreadRun());
            mWorkThread.Start();
        }

        private void ThreadRun()
        {
            DateTime time = new DateTime();
            while (mIsWorking)
            {
                mRunningEvent.WaitOne();       //是否继续
                string timeShow = time.ToString("HH:mm:ss.ff");
                this.BeginInvoke(new Action(delegate
                {
                    lbTime.Text = timeShow;
                }));
                mTickEvent.WaitOne(100);    //100ms循环一次
                time = time.AddMilliseconds(100);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (mIsRunning)    
            {
                mIsRunning = false;
                btnRun.Text = "Start";
                mRunningEvent.Reset();   //暂停
            }
            else   
            {
                mIsRunning = true;
                btnRun.Text = "Stop";
                mRunningEvent.Set();    //启动
            }
        }

        private void FormInterrupt2_FormClosing(object sender, FormClosingEventArgs e)
        {
            mIsWorking = false;
            mRunningEvent.Set();
            mTickEvent.Set();
            if (!mWorkThread.Join(1000))  //线程是否能够正常退出
            {
                if (null != mWorkThread && mWorkThread.IsAlive)     //非正常退出时，强制关闭(不会进入)
                {
                    mWorkThread.Abort();
                }
            }
        }
    }
}
