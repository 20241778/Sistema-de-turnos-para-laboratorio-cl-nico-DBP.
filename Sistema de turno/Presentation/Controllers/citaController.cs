using Microsoft.AspNetCore.Mvc;
using Presentation.Services.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // GET: /Cita
        public async Task<IActionResult> Index()
        {
            var citas = await _citaService.GetAllAsync();
            return View(citas);
        }
        public async Task<ActionResult<IEnumerable<Citadata>>> GetByIdAsync(Guid id)
        {
            var result = await _citaService.GetByIdAsync(id);
            return Ok(result);
        }

        public IActionResult Create()
        {
            //var citas = await _citaService.GetAllAsync();
            return View();
        }

        public IActionResult Details()
        {
            //var citas = await _citaService.GetAllAsync();
            return View();
        }

        public IActionResult Edit()
        {
            //var citas = await _citaService.GetAllAsync();
            return View();
        }
    }
}
