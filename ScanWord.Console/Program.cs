using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using ScanWord.Core.Common;
using ScanWord.Core.Data;
using ScanWord.Core.Entity;
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
            var start = Environment.TickCount;
            var words = repository.GetWords();
            Console.WriteLine("Get Words time: {0} ms.", Environment.TickCount - start);

            var compositions = ScanFile("C:/Interstellar.srt", words);
            
            repository.AddCompositions(compositions);
            Console.WriteLine("Add Compositions time: {0} ms.", Environment.TickCount - start);

            Console.ReadLine();
        }

        /// <summary>
        /// Scans a file and displays statistics on console.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <param name="existingWords">Existing words to be used in the composition.</param>
        /// <returns>The composition collection.</returns>
        private static IEnumerable<Composition> ScanFile(string absolutePath, ConcurrentBag<Word> existingWords)
        {
            var start = Environment.TickCount;
            var parser = Container.Resolve<IScanWordParser>();
            var words = parser.ParseFile(absolutePath, existingWords);
            Console.WriteLine("Parse File time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("All words: " + words.Count);
            var uniqueWords = words.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("Unique words: " + uniqueWords);
            return words;
        }
    }
}