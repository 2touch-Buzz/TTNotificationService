# TTNotificationService

TTNotificationService is an ASP.NET Core 8.0 Web API service for handling email and text message notifications. This service provides a scalable foundation with comprehensive logging, error handling, and correlation tracking for notification delivery.

## Overview

This service is built under the `Xenios.TTNotificationService` namespace and provides stubbed endpoints for email and text message notifications. The current implementation returns "Not Implemented" responses (HTTP 501) as full notification functionality will be added in future iterations.

## Features

- **ASP.NET Core 8.0 Web API** with modern C# language features
- **Swagger/OpenAPI documentation** (available in development environment)
- **Structured logging with Serilog**
  - Daily rolling log files with 7-day retention
  - Console and file output
  - JSON structured logging
- **Correlation ID middleware** for end-to-end request traceability
- **Exception handling middleware** with standardized error responses
- **Response wrapping** with consistent `ApiResponse<T>` format
- **Unit testing** with xUnit framework

## Project Structure

```
TTNotificationService/
├── Xenios.TTNotificationService.sln          # Solution file
├── TTNotificationService/                     # Main web service project
│   ├── Controllers/
│   │   └── NotificationController.cs         # API endpoints
│   ├── Middleware/
│   │   ├── CorrelationIdMiddleware.cs        # Request correlation tracking
│   │   └── ExceptionHandlingMiddleware.cs    # Global exception handling
│   ├── Models/
│   │   ├── ApiResponse.cs                    # Standard response wrapper
│   │   ├── SendEmailRequest.cs               # Email request model
│   │   └── SendTextMessageRequest.cs         # Text message request model
│   ├── Program.cs                            # Application startup
│   ├── appsettings.json                      # Configuration (with Serilog)
│   └── TTNotificationService.csproj          # Project file
└── Xenios.TTNotificationService.Tests/       # Unit test project
    ├── NotificationControllerTests.cs        # Controller tests
    ├── CorrelationIdMiddlewareTests.cs       # Middleware tests
    └── Xenios.TTNotificationService.Tests.csproj
```

## Build and Run Instructions

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 (optional, but recommended)

### Building the Solution
```bash
# Clone the repository
git clone <repository-url>
cd TTNotificationService

# Restore packages and build
dotnet restore
dotnet build
```

### Running the Service
```bash
# Run in development mode
dotnet run --project TTNotificationService

# Or using Visual Studio: Set TTNotificationService as startup project and press F5
```

The service will start and be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### Swagger Documentation
When running in development mode, Swagger UI is available at:
- `https://localhost:5001/swagger`

## API Endpoints

### POST /notification/email
Sends an email notification (currently stubbed).

**Request Body:**
```json
{
  "to": "recipient@example.com",
  "subject": "Email Subject",
  "body": "Email body content"
}
```

**Response:** HTTP 501 Not Implemented
```json
{
  "success": false,
  "message": "Not Implemented",
  "data": null
}
```

### POST /notification/text
Sends a text message notification (currently stubbed).

**Request Body:**
```json
{
  "phoneNumber": "+1234567890",
  "message": "Text message content"
}
```

**Response:** HTTP 501 Not Implemented
```json
{
  "success": false,
  "message": "Not Implemented",
  "data": null
}
```

## Testing

### Running Unit Tests
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run tests for specific project
dotnet test Xenios.TTNotificationService.Tests
```

### Test Coverage
Current test coverage includes:
- Controller endpoint behavior validation
- Correlation ID middleware functionality
- Request logging verification
- Response format validation

## Logging Configuration

### Serilog Setup
The service uses Serilog for structured logging with the following configuration:

**Log Levels:**
- Default: Information
- ASP.NET Core: Warning
- System: Warning

**Log Outputs:**
- Console (for development)
- Rolling files in `/logs` directory
  - File pattern: `log-yyyyMMdd.txt`
  - Retention: 7 days
  - Format: Compact JSON

**Log Enrichment:**
- Correlation ID (from middleware)
- Machine name
- Thread ID
- Log context properties

### Correlation ID Tracking
Each HTTP request receives a correlation ID for end-to-end traceability:

- **Incoming requests:** Checks for `X-Correlation-ID` header
- **ID generation:** Creates new GUID if none provided or invalid
- **Response headers:** Returns correlation ID in `X-Correlation-ID` header
- **Logging:** Includes correlation ID in all log entries during request processing

## Configuration

### appsettings.json Structure
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"]
  },
  "AllowedHosts": "*"
}
```

## Future Enhancements

This initial scaffold provides the foundation for:
- Email service integration (SMTP, SendGrid, etc.)
- SMS service integration (Twilio, Azure Communication Services, etc.)
- Authentication and authorization
- Rate limiting and throttling
- Message queuing and retry logic
- Delivery status tracking
- Template management

## Development Notes

- **C# Language Features:** Uses explicit types and required braces for all control statements
- **Namespace Convention:** All code is under `Xenios.TTNotificationService`
- **File Organization:** One class per .cs file
- **Testing Framework:** xUnit with Moq for mocking
- **Error Handling:** Global exception middleware with standardized responses