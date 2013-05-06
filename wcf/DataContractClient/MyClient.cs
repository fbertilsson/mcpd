using System;
using System.ServiceModel;
using System.Threading.Tasks;
using DataContractClient.ServRef;

namespace DataContractClient
{
    class MyClient : ClientBase<IMyService>, IMyService
    {
        public MyClient()
        {
        }

        public MyClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {            
        }

        public Contact[] GetCustomers()
        {
            return Channel.GetCustomers();
        }

        public IAsyncResult BeginGetCustomers(AsyncCallback callback, object asyncState)
        {
            return Channel.BeginGetCustomers(callback, asyncState);
        }

        public Contact[] EndGetCustomers(IAsyncResult result)
        {
            return Channel.EndGetCustomers(result);
        }

        public Contact[] GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetSuppliers(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public Contact[] EndGetSuppliers(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public CustomerList2 GetCustomers2()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetCustomers2(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public CustomerList2 EndGetCustomers2(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public SupplierList2 GetSuppliers2()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetSuppliers2(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public SupplierList2 EndGetSuppliers2(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public Contact[] GetCustomersWithGenericList()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetCustomersWithGenericList(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public Contact[] EndGetCustomersWithGenericList(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public SpezialeContact GetSpecialNameCustList()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetSpecialNameCustList(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public SpezialeContact EndGetSpecialNameCustList(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public Moods GetCurrentMoodTestingEnum()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetCurrentMoodTestingEnum(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public Moods EndGetCurrentMoodTestingEnum(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public SnakePitfall GetSnakePitfallTestingEnumPitfall()
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetSnakePitfallTestingEnumPitfall(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public SnakePitfall EndGetSnakePitfallTestingEnumPitfall(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}
