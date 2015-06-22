﻿using System.Linq;
using Microsoft.Practices.Unity;
using ScanWord.Core.Common;
using ScanWord.Core.Data;
using ScanWord.DependencyResolution;

namespace ScanWord.Console
{
    using System;

    /// <summary>
    /// Console for ScanWord program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Unity container.
        /// </summary>
        private static readonly IUnityContainer Container = UnityConfig.GetConfiguredContainer();

        /// <summary>
        /// Entry point of the console program.
        /// </summary>
        /// <param name="args">Console arguments.</param>
        public static void Main(string[] args)
        {
            var repository = Container.Resolve<IScanDataRepository>();
            var parser = Container.Resolve<IScanWordParser>();
            /*var dataHandler = Container.Resolve<IScanWordDataHandler>();*/

            var start = Environment.TickCount;
            var compositions = parser.ParseFile("C:/Interstellar.srt");
            var materialWords = compositions.GroupBy(w => w.Word.TheWord).Select(c => c.Key).AsQueryable();
            Console.WriteLine("Parse File time: {0} ms.", Environment.TickCount - start);

            start = Environment.TickCount;
            var existingWords = repository.GetWords(materialWords);
            Console.WriteLine("Get Words time: {0} ms.", Environment.TickCount - start);

            start = Environment.TickCount;
            compositions = parser.ParseFile("C:/Interstellar.srt", existingWords);
            Console.WriteLine("Parse File time: {0} ms.", Environment.TickCount - start);

            /*start = Environment.TickCount;
            dataHandler.MergeWithExisting(compositions);
            Console.WriteLine("Merge With Existing time: {0} ms.", Environment.TickCount - start);*/

            Console.WriteLine("All words: " + compositions.Count);
            var uniqueWords = compositions.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("Unique words: " + uniqueWords);

            start = Environment.TickCount;
            repository.AddCompositions(compositions);
            Console.WriteLine("Add Compositions time: {0} ms.", Environment.TickCount - start);

            Console.ReadLine();
        }
    }
}