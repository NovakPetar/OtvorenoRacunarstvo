using Autoceste.BLL.Models;
using Autoceste.BLL.Services;
using Autoceste.DAL.Models;
using Autoceste.WebAPI2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autoceste.WebAPI2.Controllers
{
    [Route("api/v1/")]
    public class AutocesteController : ControllerBase
    {
        private AutocesteService autocesteService;
        private OrContext orContext;
        public AutocesteController(OrContext orContext)
        {
            this.orContext = orContext;
            autocesteService = new AutocesteService(orContext);
        }

        [HttpGet, Route("autoceste/{id}")]
        public IActionResult GetAutocestaById(int id)
        {
            try
            {
                var autocesta = autocesteService.GetAutocestaById(id);

                var response = new ResponseWrapper();

                if (autocesta == null)
                {
                    response.Status = "Not found";
                    response.Message = $"Autocesta s identifikatorom {id} nije pronađena.";
                    return NotFound(response);
                }

                response.Status = "OK";
                response.Message = $"Dohvaćena je autocesta s identifikatorom {id}.";

                response.Response = autocesta;
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }

        [HttpGet, Route("autoceste")]
        public IActionResult GetAutoceste([FromQuery] string searchProperty, string searchTerm)
        {
            try
            {
                if (searchProperty == null || searchTerm == null) //bez searcha, dohvati sve
                {
                    var autoceste = autocesteService.GetAutoceste();

                    var response = new ResponseWrapper();

                    if (autoceste == null || autoceste.Count == 0)
                    {
                        response.Status = "Not found";
                        response.Message = $"Nije pronađena niti jedna autocesta.";
                        return NotFound(response);
                    }

                    response.Status = "OK";
                    response.Message = $"Lista autocesta je dohvaćena.";
                    response.Response = autoceste;

                    return Ok(response); 
                } else //search
                {
                    List<Autocesta> autoceste = null;
                    try
                    {
                        autoceste = autocesteService.GetAutocesteFiltered(searchProperty, searchTerm);
                    }
                    catch (ArgumentException)
                    {
                        return BadRequest(new ResponseWrapper
                        {
                            Status = "Bad request",
                            Message = "SearchProperty nije valjan. Valjane vrijednosti su: NeformalniNaziv, Dionica, Oznaka, Duljina."
                        });
                    }

                    var response = new ResponseWrapper();

                    if (autoceste == null || autoceste.Count == 0)
                    {
                        response.Status = "Not found";
                        response.Message = $"Provedeno pretraživanje po uvjetu {searchProperty}={searchTerm}. Niti jedna autocesta nije pronađena.";
                        return NotFound(response);
                    }

                    response.Status = "OK";
                    response.Message = $"Provedeno pretraživanje po uvjetu {searchProperty}={searchTerm}. Dohvaćene su autoceste koje zadovoljavaju uvjet.";
                    response.Response = autoceste;
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }

    }
}
