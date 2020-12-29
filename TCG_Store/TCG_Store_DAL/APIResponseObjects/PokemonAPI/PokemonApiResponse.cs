namespace TCG_Store_DAL.APIResponseObjects.PokemonAPI
{
    public class PokemonApiResponse
    {
        /// <summary>
        /// Name of the pokemon card
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Link to the low res image of the card
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// Link to the hi-res image of the card
        /// </summary>
        public string imageUrlHiRes { get; set; }
        /// <summary>
        /// Array of the types of the card
        /// </summary>
        public string[] types { get; set; }
        /// <summary>
        /// The type of card it is: Pokemon, Trainer, Energy...
        /// </summary>
        public string supertype { get; set; }
        /// <summary>
        /// The type of card it is: Basic, Stage 2....
        /// </summary>
        public string subtype { get; set; }
        /// <summary>
        /// The rarity of the card in the set
        /// </summary>
        public string rarity { get; set; }
        /// <summary>
        /// The set ID of the card
        /// </summary>
        public string id { get; set; }
    }
}
