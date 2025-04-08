namespace Taxually.TechnicalTest.Clients
{
    public interface ITaxuallyQueueClient {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}
