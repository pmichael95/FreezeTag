# ABOUT

A simple Unity3D application to demonstrate AI movement techniques in a finite space.

This was built using Unity 5.6.3f1, but is compatible with earlier and later versions.

Please open this project and press the play button while on MainScene.unity.

Note that for integrity's sake, I've kept the original scenes demonstrating the Omega Fighter asset, but these do not reflect the project.


# CONTROLS

All movement is automated. The Main Camera is fixed. 

To change from Kinematic to Steering mode, please press T as indicated, and the mode will update and display.


# BEHAVIORS

This project features Kinematic behaviors, notably:

- Kinematic Arrive;

- Kinematic Seek;

- Kinematic Flee;

- Kinematic Align.


Additionally, this project also features Steering behaviors, such as:

- Steering Arrive/Pursue;

- Steering Seek;

- Steering Flee;

- Steering Align.


There is also a 'Wander' behavior implemented, applicable to both physics modes.

Additionally, the game of Freeze Tag is present. The tagged player is selected at random at each play.

The tagged player will seek and chase the closest target at the time he's assigned the tagged role.

When he tags (freezes) a target, he will then seek the next closest one. 

Frozen targets cannot move. However, unfrozen characters may unfreeze the frozen ones.

To achieve that, Not Frozen units will actively seek the Frozen units, if any.


# ASSETS SOURCES

SkyBox (Purple Nebula): https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-purple-nebula-2967


Omega Fighter (Space Ship): https://assetstore.unity.com/packages/3d/vehicles/space/sci-fi-spaceship-omega-fighter-19137
