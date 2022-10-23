using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.Models.Currency;

namespace CurrencyExchangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }
        [HttpGet]
        public async Task<UpdateCurrenciesResponse> GetAllCurrencies()
        {
            return await currencyService.GetAndUpdateCurrencies();
        }

        [HttpGet(nameof(Convert))]
        public async Task<CurrencyConvertResponse> Convert([FromQuery]CurrencyConvertModel model)
        {
            return await currencyService.ConvertAsync(model);
        }

        [HttpGet(nameof(OneToManyConvert))]
        public async Task<OneToManyResponseModel> OneToManyConvert([FromQuery]OneToManyConvertModel model)
        {
            return await currencyService.OneToManyConvert(model);
        }
    }
}
