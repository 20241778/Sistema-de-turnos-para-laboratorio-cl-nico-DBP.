using Microsoft.AspNetCore.Mvc;
using LabClinic.Applicattion.Interfaces;
using LabClinic.Applicattion.DATA;

namespace Sistema_de_turno.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _service;

        public PacientesController(IPacienteService service)
        {
            _service = service;
     }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacientedata>>> GetAll()
  {
  var result = await _service.GetAllAsync();
       return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pacientedata>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
     if (result == null)
            return NotFound();
     return Ok(result);
 }

  [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Pacientedata>>> Search([FromQuery] string term)
        {
      var result = await _service.SearchByNameAsync(term);
  return Ok(result);
        }

        [HttpPost]
    public async Task<ActionResult<Pacientedata>> Create([FromBody] CreateUpdatePacientedata dto)
    {
       var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

      [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdatePacientedata dto)
        {
    try
       {
    await _service.UpdateAsync(id, dto);
      return NoContent();
            }
            catch (KeyNotFoundException)
            {
            return NotFound();
      }
        }

        [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(Guid id)
        {
            try
        {
   await _service.DeleteAsync(id);
                return NoContent();
  }
            catch (KeyNotFoundException)
        {
  return NotFound();
      }
   }
    }
}
