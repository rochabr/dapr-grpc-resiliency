using Grpc.Core;

namespace WeatherService.Services;

public class WeatherForecastService : WeatherForecast.WeatherForecastBase
{
    private readonly Random _random = new();
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public override Task<WeatherResponse> GetWeather(WeatherRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Received weather request for location: {Location}", request.Location);

        // Simulate random failures
        if (_random.NextDouble() < 0.3)
        {
            _logger.LogWarning("Simulating service failure");
            throw new RpcException(new Status(StatusCode.Internal, "Random service failure"));
        }

        // Simulate random delays
        if (_random.NextDouble() < 0.3)
        {
            _logger.LogWarning("Simulating service delay");
            Thread.Sleep(2000);
        }

        var response = new WeatherResponse
        {
            Temperature = _random.Next(-10, 35),
            Conditions = new[] { "Sunny", "Cloudy", "Rainy", "Snowy" }[_random.Next(4)]
        };

        _logger.LogInformation("Returning weather: {Temperature}°C, {Conditions}", 
            response.Temperature, response.Conditions);

        return Task.FromResult(response);
    }
}