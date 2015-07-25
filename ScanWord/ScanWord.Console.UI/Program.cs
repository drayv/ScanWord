using System.Linq;
using System.Threading;
using Microsoft.Practices.Unity;
using ScanWord.Core.Abstract;
using ScanWord.Core.Data;
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

            t1.Start();
            t2.Start();

            Console.ReadLine();
        }

        private static void TestThread1()
        {
            var repositories = Container.Resolve<IScanDataUnitOfWork>();
            var parser = Container.Resolve<IScanWordParser>();

            var start = Environment.TickCount;
            var compositions = parser.ParseAllWordsInFile("E:/true_detective.txt");
            Console.WriteLine("(T1) Parse File time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("(T1) All words: " + compositions.Count);
            var uniqueWords = compositions.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("(T1) Unique words: " + uniqueWords);

            start = Environment.TickCount;
            repositories.CompositionsRepository().Insert(compositions);
            repositories.Commit();

            Console.WriteLine("(T1) Add Compositions time: {0} ms.", Environment.TickCount - start);
        }

        private static void TestThread2()
        {
            var repositories = Container.Resolve<IScanDataUnitOfWork>();
            var parser = Container.Resolve<IScanWordParser>();

            var start = Environment.TickCount;
            var words = parser.ParseUnigueWordsInFile("E:/true_detective.txt");
            Console.WriteLine("(T2) Parse File time: {0} ms.", Environment.TickCount - start);
            Console.WriteLine("(T2) Unique words: " + words.Count);

            var allWordsCount = words.Aggregate(0, (current, word) => current + word.Count);
            Console.WriteLine("(T2) But all words: " + allWordsCount);

            start = Environment.TickCount;
            repositories.WordsRepository().Insert(words);
            repositories.Commit();

            Console.WriteLine("(T2) Add Compositions time: {0} ms.", Environment.TickCount - start);
        }
    }
}