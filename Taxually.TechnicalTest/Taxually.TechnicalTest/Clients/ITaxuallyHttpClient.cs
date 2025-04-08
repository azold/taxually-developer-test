namespace Taxually.TechnicalTest.Clients
{
    public interface ITaxuallyHttpClient {
        public Task PostAsync<TRequest>(string url, TRequest request);
    }
}
