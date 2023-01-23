using SoftCorpTestApp.Core.Interfaces.Infrastructure;
using SoftCorpTestApp.Core.Services;
using SoftCorpTestApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MonitorWorker>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ICoingeckoIntegration, CoingeckoIntegration>();

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
