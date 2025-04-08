using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Utils;
using Taxually.TechnicalTest.Utils.VatRegsitrationFactory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase {
        private readonly IVatRegistrationFactory registrationFactory;

        public VatRegistrationController(IVatRegistrationFactory vatRegistrationFactory) {
            registrationFactory = vatRegistrationFactory;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }            

            try {
                var registration = registrationFactory.CreateRegistration(request.Country);
                await registration.RegisterAsync(request);
                return Ok();

            } catch (NotSupportedException ex) {
                return BadRequest(ex.Message);
            } catch (Exception ex) {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
