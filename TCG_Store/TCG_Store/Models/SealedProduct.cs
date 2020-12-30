namespace TCG_Store.Models
{
    public class SealedProduct
    {
        /// <summary>
        /// The ID of the sealed product
        /// </summary>
        public int SealedProductID { get; set; }
        /// <summary>
        /// The ID of the set
        /// </summary>
        public int SetID { get; set; }
        /// <summary>
        /// The name of the sealed product
        /// </summary>
        public string SealedProductName { get; set; }
        /// <summary>
        /// The price of the sealed product
        /// </summary>
        public decimal Price { get; set; }
    }
}
