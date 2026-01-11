# LogiTrack 🚚
**Transport & Tracking Microservices Demo – .NET 8**

LogiTrack ist ein Beispielprojekt zur Demonstration moderner **Backend- und Microservice-Architektur**
mit **C# / .NET 8**, **RabbitMQ**, **Docker** und **Kubernetes**.  
Das Projekt wurde speziell als **Portfolio-Projekt für Bewerbungen** entwickelt.

---

## 🎯 Ziel des Projekts
Dieses Projekt zeigt praxisnah:

- Entwicklung von **RESTful APIs** mit ASP.NET Core
- **Event-Driven Architecture** mit RabbitMQ
- Einsatz von **Entity Framework Core** mit relationalen Datenbanken
- **Containerisierung** mit Docker & Docker Compose
- Vorbereitung für **Kubernetes-Deployments**
- **Unit Tests** mit xUnit & Moq
- **CI/CD** mit GitHub Actions
- Clean Code & klare Projektstruktur

---

## 🧱 Architektur-Übersicht

LogiTrack besteht aus mehreren logisch getrennten Services:

### 🔹 TransportService
- REST API zur Verwaltung von Transportaufträgen
- Erstellt neue Transporte
- Publiziert Events (z. B. `TransportCreated`) über RabbitMQ

### 🔹 TrackingService
- Abonniert Transport-Events
- Verarbeitet Tracking-Informationen
- Simuliert Status-Updates

### 🔹 NotificationService (optional / Demo)
- Konsumiert Events
- Simuliert Benachrichtigungen (z. B. E-Mail / Log)

Die Services kommunizieren **asynchron über RabbitMQ**, um lose Kopplung zu gewährleisten.

---

## 🛠 Tech Stack

- **C# / .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **RabbitMQ**
- **MySQL**
- **Docker & Docker Compose**
- **Kubernetes (Manifeste)**
- **xUnit & Moq (Unit Tests)**
- **GitHub Actions (CI/CD)**

---

## 🚀 Quickstart (lokal starten)

### Voraussetzungen
- Docker & Docker Compose
- .NET 8 SDK (optional, wenn lokal entwickelt wird)

### Schritte
git clone https://github.com/Majd-Assaf/LogiTrack.git
cd LogiTrack
docker-compose up --build
API unter http://localhost:8000/swagger testen

## 🔮 Vision & Roadmap

LogiTrack wird schrittweise zu einer **Visibility Platform für Transport- und Logistikprozesse**
weiterentwickelt.

### Geplante Erweiterung: Visibility Platform

Ziel ist der Aufbau einer **zentralen Visibility-Oberfläche**, die über eine
**universelle API** folgende Informationen aggregiert und darstellt:

- 📦 Shipments & Transportaufträge
- 🛣️ Milestones (Status, Events, Fortschritt)
- 📄 Dokumente (z. B. Lieferscheine, Rechnungen)
- ⏱️ Historische Tracking-Daten

### 🔗 Universal API (API-First Ansatz)
Die Backend-Services stellen eine **standardisierte REST API** bereit,
die als zentrale Integrationsschicht dient und unabhängig vom Frontend genutzt werden kann.

### 🖥️ Frontend (Angular – geplant)
Die Visibility Platform wird mit **Angular** umgesetzt und bietet:

- Moderne, responsive Benutzeroberfläche
- Dashboard mit Transport-Status
- Detailansichten für Shipments & Milestones
- Klare Trennung von Backend & Frontend

### 🎯 Ziel
Das Projekt soll eine realistische **Enterprise-nahe Architektur**
abbilden, wie sie in modernen Logistik- und SaaS-Plattformen eingesetzt wird.





