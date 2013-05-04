using System;
using System.Runtime.Serialization;

namespace wshttp1
{
    [DataContract]
    public class FoulLanguageException : Exception
    {
        public FoulLanguageException()
        {
        }

        public FoulLanguageException(string message)
            : base(message)
        {
        }
    }
}
