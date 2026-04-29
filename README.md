# GDIM33 Vertical Slice
## Milestone 1 Devlog

1. In the graph HealthStateManager, it will the player's health between Health, Critical, and Die. The graph will read the data from PlayerHealthManager by calling the GetHealth() function, and compare between a given number(Currently 50). If the health is less than this number, the player will goes into Critical state, and disable the player's run function. In the Critical state, If the health is bigger than 50, It will goes back to health state, and if the health is less or equal to 0, It will goes to Die state and end the game. I haven't complete this logic, Currently will only play a Reminder to remind player the health is low.
2. 
   
   I added some state machine. Including AimStateManager, MovementStateManager, MentalStateManager, HealthStateManager, and GameStateManager. AimStateManager, MovementStateManager are written in c#, and MentalStateManager, HealthStateManager is written in graph. I added fire mode to gun, and change the element of UI.
   The AimStateManager contains two state, Aim and Hipfire. This will control weather the crosshair appear, and the FOV, will change state if player press RMB. The MovementStateManager contains four states, Idle, Walk, Run, Crouch. This will controls the animation of the player, and the speed of the player. 

## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- [Movement Animations](https://www.mixamo.com/#/?page=1&type=Motion%2CMotionPack)
- [Guns and Player](https://assetstore.unity.com/packages/3d/animations/tactical-fps-animations-311410)
