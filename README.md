# ShippingSystem-CleanArchitecture
## Architecture Diagram

The following diagram illustrates the system's five-tier architecture and the flow of dependencies between its layers.

```mermaid
flowchart TD
    User["User / Admin"]

    UI["UI Layer<br/>Admin Dashboard & MVC"]
    API["API Layer<br/>RESTful Web APIs"]

    BL["Business Logic Layer (BL)<br/>Services, DTOs & Business Rules"]

    DAL["Data Access Layer (DAL)<br/>Repositories & Entity Framework Core"]

    DOMAIN["Domain Layer<br/>Entities, Interfaces & Base Classes"]

    DB[("SQL Server Database")]

    User --> UI
    User --> API

    UI --> BL
    API --> BL

    BL --> DOMAIN
    BL --> DAL

    DAL --> DOMAIN
    DAL --> DB
```

### Main Layers

| Layer | Responsibility |
|---|---|
| UI | Admin dashboard and user interface. |
| API | Exposes RESTful endpoints for clients. |
| BL | Handles business logic, services, and DTOs. |
| DAL | Handles database operations using repositories and Entity Framework Core. |
| Domain | Contains core entities, interfaces, and shared abstractions. |
