using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Services.Interfaces;

namespace Presentation.Services.Implementations
{
    public class CitaApiService : ICitaService
    {
        private readonly HttpClient _http;

        public CitaApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api"); ;
        }

        public async Task<IEnumerable<Citadata>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Citadata>>($"api/citas/");
            return res ?? Array.Empty<Citadata>();
        }

        public async Task<Citadata?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Citadata>($"api/citas/{id}");
        }

        public async Task<IEnumerable<Citadata>> GetByPacienteAsync(Guid pacienteId)
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Citadata>>($"api/citas/bypaciente/{pacienteId}");
            return res ?? Array.Empty<Citadata>();
        }

        public async Task<IEnumerable<Citadata>> GetByTecnicoAndDateRangeAsync(Guid tecnicoId, DateTime from, DateTime to)
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Citadata>>($"api/citas/bytecnico/{tecnicoId}?from={from:o}&to={to:o}");
            return res ?? Array.Empty<Citadata>();
        }

        public async Task<bool> HasConflictingAppointmentAsync(Guid tecnicoId, DateTime fecha)
        {
            var result = await _http.GetFromJsonAsync<bool?>($"api/citas/hasconflict/{tecnicoId}?fecha={fecha:o}");
            return result ?? false;
        }
    }
}
