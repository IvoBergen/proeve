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

1. Clone de repository:
```bash
git clone https://github.com/<jouwgebruikersnaam>/NavalRadarGame.git
```

2. Open het project in **Unity 2022.3.62f3**

3. Zorg dat de volgende packages zijn geïnstalleerd:
   - TextMeshPro
   - Unity Input System

4. Open de scene `MainScene` en druk op **Play**.

---

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

## Credits

| Naam | Rol |
|---|---|
| Akari Le | Clipboard, Clues |
| Owen Stas | Interaction, Timer, Guess System |
| Ivo Bergen | Dialogue, NPC Movement |
| Christiaan Oostwouder | Movement |
