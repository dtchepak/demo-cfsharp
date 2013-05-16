using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using System.IO;

namespace CFSharp.C
{
    public class Misc
    {
        private static int counter = 0;

        private static void IncCounter()
        {
            counter = counter + 1;
            Console.WriteLine("{0}", counter);
        }

        [Test]
        public void LookMaSideEffects()
        {
            IncCounter();
            IncCounter();
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }

        [Test]
        public void TestAdd()
        {
            Add(1, 2).ShouldBe(3);
        }

        public static Func<int, int> CAdd(int a)
        {
            return b => a + b;
        }

        [Test]
        public void TestCurriedAdd()
        {
            CAdd(1)(2).ShouldBe(3);
        }

        private static int CAddOne(int b)
        {
            return CAdd(1)(b);
        }

        [Test]
        public void TestCurriedAddOne()
        {
            CAddOne(2).ShouldBe(3);
        }

        private static Func<int, Func<int, Func<int, int>>> Sum(int a)
        {
            return b => c => d => a + b + c + d;
        }
        
        [Test]
        public void TestSum()
        {
            Sum(1)(2)(3)(4).ShouldBe(10);
        }

        [Test]
        public void UseLambda()
        {
            var list = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var result = list.Where(x => x%2 == 0);

            Console.WriteLine(string.Join(", ", result));
        }

        private static string Describe(string language)
        {
            string description;
            if (language == "C#") {
                description = "nice";
            } else if (language == "F#") {
                description = "awesome";
            } else if (language == "Haskell") {
                description = "begone, zealot!";
            } else description = "I don't know that language"; 
            return language + ": " + description;
        }

        [Test]
        public void TestDescribe()
        {
            Describe("Haskell").ShouldBe("Haskell: begone, zealot!");
        }

        [Test]
        public void Loops()
        {
            var counter = 0;
            var array = new[] {1, 2, 3};
            foreach (var x in array) {
                // ...
                Console.WriteLine(x);
            }
            for (int i = 0; i < array.Length; i++) {
                // ...
                Console.WriteLine(array[i]);
            }
            while (counter < 3) {
                // ...
                Console.WriteLine("stuff");
                counter++;
            }
        }

        private static string TopFsharpComments(IEnumerable<string> comments)
        {
            var top = comments
                .Where(x => x.Contains("F#"))
                .Take(5);
            return string.Join("\n", top);
        }

        private static string TopFsharpComments2(IEnumerable<string> comments)
        {
            var filtered = comments .Where(x => x.Contains("F#"));
            var top = filtered.Take(5);
            return string.Join("\n", top);
        }

        private static IEnumerable<string> CommentsForLanguages(IEnumerable<string> languages)
        {
            return
                from lang in languages
                from comments in CommentsFor(lang)
                where comments.Contains("blah")
                select lang + ": " + comments;

        }

        private static IEnumerable<string> CommentsFor(string lang)
        {
            return new[] {
                "blah blah " + lang + " blah", 
                "something " + lang,
                "foo blah " + lang
            };
        }

        [Test]
        public void TestCommentsForLanguages()
        {
            Console.WriteLine(string.Join("\n", CommentsForLanguages(new[] {"C#", "F#"})));
        }

        private static void Foo(string fileName) {
            try {
                using (var f = File.CreateText(fileName)) {
                    f.WriteLine("hi!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("something went wrong: {0}", ex);
            }
        }

        [Test]
        public void ParseExample()
        {
            string s = "bob";
            int result;
            if (Int32.TryParse(s, out result))
            {
                //use result
            }
        }
    }
}