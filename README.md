# Dapr gRPC Demo with Resiliency Patterns

This demo showcases Dapr service invocation using gRPC with built-in resiliency patterns including retries, circuit breakers, and timeouts.

## Prerequisites

- .NET 7.0 or later
- Dapr CLI
- Docker

## Project Structure

- `src/WeatherService`: gRPC service providing weather forecasts
- `src/WeatherClient`: Console application consuming the weather service
- `dapr/config`: Dapr configuration files
- `dapr/components`: Dapr component definitions

## Running the Demo

1. Start the Weather Service:
```bash
cd src/WeatherService
dapr run --app-id weatherservice --app-port 5000 --app-protocol grpc --resources-path ../../components -- dotnet run
```

2. Start the Weather Client:
```bash
cd src/WeatherClient
dapr run --app-id weatherclient --resources-path ../../components -- dotnet run
```

## Resiliency Patterns

The demo implements three resiliency patterns:

- Retries with exponential backoff
- Circuit breaker
- Timeouts

Configure these patterns in `components/resiliency.yaml`.