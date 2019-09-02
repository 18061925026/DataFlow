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
    public partial class FormParallel : Form
    {
        Task mProjectManager;           //项目经理
        TaskScheduler mUISchedular;
        int mParallelNum = 16;          //并行数量
        int mCalcTimes = 1000000;       //总计算次数
        int mRefreshInterval = 100;     //刷新界面的周期

        public FormParallel()
        {
            InitializeComponent();
            mUISchedular = TaskScheduler.FromCurrentSynchronizationContext();
            mProjectManager = new Task(ProjectMangerWorking);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = mCalcTimes;
            mProjectManager.Start();   //项目启动...
        }

        /// <summary>
        /// 主任务管理器，主要负责管理各个子任务
        /// </summary>
        private void ProjectMangerWorking()
        {
            double currentResult = 0;
            Task delayTask = Task.Delay(0);
            for (int i = 0; i < mCalcTimes; i++)
            {
                Task<double>[] paralletTasks = new Task<double>[mParallelNum];    //并行任务集合

                //for (int j = 0; j < mParallelNum; j++)    
                Parallel.For(0, mParallelNum, (j) =>       //并行子循环
                {
                    int subIndex = j;   //？一定要用局部变量缓存？
                    int mainIndex = i;        //？一定要用局部变量缓存？
                    Task<double> subTask = new Task<double>(o => CalcSub(mParallelNum, subIndex, mainIndex), null);    //每个分项的计算
                    paralletTasks[j] = subTask;
                    subTask.Start();
                });
                Task<double[]> subResultsTask = Task.WhenAll(paralletTasks);    //等待所有并行任务完成
                Task<double> sumTask = subResultsTask.ContinueWith(o => SumFun(o.Result));    //汇总总和
                currentResult += sumTask.Result;

                if (delayTask.IsCompleted || i == mCalcTimes - 1)   //刷新界面
                {
                    double showValue = currentResult * 4;
                    new Task(new Action(delegate { DisplayResult(i, showValue); })).Start(mUISchedular);
                    delayTask = Task.Delay(mRefreshInterval);    //下一次刷新
                }
            }

            new Task(ShowComplete).Start(mUISchedular);     //报告完成
        }

        /// <summary>
        /// 计算子节点的值
        /// </summary>
        /// <param name="paralletNum"></param>
        /// <param name="currentIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private double CalcSub(int paralletNum,int subIndex, int mainIndex)
        {
            double ratio = subIndex % 2 == 0 ? 1.0 : -1.0;
            return ratio / ((mainIndex * paralletNum + subIndex) * 2 + 1);
        }

        /// <summary>
        /// 计算子节点总和
        /// </summary>
        /// <param name="subResults"></param>
        /// <returns></returns>
        private double SumFun(double[] subResults)
        {
            return subResults.Sum();
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <param name="showValue"></param>
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
