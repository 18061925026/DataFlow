using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormDataFlow());

            //TestActionBlock();
            //TestTransformBlock();
            //TestTransformManyBlock();
            //TestBroadcastBlock();
            //TestWriteOnceBlock();
            //TestBatchBlock();
            //TestJoinBlock();
            //TestBatchedJoinBlock();

            //TestBlocks01();
            TestBlocks02();

            Console.ReadLine();
        }

        private static void TestActionBlock()
        {
            //var actionBlock = new ActionBlock<int>(n => Console.WriteLine(n));
            var actionBlock = new ActionBlock<int>(n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(n);
            }, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3 });
            for (int i = 0; i < 30; i++)
            {
                actionBlock.Post(i * 10);
            }

            actionBlock.Complete();
            actionBlock.Completion.Wait();
        }

        private static void TestTransformBlock()
        {
            //var transformBlock = new TransformBlock<int, double>(n => Math.Sqrt(n));

            //transformBlock.Post(10);
            //transformBlock.Post(20);
            //transformBlock.Post(30);

            //for (int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine(transformBlock.Receive());
            //}

            var tbUrl = new TransformBlock<string, string>((url) =>
            {
                WebClient webClient = new WebClient();
                string res = webClient.DownloadString(new Uri(url));
                return res;
            });

            tbUrl.Post("https://www.cnblogs.com/haoxinyue/archive/2013/03/01/2938959.html");
            tbUrl.Post("https://zhidao.baidu.com/question/570037232.html");

            Console.WriteLine(tbUrl.Receive());
            Console.WriteLine(tbUrl.Receive());
        }

        private static void TestTransformManyBlock()
        {
            //var transformManyBlock = new TransformManyBlock<string, char>(s => s.ToCharArray());
            //transformManyBlock.Post("Hello");
            //transformManyBlock.Post("World");

            //for (int i = 0; i < ("Hello" + "World").Length; i++)
            //{
            //    Console.WriteLine(transformManyBlock.Receive());
            //}

            //var tmb = new TransformManyBlock<int, int>((i) => { return new int[] { i, i + 1 }; });
            //ActionBlock<int> ab = new ActionBlock<int>((i) => Console.WriteLine(i));
            //tmb.LinkTo(ab);
            //for (int i = 0; i < 4; i++)
            //{
            //    tmb.Post(i);
            //}
            //Console.WriteLine("Finished post");
        }

        private static void TestBroadcastBlock()
        {
            var bb = new BroadcastBlock<int>(i => { return i; });

            var displayBlock = new ActionBlock<int>(i => Console.WriteLine("Displayed " + i));
            var saveBlock = new ActionBlock<int>(i => Console.WriteLine("Saved " + i));
            var sendBlock = new ActionBlock<int>(i => Console.WriteLine("Send " + i));

            bb.LinkTo(displayBlock);
            bb.LinkTo(saveBlock);
            bb.LinkTo(sendBlock);

            for (int i = 0; i < 4; i++)
            {
                bb.Post(i);
            }
        }

        private static void TestWriteOnceBlock()
        {
            var bb = new WriteOnceBlock<int>(i => { return i; });

            var displayBlock = new ActionBlock<int>(i => Console.WriteLine("Displayed " + i));
            var saveBlock = new ActionBlock<int>(i => Console.WriteLine("Saved " + i));
            var sendBlock = new ActionBlock<int>(i => Console.WriteLine("Send " + i));

            bb.LinkTo(displayBlock);
            bb.LinkTo(saveBlock);
            bb.LinkTo(sendBlock);

            for (int i = 0; i < 4; i++)
            {
                bb.Post(i);
            }
        }

        private static void TestBatchBlock()
        {
            //var batchBlock = new BatchBlock<int>(10);
            //for (int i = 0; i < 13; i++)
            //{
            //    batchBlock.Post(i);
            //}
            //batchBlock.Complete();
            //Console.WriteLine("The sum of the elements in batch 1 is {0}.", batchBlock.Receive().Sum());
            //Console.WriteLine("The sum of the elements in batch 2 is {0}.", batchBlock.Receive().Sum());

            var bb = new BatchBlock<int>(3);
            var ab = new ActionBlock<int[]>(i =>
            {
                string s = string.Empty;
                foreach (int m in i)
                {
                    s += m + " ";
                }
                Console.WriteLine(s);
            });

            bb.LinkTo(ab);
            for (int i = 0; i < 10; i++)
            {
                bb.Post(i);
            }
            bb.Complete();
        }

        private static void TestJoinBlock()
        {
            //var joinBlock = new JoinBlock<int, int, char>();

            //joinBlock.Target1.Post(3);
            //joinBlock.Target1.Post(6);

            //joinBlock.Target2.Post(5);
            //joinBlock.Target2.Post(4);

            //joinBlock.Target3.Post('+');
            //joinBlock.Target3.Post('-');

            //for (int i = 0; i < 2; i++)
            //{
            //    var data = joinBlock.Receive();
            //    switch (data.Item3)
            //    {
            //        case '+':
            //            Console.WriteLine("{0} + {1} = {2}", data.Item1, data.Item2, data.Item1 + data.Item2);
            //            break;
            //        case '-':
            //            Console.WriteLine("{0} - {1} = {2}", data.Item2, data.Item1, data.Item2 - data.Item1);
            //            break;
            //        default:
            //            Console.WriteLine("Unknown operator '{0}'.", data.Item3);
            //            break;
            //    }
            //}

            var jb = new JoinBlock<int, string>();
            var ab = new ActionBlock<Tuple<int, string>>(i =>
            {
                Console.WriteLine(i.Item1 + " " + i.Item2);
            });

            jb.LinkTo(ab);

            for (int i = 0; i < 5; i++)
            {
                jb.Target1.Post(i);
            }

            for (int i = 5; i > 0; i--)
            {
                Thread.Sleep(1000);
                jb.Target2.Post(i.ToString());
            }
        }

        private static void TestBatchedJoinBlock()
        {
            //Func<int, int> DoWork = n =>
            //{
            //    if (n < 0)
            //    {
            //        throw new ArgumentOutOfRangeException();
            //    }
            //    return n;
            //};

            //var batchedJoinBlock = new BatchedJoinBlock<int, Exception>(7);
            //foreach (int i in new int[] { 5, 6, -7, -22, 13, 55, 0 })
            //{
            //    try
            //    {
            //        batchedJoinBlock.Target1.Post(DoWork(i));
            //    }
            //    catch (ArgumentOutOfRangeException ex)
            //    {
            //        batchedJoinBlock.Target2.Post(ex);
            //    }
            //}
            //var res = batchedJoinBlock.Receive();
            //foreach (int n in res.Item1)
            //{
            //    Console.WriteLine(n);
            //}
            //foreach (Exception e in res.Item2)
            //{
            //    Console.WriteLine(e.Message);
            //}

            var bjb = new BatchedJoinBlock<int, string>(3);
            var ab = new ActionBlock<Tuple<IList<int>, IList<string>>>(i =>
            {
                Console.WriteLine("------------------------------------");
                foreach (int m in i.Item1)
                {
                    Console.WriteLine(m);
                }
                foreach (string s in i.Item2)
                {
                    Console.WriteLine(s);
                }
            });
            bjb.LinkTo(ab);
            for (int i = 0; i < 5; i++)
            {
                bjb.Target1.Post(i);
            }
            for (int i = 5; i > 0; i--)
            {
                bjb.Target2.Post(i.ToString());
            }
        }

        private static void TestBlocks01()
        {
            ConcurrentExclusiveSchedulerPair pair = new ConcurrentExclusiveSchedulerPair();
            var readerAB1 = new ActionBlock<int>(i =>
            {
                Console.WriteLine("ReaderAB1 begin handling." + " Execute Time:" + DateTime.Now);
                Thread.Sleep(500);
            }, new ExecutionDataflowBlockOptions() { TaskScheduler = pair.ConcurrentScheduler });
            var readerAB2 = new ActionBlock<int>(i =>
            {
                Console.WriteLine("ReaderAB2 begin handling." + " Execute Time:" + DateTime.Now);
                Thread.Sleep(500);
            }, new ExecutionDataflowBlockOptions() { TaskScheduler = pair.ConcurrentScheduler });
            var readerAB3 = new ActionBlock<int>(i =>
            {
                Console.WriteLine("ReaderAB3 begin handling." + " Execute Time:" + DateTime.Now);
                Thread.Sleep(500);
            }, new ExecutionDataflowBlockOptions() { TaskScheduler = pair.ConcurrentScheduler });

            var writerAB1 = new ActionBlock<int>(i =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WriterAB1 begin handling." + " Execute Time:" + DateTime.Now);
                Console.ResetColor();
                Thread.Sleep(3000);
            }, new ExecutionDataflowBlockOptions() { TaskScheduler = pair.ConcurrentScheduler });

            var bb = new BroadcastBlock<int>(i => { return i; });
            bb.LinkTo(readerAB1);
            bb.LinkTo(readerAB2);
            bb.LinkTo(readerAB3);

            Task.Run(() =>
            {
                while (true)
                {
                    bb.Post(1);
                    Thread.Sleep(1000);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(6000);
                    writerAB1.Post(1);
                }
            });
        }

        private static void TestBlocks02()
        {
            var bb = new BufferBlock<int>();
            var ab1 = new ActionBlock<int>(i =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("ab1 handle data" + i + " Execute Time:" + DateTime.Now);
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 2});
            var ab2 = new ActionBlock<int>(i =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("ab2 handle data" + i + " Execute Time:" + DateTime.Now);
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 2 });
            var ab3 = new ActionBlock<int>(i =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("ab3 handle data" + i + " Execute Time:" + DateTime.Now);
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 2 });

            bb.LinkTo(ab1);
            bb.LinkTo(ab2);
            bb.LinkTo(ab3);

            for (int i = 0; i < 9; i++)
            {
                bb.Post(i);
            }
        }
    }
}
