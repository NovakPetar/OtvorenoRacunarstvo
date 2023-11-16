using Autoceste.BLL.Models;
using Autoceste.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Autoceste.BLL.Services
{
    public class OpenDataService
    {
        private OrContext context;
        public OpenDataService(OrContext context)
        {
            this.context = context;
        }
        public string GetJson(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT get_my_json()", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string output = (string)reader["get_my_json"];
                            return output;
                        }
                    }
                }
            }
            return null;
        }

        public string GetCsv(string connectionString)
        {
            string fullPath = "C:\\Temp\\autoceste.csv";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand($"SELECT get_my_csv(\'{fullPath}\')", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.ExecuteReader();
                }
            }
            string csv = File.ReadAllText(fullPath);
            return csv;
        }

        public string GetJsonFiltered(string searchProperty, string searchTerm)
        {
            var npFilterBy = new string[] { "Naziv", "GeoSirina", "GeoDuzina", "ImaEnc", "Any" };
            var autocesteFilterBy = new string[] { "Oznaka", "Duljina", "Dionica", "NeformalniNaziv" };

            var autoceste = new List<Autocesta>();
            string json = "";

            if (npFilterBy.Contains(searchProperty)) 
            {
                var naplatneService = new NaplatnePostajeService(this.context);
                var autocesteService = new AutocesteService(this.context);

                var naplatne = naplatneService.GetNaplatnePostajeFiltered(searchProperty, searchTerm);

                foreach(NaplatnaPostaja naplatnaPostaja in naplatne)
                {
                    if(!autoceste.Any(a => a.Id == naplatnaPostaja.AutocestaId))
                    {
                        // ta autocesta jos ne postoji u listi

                        var autocesta = autocesteService.GetAutocestaById(naplatnaPostaja.AutocestaId ?? 0);
                        autocesta.NaplatnePostaje = new List<NaplatnaPostaja> { naplatnaPostaja };
                        autoceste.Add(autocesta);
                    } else
                    {
                        autoceste.Where(a => a.Id == naplatnaPostaja.AutocestaId).First().NaplatnePostaje.Add(naplatnaPostaja);
                    }
                }

            } else if (autocesteFilterBy.Contains(searchProperty))
            {
                var autocesteService = new AutocesteService(this.context);
                var filter = autocesteService.GetFilter(searchProperty, searchTerm);

                foreach (var a in context.Autocestes.Include(a => a.Naplatnepostajes).Where(filter))
                {
                    var newAutocesta = new Autocesta
                    {
                        Id = a.Id,
                        NeformalniNaziv = a.Neformalninaziv,
                        Oznaka = a.Oznaka,
                        Duljina = a.Duljina,
                        Dionica = a.Dionica,
                        NaplatnePostaje = new List<NaplatnaPostaja>()
                    };

                    foreach(var np in a.Naplatnepostajes)
                    {
                        newAutocesta.NaplatnePostaje.Add(new NaplatnaPostaja
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

                    autoceste.Add(newAutocesta);
                }

            } else
            {
                throw new Exception("searchProperty not applicable");
            }

            //pretvori autoceste u json
            json = JsonConvert.SerializeObject(autoceste, new JsonSerializerSettings
            {
                ContractResolver = new IgnorePropertyContractResolver(),
            });

            return json;
        }

        public string GetCsvFiltered(string searchProperty, string searchTerm)
        {
            var npFilterBy = new string[] { "Naziv", "GeoSirina", "GeoDuzina", "ImaEnc", "Any" };
            var autocesteFilterBy = new string[] { "Oznaka", "Duljina", "Dionica", "NeformalniNaziv" };

            var autoceste = new List<Autocesta>();

            if (npFilterBy.Contains(searchProperty))
            {
                var naplatneService = new NaplatnePostajeService(this.context);
                var autocesteService = new AutocesteService(this.context);

                var naplatne = naplatneService.GetNaplatnePostajeFiltered(searchProperty, searchTerm);

                foreach (NaplatnaPostaja naplatnaPostaja in naplatne)
                {
                    if (!autoceste.Any(a => a.Id == naplatnaPostaja.AutocestaId))
                    {
                        // ta autocesta jos ne postoji u listi

                        var autocesta = autocesteService.GetAutocestaById(naplatnaPostaja.AutocestaId ?? 0);
                        autocesta.NaplatnePostaje = new List<NaplatnaPostaja> { naplatnaPostaja };
                        autoceste.Add(autocesta);
                    }
                    else
                    {
                        autoceste.Where(a => a.Id == naplatnaPostaja.AutocestaId).First().NaplatnePostaje.Add(naplatnaPostaja);
                    }
                }

            }
            else if (autocesteFilterBy.Contains(searchProperty))
            {
                var autocesteService = new AutocesteService(this.context);
                var filter = autocesteService.GetFilter(searchProperty, searchTerm);

                foreach (var a in context.Autocestes.Include(a => a.Naplatnepostajes).Where(filter))
                {
                    var newAutocesta = new Autocesta
                    {
                        Id = a.Id,
                        NeformalniNaziv = a.Neformalninaziv,
                        Oznaka = a.Oznaka,
                        Duljina = a.Duljina,
                        Dionica = a.Dionica,
                        NaplatnePostaje = new List<NaplatnaPostaja>()
                    };

                    foreach (var np in a.Naplatnepostajes)
                    {
                        newAutocesta.NaplatnePostaje.Add(new NaplatnaPostaja
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

                    autoceste.Add(newAutocesta);
                }

            }
            else
            {
                throw new Exception("searchProperty not applicable");
            }

            var csv = new StringBuilder();
            csv.AppendLine("oznaka,neformalninaziv,dionica,duljina,naziv,geoduzina,geosirina,imaenc,kontakt");
            foreach(Autocesta a in autoceste)
            {
                foreach(var np in a.NaplatnePostaje)
                {
                    csv.AppendJoin(',', new string[]
                    {
                        a.Oznaka,
                        ToCsvString(a.NeformalniNaziv),
                        ToCsvString(a.Dionica),
                        a.Duljina.ToString(CultureInfo.InvariantCulture),
                        np.Naziv,
                        np.GeoDuzina?.ToString(CultureInfo.InvariantCulture),
                        np.GeoSirina?.ToString(CultureInfo.InvariantCulture),
                        np.ImaEnc ? "t" : "f",
                        ToCsvString(np.Kontakt)
                    });
                    csv.AppendLine();
                    //csv.Append(a.Oznaka + ",");
                    //if(a.NeformalniNaziv != null)
                    //{
                    //    csv.Append(a.NeformalniNaziv.Contains(",") ? ("\"" + a.NeformalniNaziv + "\"") : a.NeformalniNaziv);
                    //}
                    //csv.Append(",");
                    //csv.Append(a.Duljina + ",");
                    //csv.Append(np.Naziv + ",");
                    //csv.Append(np.GeoSirina + ",");
                    //csv.Append(np.GeoDuzina + ",");
                    //csv.Append(np.ImaEnc + ",");
                }
            }
            return csv.ToString();
        }

        private string ToCsvString(string s)
        {
            if (s == null) return s;
            return s.Contains(",") ? ("\"" + s + "\"") : s;
        }
    }
}
