# 🚀 PlatformService (ASP.NET Core + gRPC + Kubernetes)

This project is a **microservice built with ASP.NET Core (.NET 9)** that provides REST and gRPC APIs for managing platforms.  
It’s fully containerized with **Docker**, orchestrated via **Kubernetes**, and integrated with **SQL Server** and **RabbitMQ** for messaging.

---

## 🧠 Features

- RESTful API endpoints (HTTP)
- gRPC service (on port 666)
- SQL Server backend (via Kubernetes)
- RabbitMQ message broker for event-based communication
- Dockerized and deployable to Azure or local Kubernetes clusters
- Configurable Ingress with NGINX

---

## ⚙️ Tech Stack

| Component | Technology |
|------------|-------------|
| Framework | .NET 9 (ASP.NET Core) |
| Communication | HTTP + gRPC |
| Database | Microsoft SQL Server (Express) |
| Messaging | RabbitMQ |
| Orchestration | Kubernetes (YAML manifests) |
| Reverse Proxy | NGINX Ingress Controller |
| Cloud | Azure Container Instances / Kubernetes Service |
| Containerization | Docker |

---

## 🧩 Project Structure

PlatformService/
│
├── Dockerfile
├── .dockerignore
├── .gitignore
├── README.md
├── kubernetes/
│ ├── platforms-depl.yaml
│ ├── mssql-depl.yaml
│ ├── rabbitmq-depl.yaml
│ ├── ingress-srv.yaml
│ └── ...
├── Controllers/
│ └── PlatformsController.cs
├── Protos/
│ └── platforms.proto
├── Services/
│ └── GrpcPlatformService.cs
└── Program.cs