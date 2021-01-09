using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LenelServices.Repositories.DTO;
using LenelServices.Repositories.Interfaces;

namespace LenelServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReader_REP_LOCAL _reader_REP_LOCAL;

        public ReaderController(IReader_REP_LOCAL reader_REP_LOCAL)
        {
            _reader_REP_LOCAL = reader_REP_LOCAL;
        }

        // GET: api/Reader
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //GET: api/Reader/5
        [HttpGet("/api/Reader/configLectora/{panelID}/{readerID}")]
        public async Task<object> ConfiguracionLectora(string panelID, string readerID)
        {
            try
            {
                return await _reader_REP_LOCAL.ConfiguracionLectora(panelID, readerID);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // POST: /api/Reader/AbrirPuerta
        [HttpPost("/api/Reader/AbrirPuerta")]
        public async Task<object> AbrirPuerta([FromBody] ReaderPath_DTO pathReader)
        {
            try {
                return await _reader_REP_LOCAL.AbrirPuerta(pathReader);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("/api/Reader/BloquearPuerta")]
        public async Task<object> BloquearPuerta([FromBody] ReaderPath_DTO pathReader)
        {
            try
            {
                return await _reader_REP_LOCAL.BloquearPuerta(pathReader);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // PUT: api/Reader/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
