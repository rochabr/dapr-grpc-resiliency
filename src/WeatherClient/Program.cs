using Dapr.Client;
using WeatherService;

using var daprClient = new DaprClientBuilder()
    .UseGrpcEndpoint("http://localhost:50001")
    .Build();

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

while (!cts.Token.IsCancellationRequested)
{
    try
    {
        Console.WriteLine("\nRequesting weather forecast...");
        
        var request = new WeatherRequest { Location = "Seattle" };
        var response = await daprClient.InvokeMethodGrpcAsync<WeatherRequest, WeatherResponse>(
            "weatherservice",
            "GetWeather",
            request,
            cancellationToken: cts.Token);

        Console.WriteLine($"Temperature: {response.Temperature}°C");
        Console.WriteLine($"Conditions: {response.Conditions}");
    }
    catch (Dapr.DaprException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    await Task.Delay(2000, cts.Token);
}