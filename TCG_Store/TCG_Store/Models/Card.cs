namespace TCG_Store.Models
{
    public class Card
    {
        /// <summary>
        /// The auto generated ID of the card in the database
        /// </summary>
        public int CardID { get; set; }
        /// <summary>
        /// The code for the card in the set
        /// </summary>
        public string CardCodeInSet { get; set; }
        /// <summary>
        /// The ID of the Set that the card belongs to in the database
        /// </summary>
        public int SetID { get; set; }
        /// <summary>
        /// The Name of the card
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// The rarity of the card
        /// </summary>
        public string Rarity { get; set; }
        /// <summary>
        /// The price of the card
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The attribute of a Yugioh card or the elemental type of a pokemon card
        /// </summary>
        public string ElementalType { get; set; }
        /// <summary>
        /// The type of card in the each game. YGO: Dragon, Continues, Counter ETC.... Pokemon: Basic ETC...
        /// </summary>
        public string SubType { get; set; }
        /// <summary>
        /// The type of card in each game. YGO: Trap, Spell, Monster ETC... Pokemon: Pokemon, Energy, Trainer ETC...
        /// </summary>
        public string SuperType { get; set; }
        /// <summary>
        /// Link to the normal sized picture of the card
        /// </summary>
        public string PictureLink { get; set; }
        /// <summary>
        /// Link to the small picture of the card
        /// </summary>
        public string PictureSmallLink { get; set; }
        /// <summary>
        /// The API ID that is used by the API to identify each card
        /// </summary>
        public string APIImageID { get; set; }
    }
}
