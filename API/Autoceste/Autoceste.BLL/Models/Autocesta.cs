using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoceste.BLL.Models
{
    public class Autocesta
    {
        public int Id { get; set; }
        public double Duljina { get; set; }
        public string Oznaka { get; set; }
        public string? NeformalniNaziv { get; set; }
        public string Dionica { get; set; }
        public List<NaplatnaPostaja> NaplatnePostaje { get; set; }
    }
}
