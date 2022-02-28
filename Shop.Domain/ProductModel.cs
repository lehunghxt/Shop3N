using System;

namespace Shop.Domain
{
    public class ProductModel
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public string ProName { get; set; }
        public string ProCode { get; set; }
        public string ProSlug { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> CreateBy { get; set; }
        public virtual UserModel tblUser { get; set; }
    }
}
