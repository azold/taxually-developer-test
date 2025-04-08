using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public class VatRegistrationFactory : IVatRegistrationFactory
    {

        private readonly IServiceProvider serviceProvider;

        public VatRegistrationFactory(IServiceProvider sProvider) {
            this.serviceProvider = sProvider;
        }

        public IVatRegistration CreateRegistration(string country) {
            if (!Enum.TryParse<CountryCode>(country, true, out var countryCode)) {
                // If the country code is invalid, throw an exception with a more clear message
                throw new NotSupportedException($"The country code '{country}' is not valid or supported.");
            }

            var registrations = GetRegistrations();
            if (registrations.TryGetValue(countryCode, out var registrationFactory)) {
                return registrationFactory();
            }

            throw new NotSupportedException($"Country '{country}' is not supported for VAT registration.");        
        }

        private Dictionary<CountryCode, Func<IVatRegistration>> GetRegistrations() {
        
            return new Dictionary<CountryCode, Func<IVatRegistration>> {
                    { CountryCode.GB, () => serviceProvider.GetRequiredService<UKVatRegistration>()},
                    { CountryCode.FR, () => serviceProvider.GetRequiredService<FrenchVatRegistration>()},
                    { CountryCode.DE, () => serviceProvider.GetRequiredService<GermanVatRegistration>()}
            };
        }
    }
}
