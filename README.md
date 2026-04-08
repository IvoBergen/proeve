# Zeebeeld Operator  

> **Genre:** Detective / Simulatie  
> **Platform:** PC  
> **Status:** In ontwikkeling (Prototype)  
> **Unity versie:** 2022.3.62f3  

---

## Over het spel  

Je speelt als marinier op een marineschip in de rol van **radartopist**. Jouw taak is het identificeren van schepen in de omgeving. Met behulp van radar, een verrekijker en communicatie bepaal je of een schip vijandig of vriendelijk is.  

Het spel draait om **observatie, analyse en besluitvorming onder tijdsdruk**.  

### Kernpunten  
- Start je dag in je hut met een korte tutorial  
- Observeer bemanningsleden en de omgeving  
- Verzamel aanwijzingen (clues)  
- Gebruik de clipboard om schepen te analyseren  
- Identificeer het vijandelijke schip voordat de tijd om is  

---

## Gameplay  

| Actie | Besturing |
|------|----------|
| Bewegen | WASD |
| Rondkijken | Muis |
| Interacteren | E |
| Clipboard openen | TAB |
| Schip selecteren | Pijltjestoetsen |
| Bevestigen | Enter |

> Een spelronde duurt **5 minuten**  

---

## Features / Systemen  

- **Timer systeem** – bepaalt de speelduur en verliesconditie  
- **Interaction systeem** – interactie met objecten en NPC’s  
- **Guess systeem** – identificeren van schepen  
- **Dialogue systeem** – communicatie met NPC’s  
- **Movement systeem** – speler- en camerabesturing  
- **Clipboard systeem** – verzamelen en analyseren van informatie  

---

## Installatie  

1. Clone de repository  

4. Klik op **Play** in de Unity Editor  

---

## Game Mechanics  

### Timer (door Owen Stas)  
Dit systeem geeft de speler **5 minuten** om een beslissing te maken. Wanneer de tijd op is, verliest de speler automatisch.  
**User story:**  
> Als speler wil ik een timer die visueel en auditief intensiever wordt naarmate de tijd afloopt, zodat ik de urgentie en stakes voel toenemen. 


![Timer gif](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Timer.gif)  

Visualisatie van het timerscript:  
![Timer visualsheet](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Timer-visualsheet.png)  

* [TimerScript](ZeebeeldOperator/Assets/Scripts/Timer/LevelTimer.cs)

---

### Interaction Systeem (door Owen Stas)  
Met dit systeem kan de speler interactie hebben met objecten en de spelwereld.  

**User story:**  
> Als speler wil ik kunnen interacteren met de wereld om informatie te verzamelen.  

![Interaction gif](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Interaction.gif)  

Visualisatie:  
![Interaction visualsheet](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Interaction-System-BWP-VWO-Visualsheet.png)  

Script:  
* [Interaction script](ZeebeeldOperator/Assets/Scripts/InteractionSystem/PlayerInteraction.cs)
---
### Guess Systeem (door Owen Stas)  

Met dit systeem kan de speler het vijandelijke schip raden

**User story:**  
> Als speler wil ik een schip kunnen kiezen en direct feedback krijgen op mijn guess, zodat ik weet of ik correct heb gehandeld en de druk van mijn keuzes voel. 

![GuessSysteem gif](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/GuessSysteem.gif)  

Visualisatie:  
![Interaction visualsheet](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Guess-System-BWP-VWO-Visualsheet.png)  

Script:  
* [Guess scripts](ZeebeeldOperator/Assets/Scripts/Guess%20System)

## Playtesting & Feedback  

Usertests en feedback zijn te vinden in de wiki:  
* [Bekijk de hier usertests](https://github.com/IvoBergen/proeve/wiki/UserTest) 

---

## Contributie  

Gebruik de volgende branch-structuur:  

- Schrijf duidelijke commit messages  
- Pull requests moeten getest en goedgekeurd worden  
- Verwijder je branch na een succesvolle merge  

---

## Code Conventies  

| Regel | Voorbeeld |
|------|----------|
| Private variabelen beginnen met `_` | `_slider`, `_text` |
| Public variabelen zonder `_` | `sensX`, `sensY` |
| Functies gebruiken PascalCase | `OnSliderChanged()` |
| Debug.Log verwijderen na testen | — |
| Gebruik summaries | `/// <summary>` |

---

## Credits  

| Naam | Rol |
|------|-----|
| Akari Le | Clipboard, Clues |
| Owen Stas | Interaction, Timer, Guess System |
| Ivo Bergen | Dialogue, NPC Movement |
| Christiaan Oostwouder | Movement |

---