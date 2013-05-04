using System.Linq;

namespace CustomBinding
{
    class EchoService : IEchoService
    {
        public string Echo(string text)
        {
            return "Echo: " + text;
        }
    }
}
