namespace TCG_Store_DAL.APIResponseObjects.PokemonAPI
{
    /// <summary>
    /// Root API Reponse object from the Pokemon TCG API
    /// </summary>
    public class PokemonApiReponseRoot
    {
        /// <summary>
        /// Root array for the cards
        /// </summary>
        public PokemonApiResponse[] cards { get; set; }
    }
}
