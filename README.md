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

## System Workflow Diagram

The following diagram illustrates the main workflows of the Shipping Management System, showing the interactions between clients, administrators, and the system's core features.

```mermaid
flowchart TD
    A["Shipping Management System"] --> B["Client Website"]
    A --> C["Admin Dashboard"]

    %% Client Website
    B --> D["Client Authentication"]
    D --> E["Client Services"]

    E --> F["Create Shipment"]
    E --> G["Track Shipment"]
    E --> H["Manage My Shipments"]
    E --> I["Manage Profile"]
    E --> J["Manage Subscription"]

    F --> K["Enter Shipment Details"]
    K --> L["Calculate Shipping Cost"]
    L --> M["Confirm Shipment"]
    M --> N["Shipment Created"]

    G --> O["Enter Tracking Number"]
    O --> P["View Shipment Status"]

    H --> Q["View Shipment History"]
    H --> R["View Shipment Details"]

    %% Admin Dashboard
    C --> S["Admin Authentication"]
    S --> T["Administration"]

    T --> U["Manage Users"]
    T --> V["Manage Shipments"]
    T --> W["Manage Shipping Types"]
    T --> X["Manage Cities & Countries"]
    T --> Y["Manage Subscriptions"]
    T --> Z["Manage Payments"]
    T --> AA["Manage Shipment Status"]
    T --> AB["Manage System Settings"]

    %% Shared system
    N --> AC[("Shipping Database")]
    P --> AC
    Q --> AC
    U --> AC
    V --> AC
    W --> AC
    X --> AC
    Y --> AC
    Z --> AC
    AA --> AC
    AB --> AC
```

### Main System Modules

| Client Website | Admin Dashboard |
|---|---|
| Register and Login | View and manage all users |
| Create shipments | View and manage all shipments |
| Track shipments | Manage shipment statuses |
| View shipment history | Manage shipping types |
| Manage profile | Manage cities and countries |
| Manage subscriptions | Manage subscription packages |
| View payments | Manage payments and settings |

