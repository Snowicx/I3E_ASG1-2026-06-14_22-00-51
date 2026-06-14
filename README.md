# I3E_ASG1
Assigment 1
**[https://github.com/Snowicx/I3E_ASG1](https://github.com/Snowicx/I3E_ASG1-2026-06-14_22-00-51/tree/main)

## 🧀 Escape from the Giant Kitchen

Welcome to **Escape from the Giant Kitchen**, a fast-paced 3D puzzle-platformer built in Unity! You play as a tiny mouse trapped in a massive kitchen. Your only way out is a vent guarded by a lazy, demanding rat who refuses to move until you bring him enough cheese. Navigating a kitchen this size is dangerous—one wrong step into puddles of spilled water will send you right back to the beginning.

---

## Game Story & Objective
You find yourself in a gargantuan kitchen full of towering countertops, giant appliances, and scattered pieces of delicious cheese. The path to freedom lies through a ventilation shaft, but a lazy rat is blocking the door. 

Your objective is simple: **Scour the kitchen, collect the target amount of cheese, bring it to the Deposit Zone in front of the vent, and escape!**

---

## Core Gameplay Mechanics

### 1. Cheese Collection System
* **How it works:** Pieces of cheese spin continuously around their Y-axis in the kitchen to catch your attention. 
* **The Interaction:** When you walk close to a piece of cheese, a proximity trigger detects your presence and prompts you in the Unity Console.
* **Control:** Pressing **`[E]`** while near the cheese picks it up, removes it from the world, and increments your temporary inventory (`heldCheese`).

### 2. Deposit Zone Logic
* **How it works:** There is an invisible, transparent trigger zone set up directly in front of the guarded vent door.
* **Entering the Zone:** When you walk into this box, a prompt appears in your console. 
* **Depositing:** Pressing **`[E]`** while standing inside this zone transfers all the cheese you are currently carrying into the global `depositedScore` tracker, resetting your hand inventory to 0. 

### 3. The Guard Rat & Door Dialogue 
The lazy rat acts as a decorative gatekeeper. Your progress and interaction with the door triggers conditional dialogue states printed straight to the game console:
* **Game Start / Restart:** As soon as the level loads, the rat sets the stakes:
  > *"Rat: 'Pss... gather me all the cheese and I will let you in!'"*
* **Entering the Deposit Zone:** Walking into the invisible area displays your input action:
  > *"Press [ E ] to give cheese"*
* **Insufficient Cheese Deposited:** If you attempt to open the door before reaching the required target score, the rat rejects you and dynamically calculates what's missing:
  > *"Rat: 'Not enough! GET ME X MORE!'"*
* **Target Score Met (5 Cheese Default):** When you successfully deposit the final piece of cheese, the rat relents, and the door's automatic animator opens the Y-axis path to the vent:
  > *"Rat: 'Okay fine. Here you go...'"*

### 4. Spilled Water Hazard (Level Restart)
* **The Threat:** Spilled puddles of water are scattered across the kitchen floor. Because you are a small mouse, falling or stepping into these water hazards is completely fatal!
* **How it works:** The water objects use specialized physical collision meshes tagged as `Hazard`. 
* **The Penalty:** The absolute millisecond your player capsule contacts a water object, the logic triggers a full scene reload via Unity's `SceneManagement`. 
* **The Consequence:** The game immediately wipes clean: you lose all your currently held cheese, your deposited score resets to 0, all collected cheeses instantly respawn back in their original spots in the kitchen, and the rat's opening dialogue prints fresh to the console.

---

## Controls Reference

| **Move Around** | `W` `A` `S` `D` / Arrow Keys | Anywhere in the kitchen |
| **Pick Up Cheese** | **`E`** | While standing inside a spinning cheese collider |
| **Deposit Cheese** | **`E`** | While standing inside the invisible Deposit Zone |

---

## Technical Architecture & Scripts

1. **`PlayerScript.cs`**: The brain of the player character. It tracks inventory (`heldCheese`, `depositedScore`), handles key inputs (`Keyboard.current.eKey`), listens for trigger entries (`OnTriggerEnter`), and controls level reloading upon contacting hazards.
2. **`CollectibleScript.cs`**: Attached to each cheese prefab. It handles the continuous aesthetic spin (`transform.Rotate`) and alerts the Player Script when the player enters or leaves its pick-up range.
3. **`DoorScript.cs`**: Attached to the escape vent door. It receives the command from the player script once the cheese target is satisfied and fires the specific cross-fading triggers (`OpenDoor` / `CloseDoor`) inside the Unity Animator Controller to mechanically swing the door out of the way.

---

### Desired Setup & Settings
* **Unity Runtime:** Built and optimized for **Unity 2022.3 LTS** (using the Input System package).
* **Display Resolution:** 1920x1080 (Full HD) recommended.
* **Aspect Ratio:** 16:9 widescreen display.
* **Input Devices:** Keyboard and Mouse are required.
* **Audio:** Stereo headphones/speakers recommended for audio cues.
