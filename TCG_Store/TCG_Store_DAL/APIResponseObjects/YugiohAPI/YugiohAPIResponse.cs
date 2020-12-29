using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.APIResponseObjects.YugiohAPI
{
    public class YugiohAPIResponse
    {
        public string name { get; set; }
        public string type { get; set; }
        public string race { get; set; }
        public string attribute { get; set; }
        public List<YugiohCardSet> card_sets { get; set; }
    }
}
