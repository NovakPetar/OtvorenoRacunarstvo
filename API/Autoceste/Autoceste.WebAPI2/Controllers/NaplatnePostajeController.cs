using Autoceste.BLL.Models;
using Autoceste.BLL.Services;
using Autoceste.BLL.Validators;
using Autoceste.DAL.Models;
using Autoceste.WebAPI2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autoceste.WebAPI2.Controllers
{
    [Route("api/v1/")]
    public class NaplatnePostajeController : ControllerBase
    {
        private NaplatnePostajeService npService;
        private OrContext orContext;
        public NaplatnePostajeController(OrContext orContext)
        {
            npService = new NaplatnePostajeService(orContext);
            this.orContext = orContext;
        }

        [HttpGet, Route("naplatne-postaje/{id}")]
        public IActionResult GetNaplatnaPostajaById(int id)
        {
            try
            {
                var response = new ResponseWrapper();

                var np = npService.GetNaplatnaPostajaById(id);

                if (np == null)
                {
                    response.Status = "Not found.";
                    response.Message = $"Naplatna postaja s identifikatorom {id} nije pronađena.";
                    return NotFound(response);
                }
                response.Status = "OK";
                response.Message = $"Dohvaćena je naplatna postaja s identifikatorom {id}.";
                response.Response = np;

                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }

        [HttpGet, Route("naplatne-postaje")]
        public IActionResult GetNaplatnePostaje(string searchProperty, string searchTerm)
        {
            try
            {
                if (searchProperty == null || searchTerm == null) //bez searcha, dohvati sve
                {
                    var response = new ResponseWrapper();

                    var np = npService.GetNaplatnePostaje();

                    if (np == null || np.Count == 0)
                    {
                        response.Status = "Not found.";
                        response.Message = $"Nije pronađena niti jedna naplatna postaja.";
                        return NotFound(response);
                    }

                    response.Status = "OK";
                    response.Message = $"Dohvaćena je lista naplatnih postaja.";
                    response.Response = np;

                    return Ok(response); 

                } else //search
                {
                    List<NaplatnaPostaja> np = null;
                    try
                    {
                        np = npService.GetNaplatnePostajeFiltered(searchProperty, searchTerm);
                    }
                    catch (ArgumentException)
                    {
                        return BadRequest(new ResponseWrapper
                        {
                            Status = "Bad request",
                            Message = "SearchProperty nije valjan. Valjane vrijednosti su: Naziv, Kontakt, GeoDuzina, GeoSirina, ImaEnc, Any."
                        });
                    }

                    var response = new ResponseWrapper();

                    if (np == null || np.Count == 0)
                    {
                        response.Status = "Not found";
                        response.Message = $"Provedeno pretraživanje po uvjetu {searchProperty}={searchTerm}. Niti jedna naplatna postaja nije pronađena.";
                        return NotFound(response);
                    }

                    response.Status = "OK";
                    response.Message = $"Provedeno pretraživanje po uvjetu {searchProperty}={searchTerm}. Dohvaćene su naplatne postaje koje zadovoljavaju uvjet.";
                    response.Response = np;
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }

        }

        [HttpGet, Route("autoceste/{autocestaId}/naplatne-postaje")]
        public IActionResult GetNaplatnePostajeByAutocestaId(int autocestaId)
        {
            try
            {
                var response = new ResponseWrapper();

                var np = npService.GetNaplatnePostajeByAutocestaId(autocestaId);

                if (np == null || np.Count == 0)
                {
                    response.Status = "Not found.";
                    response.Message = $"Nije pronađena niti jedna naplatna postaja na autocesti s identifikatorom {autocestaId}.";
                    return NotFound(response);
                }

                response.Status = "OK";
                response.Message = $"Dohvaćena je lista naplatnih postaja na autocesti s identifikatorom {autocestaId}.";
                response.Response = np;
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }


        [HttpPost, Route("naplatne-postaje")]
        public IActionResult Create([FromBody] NaplatnaPostaja naplatnaPostaja)
        {
            try
            {
                var response = new ResponseWrapper();

                var validator = new NaplatnaPostajaValidator(this.orContext);
                var validationResult = validator.Validate(naplatnaPostaja);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    response.Status = "Bad request";
                    response.Message = validationResult;
                    return BadRequest(response);
                }

                var created = this.npService.CreateNaplatnaPostaja(naplatnaPostaja);
                response.Status = "Created";
                response.Message = $"Kreirana naplatna postaja s identifikatorom {created.Id}.";
                response.Response = new
                {
                    Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/api/v1/naplatne-postaje/{created.Id}"
                };

                return StatusCode(201, response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }

        [HttpDelete, Route("naplatne-postaje/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var response = new ResponseWrapper();

                var result = this.npService.DelateNaplatnaPostaja(id);
                
                if (result)
                {
                    response.Status = "Deleted";
                    response.Message = "Naplatna postaja je obrisana.";
                    return StatusCode(200, response);
                }

                response.Status = "Not found";
                response.Message = $"Ne postoji naplatna postaja s identifikatorom {id}.";
                return StatusCode(404, response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }

        [HttpPut, Route("naplatne-postaje/{id}")]
        public IActionResult Update(int id, [FromBody] NaplatnaPostaja naplatnaPostaja)
        {
            try
            {
                var response = new ResponseWrapper();

                var validator = new NaplatnaPostajaValidator(this.orContext);
                var validationResult = validator.Validate(naplatnaPostaja);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    response.Status = "Bad request";
                    response.Message = validationResult;
                    return BadRequest(response);
                }

                var updated = this.npService.UpdateNaplatnaPostaja(id, naplatnaPostaja);

                if (updated == null)
                {
                    response.Status = "Not found";
                    response.Message = $"Naplatna postaja s identifikatorom {id} nije pronađena.";

                    return NotFound(response);
                }

                response.Status = "Updated";
                response.Message = $"Naplatna postaja je ažurirana.";
                response.Response = updated;

                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, ErrorResponses.GeneralException);
            }
        }
    }
}
