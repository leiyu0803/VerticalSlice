# GDIM33 Vertical Slice
## Milestone 1 Devlog

1. In the graph HealthStateManager, it will the player's health between Health, Critical, and Die. The graph will read the data from PlayerHealthManager by calling the GetHealth() function, and compare between a given number(Currently 50). If the health is less than this number, the player will goes into Critical state, and disable the player's run function. In the Critical state, If the health is bigger than 50, It will goes back to health state, and if the health is less or equal to 0, It will goes to Die state and end the game. I haven't complete this logic, Currently will only play a Reminder to remind player the health is low.
2. <img width="960" height="720" alt="Untitled drawing (2)" src="https://github.com/user-attachments/assets/3af74bb8-ef60-4ed0-b827-927057be751f" />

   
   I added some state machine. Including AimStateManager, MovementStateManager, MentalStateManager, HealthStateManager, and GameStateManager. AimStateManager, MovementStateManager are written in c#, and MentalStateManager, HealthStateManager is written in graph. I added fire mode to gun, and change the element of UI.
   The AimStateManager contains two state, Aim and Hipfire. This will control weather the crosshair appear, and the FOV, will change state if player press RMB. The MovementStateManager contains four states, Idle, Walk, Run, Crouch. This will controls the animation of the player, and the speed of the player. 

## Milestone 2 Devlog

### Question 1

1. Let enemy have animation
   1. Create an Animator and attach it to the enemy
   2. Import the Idle animation and set it to loop
   3. Check to see if it is working properly
2. Add additional animation states
   1. Import anger, attack, and death animations
   2. Add Transition
   3. Add parameters
3. Link enemy animations to enemy states
   1. Add transition conditions
   2. Adjust parameters in the script
   3. Check to see if it is working properly
   4. Adjust the animation speed to better match the enemy's movements

### Question 2

Yes, this has been very helpful. It provides a clear roadmap for how to animate enemies and allows me to confirm more quickly whether I’m doing it right. If I were to do this again, I would break each step down into even finer details. The current steps aren’t quite detailed enough.

### Question 3

I use SetBool() and SetTrigger() in my script to adjust the Animator's parameters. This allows me to control the enemy's animation state using code.

Related scripts: `AIManager.cs`, `EnemyHealth.cs`

Graph screenshot:

<img width="1610" height="920" alt="image" src="https://github.com/user-attachments/assets/c3353ee0-43ab-4e38-a805-7db1fb71b090" />


### Question 4

Enemy Animator. Includes idle, agitated, moving, attacking, and death states. Related content can be found in `AIManager.cs`, `EnemyHealth.cs`, and the enemy's Animator Controller.

## Milestone 3 Devlog

### Question 1

I added two shader graph. First graph creates a outline for the enemy. This effect will only appear when enemy is being scanned. I use a normalized Object Vector multiply by 0.005, then add the object's location. Also, I make the render face to back, so this will only act like a outline but not block the enemy. Second graph is for the block in the level. This shader will only take the block's word space, ignore the mesh. So I can change the scale of the block with out stretch the texture.

<img width="965" height="425" alt="image" src="https://github.com/user-attachments/assets/4ee672ef-af7c-4258-863f-2d7e6d374556" />

<img width="879" height="516" alt="image" src="https://github.com/user-attachments/assets/2afaaa8a-1438-4bc7-b51d-eeb2f7e6ebdb" />

### Question 2

I changed some UI element. Now the UI will not block the cross hair. 

I added the health, attack speed and walk speed of the enemy, since many tester think the enemy is too easy to kill. Now player need 20 bullet to kill the enemy.

I changed the color of the enemy outline. Now the outline will not like like the same color of the crosshair when player is aiming at the enemy.

### Question 3

I added the scan effect. Player can press 5 to use a battery to scan the enemy. When the enemy is being scanned, It will show a outline and have a frame (Like a wall hack). The health of the enemy will also shown.

I added fire mode switch. Player can press b to switch between full auto and semi auto. This change will effect on UI.

## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- [Movement Animations](https://www.mixamo.com/#/?page=1&type=Motion%2CMotionPack)
- [Guns and Player](https://assetstore.unity.com/packages/3d/animations/tactical-fps-animations-311410)
