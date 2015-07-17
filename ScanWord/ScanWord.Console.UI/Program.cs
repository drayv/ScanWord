using System.Linq;
using System.Threading;
using Microsoft.Practices.Unity;
using ScanWord.Domain.Common;
using ScanWord.Domain.Data;
using ScanWord.Infrastructure;

namespace ScanWord.Console.UI
{
    using System;

    /// <summary>Console for ScanWord program.</summary>
    public class Program
    {
        /// <summary>Unity container.</summary>
        private static readonly IUnityContainer Container = ScanUnityConfig.GetConfiguredContainer();

        /// <summary>Entry point of the test console program.</summary>
        /// <param name="args">Console arguments.</param>
        public static void Main(string[] args)
        {
            var t1 = new Thread(TestThread1);
            var t2 = new Thread(TestThread2);

            //TODO: add transaction's to avoid situation like this:
            t1.Start();
            //t2.Start(); <- try this error

            Console.ReadLine();
        }

        private static void TestThread1()
        {
            var repository = Container.Resolve<IScanDataRepository>();
            var parser = Container.Resolve<IScanWordParser>();

            var start = Environment.TickCount;
            var compositions = parser.ParseFile("E:/true_detective.txt");
            Console.WriteLine("(T1) Parse File time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("(T1) All words: " + compositions.Count);
            var uniqueWords = compositions.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("(T1) Unique words: " + uniqueWords);

            start = Environment.TickCount;
            repository.AddCompositionsAsync(compositions).Wait();
            Console.WriteLine("(T1) Add Compositions time: {0} ms.", Environment.TickCount - start);
        }

        private static void TestThread2()
        {
            var repository = Container.Resolve<IScanDataRepository>();
            var parser = Container.Resolve<IScanWordParser>();

            var start = Environment.TickCount;
            var compositions = parser.ParseFile("E:/true_detective.txt");
            Console.WriteLine("(T2) Parse File time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("(T2) All words: " + compositions.Count);
            var uniqueWords2 = compositions.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("(T2) Unique words: " + uniqueWords2);

            start = Environment.TickCount;
            repository.AddCompositionsAsync(compositions).Wait();
            Console.WriteLine("(T2) Add Compositions time: {0} ms.", Environment.TickCount - start);
        }
    }
}