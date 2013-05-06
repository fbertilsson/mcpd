using System;
using System.Collections.Generic;

namespace DataContractService
{
    
    class MyService : IMyService
    {
        public CustomerList GetCustomers()
        {
            return new CustomerList
                {
                    new Contact  {Name = "Ola Norman" }, 
                    new Contact { Name = "Anders Svensson"}
                };
        }

        public SupplierList GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public CustomerList2 GetCustomers2()
        {
            throw new NotImplementedException();
        }

        public SupplierList2 GetSuppliers2()
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetCustomersWithGenericList()
        {
            throw new NotImplementedException();
        }

        public SpecialNameCustList<Contact> GetSpecialNameCustList()
        {
            throw new NotImplementedException();
        }

        public Moods GetCurrentMoodTestingEnum()
        {
            throw new NotImplementedException();
        }

        public SnakePitfall GetSnakePitfallTestingEnumPitfall()
        {
            throw new NotImplementedException();
        }
    }
}
