# Project: Prometheus Simulator

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Sam Peterson
-   Section: 05

## Simulation Design

In this simulation, two agents, Man and Gods, interact with each other in the world. Man is scared of the Gods, and the Gods wish to antagonize Man. Using the Mouse, the player can drop fire into the world, causing a reaction from both Man and Gods. The Gods will attempt to snuff out the flame, while Man will attempt to acquire it and bring it back to their villages. Once a Man has a flame, the others will attempt to protect him by flocking around him, while the Gods will attempt to stop the Man who has the flame from returning it to the Village. If it is returned or smothered, both agents revert to their original states.

### Controls

- Move the Camera about the world (Arrow Keys)
- Place Fire (Left Mouse Button)

## MAN

Primitive mankind attempting to acquire and use fire from Prometheus.

### Evade

**Objective:** Avoid the Gods.

#### Steering Behaviors

- Evade - Nearest God
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Man
   
#### State Transistions

- This state will activate once all Fire in the world has been captured or smothered.
   
### Search

**Objective:** Man will search for Fire placed by the player.

#### Steering Behaviors

- Seek - Nearest Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Man
   
#### State Transistions

- Agents will transition to this state when Fire is placed by the player in the world.

### Protect

**Objective:** Man will protect another Man with a flame by flocking around him.

#### Steering Behaviors

- Flock: Nearest Man with Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Man

#### State Transitions

- Man will transition to this State when there is no more fire in the world, but a Man is carrying one

## GODS

The Gods of Olympus attempting to stop the secret of Fire from reaching mankind.

### FLOCK

**Objective:** The Gods will flock together when no flames are in the world.

#### Steering Behaviors

- Flock - Nearest God
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Gods
   
#### State Transistions

- This State can be reached when no fires are in the world.
   
### SEEK

**Objective:** Gods will seek out Flames placed by the Player.

#### Steering Behaviors

- Seek - Nearest Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Gods

#### State Transistions

- Gods reach this state when a flame is placed into the world by the Player

### CHASE

**Objective:** Gods will chase Man that carry flames.

#### Steering Behaviors

- Seek - Nearest Man with Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other instances of Gods

#### State Transitions

- Gods will reach this state when no flames are in the world, but a Man is carrying a flame.

## Sources

-   Background Soil: https://www.freepik.com/free-vector/brown-soil-texture-background_3478044.htm#query=soil&position=0&from_view=keyword

## Make it Your Own

- My game is different for me in the player interaction aspect, in the placement of the fire and the subsequent reaction from both sides in a different manner: Man trying to claim the fire, and Gods trying to snuff it out.

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

