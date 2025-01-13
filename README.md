# ContosoPizza

ContosoPizza is a simple ASP.NET Core Web API project for managing pizzas. It includes endpoints for creating, reading, updating, and deleting pizzas.

## Project Structure

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Running the Application

1. Clone the repository:

  `
    git clone https://github.com/yourusername/ContosoPizza.git
    cd ContosoPizza
    `

2. Restore the dependencies:

    ```sh
    dotnet restore
    ```

3. Run the application:

    ```sh
    dotnet run
    ```

4. The application will start and be accessible at `https://localhost:7004` (HTTPS) or `http://localhost:5191` (HTTP).

### API Endpoints

- **WeatherForecast**
  - `GET /weatherforecast/` - Retrieves a list of weather forecasts.

- **Pizza**
  - `GET /pizza/` - Retrieves a list of all pizzas.
  - `GET /pizza/{id}` - Retrieves a specific pizza by ID.
  - `POST /pizza/` - Creates a new pizza.
  - `PUT /pizza/{id}` - Updates an existing pizza.
  - `DELETE /pizza/{id}` - Deletes a pizza by ID.

### Swagger

Swagger is configured for API documentation and can be accessed at `https://localhost:7004/swagger` or `http://localhost:5191/swagger`.

### Project Configuration

-  General application settings.
-  Development-specific settings.
-  Configuration for launching the application.

## Setting Up OpenTelemetry for Prometheus

To set up and export OpenTelemetry metrics to Prometheus, follow these steps:

1. **Install Required Packages**:
  Ensure you have the necessary OpenTelemetry packages installed. Run the following commands to add the required packages:

   ```
    <PackageReference Include="OpenTelemetry" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Exporter.Console" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Exporter.OpenTelemetryProtocol" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Exporter.Prometheus.AspNetCore" Version="1.9.0-beta.2" />
    <PackageReference Include="OpenTelemetry.Extensions.Hosting" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Instrumentation.AspNetCore" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Instrumentation.Http" Version="1.9.0" />
    <PackageReference Include="OpenTelemetry.Instrumentation.Runtime" Version="1.9.0" />
    ```
2. **Configure OpenTelemetry in Program.cs**:
  Update your Program.cs file to configure OpenTelemetry and export metrics to Prometheus. Ensure the following changes are made:
  * Add the necessary using directives at the top of the file:
    ```
    using OpenTelemetry.Metrics;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
      ```
  * Register the InstrumentationService:
  `builder.Services.AddSingleton<InstrumentationService>();`
  * Configure OpenTelemetry for tracing and metrics:
  Please check the code around line 22. Feel free to ignore trace setup.
  * Expose the Prometheus scraping endpoint:
  `app.UseOpenTelemetryPrometheusScrapingEndpoint(); // Expose the Prometheus scraping endpoint at /metrics`

3. **Verify Prometheus Metrics Endpoint**:
  Run your application using the following command:
  
    `dotnet run --launch-profile https`

    Access the Prometheus metrics endpoint at https://localhost:7004/metrics or http://localhost:5191/metrics to verify that metrics are being collected and exported.

## License

This project is licensed under the MIT License.