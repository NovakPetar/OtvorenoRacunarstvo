using Autoceste.BLL.Models;
using Autoceste.BLL.Services;
using Autoceste.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoceste.BLL.Validators
{
    public class NaplatnaPostajaValidator
    {
        private OrContext context;
        public NaplatnaPostajaValidator(OrContext context)
        {
            this.context = context;
        }
        private const double najsjevernijaTockaHrv = 46.55;
        private const double najjuznijaTockaHrv = 42.383333;
        private const double najistocnijaTockaHrv = 13.5;
        private const double najzapadnijaTockaHrv = 19.45;
        public string Validate(NaplatnaPostaja naplatnaPostaja)
        {
            if (naplatnaPostaja == null)
            {
                return "Zahtjev nije valjan JSON.";
            }
            if (string.IsNullOrEmpty(naplatnaPostaja.Naziv))
            {
                return "Unesite naziv naplatne postaje.";
            }
            if (naplatnaPostaja.GeoDuzina < najistocnijaTockaHrv || naplatnaPostaja.GeoDuzina > najzapadnijaTockaHrv)
            {
                return $"Geografska dužina mora biti između najistočnije({najistocnijaTockaHrv}) i najzapadnije({najzapadnijaTockaHrv}) točke Republike Hrvatske";
            }
            if (naplatnaPostaja.GeoSirina < najjuznijaTockaHrv || naplatnaPostaja.GeoSirina > najsjevernijaTockaHrv)
            {
                return $"Geografska širina mora biti između najjužnije({najjuznijaTockaHrv}) i najsjevernije({najsjevernijaTockaHrv}) točke Republike Hrvatske";
            }

            if (naplatnaPostaja.AutocestaId == null)
            {
                return "Unesite idenifikator autoceste (AutocestaId).";
            }

            var autocesta = new AutocesteService(context).GetAutocestaById(naplatnaPostaja.AutocestaId ?? 0);
            if (autocesta == null)
            {
                return $"Ne postoji autocesta s identifikatorom {naplatnaPostaja.AutocestaId}.";
            }

            return null;
        }
    }
}
