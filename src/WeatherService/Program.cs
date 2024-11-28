using WeatherService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGrpcService<WeatherForecastService>();
app.MapSubscribeHandler();

app.Run();
