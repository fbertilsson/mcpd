using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DataContractService
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        CustomerList GetCustomers();

        [OperationContract]
        SupplierList GetSuppliers();

        [OperationContract]
        CustomerList2 GetCustomers2();

        [OperationContract]
        SupplierList2 GetSuppliers2();

        [OperationContract]
        List<Contact> GetCustomersWithGenericList();

        [OperationContract]
        SpecialNameCustList<Contact> GetSpecialNameCustList();

        [OperationContract]
        Moods GetCurrentMoodTestingEnum();

        [OperationContract]
        SnakePitfall GetSnakePitfallTestingEnumPitfall();
    }


    /// <summary>
    /// Contact array will be used since there is no CollectionDataContract attribute.
    /// Note that the interface data type will be the same for these two, even though the class name differs.
    /// </summary>
    public class CustomerList : Collection<Contact> { }
    public class SupplierList : Collection<Contact> { }


    /// <summary>
    /// Decorating with CollectionDataContract makes the two data types have their own names in the client interface, if you generate a service ref.
    /// </summary>
    [CollectionDataContract]
    public class CustomerList2 : Collection<Contact> { }
    [CollectionDataContract]
    public class SupplierList2 : Collection<Contact> { }


    [CollectionDataContract(Name = "Speziale{0}")]           // Let's have myself a special name for this data type, e.g. SpezialeContact
    public class SpecialNameCustList<T> : Collection<T> { }


    [DataContract]
    public class Contact
    {
        public string Name { get; set; }
    }

    public enum Moods
    {
        Terrible,
        Bad,
        Good,
        JollyGood,
    }

    [DataContract]
    public enum SnakePitfall
    {
        [EnumMember]
        Python,
        // Ooops, forgot to specify EnumMember attribute for Boa => It will not be in the contract
        Boa,
    }
}

