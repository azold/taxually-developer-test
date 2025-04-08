using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils.VatRegsitrationFactory
{
    public class GermanVatRegistration : IVatRegistration {

        private readonly ISerializerManager _serializerManager;

        public GermanVatRegistration(ISerializerManager serializerManager)
        {
            _serializerManager = serializerManager;
        }

        public async Task RegisterAsync(VatRegistrationRequest request)
        {
            await _serializerManager.EnqueueVatDataAsync(Consts.XML_QUEUE_NAME, request);
        }
    }
}
