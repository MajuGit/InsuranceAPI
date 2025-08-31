using InsuranceAPI.Application.Interfaces;
using InsuranceAPI.Domain.Entities;
using InsuranceAPI.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InsuranceAPI.XTests.Controllers
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepo;
        private readonly Mock<IClaimRepository> _mockClaimRepo;
        private readonly PatientsController _controller;

        public PatientControllerTests()
        {
            _mockPatientRepo = new Mock<IPatientRepository>();
            _mockClaimRepo = new Mock<IClaimRepository>();
            _controller = new PatientsController(_mockPatientRepo.Object, _mockClaimRepo.Object);
        }

        // ----------------------
        // Test: GetPatient
        // ----------------------
        [Fact]
        public async Task GetPatient_ReturnsOk_WithPatient()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "fathima" };
            _mockPatientRepo.Setup(repo => repo.GetPatientByIdAsync(1))
                .ReturnsAsync(patient);

            // Act
            var result = await _controller.GetPatient(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(patient, result.Value);
        }

        // ----------------------
        // Test: GetClaims
        // ----------------------
        [Fact]
        public async Task GetClaims_ReturnsOk_WithClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { Id = 1, PolicyId = 1, ClaimAmount = 500 },
                new Claim { Id = 2, PolicyId = 1, ClaimAmount = 1000 }
            };

            _mockClaimRepo.Setup(repo => repo.GetClaimsByPatientIdAsync(1))
                .ReturnsAsync(claims);

            // Act
            var result = await _controller.GetClaims(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(claims, result.Value);
        }

        // ----------------------
        // Test: SubmitClaim
        // ----------------------
        [Fact]
        public async Task SubmitClaim_ReturnsOk_WithSubmittedClaim()
        {
            // Arrange
            var claim = new Claim { Id = 1, PolicyId = 0, ClaimAmount = 800 };

            _mockClaimRepo.Setup(repo => repo.SubmitClaimAsync(It.IsAny<Claim>()))
                .ReturnsAsync((Claim c) => { c.Id = 10; return c; });

            // Act
            var result = await _controller.SubmitClaim(1, claim) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var submitted = Assert.IsType<Claim>(result.Value);
            Assert.Equal(10, submitted.Id);
            Assert.Equal(1, submitted.PolicyId); // controller should override
        }

        // ----------------------
        // Test: GetAllPatients
        // ----------------------
        [Fact]
        public async Task GetAllPatients_ReturnsOk_WithPatientsList()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "fathima" },
                new Patient { Id = 2, Name = "suhara" }
            };

            _mockPatientRepo.Setup(repo => repo.GetAllPatientsAsync())
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.GetAllPatients() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(patients, result.Value);
        }
    }
}
