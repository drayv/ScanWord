// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Maksym Shchyhol">
//   Copyright (c) Maksym Shchyhol. All Rights Reserved
// </copyright>
// <summary>
//   Console for ScanWord program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScanWord.Console
{
    using System;
    using System.Linq;
    using Microsoft.Practices.Unity;
    using ScanWord.Core.Common;
    using ScanWord.DependencyResolution;

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
        /// <param name="args">
        /// Console arguments.
        /// </param>
        public static void Main(string[] args)
        {
            ScanFile("E:/PROJECTS/TESTS/ScanWord/Docs/Test.txt");
            Console.ReadLine();
        }

        /// <summary>
        /// Scans a file and displays statistics on console.
        /// </summary>
        /// <param name="absolutePath">
        /// The absolute path.
        /// </param>
        private static void ScanFile(string absolutePath)
        {
            var start = Environment.TickCount;
            var parser = Container.Resolve<IScanWordParser>();
            var words = parser.ParseFile(absolutePath);
            Console.WriteLine("Time: {0} ms.", Environment.TickCount - start);

            Console.WriteLine("All words: " + words.Count);
            var uniqueWords = words.GroupBy(w => w.Word.TheWord).Count();
            Console.WriteLine("Unique words: " + uniqueWords);
        }
    }
}