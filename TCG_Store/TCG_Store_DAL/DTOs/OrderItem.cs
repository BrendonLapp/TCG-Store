using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.DTOs
{
    public class OrderItemDTO
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int InventoryID { get; set; }
        public int OrderQuantity { get; set; }
        public decimal OrderItemPrice { get; set; }
    }
}
