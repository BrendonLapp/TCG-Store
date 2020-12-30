namespace TCG_Store.Models
{
    public class Set
    {
        /// <summary>
        /// ID of the set
        /// </summary>
        public int SetID { get; set; }
        /// <summary>
        /// ID of the game the set belongs to
        /// </summary>
        public int GameID { get; set; }
        /// <summary>
        /// The set code for the set
        /// </summary>
        public string SetCode { get; set; }
        /// <summary>
        /// The name of the set
        /// </summary>
        public string SetName { get; set; }
    }
}
