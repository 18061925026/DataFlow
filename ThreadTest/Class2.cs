using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    public class ClassThreadTest2
    {
        public delegate void RefreshHandle(object sender, EventArgs e);
        public event RefreshHandle OnRefresh;

        Thread mWorkThread;         //工作线程
        object mLockKey1 = new object();
        object mLockKey2 = new object();

        public ClassThreadTest2()
        {
            mWorkThread = new Thread(o => ThreadRun());
            mWorkThread.Start();
        }

        private void ThreadRun()
        {
            while (true)
            {
                #region - 1 -
                try
                {
                    Monitor.Enter(mLockKey1);
                    //....一万行代码
                }
                finally
                {
                    Monitor.Exit(mLockKey1);
                }
                #endregion

                #region - 2 -
                try
                {
                    Monitor.Enter(mLockKey1);
                    lock (mLockKey2)
                    {
                        //.....
                    }
                }
                finally
                {
                    Monitor.Exit(mLockKey1);
                }
                #endregion

                #region - 3 -
                try
                {
                    Monitor.Enter(mLockKey1);
                    if (OnRefresh != null)
                    {
                        OnRefresh(this, new EventArgs());
                    }
                }
                finally
                {
                    Monitor.Exit(mLockKey1);
                }
                #endregion
            }
        }
    }
}
