﻿using Autoceste.BLL.Models;
using Autoceste.DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Autocesta> GetAutoceste(bool includeNaplatnePostaje = false)
        {
            var list = new List<Autocesta>();
            foreach (var a in context.Autocestes)
            {
                var autocesta = new Autocesta
                {
                    Id = a.Id,
                    NeformalniNaziv = a.Neformalninaziv,
                    Oznaka = a.Oznaka,
                    Duljina = a.Duljina,
                    Dionica = a.Dionica
                };

                list.Add(autocesta);
            }

            if (includeNaplatnePostaje)
            {
                foreach (Autocesta autocesta in list)
                {
                    var npService = new NaplatnePostajeService(this.context);
                    autocesta.NaplatnePostaje = npService.GetNaplatnePostajeByAutocestaId(autocesta.Id);
                }
            }
            

            return list;
        }

        public List<Autocesta> GetAutocesteFiltered(string searchProperty, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return GetAutoceste();
            Func<DAL.Models.Autoceste, bool> filter = GetFilter(searchProperty, searchTerm);

            return GetAutocesteFiltered(filter);
        }

        internal Func<DAL.Models.Autoceste, bool> GetFilter(string searchProperty, string searchTerm)
        {
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
                    throw new ArgumentException("searchProperty not applicable");
            }

            return filter;
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
