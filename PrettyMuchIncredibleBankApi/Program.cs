using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();


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