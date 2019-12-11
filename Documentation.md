# Description of the Problem
Name: Graphical Test App

Problem: 

Evidence that includes:

* Submitted executable for Graphical Test Application that makes use of your maths classes to implement the following:
* Example of matrix hierarchy to manipulate visible elements
* Example of game objects moving using velocity and acceleration with vectors
* Example of simple collision detection


# Input information

All input is done through the keyboard.

* W - Moving Upwards in game and upwards in the menu.
* S - Moving Downwards and downwards in the menu.
* A - Moving Left
* D - Moving Right

Spacebar - Firing the players gun

E - Used for selection on the menu

# Output information
The program will output the player, every projectile on the screen as well as the players stats.

# User Interface
Graphical Test Application: Handles the entire game, displays the game.

# Design Documentation
System Architecture Description

![Flowchart](Flowchart.PNG)

---
# Imported Classes

* ### AABB 

Provides an AABB hitbox for everything that might need one.
* ### Actor

Used for the scene and as the basis for almost everything in the game.
* ### Entity

Used for entities such as the player and enemy.
* ### Game

Creates and runs the game
* ### Input

Handles input from the keyboard
* ### Matrix3

A 3 Dimensional Matrix
* ### Matrix4

A 4 dimensional matrix
* ### Timer

A timer class that acts as a stopwatch
* ### Vector3

A 3 dimensional vector
* ### Vector4

A 4 dimensional vector

* ### RayLib
The imported library that runs most of the game.

---

## Type 'BossFightController'
The mastermind behind most of the game, the Boss fight controller handles the AI, calls the various phases off the boss, and spawns in it's weapons.

---

#### Field _root
Type: Actor

Desc:

---

#### Field _phaseOne
Type: Actor

Desc: An empty actor used to hold the first phase of the bossfight. When it's phase is selected it is instantiated and called.

---

#### Field _phaseTwo
Type: Actor

Desc: An empty actor used to hold the second phase of the bossfight. When it's phase is selected it is instantiated and called.

---


#### Field _phaseThree
Type: Actor

Desc: An empty actor used to hold the third phase of the bossfight. When it's phase is selected it is instantiated and called.

---

#### Field _phaseFour
Type: Actor

Desc: An empty actor used to hold the fourth phase of the bossfight. When it's phase is selected it is instantiated and called.

---


#### Field _phaseFive
Type: Actor

Desc: An empty actor used to hold the fifth phase of the bossfight. When it's phase is selected it is instantiated and called.

---

#### Field _phaseSix
Type: Actor

Desc: An empty actor used to hold the six phase of the bossfight. When it's phase is selected it is instantiated and called.

---

#### Field _phaseSeven
Type: Actor

Desc: An empty actor used to hold the seven phase of the bossfight. When it's phase is selected it is instantiated and called.

---

#### Field CurrentPhase
Type: Actor Property

Desc: Gets and sets the currentphase so the spawners always spawn turrets properly.

---

#### Field _phaseOneStarted
Type: Bool

Desc: A bool used to check if phase one has been started.

---

#### Field _phaseTwoStarted
Type: Bool

Desc: A bool used to check if phase two has been started.

---

#### Field _phaseThreeStarted
Type: Bool

Desc: A bool used to check if phase three has been started.

---

#### Field _phaseFourStarted
Type: Bool

Desc: A bool used to check if phase four has been started.

---

#### Field _phaseFiveStarted
Type: Bool

Desc: A bool used to check if phase five has been started.

---

#### Field _phaseSixStarted
Type: Bool

Desc: A bool used to check if phase six has been started.

---

#### Field _phaseSevenStarted
Type: Bool

Desc: A bool used to check if phase seven has been started.

---

#### Field _attacking
Type: Bool

Desc: A bool used to check if the enemy is currently attacking with one of its beam attacks.

---

#### Field _attackNum
Type: Byte

Desc: A byte used to alternate between two different attacks.

---

#### Field _enemyCenterSprite
Type: Sprite

Desc: The main enemies sprite

