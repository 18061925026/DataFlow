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

    public partial class FormLoopTest : Form
    {
        Thread mWorkThread;         //工作线程
        volatile bool mIsWorking = true;    //线程是否退出
        volatile bool mIsRunning = false;    //是否暂停

        public FormLoopTest()
        {
            InitializeComponent();
            mWorkThread = new Thread(o => ThreadRun());
            mWorkThread.IsBackground = true;   //后台线程会随着进程的退出而结束
            mWorkThread.Start();

        }

        private void ThreadRun()
        {
            DateTime time = new DateTime();
            while (mIsWorking)
            {
                if (mIsRunning)
                {
                    time = time.AddMilliseconds(1000);
                    //Console.WriteLine("Invoke Start");
                    this.Invoke(new Action(delegate
                    {
                        //Console.WriteLine("Invoke In");
                        lbTime.Text = time.ToString("HH:mm:ss");
                    }));
                    //Console.WriteLine("Invoke End");
                }
                Thread.Sleep(1000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mIsWorking = false;
            if (!mWorkThread.Join(2000))   //线程是否能够正常退出
            {
                if (null != mWorkThread && mWorkThread.IsAlive)     //非正常退出时，强制关闭(不会进入)
                {
                    mWorkThread.Abort();
                }
            }
        }
        private void FormLoopTest_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            mIsRunning = !mIsRunning;
            btnRun.Text = mIsRunning ? "Stop":"Start";
        }
    }
}
