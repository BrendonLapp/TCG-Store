namespace TCG_Store.Models
{
    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int InventoryID { get; set; }
        public int OrderQuantity { get; set; }
        public decimal OrderItemPrice { get; set; }
    }
}
