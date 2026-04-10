# Zeebeeld Operator  

> **Genre:** Detective / Simulation  
> **Platform:** PC  
> **Status:** In development (Prototype)  
> **Unity version:** 2022.3.62f3  

---

## About the Game  

You play as a marine on a naval ship in the role of a **radar operator**. Your task is to identify ships in the surrounding area. Using radar, binoculars, and communication, you determine whether a ship is hostile or friendly.  

The game revolves around **observation, analysis, and decision-making under time pressure**.  

### Core Features  

- Start your day in your cabin with a short tutorial  
- Observe crew members and the environment  
- Collect clues  
- Use the clipboard to analyze ships  
- Identify the hostile ship before time runs out  

---

## Gameplay  

| Action | Controls |
|--------|----------|
| Move | WASD |
| Look around | Mouse |
| Interact | E |
| Open clipboard | TAB |
| Select ship | Arrow keys |
| Confirm | Enter |

> A game round lasts **5 minutes**  

---

## Features / Systems  

- **Timer System** – determines game duration and loss condition  
- **Interaction System** – interaction with objects and NPCs  
- **Guess System** – identifying ships  
- **Dialogue System** – communication with NPCs  
- **Movement System** – player and camera control  
- **Clipboard System** – collecting and analyzing information  

---

## Installation  

1. Clone the repository  
2. Press **Play** in the Unity Editor  

---

## Game Mechanics  

### Timer (by Owen Stas)  

This system gives the player **5 minutes** to make a decision. When the time runs out, the player automatically loses.  

**User story:**  
> As a player, I want a timer that becomes visually and audibly more intense as time runs out, so that I feel the urgency and stakes increasing.  

![Timer gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Timer.gif)  

Visualization of the timer script:  
![Timer visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Timer-visualsheet.png)  

Script:  
- [TimerScript](ZeebeeldOperator/Assets/Scripts/Timer/LevelTimer.cs)  

---

### Interaction System (by Owen Stas)  

This system allows the player to interact with objects and the game world.  

**User story:**  
> As a player, I want to interact with the world to gather information.  

![Interaction gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Interaction.gif)  

Visualization:  
![Interaction visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Interaction-System-BWP-VWO-Visualsheet.png)  

Script:  
- [Interaction script](ZeebeeldOperator/Assets/Scripts/InteractionSystem/PlayerInteraction.cs)  

---

### Guess System (by Owen Stas)  

This system allows the player to guess the hostile ship.  

**User story:**  
> As a player, I want to select a ship and receive immediate feedback on my guess, so that I know whether I acted correctly and feel the pressure of my decisions.  

![Guess System gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/GuessSysteem.gif)  

Visualization:  
![Guess system visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Guess-System-BWP-VWO-Visualsheet.png)  

Script:  
- [Guess scripts](ZeebeeldOperator/Assets/Scripts/Guess%20System)  

---

### Dialogue System (by Ivo Bergen)  

This system allows the player to talk with NPCs.  

**User story:**  
> As a player, I want a dialogue system so I can gather clues from other NPCs.  

![Dialogue Visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/DialogueSystem.png)  

![Dialogue Gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Dialogue.gif)  

Script:  
- [Dialogue scripts](ZeebeeldOperator/Assets/Scripts/Dialogue)  

---

### NPC Movement (by Ivo Bergen)  

This system allows NPCs to walk around the map.  

**User story:**  
> As a player, I want to see NPCs moving around to make the game feel more alive.  

![NPC Movement Gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/NPCMovement.gif)  

![NPC Movement Visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/NPCMovement.png)  

Script:  
- [NPC scripts](ZeebeeldOperator/Assets/Scripts/NPCSystems)  

---

### Main Menu (by Ivo Bergen & Owen Stas)  

This system provides the player with a menu to adjust settings and start the game.  

**User story:**  
> As a player, I want a menu so that I can start or exit the game.  

![Main Menu Gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/MainMenu.gif)  

![Menu Visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/Menu.png)  

Script:  
- [Menu script(s)](ZeebeeldOperator/Assets/Scripts/GameManagerScripts/MenuManager.cs)  

---

### Goal System  

This system gives the player a clear objective so they always know what to do.  

**User story:**  
> As a player, I want my goal to be clearly communicated so that I am never confused about what to do.  

![Goal System Gif](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/GoalSystem.gif)  

![Goal System Visualsheet](https://github.com/IvoBergen/proeve/blob/develop/ReadMEFiles/GoalSystem-Visualsheet.png)  

Script:  
- [Goal script(s)](ZeebeeldOperator/Assets/Scripts/GoalSystem/GoalManager.cs)  

---

## Playtesting & Feedback  

User tests and feedback can be found in the wiki:  

- [View user tests here](https://github.com/IvoBergen/proeve/wiki/UserTest)  

---

## Contribution  

Use the following branch structure:  

- Write clear commit messages  
- Pull requests must be tested and approved  
- Delete your branch after a successful merge  

---

## Code Conventions  

| Rule | Example |
|------|--------|
| Private variables start with `_` | `_slider`, `_text` |
| Public variables without `_` | `sensX`, `sensY` |
| Functions use PascalCase | `OnSliderChanged()` |
| Remove Debug.Log after testing | — |
| Use summaries | `/// <summary>` |

---

## Credits  

| Name | Role |
|------|------|
| Akari Le | Clipboard, Clues |
| Owen Stas | Interaction, Timer, Guess System |
| Ivo Bergen | Dialogue, NPC Movement |
| Christiaan Oostwouder | Movement |

---