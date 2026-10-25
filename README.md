# Red Java

Red Java is a 2D side-scrolling platformer made in Unity. The player character moves and jumps through
three levels, collects coins, picks up chests that restore health, and avoids enemies that shoot
bullets, moving saw blades and water. The in-game text is in Turkish.

[TODO: screenshot - add an image, for example docs/screenshot.png, and reference it here]

## Unity version

Unity 2020.1.0f1 (from `ProjectSettings/ProjectVersion.txt`).

## Opening and running

1. Install Unity 2020.1.0f1 through Unity Hub.
2. In Unity Hub, choose "Add" and select the root folder of this repository.
3. Open the project. On first open Unity rebuilds the `Library` folder and can take a few minutes.
4. Open `Assets/anamenu.unity` (the main menu) and press Play.

Scenes included in the build, in order: `anamenu`, `level1`, `level2`, `level3`, `Ayarlar` (graphics settings).

## Controls

Input is read through the Standard Assets `CrossPlatformInputManager`.

| Action     | Keyboard                      |
|------------|-------------------------------|
| Move       | Left / Right arrow, or A / D  |
| Jump       | Space                         |

The level scenes also contain the Standard Assets `MobileSingleStickControl` on-screen controls,
used when building for mobile targets.

## Gameplay rules (from the code)

- The player starts each level with 100 health.
- A bullet hit costs 1 health; touching an enemy or a saw costs 10.
- A chest restores 10 health, up to 100.
- Falling into water sets health to 0. At 0 health the screen fades to black and the main menu loads.
- Reaching the level end trigger loads the next scene in the build order.
- Levels are unlocked in the level select menu based on progress saved with `PlayerPrefs`.

## Code structure

Project scripts (third-party code under `Assets/Standard Assets`, `Assets/SampleScenes` and
`Assets/BayatGames` is not listed):

| Script                          | Responsibility |
|---------------------------------|----------------|
| `Assets/Scripts/karakterKontrol.cs` | Player controller. Horizontal movement and a single jump with `Rigidbody2D`, sprite-based idle, walk and jump animation, smoothed camera follow, health and coin UI, trigger handling for bullets, enemies, saws, chests, coins, water and the level end, and saving level progress. |
| `Assets/Scripts/dusmanKontrol.cs`   | Enemy. Patrols back and forth between waypoints, raycasts toward the player, speeds up, switches sprite and fires bullets at random intervals when the player is in sight. Includes an editor inspector button that adds waypoints, and gizmos that draw the patrol path. |
| `Assets/Scripts/kursunKontrol.cs`   | Bullet. On spawn, applies a force in the direction from the enemy to the player. |
| `Assets/Scripts/testere.cs`         | Saw blade hazard. Rotates continuously and patrols between waypoints, with the same editor waypoint button and gizmos as the enemy. |
| `Assets/anaMenuKontrol.cs`          | Main menu. Start, level select (with locks for levels not yet reached), settings and quit buttons. |
| `Assets/ayarlar.cs`                 | Settings scene. Sets the graphics quality level to low, medium or high and returns to the main menu. |
| `Assets/AltınKontrol.cs`            | Loops the coin sprite animation. |
| `Assets/canver.cs`                  | Plays the chest opening sprite animation once and holds the last frame. |
| `Assets/suAnimasyonu.cs`            | Loops the water sprite animation. |

Main prefabs are in `Assets/Prefabs` (player `Java`, `Dusman`, `Kursun`, `Testere`, `Altın`, `Sandık`, `Su`,
`LevelBitti` and level pieces).

## Third-party assets

- Unity Standard Assets (CrossPlatformInput, sample scenes)
- Bayat Games "Free Platform Game Assets"
- ARCADECLASSIC font

[TODO: confirm the license and source of each third-party asset and the font]
