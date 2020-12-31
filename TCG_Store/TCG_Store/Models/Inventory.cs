namespace TCG_Store.Models
{
    public class Inventory
    {
        /// <summary>
        /// The inventory ID
        /// </summary>
        public int InventoryID { get; set; }
        /// <summary>
        /// The ID of the card in the CardsInSet table. Can be null
        /// </summary>
        public int CardID { get; set; }
        /// <summary>
        /// The ID of the sealed product in the sealed product table. Can be null
        /// </summary>
        public int SealedProductID { get; set; }
        /// <summary>
        /// The quantity in stock in the inventory
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The quality ID in the quality table
        /// </summary>
        public int QualityID { get; set; }
        /// <summary>
        /// Whether the inventory item is a first edition or not. Can be null
        /// </summary>
        public bool FirstEdition { get; set; }
    }
}
