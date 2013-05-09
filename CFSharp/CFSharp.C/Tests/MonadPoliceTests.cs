using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using NSubstitute.Experimental;
using NUnit.Framework;

namespace CFSharp.C.Tests
{
    public class MonadPoliceTests
    {
        private IEmailGateway _emailer;
        private ImDave _dave;
        private MonadPolice _subject;

        [SetUp]
        public void SetUp()
        {
            _emailer = Substitute.For<IEmailGateway>();
            _dave = Substitute.For<ImDave>();
            _subject = new MonadPolice(_dave, _emailer);
        }

        [Test]
        public void ShouldNotifyXerxWithEveryMBomb()
        {
            var ramblings = new [] {
                "tea?",
                "couldn't we use a monad for that?",
                "anyone for tea?",
                "blah blah blah haskell blah blah monad blah blah",
                "a cup of tea is a lot like the Maybe monad..."
            };
            _dave.RecentRamblings().Returns(ramblings);

            _subject.Surveil();

            _emailer.Received().Send("xerxesb", Arg.Any<string>(), Arg.Any<string>(), ramblings[1]);
            _emailer.Received().Send("xerxesb", Arg.Any<string>(), Arg.Any<string>(), ramblings[3]);
            _emailer.Received().Send("xerxesb", Arg.Any<string>(), Arg.Any<string>(), ramblings[4]);
            _emailer.ReceivedWithAnyArgs(3).Send(null, null, null, null);
        }

        [Test]
        public void Sample()
        {
            var ramblings = new [] {
                "tea?",
                "couldn't we use a monad for that?",
                "anyone for tea?",
                "blah blah blah haskell blah blah monad blah blah",
                "a cup of tea is a lot like the Maybe monad..."
            };
            _dave.RecentRamblings().Returns(ramblings);
            var emailer = new ConsoleEmailGateway();

            new MonadPolice(_dave, emailer).Surveil();
        }

        private class ConsoleEmailGateway : IEmailGateway
        {
            public void Send(string to, string @from, string subject, string message)
            {
                Console.WriteLine(subject + ": " + message);
            }
        }
    }
}
