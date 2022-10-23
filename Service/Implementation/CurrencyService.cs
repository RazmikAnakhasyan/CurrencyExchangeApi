using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository.Abstraction;
using Repository.Entity;
using Service.Abstraction;
using Shared.HttpHelper.Implementation;
using Shared.Models.Currency;
using System;
using System.Reflection;

namespace Service.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly string apiKey;
        private readonly string baseUrl;
        private readonly Dictionary<string, string> header;
        public CurrencyService(ICurrencyRepository currencyRepository, IConfiguration configuration, IMapper mapper)
        {
            this.currencyRepository = currencyRepository;
            this.configuration = configuration;
            apiKey = configuration["CurrencyApi:ApiKey"];
            baseUrl = configuration["CurrencyApi:BaseUrl"];
            header = new Dictionary<string, string>() { [nameof(apiKey)] = apiKey };
            this.mapper = mapper;
        }

        public async Task<UpdateCurrenciesResponse> GetAndUpdateCurrencies()
        {
            var actionLink = configuration["CurrencyApi:Symbols"];
            var request = await HttpClientHelper<object, CurrenciesListModel>.GetRequestAsync(
                                 baseUrl: baseUrl,
                                 actionLink: actionLink,
                                 headers: header);
            var result = new UpdateCurrenciesResponse();
            foreach (var symbol in request.symbols)
            {
                if (currencyRepository.GetAsync(x => x.IsoCode == symbol.Key).Result != null)
                {
                    result.ExistingCurrenciesCount++;
                }
                else
                {
                    await currencyRepository.AddAsync(new Currency
                    {
                        Id = 0,
                        IsoCode = symbol.Key,
                        Name = symbol.Value
                    });
                    result.UpdatedCurrenciesCount++;
                }
            }
            return result;
        }

        public async Task<CurrencyConvertResponse> ConvertAsync(CurrencyConvertModel model)
        {
            var actionLink = configuration["CurrencyApi:Convert"];
            return await HttpClientHelper<CurrencyConvertModel, CurrencyConvertResponse>.GetRequestAsync(
                                 baseUrl: baseUrl,
                                 actionLink: actionLink,
                                 requestModel: model,
                                 headers: header);
        }

        public async Task<OneToManyResponseModel> OneToManyConvert(OneToManyConvertModel model)
        {
            var actionLink = configuration["CurrencyApi:Latest"];
            var response = await HttpClientHelper<OneToManyConvertModel, OneToManyResponseModel>.GetRequestAsync(
            baseUrl: baseUrl,
            actionLink: actionLink,
            requestModel: model,
            headers: header);

            return response;
        }
    }
}
