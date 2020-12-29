using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG_Store_DAL.DTOs
{
    public class SetDTO
    {
        public int SetID { get; set; }
        public int GameID { get; set; }
        public string SetCode { get; set; }
        public string SetName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
