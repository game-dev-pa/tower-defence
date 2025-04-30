# Tower Defense Game (Unity)
A simple but well-architected tower defense game built with Unity, featuring creeps, turrets, waves, and base defense mechanics. Designed with SOLID principles, dependency injection, and clean separation of concerns.

---

## Documentation

### How to Play
- Press Play in the MainScene. You will start with 100 coins, displayed in the top-left corner.
- Waves of creeps spawn randomly from four spawn points and march toward the base.
- Use the Regular and Freezing buttons at the bottom-right to select which turret to place.
- Click on the terrain to place the selected turret. (Each turret has a coin cost. If you lack funds, placement is blocked, and the coin text flashes red.)
- Regular Turrets deal damage, while Freezing Turrets slow creeps with a speed debuff.
- When a creep reaches the base, it deals damage. If base health reaches zero, the Lose popup appears.
- If all creeps from all waves are destroyed before the base is, the Win popup appears.

---

### Game Design (Assets/Scripts/Config)
- Starting Coins and BaseMaxHealth can be set in `GameConfigSO`.
- A creep's MovementSpeed, Health, RewardsOnKill, and DamageToBase are defined in `CreepDataSO`.
- Waves, NumberOfCreepsToSpawn, and SpawnInterval are defined in `WaveDataSO`.
- Turret stats, such as TurretFireRate, TurretImpactRadius, DamageToCreep, and TurretCost, are fetched from `TurretDataSO`.

---

### Architecture Principles
Followed core architectural and coding principles:
- **POCO-First Design**: Core logic in Plain Old C# Objects, using MonoBehaviour only for Unity hooks
- **SOLID Principles**: Single responsibility, dependency injection, and Strategy pattern
- **VContainer**: For dependency injection and lifetime management
- **MVP-Style**: Business logic separated from presentation
- **Async/Await**: For wave spawning with cancellation support
- **Observer Pattern**: Events for game state changes

---

## Systems and Implementation

### 1. Game State
- Tracks coins, wave index, and win/loss conditions
- Win condition requires both wave completion and all creeps dead

### 2. Creep Pooling
- Custom pooling system for creep management

### 3. Base Defense
- Proximity-based damage system to base

### 4. Turret System
- Strategy pattern for different turret behaviors
- Projectile pooling for efficiency

### 5. Economy System
- Reactive UI updates for coins and health

### 6. Wave System
- Async wave sequencing with cancellation support

### 7. Pause Management
- Custom pause system controlling game elements individually

---

## Potential Improvements
- Turret selection via keyboard input
- Better spawn point selection logic
- Additional turret/creep types
- Game reset after win/loss

---

## License  
This project is licensed under the **MIT License**.
*Note: Created for portfolio/educational purposes. Not for commercial use.*  
