# SpaceTracker

**Your Gateway to Space Exploration**

SpaceTracker is an interactive, data-driven space exploration platform built with .NET 8, Blazor, Entity Framework Core, and CesiumJS. It provides real-time tracking of satellites, launches, astronauts, spacecraft, and historical space programs—all powered by multiple live APIs and a locally persisted SQLite database.

---
### APIs Used
- **N2YO API** – ISS position, satellites, passes
- **SpaceDevs API** – Astronauts, spacecraft, programs
- **RocketLaunchLive API** – Upcoming launches
- **OpenMeteo API** – Weather for ISS observer
- **Space News API** – Space industry news and articles

---
## Project Architecture EXPLAINED
spaceTracker/
├── BackgroundServices/        -> Hosted services (e.g., auto-refreshing cached API data)
│
├── Components/               -> All Blazor UI or webpage components
│   ├── Layout/               -> Shared layouts + navigation (global UI structure)
│   ├── Pages/                -> Actual app pages (ISS, Launches, Astronauts, etc.)
│   ├── Routes.razor          -> Defines app routing
│   └── _Imports.razor        -> Global using statements for Razor components
│
├── Data/                     -> Database layer
│   ├── Entities/             -> EF Core database models (tables)
│   ├── Seeders/              -> Initial DB population code
│   └── SpaceTrackerDbContext -> EF Core context + DB config
│
├── Models/                   -> C# models for API responses + structured data
│
├── Services/                 -> API service layer (N2YO, SpaceDevs, Weather, Launches, SpaceNews)
│                             -> Handles all external data fetching + validation
│
├── wwwroot/                  -> Static frontend assets (CSS, JS interop, images)
│
├── Program.cs                -> App bootstrap + dependency injection setup
├── spaceTracker.csproj       -> Project configuration
├── spaceTracker.db           -> Local SQLite database
└── README.md                 -> Project documentation


