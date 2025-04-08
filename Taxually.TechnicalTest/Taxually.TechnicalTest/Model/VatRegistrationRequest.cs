using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Model
{
    public class VatRegistrationRequest {
        [Required(ErrorMessage = "CompanyName is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CompanyId is required.")]
        public string CompanyId { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
    }
}
