using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoceste.BLL.Models
{
    public class NaplatnaPostaja
    {
        public int Id { get; set; }
        public int? AutocestaId { get; set; }
        public string Naziv { get; set; }
        public double? GeoDuzina { get; set; }
        public double? GeoSirina { get; set; }
        public bool ImaEnc { get; set; }
        public string? Kontakt { get; set; }
    }
}
