using Microsoft.AspNetCore.Mvc;
using LabClinic.Applicattion.Interfaces;
using LabClinic.Applicattion.DATA;

namespace Sistema_de_turno.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebasController : ControllerBase
  {
 private readonly IPruebaService _service;

        public PruebasController(IPruebaService service)
        {
       _service = service;
 }

        [HttpGet]
     public async Task<ActionResult<IEnumerable<Pruebadata>>> GetAll()
  {
     var result = await _service.GetAllAsync();
         return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pruebadata>> GetById(Guid id)
{
            var result = await _service.GetByIdAsync(id);
  if (result == null)
           return NotFound();
       return Ok(result);
        }

   [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<Pruebadata>> GetByCodigo(string codigo)
{
var result = await _service.GetByCodigoAsync(codigo);
  if (result == null)
          return NotFound();
        return Ok(result);
        }

[HttpPost]
        public async Task<ActionResult<Pruebadata>> Create([FromBody] CreateUpdatePruebadata dto)
        {
       var result = await _service.CreateAsync(dto);
     return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
   public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdatePruebadata dto)
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
