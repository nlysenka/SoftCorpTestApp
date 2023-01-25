using SoftCorpTestApp.Core.Configuration;
using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Interfaces.Services;
using SoftCorpTestApp.Core.Services;
using SoftCorpTestApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MonitorWorker>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(CoinGeckoConfiguration)).Get<CoinGeckoConfiguration>());

builder.Services.AddSingleton<ICoinGeckoIntegration, CoinGeckoIntegration>();
builder.Services.AddSingleton<IWorkerControl, WorkerControl>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
