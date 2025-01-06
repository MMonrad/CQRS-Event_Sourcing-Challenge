using EventFlow.Extensions;
using PMI.Commands;
using PMI.Commands.CommandHandlers;
using PMI.Domain;
using PMI.Domain.Commands;
using PMI.Domain.Events;
using PMI.Domain.ReadModels;
using PMI.Queries;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEventFlow(ef => ef
    .AddEvents(typeof(CreatedAccountEvent), typeof(DepositedMoneyEvent))
    .AddCommands(typeof(CreateAccountCommand))
    .AddCommandHandlers(typeof(CreateAccountCommandHandler))
    .UseInMemoryReadStoreFor<AccountReadModel>()
);

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