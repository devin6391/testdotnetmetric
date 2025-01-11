# ContosoPizza

ContosoPizza is a simple ASP.NET Core Web API project for managing pizzas. It includes endpoints for creating, reading, updating, and deleting pizzas.

## Project Structure

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Running the Application

1. Clone the repository:

    ```sh
    git clone https://github.com/yourusername/ContosoPizza.git
    cd ContosoPizza
    ```

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

-  - General application settings.
-  - Development-specific settings.
-  - Configuration for launching the application.

## License

This project is licensed under the MIT License.