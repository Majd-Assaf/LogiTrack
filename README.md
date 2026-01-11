 # LogiTrack (Regenerated) — TransportService variant

Microservice demo project showcasing:
- .NET 8 backend services
- RabbitMQ event-driven flow
- MySQL (EF Core)
- Docker + docker-compose
- Basic Kubernetes manifests
- Unit tests with xUnit & Moq
- GitHub Actions CI

## Services
- TransportService (REST API) — creates transports and publishes events
- TrackingService (Worker) — consumes transport.created events
- NotificationService (Worker) — simulates sending notifications

## Quickstart (local)
1. Install Docker & Docker Compose
2. Build & start:
   ```bash
   docker-compose up --build

