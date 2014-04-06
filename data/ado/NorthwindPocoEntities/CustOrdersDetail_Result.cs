using System;

namespace NorthwindPocoEntities
{
    public class CustOrdersDetail_Result
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<decimal> ExtendedPrice { get; set; }
    }
}
