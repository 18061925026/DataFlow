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

namespace AsyncTest
{
    public partial class AsyncTestForm : Form
    {
        int mMaxValue = 100;

        public AsyncTestForm()
        {
            InitializeComponent();
            progressBar1.Maximum = progressBar2.Maximum = progressBar3.Maximum = mMaxValue;
        }

        private async void btnStart1_Click(object sender, EventArgs e)
        {
            while (progressBar1.Value != progressBar1.Maximum && !this.IsDisposed)
            {
                Task<object> task = new Task<object>(AddFunction1, progressBar1.Value);
                task.Start();
                progressBar1.Value = (int)(await task);
            }
        }

        private async void btnStart2_Click(object sender, EventArgs e)
        {
            while (progressBar2.Value != progressBar2.Maximum && !this.IsDisposed)
            {
                Task<object> task = new Task<object>(AddFunction2, progressBar2.Value);
                task.Start();
                progressBar2.Value = (int)(await task);
            }
        }

        private async void btnStart3_Click(object sender, EventArgs e)
        {
            while (progressBar3.Value != progressBar3.Maximum && !this.IsDisposed)
            {
                Task<object> task = new Task<object>(AddFunction3, progressBar3.Value);
                task.Start();
                progressBar3.Value = (int)(await task);
            }
        }

        /*后台运行的方法*/
        private object AddFunction1(object current)
        {
            Thread.Sleep(50);
            return ((int)current + 1);
        }
        private object AddFunction2(object current)
        {
            Thread.Sleep(80);
            return ((int)current + 1);
        }
        private object AddFunction3(object current)
        {
            Thread.Sleep(120);
            return ((int)current + 1);
        }
    }
}
