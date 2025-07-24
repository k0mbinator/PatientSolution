using PatientSolution.Shared.Models;
using System.Net.Http.Json;

namespace PatientSolution.Client.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            var patients = await _httpClient.GetFromJsonAsync<List<Patient>>("api/patients");
            return patients ?? new List<Patient>();
        }
    }
}
