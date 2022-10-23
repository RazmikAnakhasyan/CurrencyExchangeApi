using Shared.Models.Currency;

namespace Service.Abstraction
{
    public interface ICurrencyService
    {
        Task<CurrencyConvertResponse> ConvertAsync(CurrencyConvertModel model);
        Task<UpdateCurrenciesResponse> GetAndUpdateCurrencies();
        Task<OneToManyResponseModel> OneToManyConvert(OneToManyConvertModel model);
    }
}
