using InsuranceAPI.Application.Interfaces;
using InsuranceAPI.Domain.Entities;
using InsuranceAPI.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceAPI.NTests.Controllers
{
    [TestFixture]
    public class PatientControllerNTests
    {
        private Mock<IPatientRepository> _mockPatientRepo;
        private Mock<IClaimRepository> _mockClaimRepo;
        private PatientsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockPatientRepo = new Mock<IPatientRepository>();
            _mockClaimRepo = new Mock<IClaimRepository>();

            _controller = new PatientsController(_mockPatientRepo.Object, _mockClaimRepo.Object);
        }

        // ----------------------
        // Test: GetPatient
        // ----------------------
        [Test]
        public async Task GetPatient_ReturnsOk_WithPatient()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "fathima" };
            _mockPatientRepo.Setup(repo => repo.GetPatientByIdAsync(1))
                .ReturnsAsync(patient);

            // Act
            var result = await _controller.GetPatient(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(patient));
        }

        // ----------------------
        // Test: GetClaims
        // ----------------------
        [Test]
        public async Task GetClaims_ReturnsOk_WithClaims()
        {
            // Arrange
            var claims = new List<Claim> {
                new Claim { Id = 1, PolicyId = 1, ClaimAmount = 500 },
                new Claim { Id = 2, PolicyId = 1, ClaimAmount = 1000 }
            };

            _mockClaimRepo.Setup(repo => repo.GetClaimsByPatientIdAsync(1))
                .ReturnsAsync(claims);

            // Act
            var result = await _controller.GetClaims(1) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(claims));
        }

        // ----------------------
        // Test: SubmitClaim
        // ----------------------
        [Test]
        public async Task SubmitClaim_ReturnsOk_WithSubmittedClaim()
        {
            // Arrange
            var claim = new Claim { Id = 1, PolicyId = 0, ClaimAmount = 800 };

            _mockClaimRepo.Setup(repo => repo.SubmitClaimAsync(It.IsAny<Claim>()))
                .ReturnsAsync((Claim c) => { c.Id = 10; return c; });

            // Act
            var result = await _controller.SubmitClaim(1, claim) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var submitted = result.Value as Claim;
            Assert.That(submitted, Is.Not.Null);
            Assert.That(submitted.Id, Is.EqualTo(10));
            Assert.That(submitted.PolicyId, Is.EqualTo(1)); // should be set by controller
        }

        // ----------------------
        // Test: GetAllPatients
        // ----------------------
        [Test]
        public async Task GetAllPatients_ReturnsOk_WithPatientsList()
        {
            // Arrange
            var patients = new List<Patient> {
                new Patient { Id = 0, Name = "fathima" },
                new Patient { Id = 2, Name = "suhara" }
            };

            _mockPatientRepo.Setup(repo => repo.GetAllPatientsAsync())
                .ReturnsAsync(patients);

            // Act
            var result = await _controller.GetAllPatients() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(patients));
        }
    }
}
