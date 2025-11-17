Annotaded Bibliography
Anthony Petrosino

**Nav Menu and Homepage** 
(NavMenu.razor, NavMenu.razor.css, Home.razor, Home.razor.css)

    I made the homepage to be a hub with info on all SpaceTracker features. It has interactive cards linking to dashboards, astronauts, spacecraft, launches, space news and programs.

    The main navigation bar is visible across all pages, displaying a dropdown menu system with four categories (Dashboards, Explore, Events, Site Info) using Blazor's NavLink component for client side routing. I used AI to help me make custom CSS for dropdown styling and a more responsive design to enable smooth navigation without page reloads.

**Space News** 
(spaceNewsModel.cs, spaceNewsService.cs, SpaceNews.razor, SpaceNews.razor.css)

    The space news webpage displays paginated space news articles from multiple sources using a three-layer architecture that is used throughout our project: spaceNewsModel.cs defines the data structure for articles, spaceNewsService.cs handles API calls to fetch and process the data, and SpaceNews.razor renders the webpage with a scrolling news source banner and the option to load additional articles. This pattern helped me understand the difference between data models, logic services, and presentation components in Blazor. I consulted documentation from SNAPI (Space News API) to better understand how to interact with it's endpoints.

*** 2d Dashboard ***
()


*** 3d Dashboard ***
()

The vast majority of my css came from my built-in AI editor in my IDE, Amazon Q which greatly helped me focus on the C# aspects of my project rather than styling the frontend.