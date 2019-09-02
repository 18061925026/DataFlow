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

namespace TaskTest
{
    public partial class FormSeries : Form
    {
        Task mProjectManager;           //项目经理
        TaskScheduler mUISchedular;
        int mCalcTimes = 1000000;       //总计算次数
        int mRefreshInterval = 100;     //刷新界面的周期

        public FormSeries()
        {
            InitializeComponent();
            mUISchedular = TaskScheduler.FromCurrentSynchronizationContext();
            mProjectManager = new Task(ProjectMangerWorking);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = mCalcTimes;
            mProjectManager.Start();  //项目启动...
        }


        /// <summary>
        /// 主任务管理器，主要负责管理各个子任务
        /// </summary>
        private void ProjectMangerWorking()
        {
            Task<object> calcTask = new Task<object>(CalcFunction, null);
            Task delayTask = Task.Delay(0);
            for (int i = 0; i < mCalcTimes; i++)
            {
                calcTask.Start();
                object result = calcTask.Result;
                calcTask = calcTask.ContinueWith(o => new Task<object>(CalcFunction, result)).Result;    //计算下一个循环
                if (delayTask.IsCompleted || i == mCalcTimes - 1)   //刷新界面
                {
                    object[] array = (object[])result;
                    double currentValue = (double)array[0];
                    int currentIndex = (int)array[1];
                    double showValue = currentValue * 4;
                    new Task(new Action(delegate { DisplayResult(currentIndex, showValue); })).Start(mUISchedular);
                    delayTask = Task.Delay(mRefreshInterval);   //下一次刷新
                }
            }
            new Task(ShowComplete).Start(mUISchedular);
        }

        /// <summary>
        /// 计算一个节点
        /// </summary>
        /// <param name="state">一个数组，索引0为当前值，索引1为当前的计算次数</param>
        /// <returns></returns>
        private object CalcFunction(object state)
        {
            object[] array;
            if (state == null)
            {
                array = new object[] { 0.0, 0 };
            }
            else
            {
                array = (object[])state;
            }
            double currentValue = (double)array[0];
            int currentIndex = (int)array[1];

            double result = 0;
            if (currentIndex % 2 == 0)
            {
                result = currentValue + 1.0 / (currentIndex * 2 + 1);
            }
            else
            {
                result = currentValue - 1.0 / (currentIndex * 2 + 1);
            }

            object[] resultArr = new object[2];
            resultArr[0] = result;
            resultArr[1] = currentIndex + 1;

            return resultArr;
        }

        private void DisplayResult(int currentIndex, double showValue)
        {
            txtResult.Text = string.Format("Times : {0}\r\nValue : {1}", currentIndex, showValue);
            progressBar1.Value = currentIndex;
        }

        /// <summary>
        /// 报告完成
        /// </summary>
        private void ShowComplete()
        {
            MessageBox.Show("Calc PI Complete!");
        }
    }
}
