using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    public class ClassThreadTest
    {
        Thread mWorkThread;         //工作线程
        MyClassModal mMyClass;
        int mCount = 0;


        public ClassThreadTest()
        {
            mWorkThread = new Thread(o => ThreadRun());
            mWorkThread.Start();
            mMyClass = new MyClassModal();
        }

        public void Clear()
        {
            mMyClass = null;
            mCount = 0;
        }

        private void ThreadRun()
        {
            while (true)
            {
                //异步隐患
                if (mMyClass != null)
                {
                    mMyClass.MyClassFun();
                }
                mCount++;  
            }
        }
    }

    public class MyClassModal
    {
        public void MyClassFun()
        {

        }
    }
}
