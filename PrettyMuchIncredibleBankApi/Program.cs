using EventFlow.Extensions;
using PMI.Commands;
using PMI.Domain.Commands;
using PMI.Domain.Events;
using PMI.Domain.ReadModels;
using PMI.Queries;
using PMI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEventFlow(ef => ef
    .AddDefaults(typeof(Program).Assembly) // Adds all events, entities etc.
    .UseInMemoryReadStoreFor<AccountReadModel>()
    .UseInMemoryReadStoreFor<LedgerReadModel>()
);
builder.Services.AddSingleton<LedgerSingletonService>();
builder.Services.AddTransient<CommandService>();
builder.Services.AddTransient<QueryService>();

var app = builder.Build();
app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference(cfg =>
{
    cfg.WithTheme(ScalarTheme.Moon)
        .WithTitle("Pretty Much Incredible Bank Api")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.UseHttpsRedirection();
await app.RunAsync();