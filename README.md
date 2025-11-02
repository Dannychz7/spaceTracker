#spaceTracker








Under Development, TODO LIST:
---------------------------------------------------
1) Setup Blazor App - Done 
2) Secure API Keys via blazor user secrets - Done
3) Ensure logging is present in file - Done
4) Create .gitignore and ensure executables and config are not committed - Done
5) Create weather service, ensure proper logging and json sanitation (DONE)
6) Create Rocket Live Launch service, ensure proper logging and json sanitation (DONE)
7) Create N2YO and functions, ensure proper logging and json sanitation - DONE
8) space Devs service, ensure proper logging and json sanitation - DONE
9) Nasa service, ensure proper logging and json sanitation - on hold (due to government shutdown, all nasa apis are down, so as of now, it's useless)


Scope for each webpage
-----------------------------------------------
"/" Home.cs
    - Will give basic information about the site as a whole
"/3d" Dashboard3D.cs
    - Will contain a 3d gloab, tracks upcoming rocket launches, satlite launches, and other events
    - Will also contain a tracker for the ISS and where it is on earth
    - Will also track 5 other satlites
    - Each location will have more info about it like launch details, location, etc...
"/2d" Dashboard2D.cs
    - A simpler, easier to run version of dashboard on a 2d map
    - Displays the location of the next 5 launches on the map
    - Selectign a location brings up info about the launch / location
"/launch5" liveLaunchLoader.cs
    - Will contain a dashboard of the next top 5 all next to each other
    - Time and weather will show for current location, so we'll say worcester, ma
    - When clicking on a rocket, it will change color depending on time, weather, wind anitmation, cold, hot, etc... 
    - Each rocket will have a different animation depending on state it is in (ready, prepping, successful launch, etc...)
"/Astronouts"
    - A fun interactive Part here, showcasing famous astronought
    - name, dob, description, flights flown, etc... 
    - Maybe we'll do a search bar and maybe this bookshelf style where the screen is a astronought and the you can click through ? 
    - Something like: "overlay page navigation animation"
    - Another way to explain: a multi-page animated transition effect — where each “page” of the website moves or slides in/out with a smooth animation, almost like flipping through a digital stack of pages or cards.
"/Programs"
    - Same idea but maybe a timeline of the different programs in a timeline loaction

    - /Launches cheese will show 40 upcoming launches
    - /spacecraft
    - /ISS

.... IDK pass this point: 
"/funFacts? "

Attempt db cache for space craft and astronuts?
Attempt to make a page soley dedicated to the ISS (Basicly half of Raw data) but on its own page

