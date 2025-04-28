# **Idle Runner RPG - Core Systems**

Welcome to the Idle Runner RPG Core Systems, a modular, mobile-optimized, expandable architecture built in Unity.
This project showcases high scalability, clean structure, and production-level practices for an idle progression RPG.

### Gameplay Overview

In this system, players engage in continuous automatic running and fighting, augmented by:

* Dynamic enemy waves

* Tap-to-attack inputs

* Pet companions with passive boosts

* Skill and special attack activations

* Stat upgrades and RPG-style growth

* The game emphasizes idle mechanics, ensuring progression even while offline.

### Core Systems & Features

* #### Hero & Combat System:

1. [x] State Machine Architecture (State, IdleState, RunState, AttackState, etc.) to manage hero behavior fluidly.
2. [x] Tap Damage Controller: Adds immediate interaction for manual damage boosts.
3. [x] Special Attack System: (e.g., Rage Mode, Golden Tap, Auto Tap) with activation, cooldown, and upgrade logic.

* #### Enemy System
1. [x] Dynamic Spawner: Enemies appear per stage with adjustable difficulty scaling.
2. [x] Loot Drop Logic: Each enemy has configurable loot outcomes (e.g., coins, potions).
3. [x] Enemy States: (idle, attack, die) to make encounters more dynamic.

* #### Pet System
1. [x] Pet Manager controlling passive buffs and special skill activations (e.g., DPS boost, critical tap bonus).
2. [x] Pets can be extended easily via ScriptableObjects (PetSO, PetSkill).

#### * Progression System
1. [x] Skill Trees and Stat Upgrades using:
2. [x] Passive skills that scale over time.
3. [x] Active abilities improving player performance.
4. [x] Upgradeable Items and Special Attacks (SkillUpgrade, SpecialAttackUpgrade) integrated into UI panels.
* #### Save & Load System
1. [x] Persistent Offline Progression using custom SaveLoadManager.
2. [x] Data structure optimized for saving:
3. [x] Hero stats
4. [x] Economy state
5. [x] Pet skills
6. [x] Stage progression
7. [x] Uses PlayerPrefs abstraction layer for rapid mobile saves, with future-proofing for backend/cloud saves.

* #### Economy System
1. [x] Currency tracking (e.g., coins, gems) and cost scaling through EconomyManager.
2. [x] Designed for idle progression with exponential growth curves.

### Technical Architecture Highlights

* UI Framework Uses Enhanced Scroll View for smooth, dynamic lists in skill trees and pet management UIs.


* Animations	Powered by DOTween for performance-friendly tweening (movement, fading, scaling).


* Pooling	Object reuse strategies prepared for future optimization (enemies, loot drops).


* ScriptableObjects	Heavy use for pets, loot tables, hero damage data, potion types, skill icons, and damage icons.


* Enums	Clear enum management (AttackType, DamageType, EnemyType, etc.) for maintainable type safety.


* Offline Progression	Placeholder hooks for idle rewards calculation when returning after inactivity.


* Clean Folder Structure	Following Single Responsibility Principle (Managers, Items, Skills, Enums, Utils separated).


* Helper Utilities	CalcUtils, DescriptionUtils for cleaner math operations and UI text formatting.

### Tools and Technologies

* Unity 2022.x LTS


* DOTween (Demigiant) for smooth runtime animations


* Enhanced Scroll View for performant dynamic lists


* ScriptableObject-driven architecture for content separation


* State Pattern for scalable character behaviors


* Custom Save/Load Manager based on PlayerPrefs abstraction


* Object Pooling Readiness for scalable enemy and loot spawning


* Mobile Performance Focus: minimal GC allocations, fast scene load times