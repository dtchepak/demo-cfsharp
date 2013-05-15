using System;
using NUnit.Framework;
using Shouldly;

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
    }
}