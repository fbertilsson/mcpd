using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceModelEx;

namespace GenericMessageHandling
{
    [ThreadPoolBehavior(5, typeof(DataService))]
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

                string option = "object1";
                switch (option)
                {
                    case "Hi!":
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

        public void SetData(Message m)
        {
            throw new NotImplementedException();
        }
    }
}
