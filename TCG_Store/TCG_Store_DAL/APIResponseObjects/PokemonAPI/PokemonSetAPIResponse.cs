using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.APIResponseObjects
{
    public class PokemonSetAPIResponse
    {
        public string name { get; set; }
        public string code { get; set; }
        public DateTime releaseDate { get; set; }
    }
}
