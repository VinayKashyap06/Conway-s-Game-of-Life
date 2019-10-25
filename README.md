# Conway-s-Game-of-Life
Conway's Game of Life visualization

# Architecture Used
- **Singleton**
- **MVC**

## Data Config
Comes from Scriptable Object Game Settings.

# How to Play
Run the exercise using the start button. Default grid config is 6x6. Can be changed via Scriptable Object during compile time or via UI during Run time. Remember to stop the tick before changing config via UI.

# Scripting Roles

## Service Game:
Has scriptable Object and sets Global settings. It's a high level class to provide communication.
## Data Models
1. Has Data structures to be used for the game. 
2. Has Enums for game purpose and readbility purpose.
## Controller Grid:
1. Performs all action related to grid.
## ScriptableObjectGame Settings:
1. Has Game settings,
2. Has prefabs to be spawned.
## Controller UI:
1. Interacts with UI and Game service
## ITickable:
1. Interface with Tick() function can be used for update for non-mono behaviours.

## Globals:
1. Static class with variables to be used everywhere.
