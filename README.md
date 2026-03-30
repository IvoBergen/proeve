#  Zeebeeld operator

> **Genre:** Detective / Simulation | **Platform:** PC | **Status:** In ontwikkeling / Prototype  
> **Unity versie:** 2022.3.62f3

---

## Over het spel

Je bent een marinier op een marineschip en werkt als radartopist. Je taak is het identificeren van schepen in de omgeving. Door radar, verrekijker en communicatie bepaal je of een schip vijandig of vriendelijk is. Het spel draait om observatie, analyse en het maken van de juiste beslissingen onder druk.

**Kernpunten:**
- Start je dag in je hut met een korte tutorial.
- Observeer andere bemanningsleden terwijl je zelfstandig aan de slag gaat.
- Verzamel aanwijzingen (clues) en gebruik de clipboard om schepen te identificeren.
- Beslis welke boot de vijand is voordat de tijd om is.

---

## Gameplay

| Actie | Besturing |
|---|---|
| Bewegen | WASD |
| Rondkijken | Muis |
| Interacteren | E |
| Clipboard openen | TAB |
| Schip kiezen | Pijltjestoetsen |
| Bevestigen | Enter |

> Spelrondes duren **5 minuten**.

---

## Features / Systemen

| Systeem | Status | Auteur |
|---|---|---|
| Clipboard | ✅ Afgerond | Akari Le |
| Interaction | ✅ Afgerond | Owen Stas |
| Dialogue | ✅ Afgerond | Ivo Bergen |
| Movement | ✅ Afgerond | Christiaan Oostwouder |
| Clues | ✅ Afgerond | Akari Le |
| Timer | ✅ Afgerond | Owen Stas |
| Guess System | ✅ Afgerond | Owen Stas |
| NPC Movement | ✅ Afgerond | Ivo Bergen |
| Camera Sensitivity | 🔄 In progress | Owen Stas |
| Sprint systeem | 📋 Gepland | — |
| Dialog UI verbetering | 📋 Gepland | — |
| Interactie feedback (geluid) | 📋 Gepland | — |

---

## Installatie

----

## Contributie

Gebruik de volgende branch-naamgeving:

```
feature/<feature-naam>
bug/<bug-naam>
documentation/<doc-naam>
```

- Commit duidelijk met een beschrijving.
- Pull requests moeten getest en goedgekeurd worden door de lead dev.
- Na merge, verwijder je feature branch.

---

## Playtesting & Feedback

User feedback is verwerkt in user stories en prioriteiten.

| Feedback | Status |
|---|---|
| Camera sensitivity en Y-rotatie sneller | 🔄 In progress |
| Mogelijkheid tot rennen toevoegen | 📋 Gepland |
| Dialog UI logischer positioneren en kleiner maken | 📋 Gepland |
| Feedback bij interacties (bijv. geluid bij E) | 📋 Gepland |

---

## Code Conventies

| Regel | Voorbeeld |
|---|---|
| Private variabelen beginnen met `_` | `_slider`, `_text` |
| Public variabelen zonder `_` | `sensX`, `sensY` |
| Functies gebruiken PascalCase | `OnSliderChanged()` |
| Debug.Log verwijderen na testen | — |
| Summaries boven classes en functies | `/// <summary>` |

---

## Game mechanics 
De mechanics van team 04 marine.
## moving platform by Ivo
The moving cubes are made as an obstacle for the player. They Move to random locations decided by the input values on the X an Z axis. The cube also rotates between 90, 0 and -90 degrees to add for an extra layer of difficulty.

![MovingCubes gif](https://github.com/IvoBergen/ProefExamenRepo/blob/develop/ReadMEFiles/MovingCubes.gif)

This is the visual sheet for the moving cube scripts

![Moving Cubes visualsheet](https://github.com/IvoBergen/ProefExamenRepo/blob/develop/ReadMEFiles/VisualSheetMovingCube.png)

* [Moving Cubes](ProefExamenGame/Assets/Scripts/MovingCubes)

The moving platforms are added to challenge the player’s movement, timing, and overall control. Instead of simply walking or jumping across static surfaces, the player must carefully observe the platform’s motion and choose the right moment to move.
## Credits

| Naam | Rol |
|---|---|
| Akari Le | Clipboard, Clues |
| Owen Stas | Interaction, Timer, Guess System |
| Ivo Bergen | Dialogue, NPC Movement |
| Christiaan Oostwouder | Movement |
