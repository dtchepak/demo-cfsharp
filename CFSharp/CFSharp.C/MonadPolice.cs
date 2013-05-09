using System.Collections.Generic;
using System.Linq;

namespace CFSharp.C
{
    public interface IEmailGateway
    {
        void Send(string to, string from, string subject, string message);
    }

    public interface ImDave
    {
        IEnumerable<string> RecentRamblings();
    }

    public class MonadPolice
    {
        private readonly ImDave _dave;
        private readonly IEmailGateway _email;

        public MonadPolice(ImDave dave, IEmailGateway email)
        {
            _dave = dave;
            _email = email;
        }

        public void Surveil()
        {
            var overheard = _dave.RecentRamblings();

            var zealotTalk = overheard.Where(x => x.Contains("monad"));

            foreach (var outburstOfZealotry in zealotTalk)
            {
                _email.Send("xerxesb", "the monad police", "Lack of pragmatism detected", outburstOfZealotry);
            }
        }
    }
}
