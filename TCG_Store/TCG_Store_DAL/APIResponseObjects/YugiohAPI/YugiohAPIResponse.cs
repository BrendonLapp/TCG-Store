using System.Collections.Generic;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    /// <summary>
    /// Card API response object
    /// </summary>
    public class YugiohAPIResponse
    {
        /// <summary>
        /// ID of the card in the YGOProdeck API
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Name of the card
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The type of card it is. EG: Monster, Spell, Trap
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The type of card that it is. Maps to the type of card in the database. EG: Dragon, Quick Spell, Counter...
        /// </summary>
        public string race { get; set; }
        /// <summary>
        /// The cards attribute
        /// </summary>
        public string attribute { get; set; }
        /// <summary>
        /// List of the Card Set details. Cards belong to many different sets.
        /// </summary>
        public List<YugiohCardDetails> card_sets { get; set; }
    }
}
