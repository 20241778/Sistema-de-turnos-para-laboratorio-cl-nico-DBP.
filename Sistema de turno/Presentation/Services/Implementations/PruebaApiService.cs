using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Services.Interfaces;

namespace Presentation.Services.Implementations
{
    public class PruebaApiService : IPruebaService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7241/api/pruebas";

        public PruebaApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Pruebadata>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Pruebadata>>(BaseUrl);
            return res ?? Array.Empty<Pruebadata>();
        }

        public async Task<Pruebadata?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Pruebadata>($"{BaseUrl}/{id}");
        }

        public async Task<Pruebadata> CreateAsync(Pruebadata dto)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Pruebadata>();
        }

        public async Task<Pruebadata> UpdateAsync(Pruebadata dto)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{dto.Id}", dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Pruebadata>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<Pruebadata?> GetByCodigoAsync(string codigo)
        {
            return await _http.GetFromJsonAsync<Pruebadata>($"{BaseUrl}/bycodigo/{Uri.EscapeDataString(codigo)}");
        }
    }
}