---

#### Field _enemyCenter
Type: Enemy

Desc: The main enemy.

---

#### Field _spinner
Type: Spinner

Desc: A Spinner used to allow things to orbit around the main enemy.

---

#### Field _attackTimer
Type: Timer

Desc: A timer used to allow for a delay between certain attacks

---

#### Field _attackTimer2
Type: Timer

Desc: A timer used to allow for a delay between certain attacks

---

#### Field CutScene
Type: Static Bool

Desc: A bool used to disable attacks and damage while the cutscene is displaying.

---
### Method StartUp()
The Startup method is used to initiate the boss and place it into the game.

---
### Method PhaseChecker()
Every update this method checks to make sure the current phase hasn't changed and if it has to call the appropriate phase.

---
### Method PhaseOne/-/SevenClear()
Clears the current phase by removing the actor as a child from the main enemy.

---
### Method PhaseOne/-/SevenStart()
Starts the current phase by adding it to a child of the center enemy.

---
### Method PhaseOne/-/Seven()
Spawns the current phases weapons to the enemy.

---
### Method SpawnReverseTurret()
Spawns a 360 turret that fires before the bullets converge on their point of origin and disappear.

---
### Method SpawnReverseTurret2()
Spawns a 360 turret.

---
### Method GunShipPlatforms()
Spawns Phase ones gunship platforms.

---
### Method SpawnShotgun()
Spawns a load of turrets slightly angled that fire in a cone.

---
### Method SpawnWiggleTurret()
Spawns a turret that appears to "wiggle" slightly side to side

---
### Method SpawnRotatingTurret()
A version of the wiggle turret that turns much more.

---
### Method SpawnRotationGun()
Spawns spinning firebars that descend slowly.

---
### Method BeamWarning()
Gives a warning of the direction of an incoming beam attack.

---
### Method BeamAttack1/2()
Beams descend from the sky to attack the player giving slight gaps between them.

---
### Method BeamLadder()
A horizontal beam descends from the left side while a second horizontal beam ascends from the right side.

---
## Type 'Collectable'
Used for coins or powerups. Currently only one powerup was implemented.

---

#### Field _hitbox
Type: AABB

Desc: The hitbox for detecting collision with the player.

---

#### Field _type
Type: String

Desc: A string for the type of boon the powerup will give

---

#### Field _sprite
Type: Sprite

Desc: An uninstantiated sprite for the powerup.

---

#### Field _timer
Type: Timer

Desc: A timer to determine how long the powerup lasts.

---
### Method Touch()
Every update the powerup checks to see if the player has grabbed it.

---
### Method PowerUp()
Applies the powerup based on the 'type' field

---
## Type 'Cursor'
The menu cursor to allow for selection of the listed options and interfacing with the menu.

---

#### Field pos
Type: Byte

Desc: A byte that contains the number refrence of which option you are currently selecting.

---

#### Field maxPos
Type: Byte

Desc: A byte that limits the amount the cursor can move.

---

#### Field _called
Type: bool

Desc: A workaround bool to prevent the menu from being called twice for unknown reasons.

---

#### Field selected
Type: bool

Desc: A bool that determines if the player has selected e on the menu.

---
### Method Controls()
If any keys are pressed, if they are valid something happens.

---
### Method MenuUp()
If the cursor is allowed, it moves up

---
### Method MenuDown()
If the cursor is allowed, it moves down

---
## Type 'Enemy'
This class contains the parameters for the main boss enemy.

---
#### Field _hp_
Type: int

Desc: An integer that acts as the health for the boss.

---
#### Field HP
Type: int

Desc: A property that is just a get for _hp.

---
#### Field Speed
Type: float

Desc: Manages how fast the enemy moves.

---
#### Field _moveLeft_
Type: bool

Desc: A bool that determines if the enemy needs to move left or right

---
#### Field _moveUp
Type: bool

Desc: A bool that determines if the enemy needs to move up or down

---
#### Field _phase_
Type: int

Desc: An integer that determines the phase of the boss.

