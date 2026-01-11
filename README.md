# LogiTrack
Transport Service Microservices Demo (C# .NET 8)

Ein Beispiel-Projekt zur Demonstration moderner Backend-Technologien mit .NET,
RabbitMQ, Docker, Kubernetes und Continuous Integration.


Dieses Projekt zeigt:
- Event-Driven Architektur mit RabbitMQ
- REST API Services mit .NET
- Datenzugriff mit Entity Framework
- Testing mit xUnit & Moq
- Containerisierung (Docker / Kubernetes)
- .NET 8
- C#
- Entity Framework Core
- RabbitMQ
- MySQL
- Docker & Docker Compose
- Kubernetes (optional)
- GitHub Actions (CI/CD)
- xUnit, Moq (Tests)

## Quickstart

1. Docker installieren  
2. Repository klonen  
3. `docker-compose up --build`  
4. API unter http://localhost:8000 testen


## Services

### TransportService
- REST API für Transportanforderungen
- Publiziert `TransportCreated`

### TrackingService
- Listens auf `TransportCreated` events
- Verarbeitet Logik …

### NotificationService
- Simuliert Notification Delivery



