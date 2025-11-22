using Microsoft.AspNetCore.Mvc;
using LabClinic.Applicattion.Interfaces;
using LabClinic.Applicattion.DATA;

namespace Sistema_de_turno.Controllers
{
    [ApiController]
  [Route("api/[controller]")]
    public class TecnicosController : ControllerBase
 {
        private readonly ITecnicoService _service;

        public TecnicosController(ITecnicoService service)
        {
_service = service;
 }

        [HttpGet]
     public async Task<ActionResult<IEnumerable<Tecnicodata>>> GetAll()
        {
        var result = await _service.GetAllAsync();
    return Ok(result);
        }

        [HttpGet("{id}")]
    public async Task<ActionResult<Tecnicodata>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
          return Ok(result);
        }

        [HttpGet("especialidad/{especialidad}")]
        public async Task<ActionResult<IEnumerable<Tecnicodata>>> GetByEspecialidad(string especialidad)
        {
 var result = await _service.GetByEspecialidadAsync(especialidad);
     return Ok(result);
        }

        [HttpPost]
    public async Task<ActionResult<Tecnicodata>> Create([FromBody] CreateUpdateTecnicodata dto)
        {
      var result = await _service.CreateAsync(dto);
  return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateTecnicodata dto)
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
