using Autoceste.BLL.Models;
using Autoceste.BLL.Services;
using Autoceste.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autoceste.WebAPI.Controllers
{
    public class AutocesteController : ControllerBase
    {
        private AutocesteService autocesteService;
        public AutocesteController(OrContext orContext)
        {
            autocesteService = new AutocesteService(orContext);
        }

        [HttpGet, Route("autoceste/{id}")]
        public IActionResult GetAutocestaById(int id)
        {
            var a = autocesteService.GetAutocestaById(id);

            if (a == null) return NotFound();

            return Ok(a);
        }

        [HttpGet, Route("autoceste")]
        public IActionResult GetAutoceste()
        {
            var a = autocesteService.GetAutoceste;

            if (a == null) return NotFound();

            return Ok(a);
        }

        [HttpGet, Route("search/autoceste")]
        public IActionResult GetAutoceste(string searchProperty, string searchTerm)
        {
            var a = autocesteService.GetAutocesteFiltered(searchProperty, searchTerm);

            if (a == null) return NotFound();

            return Ok(a);
        }
    }
}
