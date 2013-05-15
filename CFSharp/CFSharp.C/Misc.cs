using System;
using NUnit.Framework;
using Shouldly;

namespace CFSharp.C
{
    public class Misc
    {
        static int counter = 0;

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
    }
}