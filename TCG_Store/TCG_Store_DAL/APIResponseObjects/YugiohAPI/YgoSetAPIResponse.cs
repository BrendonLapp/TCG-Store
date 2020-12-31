using System;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    /// <summary>
    /// API response object for Yugioh sets
    /// </summary>
    public class YgoSetAPIResponse
    {
        /// <summary>
        /// Name of the set
        /// </summary>
        public string set_name { get; set; }
        /// <summary>
        /// Code for the individual set
        /// </summary>
        public string set_code { get; set; }
        /// <summary>
        /// Release date of the set
        /// </summary>
        public DateTime tcg_date { get; set; }
    }
}
