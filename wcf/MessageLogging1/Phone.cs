using System;

namespace MessageLogging1
{
    class Phone : IPhone
    {
        public bool Call(int number)
        {
            return number > 10;
        }
    }
}
