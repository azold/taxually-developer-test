using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public class UKVatRegistration : IVatRegistration {
        private readonly ITaxuallyHttpClient httpClient;

        public UKVatRegistration(ITaxuallyHttpClient taxuallyHttpClient)
        {
            httpClient = taxuallyHttpClient;
        }

        public async Task RegisterAsync(VatRegistrationRequest request)
        {
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
