using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadPoolTest
{
    public partial class ThreadPoolForm : Form
    {
        int m_ID = 0;

        public ThreadPoolForm()
        {
            InitializeComponent();
        }

        private void ThreadRun(int id)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            this.Invoke(new Action(delegate
            {
                txtResult.AppendText(string.Format("{2}  FunID: {0} \tThreadID: {1}\tStart!\n", id, threadId, DateTime.Now.ToString("HH:mm:ss.fff: ")));
            }));

            for (int times = 0; times < 3; times++)
            {
                string result = string.Format("{3}  FunID: {0} \tThreadID: {1}\tTimes: {2}\n", id, threadId, times, DateTime.Now.ToString("HH:mm:ss.fff: "));
                Thread.Sleep(1000);
                this.Invoke(new Action(delegate
                {
                    txtResult.AppendText(result);
                }));
            }

            this.Invoke(new Action(delegate
            {
                txtResult.AppendText(string.Format("{2}  FunID: {0} \tThreadID: {1}\tEnd!\n", id, threadId, DateTime.Now.ToString("HH:mm:ss.fff: ")));
            }));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o => ThreadRun(m_ID++));
            //new Thread(o => ThreadRun(m_ID++)).Start();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_ID = 0;
            txtResult.Clear();
        }
    }
}
