namespace TCG_Store_DAL.APIResponseObjects
{
    /// <summary>
    /// Set API reponse root object from the Pokemon TCG API
    /// </summary>
    public class PokemonSetAPIResponseRoot
    {
        /// <summary>
        /// Array of the sets in the API response
        /// </summary>
        public PokemonSetAPIResponse[] sets { get; set; }
    }
}
