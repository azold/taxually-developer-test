namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public interface IVatRegistrationFactory {
        IVatRegistration CreateRegistration(string country);
    }
}
