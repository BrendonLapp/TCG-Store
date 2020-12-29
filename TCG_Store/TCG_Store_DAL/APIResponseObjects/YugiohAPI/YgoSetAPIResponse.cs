using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    public class YgoSetAPIResponse
    {
        public string set_name { get; set; }
        public string set_code { get; set; }
        public DateTime tcg_date { get; set; }
    }
}
