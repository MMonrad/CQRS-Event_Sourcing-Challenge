using EventFlow.Extensions;
using PMI;
using PMI.Commands;
using PMI.Domain.ReadModels;
using PMI.Domain.Subscribers;
using PMI.Queries;
using PMI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEventFlow(ef => ef
    .AddDefaults(typeof(Program).Assembly) // Adds all events, entities etc.
    .UseInMemoryReadStoreFor<AccountReadModel>()
);
builder.Services.AddTransient<CommandService>();
builder.Services.AddTransient<QueryService>();
builder.Services.AddTransient<WebhookService>();
builder.Services.Configure<WebhookOptions>(builder.Configuration.GetSection(nameof(WebhookOptions)));
builder.Services.AddHttpClient();

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