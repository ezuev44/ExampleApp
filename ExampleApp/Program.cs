using Entities.ProviderOne;
using Entities.ProviderTwo;
using ExampleApp.Services;
using Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IProviderService<ProviderOneSearchResponse, ProviderOneSearchRequest>, CallApiProviderOneService>(x =>
{
    var uri = "http://localhost:5079";
    x.BaseAddress = new Uri(uri);
    x.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddHttpClient<IProviderService<ProviderTwoSearchResponse, ProviderTwoSearchRequest>, CallApiProviderTwoService>(x =>
{
    var uri = "http://localhost:5202";
    x.BaseAddress = new Uri(uri);
    x.Timeout = TimeSpan.FromSeconds(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
