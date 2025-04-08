using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public class FrenchVatRegistration : IVatRegistration {
        private readonly ISerializerManager _serializerManager;

        public FrenchVatRegistration(ISerializerManager serializerManager)
        {
            _serializerManager = serializerManager;
        }

        public async Task RegisterAsync(VatRegistrationRequest request)
        {
            await _serializerManager.EnqueueVatDataAsync(Consts.CSV_QUEUE_NAME, request);
        }

    }
}
