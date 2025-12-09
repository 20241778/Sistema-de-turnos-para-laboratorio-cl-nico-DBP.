using Microsoft.AspNetCore.Mvc;
using LabClinic.Application.Interfaces;
using LabClinic.Application.DATA;

namespace Sistema_de_turno.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _service;

        public CitasController(ICitaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citadata>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Citadata>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<Citadata>>> GetByPaciente(Guid pacienteId)
        {
            var result = await _service.GetByPacienteAsync(pacienteId);
            return Ok(result);
        }

        [HttpGet("tecnico/{tecnicoId}")]
        public async Task<ActionResult<IEnumerable<Citadata>>> GetByTecnicoAndRange(
         Guid tecnicoId,
 [FromQuery] DateTime from,
         [FromQuery] DateTime to)
        {
            var result = await _service.GetByTecnicoAndRangeAsync(tecnicoId, from, to);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Citadata>> Create([FromBody] CreateCitadata dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/fecha")]
        public async Task<IActionResult> UpdateFecha(Guid id, [FromBody] DateTime nuevaFecha)
        {
            try
            {
                await _service.UpdateFechaAsync(id, nuevaFecha);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/cancelar")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                await _service.CancelAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
