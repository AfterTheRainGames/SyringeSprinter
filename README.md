# Syringe Sprinter  
**Game Jam Submission**  
*LIGHT Theme | Limitation: Speed is Key | June 7-10 2024*

## Overview  
**Syringe Sprinter** is an intense action-stealth game where players must collect syringes scattered across a dangerous environment while being relentlessly chased by an AI-controlled killer. The player must use their speed, evasion skills, and strategic decisions to collect all syringes and escape before the killer catches them.

---

## Key Contributions as a Gameplay Engineer  

### **Player Movement and Evasion**  
- Developed a smooth player movement system with responsive controls for running, jumping, and evading the AI.  
- Implemented a dynamic speed system where the player’s movement speed increases as syringes are collected, encouraging exploration and risk-taking.  
- Added gravity and jumping mechanics for evading obstacles and escaping dangerous situations.  

### **AI Behavior and Tracking**  
- Implemented an advanced AI system that uses raycasting to track the player’s position and initiate a chase when the player is within sight.  
- Designed the AI to adapt to the player’s movements, adding tension to gameplay as the killer dynamically adjusts its strategy based on the player’s location.  
- Created an AI respawn system that sends the killer back to a random spawn point if it loses sight of the player.

### **Syringe Collection System**  
- Designed a syringe collection system where the player collects syringes to increase their movement speed, light range, and intensity.  
- Incorporated a dynamic UI that updates the syringe count, shows collection progress, and displays the timer running while the player is actively avoiding the killer.  
- Added an interaction prompt for collecting syringes when in close proximity.

### **Escape Mechanic**  
- Designed an escape sequence where the player must collect all 12 syringes to unlock a final escape door.  
- Implemented a win condition that triggers once the player collects all syringes and interacts with the door, ending the game and displaying the completion time.

### **Timer and UI Feedback**  
- Created a timer that tracks the time spent by the player during the level, showing detailed minutes, seconds, and milliseconds.  
- Developed an intuitive HUD to display syringe collection progress, timer, and escape objective.

### **Audio Integration**  
- Added sound effects for syringe collection and when the AI catches the player, enhancing the suspenseful atmosphere.  
- Integrated background audio that builds tension and complements the chase theme.

---

## Challenges Overcome  

### **AI Tracking and Evasion**  
- **Issue**: Ensuring the AI consistently tracked the player without losing sight during fast movements was difficult.  
  - **Solution**: Implemented dynamic raycasting and additional checks to ensure that the AI maintains line-of-sight more accurately, even during rapid player movement.

### **Syringe Collection and Speed Scaling**  
- **Issue**: Balancing the syringe collection mechanics with increasing speed required fine-tuning to ensure progression felt rewarding and not too overwhelming.  
  - **Solution**: Carefully adjusted the speed scaling system so that the player’s pace increases gradually but remains manageable, providing both a challenge and a reward.

### **Escape Logic**  
- **Issue**: Triggering the escape sequence only after collecting all syringes without breaking immersion proved challenging.  
  - **Solution**: Created clear visual and audio cues, such as UI updates and sound effects, to notify the player when the escape door is unlocked.

---

## Reflection  
Syringe Sprinter was a thrilling challenge that combined evasion mechanics, AI programming, and time-based objectives into a fast-paced experience. Designing the gameplay systems to create a balance between tension and reward, while ensuring the player's actions were always impactful, was both challenging and rewarding. The final result demonstrates my ability to create engaging systems and manage dynamic gameplay elements under time constraints.

---

## Play the Game  
[Play Syringe Sprinter on Itch.io](https://aftertheraingames.itch.io/syringe-sprinter)
