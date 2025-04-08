using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public interface IVatRegistration {
        Task RegisterAsync(VatRegistrationRequest request);
    }
}
