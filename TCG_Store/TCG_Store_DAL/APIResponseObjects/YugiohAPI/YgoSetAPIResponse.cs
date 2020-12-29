using System;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    public class YgoSetAPIResponse
    {
        public string set_name { get; set; }
        public string set_code { get; set; }
        public DateTime tcg_date { get; set; }
    }
}
