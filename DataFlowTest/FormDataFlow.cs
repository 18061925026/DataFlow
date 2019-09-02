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
using System.IO;

namespace DataFlowTest
{
    public partial class FormDataFlow : Form
    {
        TaskScheduler mUIScheduler;

        BufferBlock<int> mBufferBlock;       //输入缓存
        TransformBlock<int, int> mMultiTranForm;     //乘法转换器
        TransformBlock<int, int> mIncreaseTranForm;     //加法转换器
        BroadcastBlock<int> mBroadcastBlock;        //将数据1分3，分发给mDisplayAction、mStringTransForm
        TransformBlock<int, string> mStringTransForm;     //字符串转换器
        ActionBlock<string> mDisplayAction;        //输出界面
        ActionBlock<int> mConsoleWriteAction;        //控制台输出
        ActionBlock<int> mSaveLogAction;        //记录日志

        DataflowLinkOptions mDataOption;   //管道连接设置
        ExecutionDataflowBlockOptions mUISchedulerOption;

        //IPropagatorBlock<int,string> mMyDataFlowBlock;   //自定义

        volatile bool mDoInput = false;

        public FormDataFlow()
        {
            InitializeComponent();

            mUIScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            mUISchedulerOption = new ExecutionDataflowBlockOptions() { TaskScheduler = mUIScheduler };
            mDataOption = new DataflowLinkOptions { PropagateCompletion = true };

            mBufferBlock = new BufferBlock<int>();    //输入缓存
            mMultiTranForm = new TransformBlock<int, int>(input => input * 100);        //输入值乘100
            mIncreaseTranForm = new TransformBlock<int, int>(input => input + 5);       //再加5

            mBroadcastBlock = new BroadcastBlock<int>(input => input);   //分发给3个部分处理

            mStringTransForm = new TransformBlock<int, string>(input => DateTime.Now.ToString("HH:mm:ss :") + input);   //转换成时间加字符串
            mConsoleWriteAction = new ActionBlock<int>(input => Console.WriteLine("BroadCast: " + input));  //控制台输出
            mSaveLogAction = new ActionBlock<int>(input => SaveLog(input));     //记录到本地日志
            mDisplayAction = new ActionBlock<string>(input => DisplayInfo(input), mUISchedulerOption);      //界面输出

            /*开始建立连接*/
            //mMyDataFlowBlock = DataflowBlock.Encapsulate(mBufferBlock, mStringTransForm);
            mBufferBlock.LinkTo(mMultiTranForm);
            mMultiTranForm.LinkTo(mIncreaseTranForm, mDataOption);
            mIncreaseTranForm.LinkTo(mBroadcastBlock);

            /*数据1分3*/
            mBroadcastBlock.LinkTo(mSaveLogAction);
            mBroadcastBlock.LinkTo(mConsoleWriteAction);
            mBroadcastBlock.LinkTo(mStringTransForm);

            mStringTransForm.LinkTo(mDisplayAction);
        }

        /// <summary>
        /// 界面输出
        /// </summary>
        /// <param name="input"></param>
        private void DisplayInfo(string input)
        {
            if (mDoInput)
            {
                int rowIndex = dataGridView1.Rows.Add(input);
                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="value"></param>
        private void SaveLog(int value)
        {
            string filename = @"D:\\MyLog.txt";
            using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff: ") + value);
                }
            }
        }

        private async void btnStartInput_Click(object sender, EventArgs e)
        {
            mDoInput = !mDoInput;
            dataGridView1.Rows.Clear();
            btnStartInput.Text = mDoInput ? "Stop" : "Input";
            while (mDoInput && !this.IsDisposed)
            {
                mBufferBlock.Post(DateTime.Now.Second);    //存入数据
                await Task.Delay(100);
            }
        }
    }
}
