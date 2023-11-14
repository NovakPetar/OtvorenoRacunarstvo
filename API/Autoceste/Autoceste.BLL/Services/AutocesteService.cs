using Autoceste.BLL.Models;
using Autoceste.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoceste.BLL.Services
{
    public class AutocesteService
    {
        private OrContext context;
        public AutocesteService(OrContext context)
        {
            this.context = context;
        }
        public Autocesta GetAutocestaById(int id)
        {
            var a = context.Autocestes.Where(a => a.Id == id).FirstOrDefault();

            if (a == null) return null;

            return new Autocesta
            {
                Id = a.Id,
                NeformalniNaziv = a.Neformalninaziv,
                Oznaka = a.Oznaka,
                Duljina = a.Duljina,
                Dionica = a.Dionica
            };
        }

        public List<Autocesta> GetAutoceste()
        {
            var list = new List<Autocesta>();
            foreach (var a in context.Autocestes)
            {
                list.Add(new Autocesta
                {
                    Id = a.Id,
                    NeformalniNaziv = a.Neformalninaziv,
                    Oznaka = a.Oznaka,
                    Duljina = a.Duljina,
                    Dionica = a.Dionica
                });
            }

            return list;
        }

        public List<Autocesta> GetAutocesteFiltered(string searchProperty, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return GetAutoceste();
            Func<Autoceste.DAL.Models.Autoceste, bool> filter = null;
            switch (searchProperty)
            {
                case "Duljina":
                    filter = a => a.Duljina.ToString().Contains(searchTerm);
                    break;
                case "Oznaka":
                    filter = a => a.Oznaka.ToLower().Contains(searchTerm.ToLower());
                    break;
                case "Dionica":
                    filter = a => a.Dionica.ToLower().Contains(searchTerm.ToLower());
                    break;
                case "NeformalniNaziv":
                    filter = (a =>
                    {
                        if (a.Neformalninaziv == null) return false;
                        return a.Neformalninaziv.ToLower().Contains(searchTerm.ToLower());
                    });
                    break;
                default:
                    throw new Exception("searchProperty not applicable");
            }
            return GetAutocesteFiltered(filter);
        }

        public List<Autocesta> GetAutocesteFiltered(Func<Autoceste.DAL.Models.Autoceste, bool> filter)
        {
            var list = new List<Autocesta>();
            foreach (var a in context.Autocestes.Where(filter))
            {
                list.Add(new Autocesta
                {
                    Id = a.Id,
                    NeformalniNaziv = a.Neformalninaziv,
                    Oznaka = a.Oznaka,
                    Duljina = a.Duljina,
                    Dionica = a.Dionica
                });
            }
            return list;
        }

    }
}
