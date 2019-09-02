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
    public partial class DualPostTest : Form
    {
        TaskScheduler mUIScheduler;

        BufferBlock<int> mBufferBlock;
        TransformBlock<int, string> mTransBlock1;
        TransformBlock<int, string> mTransBlock2;

        public DualPostTest()
        {
            InitializeComponent();
            mUIScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            mBufferBlock = new BufferBlock<int>();

            mTransBlock1 = new TransformBlock<int, string>(input => TransToString("A", input), new ExecutionDataflowBlockOptions() { BoundedCapacity = 50 });
            mTransBlock2 = new TransformBlock<int, string>(input => TransToString("B", input), new ExecutionDataflowBlockOptions() { BoundedCapacity = 10 });

            mBufferBlock.LinkTo(mTransBlock1);
            mBufferBlock.LinkTo(mTransBlock2);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtResult1.Clear();
            txtResult2.Clear();
            for (int i = 0; i < 100; i++)
            {
                mBufferBlock.Post(i + 1);
            }
            new Task(delegate { Receive1(); }).Start(mUIScheduler);
            new Task(delegate { Receive2(); }).Start(mUIScheduler);
        }

        private string TransToString(string head,int input)
        {
            Thread.Sleep(100);
            return head + input.ToString();
        }

        private async void Receive1()
        {
            while (true)
            {
                string result = await mTransBlock1.ReceiveAsync();
                if (!this.IsDisposed)
                {
                    txtResult1.AppendText(result + ",");
                }
            }
        }

        private async void Receive2()
        {
            while (true)
            {
                string result = await mTransBlock2.ReceiveAsync();
                if (!this.IsDisposed)
                {
                    txtResult2.AppendText(result + ",");
                }
            }
        }
    }
}
