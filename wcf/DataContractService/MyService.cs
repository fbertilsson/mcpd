using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContractService
{
    
    class MyService : IMyService
    {
        public CustomerList GetCustomers()
        {
            throw new NotImplementedException();
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