---
#### Field _maxPhase_
Type: int

Desc: A an integer that caps _phase.

---
#### Field Phase
Type: int

Desc: A property that is just a get for _phase.

---
#### Field _healing
Type: bool

Desc: A property that determines if the boss is invicible & healing.

---
#### Field __mainTimer_
Type: Timer

Desc: A timer used for healing and certain attacks.

---
#### Field __hitBox_
Type: AABB

Desc: The hitbox

---
#### Field HitBox
Type: AABB

Desc: gets the hitbox

---
#### Field _root_
Type: Actor

Desc: Grabs the root, used for shoots.

---
### Method StartUp()
Sets up the boss for the cutscene.

---
### Method HealthBar()
Manages the healthbar

---
### Method Heal()
Healing method that also makes the boss invincible.

---
### Method Move()
Tells the boss to bounce off the screen durring phase 6

---
### Method Move[Up/Down/Left/Right[()
Handles the boss moving in a direction

---
### Method TakeDamage()
Tells the boss to take damage if allowed.

---
## Type 'Gun'
This class contains the parameters for the main boss enemy.

---
#### Field _root
Type: Actor

Desc: Grabs the root, used for shoots.

---
### Method Shoot()
Shoots a bullet.

---
## Type 'Interface'
The players interface class.

---

### Method InterfaceDraw()
Draws the interface constantly.

---
### Method SetHp()
Constantly checks and sets the players health.

---
## Type 'Ladder'
A big ol' hitbox that travels downwards to hit the player.

---
#### Field _speed
Type: float

Desc: The ladders speed when it travels downwards.

---
#### Field _hitbox
Type: AABB

Desc: The hitbox for detecting collision with the player.

---
### Method Touch()
Checks for collision with the player and damages them.

---
### Method Touch2()
Same as touch but instantly kills the player.

---

### Method MoveUp/Down/Left/Right()
Moves the ladder in the direction specified.

---
### Method MoveDownFast()
Moves down fast

---
### Method MoveDownVeryFast()
Moves down faster.

---
## Type 'Menu'
The main menu and its sub menus

---
#### Field _spriteDisplay
Type: Sprite

Desc: Displays the sprites of the customization menu

---
#### Field _cursor
Type: Cursor

Desc: A cursor for the menu.

---
#### Field _choice
Type: byte

Desc: the players current choice and selection in the menu.

---
#### Field _startingStage
Type: byte

Desc: The phase that the boss starts at.

---
#### Field _mainMenu
Type: bool

Desc: A bool for checking to see if the menu is at the main menu.

---
#### Field _stageSelect
Type: bool

Desc: A bool for checking to see if the menu is at the stage selection menu.

---
#### Field _customizeMenu
Type: bool

Desc: A bool for checking to see if the menu is at the customization menu.

---
#### Field _weenieMode
Type: bool

Desc: A bool for checking to see if the player has activated easy mode.

---
#### Field _selectedSkinNum
Type: byte

Desc: A byte for selecting the skin

---
#### Field []skins
Type: string[]

Desc: A string array that holds the various skins

---
#### Field []menuText
Type: string[]

Desc: A string array for the menus text

---
### Method TextInitalizer()
Sets everything in the _menuText[] to reduce clunkiness from the constructor.

---
### Method Selected()
Handles player input in the menu

---
### Method MenuDraw()
A massive function that draws the menu.

---
### Method StartGame()
Tells the game it needs to start and deletes the menu.

---
## Type 'Player'
The players class and all functions relating to them.

---
#### Field _hp
Type: int

Desc: The players HP

---
#### Field _iFrames
Type: bool

Desc: A bool to make the player invicible for half a second.

---
#### Field _iframesTimer
Type: Timer

Desc: A timer for the iframes

---
#### Field _palyerGun
Type: Gun

Desc: Lets the player shoot

---
#### Field _canShoot
Type: bool

Desc: A bool for if the player can shoot

---
#### Field _currentGun
Type: string

Desc: String for the type of gun the player has

