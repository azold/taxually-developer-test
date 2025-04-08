using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Utils
{
    public class SerializerManager : ISerializerManager {
        private readonly ITaxuallyQueueClient queueClient;
        public SerializerManager(ITaxuallyQueueClient taxuallyQueueClient) {
            queueClient = taxuallyQueueClient;
        }

        public async Task EnqueueVatDataAsync(string queueName, VatRegistrationRequest request) {
            var serializedData = string.Empty;

            if (queueName.EndsWith("xml", StringComparison.OrdinalIgnoreCase)) {
                serializedData = SerializeToXml(request);
            } else {
                serializedData = SerializeToCsv(request);
            }

            var bytes = Encoding.UTF8.GetBytes(serializedData);
            await queueClient.EnqueueAsync(queueName, bytes);
        }

        private string SerializeToCsv(VatRegistrationRequest request) {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
            return csvBuilder.ToString();
        }

        private string SerializeToXml(VatRegistrationRequest request) {
            using (var stringwriter = new StringWriter()) {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, request);
                return stringwriter.ToString();
            }
        }
    }
}
