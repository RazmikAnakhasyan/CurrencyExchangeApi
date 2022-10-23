using Repository;
using Repository.Abstraction;
using Repository.Implementation;
using Service.Abstraction;
using Service.Implementation;
using Service.Profile;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<CurrencyExchangeContext>();
#region Dependency Injection
//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

builder.Services.AddAutoMapper(typeof(CurrencyProfile));
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
