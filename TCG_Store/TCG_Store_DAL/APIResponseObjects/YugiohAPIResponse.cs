using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.APIResponseObjects
{
    public class YugiohAPIResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public string race { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public string attribute { get; set; }
    }
}
