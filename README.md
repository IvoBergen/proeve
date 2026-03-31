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
general info van alle feature

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


wiki/usertest moet hier
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
// indepth over elke feature
## Timer door Owen Stas 

dit is een timer die 5 minuten heeft en dan afgaat en dan heb je het level verloren
![Timer gif](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Timer.gif ) 

Dit is de visual sheet van de timer script

![Timer visualsheet](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Timer-visualsheet.png)

* [TimerScript](ZeebeeldOperator/Assets/Scripts/Timer)   
 ## Interaction systeem door Owen Stas 


![Timer gif](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Interaction.gif ) 

Dit is de visual sheet van de timer script

![Timer visualsheet](https://github.com/IvoBergen/proeve/blob/documentation/readMEUpdate/ReadMEFiles/Interaction-System-BWP-VWO-Visualsheet.png)

* [TimerScript](ZeebeeldOperator/Assets/Scripts/InteractionSystem/PlayerInteraction.cs)

## Credits

| Naam | Rol |
|---|---|
| Akari Le | Clipboard, Clues |
| Owen Stas | Interaction, Timer, Guess System |
| Ivo Bergen | Dialogue, NPC Movement |
| Christiaan Oostwouder | Movement |
