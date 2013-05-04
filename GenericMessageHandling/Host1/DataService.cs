using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Host1
{
    
    class DataService : IDataService
    {
        const string ns = "http://f.se/";

        public Message GetData()
        {
            Message message = null;
            try
            {               
                var version = OperationContext.Current.IncomingMessageVersion;
                const string actionResponse = ns + "IDataService/GetDataResponse";

                object obj;

                string option = "object1"; // Change with your debugger
                switch (option)
                {
                    case "hi":
                        obj = "Hello there!";
                        break;
                    case "object1":
                        obj = new Comedian("David Batra", "ironic");
                        break;
                    case "object2":
                        obj = new Comedian("John Cleese", "hysteric");
                        break;
                    case "object3":
                        obj = new Comedian("Rowan Atkinson", "nerd");
                        break;
                    default:
                        obj = string.Empty;
                        break;
                }
                message = Message.CreateMessage(version, actionResponse, obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return message;
        }


        public Message GetFault()
        {
            var fc = new FaultCode("Receiver");
            var ver = OperationContext.Current.IncomingMessageVersion;
            return Message.CreateMessage(ver, fc, "My reason here", ns + "IDataService/GetFaultResponse");
        }

        public void SetData(Message m)
        {
            Console.WriteLine(m);
        }
    }
}
