## Script Overview

| Script | Purpose |
|--------|---------|
| `Enemy.cs` | Enemy behavior: movement, collision detection, death handling |
| `EnemySpowner.cs` | Manages enemy spawning within circular boundary |
| `PlayerMovement.cs` | Handles player input for movement and jumping |
| `PlayerShoot.cs` | Controls shooting mechanics and bullet spawning |
| `PlayerHealth.cs` | Manages player health and UI display |
| `ScoreManager.cs` | Tracks score progression and win conditions |
| `SceneReloader..cs` | Handles scene transitions and restart functionality |

## Setup Instructions

**Scene Configuration**
1. Open your scene file (`<YourScene>.unity`)
2. Add the scene to **Build Settings**

**Component Setup**

**EnemySpawner Configuration**
- Assign **Enemy Prefab** to the spawner
- Adjust spawn parameters:
  - `spawnInterval`: Time between enemy spawns
  - `maxEnemies`: Maximum concurrent enemies
  - `spawnRadius`: Spawning area size

**PlayerHealth Setup**
- Create a **World-Space Canvas** attached to the player
- Assign 3 HP `Image` components for visual health display
- Link `GameOverText` UI element

**ScoreManager Setup**
- Place on a persistent GameObject (recommended: *GameManager*)
- Assign UI text components:
  - `ScoreText`: Displays current score progress
  - `WinText`: Shows victory message

**SceneReloader Setup**
- Add component to *GameManager*
- No additional configuration required

**Critical Connections**
- Ensure bullet collision detection calls `Enemy.HitByBullet()` method
- Verify all UI references are properly assigned

## Controls

The game uses Unity's **Input System** for responsive controls:
- **Movement**: WASD or Arrow Keys
- **Jump**: Spacebar
- **Shoot**: Mouse click or designated shoot key

## Game Flow

1. **Start**: Player spawns with full health (3 HP), random target score generated
2. **Gameplay**: 
   - Enemies spawn continuously within the circular area
   - Player shoots enemies to increase score
   - Contact with enemies reduces health
3. **Win Condition**: Reach the target score
4. **Lose Condition**: Health reaches zero
5. **Game End**: Time pauses, result displayed, automatic restart after delay

## Development Notes

**Time Management**
- Win/lose states use `Time.timeScale = 0` to pause gameplay
- Scene restart uses realtime delay to bypass time scaling

**File Management**
- Keep Unity `.meta` files in version control
- Ignore standard Unity folders: `Library/`, `Temp/`, `Build/`

**Optional Improvements**
Consider renaming files for consistency:
- `EnemySpowner.cs` → `EnemySpawner.cs`
- `SceneReloader..cs` → `SceneReloader.cs`

**Architecture Benefits**

- Modular Design: Each script handles a specific responsibility
- Scalable: Easy to add new features or modify existing ones
- Maintainable: Clear separation makes debugging straightforward
- Reusable: Components can be easily adapted for other projects
