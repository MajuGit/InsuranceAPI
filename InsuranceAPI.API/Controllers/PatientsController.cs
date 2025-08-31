using InsuranceAPI.Application.Interfaces;
using InsuranceAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepo;
    private readonly IClaimRepository _claimRepo;

    public PatientsController(IPatientRepository patientRepo, IClaimRepository claimRepo)
    {
        _patientRepo = patientRepo;
        _claimRepo = claimRepo;
    }

    [HttpPost("AddPatient")]
    public async Task<IActionResult> AddPatient(Patient patient)
    {
        var addedPatient = await _patientRepo.AddPatientAsync(patient);
        return Ok(addedPatient);
    }

    [HttpGet("{id}/GetPatient")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var claims = await _patientRepo.GetPatientByIdAsync(id);
        return Ok(claims);
    }

    [HttpGet("{id}/GetClaims")]
    public async Task<IActionResult> GetClaims(int id)
    {
        var claims = await _claimRepo.GetClaimsByPatientIdAsync(id);
        return Ok(claims);
    }

    [HttpPost("{id}/SubmitClaim")]
    public async Task<IActionResult> SubmitClaim(int id, Claim claim)
    {
        claim.PolicyId = id;
        var submittedClaim = await _claimRepo.SubmitClaimAsync(claim);
        return Ok(submittedClaim);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllPatients")]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _patientRepo.GetAllPatientsAsync();
        return Ok(patients);
    }
}
