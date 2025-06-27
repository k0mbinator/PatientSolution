

namespace PatientSolution.Api.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatientsAsync();

    }
}