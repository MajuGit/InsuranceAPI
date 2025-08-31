using InsuranceAPI.Application.Interfaces;
using InsuranceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace InsuranceAPI.Infrastructure.Data
{
    public class TpaDbContext : DbContext
    {
        public TpaDbContext(DbContextOptions<TpaDbContext> options) : base(options) { }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<InsurancePolicy> Policies => Set<InsurancePolicy>();
        public DbSet<Claim> Claims => Set<Claim>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names, relationships, constraints here if needed
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Claim>().ToTable("Claims");
            modelBuilder.Entity<InsurancePolicy>().ToTable("Policies");
        }
    }

    public class PatientRepository : IPatientRepository
    {
        private readonly TpaDbContext _context;
        public PatientRepository(TpaDbContext context) => _context = context;

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync() =>
            await _context.Patients.ToListAsync();

        public async Task<Patient?> GetPatientByIdAsync(int id) =>
            await _context.Patients.FindAsync(id);
    }

    public class ClaimRepository : IClaimRepository
    {
        private readonly TpaDbContext _context;
        public ClaimRepository(TpaDbContext context) => _context = context;

        public async Task<Claim> SubmitClaimAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task UpdateClaimAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<Claim?> GetClaimByIdAsync(int id) =>
            await _context.Claims.FindAsync(id);

        public async Task<IEnumerable<Claim>> GetClaimsByPatientIdAsync(int patientId) =>
            await _context.Claims
                .Where(c => c.PolicyId == patientId)
                .ToListAsync();
    }
}
