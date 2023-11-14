using Autoceste.BLL.Services;
using Autoceste.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autoceste.WebAPI.Controllers
{
    public class NaplatnePostajeController : ControllerBase
    {
        private NaplatnePostajeService npService;
        public NaplatnePostajeController(OrContext orContext)
        {
            npService = new NaplatnePostajeService(orContext);
        }

        [HttpGet, Route("naplatne-postaje/{id}")]
        public IActionResult GetNaplatnaPostajaById(int id)
        {
            var np = npService.GetNaplatnaPostajaById(id);
            if (np == null) return NotFound();
            return Ok(np);
        }

        [HttpGet, Route("naplatne-postaje")]
        public IActionResult GetNaplatnePostaje()
        {
            var np = npService.GetNaplatnePostaje();
            if (np == null) return NotFound();
            return Ok(np);
        }

        [HttpGet, Route("search/naplatne-postaje")]
        public IActionResult GetNaplatnePostajeFiltered(string searchProperty, string searchTerm)
        {
            var np = npService.GetNaplatnePostajeFiltered(searchProperty, searchTerm);
            if (np == null) return NotFound();
            return Ok(np);
        }

        [HttpGet, Route("autoceste/{autocestaId}/naplatne-postaje")]
        public IActionResult GetNaplatnePostajeByAutocestaId(int autocestaId)
        {
            var n = npService.GetNaplatnePostajeByAutocestaId(autocestaId);
            if (n == null) return NotFound();
            return Ok(n);
        }
    }
}
