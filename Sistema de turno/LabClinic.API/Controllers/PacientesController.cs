using LabClinic.Applicattion.Interfaces;

namespace LabClinic.API.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Paciente dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        private IActionResult CreatedAtAction(string v, object value, object created)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Paciente dto)
        {
            if (id != dto.Id)
                return BadRequest("ID no coincide");

            return Ok(await _service.UpdateAsync(dto));
        }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return Ok(new { message = "Paciente eliminado" });
        }
    }
}

