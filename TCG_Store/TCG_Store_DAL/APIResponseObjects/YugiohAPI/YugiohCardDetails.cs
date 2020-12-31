namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    /// <summary>
    /// Details to each card in each set it belongs to
    /// </summary>
    public class YugiohCardDetails
    {
        /// <summary>
        /// The id of the card in the YGOPro API
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// The name of the card
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The set code for the card
        /// </summary>
        public string set_code { get; set; }
        /// <summary>
        /// The rarity of the card
        /// </summary>
        public string set_rarity { get; set; }
        /// <summary>
        /// The average price for the card at the time of the cards information being pulled
        /// </summary>
        public decimal set_price { get; set; }
    }
}
