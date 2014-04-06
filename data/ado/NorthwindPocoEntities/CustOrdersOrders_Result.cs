using System;

namespace NorthwindPocoEntities
{
    public class CustOrdersOrders_Result
    {
        public int OrderID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}
