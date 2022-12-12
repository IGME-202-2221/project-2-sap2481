# Project: Prometheus Simulator

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Sam Peterson
-   Section: 05

## Simulation Design

In this simulation, two agents, Man and Gods, interact with each other in the world. Man is scared of the Gods, and the Gods wish to antagonize Man. Using the Mouse, the player can drop fire into the world, causing a reaction from both Man and Gods. The Gods will attempt to snuff out the flame, while Man will attempt to acquire it for themselves. Once a Man has a flame, the others will attempt to protect him by flocking around him, while the Gods will attempt to stop the Man who has the flame. If the Gods stop the Man with the flame, the Man is destroyed. If a 10-second timer runs out, the fire is claimed for mankind. As a result of either, both Man and Gods return to their default states, and the player may drop another flame into the world.

### Controls

- Place Fire (Left Mouse Button)

## MAN

Primitive mankind attempting to acquire and use fire from Prometheus.

### Wander

**Objective:** Roam the world, avoiding the Gods

#### Steering Behaviors

- Wander
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other agents
   
#### State Transistions

- This state will activate once all Fire in the world has been captured or smothered. It is also the default state for Man.
   
### Seek

**Objective:** Man will seek out the Fire placed by the player.

#### Steering Behaviors

- Seek - Player-placed Fire
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other Agents
   
#### State Transistions

- Man will transition to this state when Fire is placed by the player in the world.

### Flock / Protect

**Objective:** Man will protect another Man with a flame by flocking around him.

#### Steering Behaviors

- Flock: Nearest Man with Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other agents

#### State Transitions

- Man will transition to this State when there is no more fire in the world, but a Man is carrying a flame.

## GODS

The Gods of Olympus attempting to stop the secret of Fire from reaching mankind.

### Wander

**Objective:** The Gods will roam the world, avoiding Man.

#### Steering Behaviors

- Wander
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other agents
   
#### State Transistions

- This State can be reached when no fires are in the world. It is also the default state for Gods.
   
### SEEK

**Objective:** Gods will seek out Flames placed by the Player.

#### Steering Behaviors

- Seek - Nearest Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other agents.

#### State Transistions

- Gods reach this state when a flame is placed into the world by the Player

### PURSUE

**Objective:** Gods will chase the Man that carries a flame.

#### Steering Behaviors

- Seek - Nearest Man with Flame
- Obstacles - Will avoid rocks, trees, and other obstacles in the world
- Seperation - Will Separate from all other agents.

#### State Transitions

- Gods will reach this state when no flames are in the world, but a Man is carrying a flame.

## Sources

- Background Soil: https://www.freepik.com/free-vector/brown-soil-texture-background_3478044.htm#query=soil&position=0&from_view=keyword
- Fire: https://pngtree.com/freepng/orange-red-flame-flame-clipart_5566526.html
- Caveman: https://www.shareicon.net/man-prehistoric-stone-age-troglodyte-primitive-people-avatar-769351
- God: https://www.dreamstime.com/greek-god-zeus-ancient-greek-god-sculpture-philosopher-face-zeus-triton-neptune-logo-design-greek-god-zeus-line-art-logo-ancient-image214874175

## Make it Your Own

- My game is different for me in the player interaction aspect, in the placement of the fire and the subsequent reaction from both sides in a different manner: Man trying to claim the fire, and Gods trying to snuff it out. I also utilize steering behaviors in unique ways, such as the use of Flocking to protect a Man who is carrying a flame.

## Known Issues

- Occasional issues with Bounds, in which an agent gets stuck on the edge of the screen

### Requirements not completed

N/A

