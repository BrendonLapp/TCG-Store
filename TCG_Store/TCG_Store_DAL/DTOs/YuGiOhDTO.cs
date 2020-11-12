using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.DTOs
{
    public class YuGiOhDTO
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
