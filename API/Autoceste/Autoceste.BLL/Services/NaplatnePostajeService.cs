using Autoceste.BLL.Models;
using Autoceste.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Autoceste.BLL.Services
{
    public class NaplatnePostajeService
    {
        private OrContext context;
        public NaplatnePostajeService(OrContext context)
        {
            this.context = context;
        }

        public NaplatnaPostaja GetNaplatnaPostajaById(int id)
        {
            var np = context.Naplatnepostajes.Where(x => x.Id == id).FirstOrDefault();

            return new NaplatnaPostaja
            {
                AutocestaId = np.Autocestaid,
                Id = np.Id,
                Naziv = np.Naziv,
                GeoDuzina = np.Geoduzina,
                GeoSirina = np.Geosirina,
                Kontakt = np.Kontakt,
                ImaEnc = np.Imaenc ?? false
            };
        }

        public List<NaplatnaPostaja> GetNaplatnePostaje()
        {
            var list = new List<NaplatnaPostaja>();
            foreach (var np in context.Naplatnepostajes)
            {
                list.Add(new NaplatnaPostaja
                {
                    AutocestaId = np.Autocestaid,
                    Id = np.Id,
                    Naziv = np.Naziv,
                    GeoDuzina = np.Geoduzina,
                    GeoSirina = np.Geosirina,
                    Kontakt = np.Kontakt,
                    ImaEnc = np.Imaenc ?? false
                });
            }
            return list;
        }

        public List<NaplatnaPostaja> GetNaplatnePostajeFiltered(Func<Naplatnepostaje,bool> filter)
        {
            var list = new List<NaplatnaPostaja>();
            foreach (var np in context.Naplatnepostajes.Include(np => np.Autocesta).Where(filter))
            {
                list.Add(new NaplatnaPostaja
                {
                    AutocestaId = np.Autocestaid,
                    Id = np.Id,
                    Naziv = np.Naziv,
                    GeoDuzina = np.Geoduzina,
                    GeoSirina = np.Geosirina,
                    Kontakt = np.Kontakt,
                    ImaEnc = np.Imaenc ?? false
                });
            }
            return list;
        }

        public List<NaplatnaPostaja> GetNaplatnePostajeByAutocestaId(int autocestaId)
        {
            var list = new List<NaplatnaPostaja>();
            foreach (var np in context.Naplatnepostajes.Where(np => np.Autocestaid == autocestaId))
            {
                list.Add(new NaplatnaPostaja
                {
                    AutocestaId = np.Autocestaid,
                    Id = np.Id,
                    Naziv = np.Naziv,
                    GeoDuzina = np.Geoduzina,
                    GeoSirina = np.Geosirina,
                    Kontakt = np.Kontakt,
                    ImaEnc = np.Imaenc ?? false
                });
            }
            return list;
        }

        public List<NaplatnaPostaja> GetNaplatnePostajeFiltered(string searchProperty, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return GetNaplatnePostaje();
            Func<Naplatnepostaje, bool> filter = GetFilter(searchProperty, searchTerm);
            return GetNaplatnePostajeFiltered(filter);
        }

        internal Func<Naplatnepostaje, bool> GetFilter(string searchProperty, string searchTerm)
        {
            Func<Naplatnepostaje, bool> filter = null;
            switch (searchProperty)
            {
                case "Naziv":
                    filter = np => np.Naziv.ToLower().Contains(searchTerm.ToLower());
                    break;
                case "Kontakt":
                    filter = (np =>
                    {
                        if (np.Kontakt == null) return false;
                        return np.Kontakt.ToLower().Contains(searchTerm.ToLower());
                    });
                    break;
                case "GeoDuzina":
                    filter = (np =>
                    {
                        if (np.Geoduzina == null) return false;
                        return np.Geoduzina.ToString().Contains(searchTerm);
                    });
                    break;
                case "GeoSirina":
                    filter = (np =>
                    {
                        if (np.Geosirina == null) return false;
                        return np.Geosirina.ToString().Contains(searchTerm);
                    });
                    break;
                case "ImaEnc":
                    filter = (np =>
                    {
                        if (np.Imaenc == null) return false;
                        return np.Imaenc == Convert.ToBoolean(searchTerm);
                    });
                    break;
                case "Any":
                    filter = (np => NaplatnaPostajaToString(np).ToLower().Contains(searchTerm.ToLower()));
                    break;
                default:
                    throw new Exception("searchProperty not applicable");
            }

            return filter;
        }

        private string NaplatnaPostajaToString(Naplatnepostaje naplatnepostaja)
        {
            StringBuilder sb = new StringBuilder($"{naplatnepostaja.Naziv} {naplatnepostaja.Autocesta?.Oznaka} {naplatnepostaja.Autocesta?.Dionica} {naplatnepostaja.Autocesta?.Dionica}");
            sb.Append(naplatnepostaja.Geosirina);
            sb.Append(naplatnepostaja.Geoduzina);
            sb.Append(naplatnepostaja.Kontakt);
            sb.Append(naplatnepostaja.Imaenc);
            sb.Append(naplatnepostaja.Autocesta.Neformalninaziv);

            return sb.ToString();
        }
    }
}
