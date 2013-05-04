using System;
using System.Runtime.Serialization;

namespace Faults1
{
    /// <summary>
    /// If I let this exception derive from Exception the service terminates the connection when throwing. Why? See Löwy p 262.
    /// </summary>
    [DataContract]
    public class FoulLanguageException2 
    {
        public FoulLanguageException2() {}

        public FoulLanguageException2(string s)
        {
            Message = s;
        }

        [DataMember]
        new string Message { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} '{1}']", typeof (FoulLanguageException2), Message);
        }
    }


    //Got a message: ... cannot be ISerializable and have the DataContract attribute. 
    //    [DataContract]
    public class FoulLanguageException : ApplicationException
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
