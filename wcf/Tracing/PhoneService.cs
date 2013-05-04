namespace Tracing
{
    class PhoneService : IPhone
    {
        public string Call(int number)
        {
            return number.ToString();
        }
    }
}
