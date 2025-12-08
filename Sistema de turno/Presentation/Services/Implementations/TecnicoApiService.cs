using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Services.Interfaces;

namespace Presentation.Services.Implementations
{
    public class TecnicoApiService : ITecnicoService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7241/api/tecnicos";

        public TecnicoApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Tecnicodata>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Tecnicodata>>(BaseUrl);
            return res ?? Array.Empty<Tecnicodata>();
        }

        public async Task<Tecnicodata?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Tecnicodata>($"{BaseUrl}/{id}");
        }

        public async Task<Tecnicodata> CreateAsync(Tecnicodata dto)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Tecnicodata>();
        }

        public async Task<Tecnicodata> UpdateAsync(Tecnicodata dto)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{dto.Id}", dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Tecnicodata>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Tecnicodata>> GetByEspecialidadAsync(string especialidad)
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Tecnicodata>>($"{BaseUrl}/byespecialidad/{Uri.EscapeDataString(especialidad)}");
            return res ?? Array.Empty<Tecnicodata>();
        }
    }
}
