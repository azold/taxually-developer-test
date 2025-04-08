using Taxually.TechnicalTest.Model;
public interface ISerializerManager {
    public Task EnqueueVatDataAsync(string queueName, VatRegistrationRequest request);
}