using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks.Dataflow;
using System.Threading;

namespace DataFlowTest
{
    public partial class PostTest : Form
    {
        TransformBlock<int,string> mTransBlock ;

        public PostTest()
        {
            InitializeComponent();
            mTransBlock = new TransformBlock<int, string>(input => TransToString(input));
        }

        private string TransToString(int input)
        {
            Thread.Sleep(100);
            return input.ToString();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            for (int i = 0; i < 100; i++)
            {
                mTransBlock.Post(i + 1);
            }

            while (!this.IsDisposed)
            {
                string r = await mTransBlock.ReceiveAsync();
                txtResult.AppendText( r + ",");
            }
        }
    }
}
