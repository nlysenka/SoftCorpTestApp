namespace SoftCorpTestApp.Core.Interfaces.Infrastructure
{
    public interface ICoingeckoIntegration
    {
        Task<string> GetTrendingAsync();
    }
}
