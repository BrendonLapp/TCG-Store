using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCG_Store.Models
{
    public class YuGiOh
    {
        public int YugiohID { get; set; }
        public string CardName { get; set; }
        public string Description { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public string CardType { get; set; }
    }
}
