# TaskManager Application

## 📌 Overview

**TaskManager** is a simple full-stack web application designed to manage daily tasks using a clean separation of concerns:

- ✅ A **.NET Core Web API** project to handle data persistence and RESTful operations.
- 🎨 A separate **ASP.NET Core MVC UI** project to interact with users and consume the API.

### ✅ Task API (TaskManager.API)
- Create, Read, Update, Delete (CRUD) tasks.
- RESTful design returning JSON responses.
- Validates input data (e.g., title is required).
- Uses `InMemory` database via **Entity Framework Core** for simplicity.

### 🎨 Task UI (TaskManager.UI)
- Clean user interface using MVC pattern.
- Consumes API endpoints to manage tasks.
- API URL is configured in `appsettings.json` and injected using `HttpClientFactory`.

---

## 🔧 Setup Instructions
API will be hosted at https://localhost:7271
Open TaskManager.UI project.

Open appsettings.json and set the API base URL:
{
  "TaskApi": {
    "BaseUrl": "https://localhost:7271"
  }
}

## cmd incase of trust issue while runnig application 
dotnet dev-certs https --trust

## packages used to build this application
newtonsoft.json
microsoft.entityframeworkcore.inmemory
pomelo.entityframeworkcore.mysql
