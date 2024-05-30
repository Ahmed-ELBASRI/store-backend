namespace store.Services.Contract
{
    public interface IMyApiService
    {
        Task<string> GetApiResponseAsync(string endpoint);

    }
}
