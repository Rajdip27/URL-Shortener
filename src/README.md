# 🔗 .NET 9 URL Shortener API

[![.NET](https://github.com/yourusername/url-shortener/actions/workflows/dotnet.yml/badge.svg)](https://github.com/yourusername/url-shortener/actions/workflows/dotnet.yml)
[![Docker](https://img.shields.io/docker/pulls/yourusername/url-shortener)](https://hub.docker.com/r/yourusername/url-shortener)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A high-performance, production-ready URL shortening service built with .NET 9, featuring Clean Architecture, CQRS, and Redis caching for optimal performance and scalability.

## 🚀 Features

- **Minimal APIs** - Low overhead, high-performance endpoints
- **Base62 Encoding** - Short, URL-safe codes
- **Redis Caching** - Lightning-fast redirects with reduced database load
- **Click Analytics** - Track visits with IP, User-Agent, and Referrer data
- **Docker Support** - Easy deployment with Docker and Docker Compose
- **Validation** - Robust input validation with FluentValidation
- **Clean Architecture** - Maintainable and testable codebase
- **CQRS Pattern** - Clear separation of commands and queries
- **API Documentation** - Swagger/OpenAPI support
- **Health Checks** - Built-in health monitoring

## 🛠️ Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerization)
- [Redis](https://redis.io/) (can be run via Docker)
- [PostgreSQL](https://www.postgresql.org/) (can be run via Docker)

## 🚀 Getting Started

### Running with Docker (Recommended)

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/url-shortener.git
   cd url-shortener
   ```

2. Start the application with Docker Compose:
   ```bash
   docker-compose up -d
   ```

3. The API will be available at `http://localhost:5000` and Swagger UI at `http://localhost:5000/swagger`

### Running Locally

1. Set up the database:
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/WebUI
   ```

2. Run the application:
   ```bash
   dotnet run --project src/WebUI
   ```

3. The API will be available at `http://localhost:5000`

## 📚 API Endpoints

### Create Short URL
```http
POST /api/urls
Content-Type: application/json

{
    "originalUrl": "https://example.com/very-long-url",
    "customCode": ""  // Optional
}
```

### Redirect to Original URL
```http
GET /{code}
```

### Get URL Analytics
```http
GET /api/urls/{code}/analytics
```

## 🧪 Running Tests

```bash
dotnet test
```

## 🏗️ Project Structure

```
src/
├── Application/          # Application layer (use cases, DTOs, validators)
├── Domain/              # Domain models and interfaces
├── Infrastructure/      # External concerns (database, caching)
└── WebUI/               # Presentation layer (API controllers, middleware)
```

## 📊 Performance

- Average response time: < 10ms for redirects (with Redis cache)
- Supports thousands of requests per second
- Memory-efficient URL storage

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [FluentValidation](https://fluentvalidation.net/)
- [MediatR](https://github.com/jbogard/MediatR)

---

Made with ❤️ by [Your Name] | [GitHub](https://github.com/yourusername)
