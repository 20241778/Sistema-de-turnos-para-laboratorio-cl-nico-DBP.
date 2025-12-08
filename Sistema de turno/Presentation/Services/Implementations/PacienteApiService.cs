using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Services.Interfaces;

namespace Presentation.Services.Implementations
{
    public class PacienteApiService : IPacienteService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://localhost:7241/api/pacientes";

        public PacienteApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Pacientedata>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Pacientedata>>(BaseUrl);
            return res ?? Array.Empty<Pacientedata>();
        }

        public async Task<Pacientedata?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Pacientedata>($"{BaseUrl}/{id}");
        }

        public async Task<Pacientedata> CreateAsync(Pacientedata dto)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Pacientedata>();
        }

        public async Task<Pacientedata> UpdateAsync(Pacientedata dto)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{dto.Id}", dto);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Pacientedata>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
