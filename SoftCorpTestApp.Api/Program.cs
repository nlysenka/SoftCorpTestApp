using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;
using SoftCorpTestApp.Core.Services;
using SoftCorpTestApp.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MonitorWorker>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(CoinGeckoConfiguration)).Get<CoinGeckoConfiguration>());

builder.Services.AddScoped<ICoinGeckoIntegration, CoinGeckoIntegration>();
builder.Services.AddSingleton<IWorkerControl, WorkerControl>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Simple monitor of cryptocurrency exchange rates",
        Description = "An test project"

    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseExceptionHandler("/exception");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
