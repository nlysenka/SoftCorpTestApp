namespace SoftCorpTestApp.Core.Interfaces.Services
{
    public interface IConverterService
    {
        Task<decimal> ConvertCoinToCurrency(decimal sum, string coin, string currency);
    }
}