## PROJECT FILES EXPLAINED AND HOW MUCH WAS AI GENERATED AND WHO CREATED THEM
```
spaceTracker/
├── BackgroundServices - 
│   └── CacheRefresherService.cs
├── Components
│   ├── App.razor - (AUTO GENERATED)
│   ├── Layout
│   │   ├── MainLayout.razor - (Anthony: 100% - basic style outline for all frontend files)
│   │   ├── MainLayout.razor.css - (AI: 100%)
│   │   ├── NavMenu.razor - (AI: 30% - basic structure ideas, Anthony: 70% - navigation menu implementation)
│   │   └── NavMenu.razor.css - (AI: 100%)
│   ├── Pages
│   │   ├── Astronauts.razor (AI: 30% — search bar, Other: 70% — Loading, database search, card UI)
│   │   ├── Astronauts.razor.css - (AI: 100%)
│   │   ├── BulkLaunches.razor (AI 20% — cleanup, bug fixes, Other: 80% — API fetching, UI, fallback images)
│   │   ├── BulkLaunches.razor.cs (AI 10% — cleanup, bug fixes, Other: 90% Visual aid, gradiants, card loading)
│   │   ├── BulkLaunches.razor.css - (AI: 100%)
│   │   ├── Dasboard2D.razor - (AI: 20% - basic structure ideas/bug fixes, Anthony: 80% - 2D map dashboard with launch locations)
│   │   ├── Dasboard2D.razor.css - (AI: 100%)
│   │   ├── Dashboard3D.razor - (AI: 20% - bug fixes, Anthony: 70% - implemented 3D dashboard with cesium, Daniel: 10% - helped with ISS live update)
│   │   ├── Dashboard3D.razor.css - (AI: 100%)
│   │   ├── Home.razor - (AI: 10% - basic structure ideas, Anthony: 90% - navigation page with info)
│   │   ├── Home.razor.css - (AI: 100%)
│   │   ├── ISS.razor - (AI: 30%, Daniel: 70% — Live ISS tracking UI + async updates)
│   │   ├── ISS.razor.css - (AI: 100%)
│   │   ├── LaunchDetail.razor (AI 20% — cleanup, bug fixes, Other: 80% — Timer, coordinates, UI functionality)
│   │   ├── LaunchDetail.razor.css - (AI: 100%)
│   │   ├── Programs
│   │   │   ├── Programs.razor - (AI: 70%, Daniel: 30% — Program list display + UI layout)
│   │   │   ├── Programs.razor.cs - (AI: 20%, Daniel: 80% — Sorting, loading, filtering logic)
│   │   │   └── Programs.razor.css - (AI: 100%)
│   │   ├── RawData.razor (AI: 100%)
│   │   ├── SpaceNews.razor - (AI: 10%, Anthony: 90% - space news feed and article display)
│   │   ├── SpaceNews.razor.css - (AI: 100%)
│   │   ├── Spacecraft.razor - (AI: 30%, Daniel: 70% — Spacecraft data display and detail UI)
│   │   └── Spacecraft.razor.css - (AI: 100%)
│   ├── Routes.razor
│   └── _Imports.razor 
├── Data
│   ├── Entities
│   │   ├── AstronautEntity.cs - (AI: 20%, Daniel: 80% — DB model mapping astronaut fields)
│   │   ├── SpaceProgramEntity.cs - (AI: 20%, Daniel: 80% — DB structure for space programs)
│   │   └── SpacecraftEntity.cs - (AI: 20%, Daniel: 80% — DB entity for spacecraft records)
│   ├── Seeders
│   │   ├── AstronautSeeder.cs - (AI: 90%, Daniel: 10% — Seeds astronaut data into DB)
│   │   ├── SpaceProgramSeeder.cs - (AI: 90%, Daniel: 10% — Seeds program data into DB)
│   │   └── SpacecraftSeeder.cs - (AI: 90%, Daniel: 10% — Seeds spacecraft entries into DB)
│   └── SpaceTrackerDbContext.cs - (Daniel: 100% — Controls DB sets + EF Core configuration)
├── Models
│   ├── n2yoModel.cs - (AI: 50%, Daniel: 50% — ISS/N2YO API response model)
│   ├── openMeteoModel.cs - (Daniel: 100% — Weather API model from OpenMeteo)
│   ├── rocketLiveModel.cs - (Daniel: 100% — Rocket launch API data model)
│   ├── spaceDevsModel.cs - (AI: 80%, Daniel: 20% — SpaceDevs launches/programs model)
│   └── spaceNewsModel.cs - (AI: 20% - basic structure ideas, Anthony: 80% - space news API response models)
├── Program.cs - (AUTOGENERATED: 50%, Daniel: 50% - Start point for the entire project)
├── README.md
├── Services
│   ├── cesiumService.cs - (AI: 20% - basic structure ideas/bug fixes, Anthony: 80% - cesium globe integration service)
│   ├── n2yoService.cs - (AI: 40%, Daniel: 60% — Fetches ISS location + satellite data)
│   ├── openMeteoService.cs - (Daniel: 100% — Handles weather API calls)
│   ├── rocketLaunchLiveService.cs - (Daniel: 100% — Retrieves live + upcoming rocket launches)
│   ├── spaceDevsService.cs - (AI: 50%, Daniel: 50% — Loads launches, spacecraft, and program data)
│   └── spaceNewsService.cs - (AI: 20% - basic structure ideas, Anthony: 80% - space news API integration)
├── spaceTracker.csproj - (AUTO GENERATED)
├── spaceTracker.db - (AUTO GENERATED)
└── wwwroot
    ├── ISS_spacecraft_model_1.png
    ├── app.css - (AUTO GENERATED)
    ├── cesium-interop.js - (AI: 50% - JS help, Anthony: 50% - JS interop for cesium globe)
    ├── favicon.png - (AUTO GENERATED)
    └── world_map.jpg
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
| **SpaceNews** | Latest space industry news and articles |
| **ISS Page** | Live ISS telemetry, visibility predictions, orbital data |
| **RawData Viewer** | Displays raw JSON from weather, N2YO, RocketLaunchLive, and SpaceDevs APIs |

---

## Contact
**Created by Lead Devolper: Daniel Chavez, Full-Stack Devolper: Anthony Petrosino, and Front-End Devolper: Jackson Wang**  


