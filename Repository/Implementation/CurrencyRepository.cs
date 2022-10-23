using Microsoft.Extensions.DependencyInjection;
using Repository.Abstraction;
using Repository.Entity;

namespace Repository.Implementation
{
    public class CurrencyRepository : GenericRepository<Currency>,ICurrencyRepository
    {
        private readonly IServiceScopeFactory serviceScope;

        public CurrencyRepository(IServiceScopeFactory serviceScope) :base(serviceScope)
        {
            this.serviceScope = serviceScope;
        }
    }
}
