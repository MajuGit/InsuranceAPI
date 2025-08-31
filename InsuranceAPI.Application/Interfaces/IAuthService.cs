namespace InsuranceAPI.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string username, string role);
    }
}
