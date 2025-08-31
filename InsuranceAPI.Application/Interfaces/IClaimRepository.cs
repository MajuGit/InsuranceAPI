using InsuranceAPI.Domain.Entities;

namespace InsuranceAPI.Application.Interfaces
{
    public interface IClaimRepository
    {
        Task<Claim> SubmitClaimAsync(Claim claim);
        Task<Claim?> GetClaimByIdAsync(int id);
        Task<IEnumerable<Claim>> GetClaimsByPatientIdAsync(int patientId);
        Task UpdateClaimAsync(Claim claim);
    }
}