---
#### Field shootSpeed
Type: float

Desc: The speed in which the player can shoot

---
#### Field _[upper/lower/left/right]Limit
Type: float

Desc: The boundries that the player can move, made into variables for ease of editting.

---
#### Field Speed
Type: float

Desc: How fast the player moves

---
#### Field DiagonalSpeed
Type: float

Desc: Calculates the players speed when moving diagonally

---
#### Field _instance
Type: static Player

Desc: A static instance of the player used mostly for the hitbox

---
#### Field _hitbox
Type: AABB

Desc: The players hitbox

---
#### Field HitBox
Type: AABB

Desc: A public property refrence to the players hitbox

---
#### Field _shootTimer
Type: Timer

Desc: A timer used in conjunction with shoot time to limit how often the player can fire.

---
#### Field Instance
Type: static player

Desc: A property to refrence _instance

---
#### Field _interface
Type: Interface

Desc: A refrence to the interface

---
### Method Shoot()
Shoots if the player can

---
### Method IFramesOff()
Turns the Iframes off if they're on and need to be set to off.

---
### Method Move()
The master move function for the player

---
### Method StatCount()
Tells the interface what the players current stats are.

---

### Method TakeDamage()
The player takes damage.

---
### Method IFrames()
The player gets IFrames that make it invincible

---
### Method Die()
Kills the player and removes them from the scene.

---
## Type 'Projectile'
The master projectile class for any projectiles that move.

---
#### Field _timer
Type: Timer

Desc: A timer used for various functions

---
#### Field Rotation
Type: float

Desc: The projectiles rotation, usually gotten from the turret it fires from

---
#### Field _speed
Type: float

Desc: How fast the projectile moves

---
#### Field Speed
Type: float

Desc: A public property to refrence _speed

---
#### Field YAccelerate
Type: float

Desc: An accelerator used for the rocket projectile.

---
#### Field _friendly
Type: bool

Desc: Prevents the player from hitting themselves.

---
### Method TouchDetection()
Detects if the projectile is colliding with a hitbox.

---
### Method DeletionTimer()
Deletes the projectile after a certain time if applicable.

---
### Method RocketDown()
The rocket accelerates downwards.

---
### Method MoveUp/Down/Left/Right()
The projectile moves lineraly in a direction

---
### Method MoveReverseDown/Up/Right/Left()
The projectile moves linearly before turning around and returning to its point of origin.

---
## Type 'Spinner'
An invisible disk that spins

---
#### Field _speed
Type: float

Desc: How fast the spinner moves if it does at all.

---
### Method Orbit()
Tells the spinner to spin in place

---
### Method FastOrbit()
Tells the spinner to spin in place fast

---
### Method MoveDown()
If applicable it tells the spinner to move downwards.

---
## Type 'Turret'
An invisible disk that spins

---
#### Field _root
Type: Actor

Desc: Refrences the root for shooting.

---
#### Field _gun
Type: Gun

Desc: Lets the turret shoot

---
#### Field _[gun/shotgun/rocket]FireInterval
Type: float

Desc: An interval used to prevent projectile spamming by limiting how often the gun is allowed to fire.

---
#### Field _rotateLeft
Type: bool

Desc: a bool used to determine if a rotation turret needs to rotate left or right

---
#### Field _wiggleLeft
Type: bool

Desc: a bool used to determine if a wiggle turret needs to rotate left or right

---
#### Field _rotation
Type: float

Desc: A rotation property and float used for rotating the turret.

---
#### Field _timer
Type: Timer

Desc: A timer used for intervals between shots

---
### Method TurretRotation()
Handles turret rotation.

---
### Method RotateLeft/Right()
Rotates in the direction specified.

---
### Method TurretWiggle()
Handles "Wiggling" which is small rotations

---
### Method WiggleLeft/Right()
Tells the turret to take mini rotations in the direction specified.

---
### Method Shotgun()
Tells the shotgun to fire

---
### Method FireGun/Rocket/GunReverse/GunReverse2()
Tells the selected gun to fire

---