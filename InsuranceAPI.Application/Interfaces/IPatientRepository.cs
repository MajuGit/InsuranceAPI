using InsuranceAPI.Domain.Entities;

namespace InsuranceAPI.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}