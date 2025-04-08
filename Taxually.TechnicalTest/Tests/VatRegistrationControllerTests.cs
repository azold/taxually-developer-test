using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Utils.VatRegsitrationFactory;
using Xunit;
using Assert = Xunit.Assert;

namespace Taxually.TechnicalTest.Tests.Controllers
{
    public class VatRegistrationControllerTests {
        private readonly Mock<IVatRegistrationFactory> factoryMock;
        private readonly Mock<IVatRegistration> registrationMock;
        private readonly VatRegistrationController vatRegistrationController;

        public VatRegistrationControllerTests() {
            factoryMock = new Mock<IVatRegistrationFactory>();
            registrationMock = new Mock<IVatRegistration>();
            vatRegistrationController = new VatRegistrationController(factoryMock.Object);
        }

        [Theory]
        [InlineData(nameof(CountryCode.GB))]
        [InlineData(nameof(CountryCode.FR))]
        [InlineData(nameof(CountryCode.DE))]
        public async Task Post_ValidRequest_ReturnsOk(string country) {
            // Arrange
            var request = new VatRegistrationRequest {
                CompanyName = "Test Company",
                CompanyId = "123456",
                Country = country
            };

            factoryMock.Setup(f => f.CreateRegistration(request.Country)).Returns(registrationMock.Object);

            // Act
            var result = await vatRegistrationController.Post(request);

            // Assert
            registrationMock.Verify(r => r.RegisterAsync(request), Times.Once);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var request = new VatRegistrationRequest {
                CompanyName = "",
                CompanyId = "123456",
                Country = nameof(CountryCode.GB)
            };

            vatRegistrationController.ModelState.AddModelError("CompanyName", "CompanyName is required.");

            // Act
            var result = await vatRegistrationController.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var serializableError = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(serializableError.ContainsKey("CompanyName"));
            Assert.Equal("CompanyName is required.", ((string[])serializableError["CompanyName"])[0]);
        }

        [Fact]
        public async Task Post_UnsupportedCountry_ReturnsBadRequest() {
            // Arrange
            var request = new VatRegistrationRequest {
                CompanyName = "Test Company",
                CompanyId = "123456",
                Country = "ES"
            };

            factoryMock.Setup(f => f.CreateRegistration(request.Country)).Throws(new NotSupportedException("Country 'ES' is not supported for VAT registration."));

            // Act
            var result = await vatRegistrationController.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Country 'ES' is not supported for VAT registration.", badRequestResult.Value);
        }

        [Fact]
        public async Task Post_ExceptionThrown_ReturnsInternalServerError() {
            // Arrange
            var request = new VatRegistrationRequest {
                CompanyName = "Test Company",
                CompanyId = "123456",
                Country = nameof(CountryCode.GB)
            };

            factoryMock.Setup(f => f.CreateRegistration(request.Country)).Returns(registrationMock.Object);
            registrationMock.Setup(r => r.RegisterAsync(request)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await vatRegistrationController.Post(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred: Test exception", statusCodeResult.Value);
        }
    }
}

