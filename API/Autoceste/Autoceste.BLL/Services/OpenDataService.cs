using Autoceste.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //TODO: implement
            var npFilterBy = new string[] { "Naziv", "GeoSirina", "GeoDuzina", "ImaEnc", "Any" };
            var autocesteFilterBy = new string[] { "Oznaka", "Duljina", "Dionica", "NeformalniNaziv" };

            if (npFilterBy.Contains(searchProperty)) 
            {
                
            } else if (autocesteFilterBy.Contains(searchProperty))
            {

            } else
            {
                throw new Exception("searchProperty not applicable");
            }
            return "";
        }

        public string GetCsvFiltered(string searchProperty, string searchTerm)
        {
            //TODO: implement
            return "";
        }
    }
}
