# SpaceTracker

**Your Gateway to Space Exploration**

SpaceTracker is an interactive, data-driven space exploration platform built with .NET 8, Blazor, Entity Framework Core, and CesiumJS. It provides real-time tracking of satellites, launches, astronauts, spacecraft, and historical space programs—all powered by multiple live APIs and a locally persisted SQLite database.

---

## Features

### Real-Time Tracking
- Live ISS position tracking
- Satellite positions & orbital paths
- Satellite passes, visibility windows, and observer-based queries
- Real-time launch events, mission details, and countdowns

### Interactive Dashboards
- **3D Dashboard** – Cesium-based globe showing ISS, satellites, launch sites
- **2D Dashboard** – Lightweight map showing next launch sites
- Smooth animations, markers, and overlays

### Astronaut Database
- Full astronaut profiles (SpaceDevs API)
- Biography, missions, agencies, timelines
- Cached locally in SQLite with auto-refresh

### Spacecraft Browser
- Specifications, configuration, mission history
- High-resolution images
- Deep metadata for spacecraft around the world

### Launch Schedules
- Global upcoming launches
- Mission details, windows, providers
- Paginated/bulk view for extended schedules

### Historical Programs Timeline
- Mercury → Gemini → Apollo → Shuttle → ISS → Artemis
- Images, descriptions, agencies, start/end dates
- Beautiful chronological timeline UI

### Raw API Viewer
- Live JSON responses from all services
- Weather, launches, ISS position, satellite position, visual passes
- Useful for debugging, transparency, and development

---

## Tech Stack

### Frontend / UI
- **Blazor Server** (Razor Components)
- **CesiumJS** (3D Globe)
- Custom CSS for all pages

### Backend
- **ASP.NET Core 9**
- **HttpClient Services** for all external APIs
- **Background data caching service**
- **EF Core + SQLite** persistence for:
  - Astronauts
  - Spacecraft
  - Space Programs

### APIs Used
- **N2YO API** – ISS position, satellites, passes
- **SpaceDevs API** – Astronauts, spacecraft, programs
- **RocketLaunchLive API** – Upcoming launches
- **OpenMeteo API** – Weather for ISS observer

---

## Project Architecture

```
spaceTracker/
│
├── Components/
│   ├── Pages/
│   │   ├── Home.razor
│   │   ├── Dashboard3D.razor
│   │   ├── Dashboard2D.razor
│   │   ├── Astronauts.razor
│   │   ├── Spacecraft.razor
│   │   ├── BulkLaunches.razor
│   │   ├── LaunchDetail.razor
│   │   ├── ISS.razor
│   │   ├── RawData.razor
│   │   └── Programs/
│   │       └── Programs.razor
│   └── Shared/...
│
├── Data/
│   ├── SpaceTrackerDbContext.cs
│   ├── Entities/
│   ├── Seeders/
│   └── Migrations/
│
├── Services/
│   ├── weatherService.cs
│   ├── RocketLaunchLiveService.cs
│   ├── n2yoService.cs
│   ├── SpaceDevsService.cs
│   └── CesiumService.cs
│
└── Program.cs (service registration + DB seeding)
```

---

## Key Pages

| Page | Description |
|------|-------------|
| **Home** | A central navigation hub introducing the project and linking to all major features |
| **3D Dashboard** | Live Cesium globe tracking ISS, satellites, launch sites, and orbital paths |
| **2D Dashboard** | Simple 2D map for upcoming launch locations |
| **Astronauts** | Database of astronauts with biographies, missions, and agencies |
| **Spacecraft** | Full spacecraft data including configuration and imagery |
| **Launches / BulkLaunches** | World launch schedule with mission details |
| **Programs** | Historical timeline of major space programs |
| **ISS Page** | Live ISS telemetry, visibility predictions, orbital data |
| **RawData Viewer** | Displays raw JSON from weather, N2YO, RocketLaunchLive, and SpaceDevs APIs |

---

## Database & Seeding

On startup, the app automatically:
- Seeds Space Programs
- Seeds Spacecraft
- Seeds Astronauts

All entities store JSON fields (`StatusJson`, `ImageJson`, `AgenciesJson`, etc.) with computed `[NotMapped]` models for structured access.

---

## Running the Project

### Prerequisites
- .NET 9 SDK
- SQLite installed
- Cesium Ion key (optional for 3D globe)

### Steps

```bash
git clone <repo>
cd spaceTracker
dotnet restore
dotnet run
```

The app automatically:
- Registers all API services
- Builds the SQLite database
- Populates seed data
- Starts the Blazor Server app

**Open your browser at:** `https://localhost:5094`

---

## Screenshots

<h2>Screenshots</h2>
<p align="center">
  <img src="screenshots/2D.png" width="30%" />
  <img src="screenshots/bulklaunches.png" width="30%" />
  <img src="screenshots/buzzAldrin.png" width="30%" />
</p>

<p align="center">
  <img src="screenshots/home.png" width="30%" />
  <img src="screenshots/programs.png" width="30%" />
  <img src="screenshots/rawData.png" width="30%" />
</p>

<p align="center">
  <img src="screenshots/spacecraft.png" width="30%" />
</p>


---

## Contact

**Created by Lead Devolper: Daniel Chavez, Full-Stack Devolper: Anthony Petrosino, and Front-End Devolper: Jackson Wang**  
College of the Holy Cross – Computer Science

For inquiries, improvements, or collaboration, feel free to reach out!