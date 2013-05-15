using System;
using NUnit.Framework;

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
    }
}