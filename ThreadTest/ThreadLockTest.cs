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
    public partial class ThreadLockTest : Form
    {
        Thread mThread1;
        Thread mThread2;
        Thread mThread3;
        Thread mThread4;
        Thread mThread5;
        Thread mThread6;
        Thread mThread7;
        Thread mThread8;

        object mKey = 1;

        List<int> mResult = new List<int>();

        public ThreadLockTest()
        {
            InitializeComponent();
        
        }
     
        private void btnStart_Click(object sender, EventArgs e)
        {
            mResult.Clear();
            mThread1 = new Thread(o => WorkingRun(1));
            mThread2 = new Thread(o => WorkingRun(2));
            mThread3 = new Thread(o => WorkingRun(3));
            mThread4 = new Thread(o => WorkingRun(4));
            mThread5 = new Thread(o => WorkingRun(5));
            mThread6 = new Thread(o => WorkingRun(6));
            mThread7 = new Thread(o => WorkingRun(7));
            mThread8 = new Thread(o => WorkingRun(8));

            mThread1.Start();
            Thread.Sleep(10);
            mThread2.Start();
            Thread.Sleep(10);
            mThread3.Start();
            Thread.Sleep(10);
            mThread4.Start();
            Thread.Sleep(10);
            mThread5.Start();
            Thread.Sleep(10);
            mThread6.Start();
            Thread.Sleep(10);
            mThread7.Start();
            Thread.Sleep(10);
            mThread8.Start();
        }
        public void WorkingRun(int v)
        {
            //lock (this)
            //{
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " Add: " + v);
                Thread.Sleep(100);
                mResult.Add(v);
            //}
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Join(",", mResult.ConvertAll(i => i.ToString()));
        }
    }
}
