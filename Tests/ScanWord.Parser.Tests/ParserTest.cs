// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserTest.cs" company="Maksym Shchyhol">
//   Copyright (c) Maksym Shchyhol. All Rights Reserved
// </copyright>
// <summary>
//   The parser test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScanWord.Parser.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ScanWord.Core.Common;
    using ScanWord.Core.Entity;
    using ScanWord.DependencyResolution;

    /// <summary>
    /// The parser test.
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        /// <summary>
        /// Unity container.
        /// </summary>
        private static readonly IUnityContainer Container = UnityConfig.GetConfiguredContainer();

        /// <summary>
        /// The parser.
        /// </summary>
        private static readonly IScanWordParser Parser = Container.Resolve<IScanWordParser>();

        /// <summary>
        /// Parse russian text test
        /// </summary>
        [TestMethod]
        public void ParseRussianTextTest()
        {
            var myTempFile = Path.Combine(Path.GetTempPath(), "ParseRussianTextTest.txt");
            using (var sw = new StreamWriter(myTempFile, false, Encoding.Default))
            {
                // All words: 22, doubles: "Это", "Вы". Unique words: 20.
                sw.Write(@"Исаак: Это лишь риторика — и вы действительно верите в это! Вы не знаете откуда пришли эти лозунги?
Джей Си Дентон: Я сдаюсь.");              
            }

            var compositions = Parser.ParseFile(myTempFile);
            Assert.AreEqual(22, compositions.Count, "Wrong result of count words!");

            var uniqueWords = CalcUniqueWords(compositions);
            Assert.AreEqual(20, uniqueWords, "Wrong result of count unique words!");

            var line = ReturnLineOfWord(compositions, "действительно");
            Assert.AreEqual(1, line, "Wrong line found!");

            line = ReturnLineOfWord(compositions, "сдаюсь");
            Assert.AreEqual(2, line, "Wrong line found!");

            var column = ReturnСolumnOfWord(compositions, "лишь");
            Assert.AreEqual(12, column, "Wrong column found!");

            column = ReturnСolumnOfWord(compositions, "Си");
            Assert.AreEqual(6, column, "Wrong column found!");

            var count = ReturnCountOfWord(compositions, "Это");
            Assert.AreEqual(2, count, "Wrong count of specific word!");

            count = ReturnCountOfWord(compositions, "риторика");
            Assert.AreEqual(1, count, "Wrong count of specific word!");
        }

        /// <summary>
        /// Parse english text test
        /// </summary>
        [TestMethod]
        public void ParseEnglishTextTest()
        {
            var myTempFile = Path.Combine(Path.GetTempPath(), "ParseEnglishTextTest.txt");
            using (var sw = new StreamWriter(myTempFile, false, Encoding.Default))
            {
                // All words: 11, doubles: "The". Unique words: 10.
                sw.Write(@"The body may heal,
but the mind isn't always so resilient");
            }

            var compositions = Parser.ParseFile(myTempFile);
            Assert.AreEqual(11, compositions.Count, "Wrong result of count words!");

            var uniqueWords = CalcUniqueWords(compositions);
            Assert.AreEqual(10, uniqueWords, "Wrong result of count unique words!");

            var line = ReturnLineOfWord(compositions, "body");
            Assert.AreEqual(1, line, "Wrong line found!");

            line = ReturnLineOfWord(compositions, "resilient");
            Assert.AreEqual(2, line, "Wrong line found!");

            var column = ReturnСolumnOfWord(compositions, "may");
            Assert.AreEqual(10, column, "Wrong column found!");

            column = ReturnСolumnOfWord(compositions, "but");
            Assert.AreEqual(1, column, "Wrong column found!");

            var count = ReturnCountOfWord(compositions, "The");
            Assert.AreEqual(2, count, "Wrong count of specific word!");

            count = ReturnCountOfWord(compositions, "mind");
            Assert.AreEqual(1, count, "Wrong count of specific word!");
        }

        /// <summary>
        /// Parse multilanguage text test
        /// </summary>
        [TestMethod]
        public void ParseMultilanguageTextTest()
        {
            var myTempFile = Path.Combine(Path.GetTempPath(), "ParseMultilanguageTextTest.txt");
            using (var sw = new StreamWriter(myTempFile, false, Encoding.Default))
            {
                // All words: 23, doubles: "Модель", "событий", "CLR". Unique words: 20.
                sw.Write(@"Модель событий CLR.
Модель событий CLR основана на делегатах (delegate). Делегаты позволяют
обращаться к методам обратного вызова (callback method), не нарушая 
безопасности типов.");
            }

            var compositions = Parser.ParseFile(myTempFile);
            Assert.AreEqual(23, compositions.Count, "Wrong result of count words!");

            var uniqueWords = CalcUniqueWords(compositions);
            Assert.AreEqual(20, uniqueWords, "Wrong result of count unique words!");

            var line = ReturnLineOfWord(compositions, "callback");
            Assert.AreEqual(3, line, "Wrong line found!");

            line = ReturnLineOfWord(compositions, "типов");
            Assert.AreEqual(4, line, "Wrong line found!");

            var column = ReturnСolumnOfWord(compositions, "delegate");
            Assert.AreEqual(43, column, "Wrong column found!");

            column = ReturnСolumnOfWord(compositions, "безопасности");
            Assert.AreEqual(1, column, "Wrong column found!");

            var count = ReturnCountOfWord(compositions, "CLR");
            Assert.AreEqual(2, count, "Wrong count of specific word!");

            count = ReturnCountOfWord(compositions, "method");
            Assert.AreEqual(1, count, "Wrong count of specific word!");
        }

        /// <summary>
        /// Calculate unique words.
        /// </summary>
        /// <param name="words">
        /// Composition entity.
        /// </param>
        /// <returns>
        /// Count of the unique words. <see cref="int"/>.
        /// </returns>
        private static int CalcUniqueWords(IEnumerable<Composition> words)
        {
            return words.GroupBy(w => w.Word.TheWord).Count();
        }

        /// <summary>
        /// Return line of word in scan words result.
        /// </summary>
        /// <param name="compositions">
        /// The words compositions.
        /// </param>
        /// <param name="word">Search word.</param>
        /// <returns>
        /// The line.<see cref="int"/>.
        /// </returns>
        private static int ReturnLineOfWord(IEnumerable<Composition> compositions, string word)
        {
            var composition = compositions.FirstOrDefault(w => w.Word.TheWord == word.ToLower());
            return composition != null ? composition.Line : 0;
        }

        /// <summary>
        /// Return column of word in scan words result.
        /// </summary>
        /// <param name="compositions">
        /// The words compositions.
        /// </param>
        /// <param name="word">Search word.</param>
        /// <returns>
        /// The column.<see cref="int"/>.
        /// </returns>
        private static int ReturnСolumnOfWord(IEnumerable<Composition> compositions, string word)
        {
            var composition = compositions.FirstOrDefault(w => w.Word.TheWord == word.ToLower());
            return composition != null ? composition.Сolumn : 0;   
        }

        /// <summary>
        /// Return count of word in scan words result.
        /// </summary>
        /// <param name="compositions">
        /// The words compositions.
        /// </param>
        /// <param name="word">Search word.</param>
        /// <returns>
        /// Count of word.<see cref="int"/>.
        /// </returns>
        private static int ReturnCountOfWord(IEnumerable<Composition> compositions, string word)
        {
            var words = compositions.Where(w => w.Word.TheWord == word.ToLower());
            return words.Count();
        }
    }
}