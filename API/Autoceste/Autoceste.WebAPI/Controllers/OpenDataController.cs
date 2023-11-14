using Autoceste.BLL.Services;
using Autoceste.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autoceste.WebAPI.Controllers
{
    public class OpenDataController : ControllerBase
    {
        private IConfiguration configuration;
        private OpenDataService openDataService;
        public OpenDataController(IConfiguration configuration, OrContext orContext)
        {
            openDataService = new OpenDataService(orContext);
            this.configuration = configuration;
        }
        [HttpGet, Route("json")]
        public IActionResult GetJson()
        {
            return Ok(openDataService.GetJson(configuration.GetConnectionString("OrDatabaseConnection")));
        }
        [HttpGet, Route("csv")]
        public IActionResult GetCsv()
        {
            return Ok(openDataService.GetCsv(configuration.GetConnectionString("OrDatabaseConnection")));
        }

        [HttpGet, Route("filtered/json")]
        public IActionResult GetJsonFiltered(string searchProperty, string searchTerm)
        {
            return Ok(openDataService.GetJsonFiltered(searchProperty, searchTerm));
        }
        [HttpGet, Route("filtered/csv")]
        public IActionResult GetCsvFiltered(string searchProperty, string searchTerm)
        {
            return Ok(openDataService.GetCsvFiltered(searchProperty, searchTerm));
        }
    }
}
