# Tic Tac Toe 2000
## Introduction
This is a sample application written within a week as a technical assessment for [Illumix](https://www.illumix.com/).
The application is a simple game of [Tic-tac-toe](https://en.wikipedia.org/wiki/Tic-tac-toe).
This was authored within the period of 2024-06-12 to 2024-06-19 by [Vi Prime](https://vprime.dev/).
All code, graphic assets, and sounds are created by Vi Prime unless otherwise noted.

## 3rd party assets
- Standard default files and structure provided by the Unity game engine, licenced by Unity
- Fonts, Input Settings, Code, and other assets loaded from Unity Package Manager, licenced by Unity Technologies.
- [Segment14 Font](Assets%2FFonts%2Fhdad-segment14) by [Paul Flo Williams](http://hisdeedsaredust.com/) sourced from [FontLibrary](https://fontlibrary.org/en/font/segment14) provided under the [SIL Open Font License](https://openfontlicense.org/)

## Legal
[Tic Tac Toe 2000](https://github.com/vprime/tic-tac-toe-2000) © 2024 by [Vi Prime](https://vprime.dev/) is licensed under [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International licence](LICENSE)

## Development

### Assessment Requirements
1. A UI starting screen that allows the user to select 1 Player or 2 Players.
2. In 1 Player mode, the user plays against the computer .( AI can be as simple as just selecting a random available spot)
3. In 2 Player mode, assume that X goes first, then alternate after each turn. ( Player 1 vs Player 2)
4. A Tic-Tac-Toe board for the user to select which spot to place.
5. The board can be implemented in 2D or 3D using placeholder assets.
6. A UI end screen that indicates Win / loss / draw and takes you back to the starting screen.
7. Please send it back when you are finished
8. Most importantly, have fun!
9. Unity URP >= 2022
10. Delivered as a whole

### Design Concept
The design is inspired by tactile buttons found on industrial hardware, and VFD segmented displays. 
The sound design is inspired by simple monophonic synths of the 80's.

### Project file hierarchy
Notable locations within the project files
- Art
  - Source files for game art
- Assets
  - Materials
    - URP material assets
  - Models
    - FBX files exported from Blender
  - Prefabs
    - Environment
      - Contains game objects spawned during gameplay
  - Scenes
    - Main scene and lighting data
  - Scripts
    - App
      - Components
        - App state structure
      - Systems
        - Behaviors for the application flow
        - Behavior for the main menu UI
    - Environment
      - EnvironmentControl mono behavior updates components to the application and game flow
      - Components
        - Structures for defining the look and feel
      - Interfaces
        - Common interfaces for environment interactions
      - Systems
        - Logic for the look and feel of the environment
    - Game
      - GameControl mono behavior updates components to the game logic
      - Components
        - Structures for the active game state
      - Systems
        - Logic for processing game state
  - Settings
    - UPR Configuration assets
    - GameData
      - Scriptable Objects containing gameplay configurations
      - Lighting configuration asset
  - Sounds
    - Sounds and music assets
  - UI Toolkit
    - PanelSettings asset
    - Documents
      - UXML Visual Tree Assets
    - UnityThemes
      - TSS Theme Assets
- README.md
  - This file
- LICENSE
  - Licence for this software

### Scene Hierarchy
The app consists of the single [Main.unity](Assets%2FScenes%2FMain.unity) scene.
- Main Camera
- EventSystem
- App Components
  - Application flow components, environment, and scene configurations.
- Loading UI
- Opening UI
- Menu UI
- Game UI
- Light Probe Group
- Directional Light
- Directional Light
- Purple Light
- Blue Light
- Post Processing Volume




