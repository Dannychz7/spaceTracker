Collaboration log and work summary
Anthony Petrosino

**Nav Menu and Homepage** 
(NavMenu.razor, NavMenu.razor.css, Home.razor, Home.razor.css)

    I made the homepage to be a hub with info on all SpaceTracker features. It has interactive cards linking to dashboards, astronauts, spacecraft, launches, space news and programs.

    The main navigation bar is visible across all pages, displaying a dropdown menu system with four categories (Dashboards, Explore, Events, Site Info) using Blazor's NavLink component for client side routing. I used AI to help me make custom CSS for dropdown styling and a more responsive design to enable smooth navigation without page reloads.

**Space News** 
(spaceNewsModel.cs, spaceNewsService.cs, SpaceNews.razor, SpaceNews.razor.css)

    The space news webpage displays paginated space news articles from multiple sources using a three-layer architecture that is used throughout our project: spaceNewsModel.cs defines the data structure for articles, spaceNewsService.cs handles API calls to fetch and process the data, and SpaceNews.razor renders the webpage with a scrolling news source banner and the option to load additional articles. This pattern helped me understand the difference between data models, logic services, and presentation components in Blazor. I consulted documentation from SNAPI (Space News API) to better understand how to interact with it's endpoints.

*** 2d Dashboard ***
(Dasboard2D.razor, Dasboard2D.razor.css)

    The 2D Dashboard shows upcoming launch locations on an interactive world map with live weather data. The goal was to show the next 5 launches as clickable markers on a world map image using their coordinates. Users are able to select different launches to view detailed mission information and current weather conditions at each launch site. It integrates SpaceDevsService for launch data and weatherService for weather info, providing a lightweight alternative to the 3D dashboard. I also did this page before 3D dashboard and it helped me get an idea as to how to go about implementing it.

*** 3d Dashboard ***
(Dashboard3D.razor, Dashboard3D.razor.css, cesiumService.cs, cesium-interop.js)

    The 3D Dashboard is an interactive Cesium globe displaying live ISS tracking and upcoming launch sites. It features an interactive 3D Earth where users can click on launch markers to view detailed mission information and you can watch the ISS move across the globe every 5 seconds. The implementation required creating a C# service wrapper (cesiumService.cs) to handle JavaScript interop with the Cesium library, along with a custom JavaScript module (cesium-interop.js) for direct globe manipulation. This was the more technically challenging than the 2D dashboard as it involved coordinating between C#, JavaScript, and external mapping APIs.

The vast majority of my css came from my built-in AI editor in my IDE, Amazon Q, which greatly helped me focus on the C# aspects of my project rather than styling the frontend. I also used Amazon Q to aid in small technical problems and to provide poc ideas as to how to go about the project.



Annotaded Bibliography


C# Essential Training 1: Types and Control Flow
https://www.linkedin.com/learning/c-sharp-essential-training-1-types-and-control-flow/explore-the-essentials?

This LinkedIn learning course helped me develop my basic understanding of C#. I refered to it throughout the project whenever I needed some more info about C#.


ASP.NET Overview
https://learn.microsoft.com/en-us/aspnet/overview

I used this comprehensive documentation to understand ASP.NET architecture and Blazor Server fundamentals. Throughout the project, I referred to specific sections covering dependency injection, service registration, and Razor component lifecycle management to implement features like real-time data updates and JavaScript interop effectively.


Spaceflight News API
https://api.spaceflightnewsapi.net/v4/docs/
https://www.spaceflightnewsapi.net/

I combed through this documentation to determine how to interact with the Spaceflight News API (SNAPI). The documentation was straightforward to me as they seem to use FastAPI, a python framework I am familiar with, and it was good to use their example endpoints online.


CesiumJS
https://cesium.com/platform/cesiumjs/

Cesium's main page provided me with clear instructions on how to implement the 3D globe for 3D dashboard.


Cesium community: Announcements, showcases, and discussion about the 3D geospatial ecosystem. 
https://community.cesium.com/

I refered to these community chats to figure out some issues I was having regarding cesium, for example how to deal with the frames when selecting a location on the 3D dashboard. It was fun to do some searching through a stack overflow like site. I used it to figure out how to make the YouTube video link work for the ISS on the 3D dashboard page as the issue was the YouTube was blocking accessing the video from within an iframe.


Amazon Q Developer
https://aws.amazon.com/q/developer/

I used my favorite extension on VS Code, Amazon Q, throughout my development process, serving as my AI coding assistant. I relied on it mostly for writing CSS, allowing me to focus on C# backend logic rather than frontend styling. Additionally, Q helped me troubleshoot technical issues, generate proof of concept code snippets, and provided architectural overviews for implementing complex features like JavaScript interop with Cesium and live data updates.