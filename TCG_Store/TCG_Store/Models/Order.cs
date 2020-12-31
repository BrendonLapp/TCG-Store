using System;
using System.Collections.Generic;

namespace TCG_Store.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int AdminID { get; set; }
        public int ShippingID { get; set; }
        public int StatusID { get; set; }
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public decimal ShippingPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public string UpdatedBy { get; set; }
        public decimal SubTotal { get; set; }
        public decimal PST { get; set; }
        public decimal HST { get; set; }
        public decimal GST { get; set; }
        public decimal Total { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
