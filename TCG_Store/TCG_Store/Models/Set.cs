using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace TCG_Store.Models
{
    public class Set
    {
        public int SetID { get; set; }
        public int GameID { get; set; }
        public string SetCode { get; set; }
        public string SetName { get; set; }
    }
}
