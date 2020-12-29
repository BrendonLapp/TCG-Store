using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    public class YugiohCardDetails
    {
        public string id { get; set; }
        public string name { get; set; }
        public string set_code { get; set; }
        public string set_rarity { get; set; }
        public decimal set_price { get; set; }
    }
}
