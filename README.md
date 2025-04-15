# Hello World Minimal API

A simple .NET Minimal API project that demonstrates a basic web API setup with multiple endpoints.

## Description

This project is a minimal .NET web API that provides different ways to get a "Hello World" response. It's built using the .NET Minimal API approach, which provides a lightweight way to create HTTP APIs with minimal code.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0 or later)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)

## Getting Started

### Running the Application

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/HelloWorldMinimalApiRailway.git
   cd HelloWorldMinimalApiRailway
   ```

2. Run the application:
   ```
   dotnet run
   ```

3. The API will be available at:
   ```
   http://localhost:5000
   ```

### Running with Docker

1. Build the Docker image:
   ```
   docker build -t minimalapi .
   ```

2. Run the container:
   ```
   docker run -p 5000:5000 minimalapi
   ```

3. Access the API at:
   ```
   http://localhost:5000
   ```

## API Endpoints

- `GET /`: Returns "Hello World!" as plain text
- `GET /json`: Returns a JSON response `{ "message": "Hello World!" }`
- `GET /json/{name}`: Returns a personalized JSON response `{ "message": "Hello {name}!" }`

### Example Responses

1. Plain text response:
   ```
   Hello World!
   ```

2. JSON response:
   ```json
   {
     "message": "Hello World!"
   }
   ```

3. Personalized JSON response:
   ```json
   {
     "message": "Hello John!"
   }
   ```

## Project Structure

- `Program.cs`: Contains the main application code and API endpoint definitions
- `Dockerfile`: Contains the Docker configuration for containerized deployment
- `appsettings.json`: Contains application configuration settings
- `appsettings.Development.json`: Contains development-specific settings

## Deployment

This project is configured for deployment on Railway. The necessary configuration files are included in the repository.

## License

This project is licensed under the MIT License - see the LICENSE file for details. 