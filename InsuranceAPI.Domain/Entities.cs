namespace InsuranceAPI.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class InsurancePolicy
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
    }

    public class Claim
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
