# 🛡️ Tower Defense (Unity) – Technical Assessment Project

This repository contains a **Tower Defense game prototype built in Unity** as part of a **timed technical assessment**. The entire implementation was completed in **under 16 hours across 4 days**, with a strong focus on:

- Clean and extensible architecture  
- SOLID principles and testability  
- Responsiveness and runtime performance  
- Team-scalable codebase for future iteration  

> ⚠️ This project was built to match a **pre-defined set of gameplay and architectural requirements**. The original instructions are paraphrased to protect confidentiality and **avoid being indexed as a direct solution** to any specific company's technical assessment.

> 🧪 This is **not a polished final product**, but rather a **framework showcasing technical depth, architectural foresight, and fast turnaround capability**.

## 🎯 Challenge Scope

The goal was to implement a functional tower defense game with the following features:

- A central **base** that enemies (creeps) try to reach  
- Creeps **spawn from predefined locations** and move straight toward the base  
- Easily configurable **creep stats** and **spawn behavior**  
- **Lose condition** if creeps reach the base (displays `LosePopup`)  
- Player can place **turrets** using mouse input  
- Turrets **automatically detect and shoot** nearby creeps  
- Projectiles apply **damage** to creeps  
- Fully tunable **damage and health** values  
- **Coin-based economy**: Turrets cost coins, creeps drop coins on death  
- Two turret types: **Standard** and **Freeze/Slow-down**  
- Multiple creep types with varying **speed** and **health**  
- A **wave system** where next wave spawns after current is cleared  
- **Win condition** when all waves are cleared and base survives (`WinPopup`)

## 🧱 Architecture and Design Philosophy

This project follows a **POCO-first, MonoBehaviour-isolated architecture**.

Key decisions and patterns:

- **SOLID Principles**: Clean separation of concerns via interfaces like `ICreep`, `ITurret`, `IProjectile`, etc.  
- **Event-Driven**: All game state transitions (win/loss, wave clear, coin updates) are based on event dispatching  
- **Strategy & Observer Patterns**: Used for different turret and creep behaviors via composition, not inheritance  
- **Scalability First**: Each system (spawning, placement, economy, waves) is decoupled and extensible  
- **DI-Ready Structure**: The architecture can support tools like `VContainer`, although no external packages were used here for minimal setup friction

## ⚙️ Features Overview

| System             | Description                                                                 |
|--------------------|-----------------------------------------------------------------------------|
| 🎮 Input System     | Unity New Input System – mouse-based turret placement                      |
| 🧠 GameManager      | Central coordinator for win/loss logic, wave state, and UI popups           |
| 🚀 Creep System     | Multiple creep types, tunable attributes, spawn-point-based instantiation   |
| 🛡️ Turret System    | Two turret types: standard and slow-down, placed dynamically by player      |
| 💥 Projectile System| Projectile logic handles damage, collision, and status effects              |
| 💰 Economy          | Kill-based coin reward system and turret purchase cost                     |
| 🕹️ Wave System      | Sequential wave control with clean transitions and completion detection     |
| 📊 UI Popups        | Win/Loss popup UI triggered via event observers                             |

## 📝 Development Notes

- **Total dev time:** ~16 hours  
- **Time span:** 4 days  
- **Unity version:** 2022.3 LTS  
- **Language:** C#  
- **Libraries:** None (for easy clone-and-play testing)

## 📁 How to Run

```bash
1. git clone https://github.com/game-dev-pa/tower-defence
2. Open the project in Unity 2022.3 LTS or a compatible version
3. Open the MainScene
4. Press Play
```

## 👨‍💻 About This Project

This was completed as a technical showcase project, ideal for evaluating:
- My ability to translate high-level feature goals into clean architecture
- How I prioritize maintainability and scalability under tight deadlines
- My decision-making and tradeoffs in systems-level Unity development


## 🙋‍♂️ Why This Repo Matters

This project is a great snapshot of how I approach **problem-solving under time constraints** with a strong emphasis on **code clarity, system thinking, and scalability**. Whether you're a recruiter, hiring manager, or fellow developer — I hope you enjoy exploring the code as much as I enjoyed building it.


## 🧠 Contact

Feel free to explore the code or reach out if you have questions, feedbacks or ideas.

[LinkedIn](https://linkedin.com/in/game-dev-pa) | [Email](mailto:game.dev.pa@gmail.com)
