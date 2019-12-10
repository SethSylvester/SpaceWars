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
The program will output the current information about the store and player.

# User Interface
User Display Menu: Handles the entire game, displays store and takes in input.

# Design Documentation
System Architecture Description

![Flowchart](Flowchart.PNG)

---

### Type 'Game'
Used to represent the actual game, it's the only thing called in program.cs

---

#### Field inMenu
A bool to determine if the user is still in the main menu.

---

#### Field playingGame
A bool to determine if the user is still playing the game.

---

#### Method Run()
Runs the game, mostly used for initalizing other methods.

---

#### Method Save()
Saves the players gold, inventory & the shops inventory.

---

#### Method Load()
Loads the save data()

---

#### Method Title()
The title menu method, brings up the title menu and keeps the player there until they decide to start the game, load the game, or exit prematurely. Calls the StartGame() function on "start game".

---

#### Method StartGame()
A method used for actually running the rest of the game, the method starts with character creation and calls other methods when necessary to ensure the shop actually works.

---

### Type 'Character'
Contains the necessary information for the player character as well as the character creation method, basically everything about the player character is stored here

---

#### Field inventory
Gives the player an inventory array, set to 0 as a default, whenever an item is added the size will increase.

#### Field playerName
String that stores the players name that they decide to input.

---

#### Field playerClass
String that stores the players class, ironically more important than the players name as the shop owner will comment on it the first time they enter the store.

---

#### Field validClass
Bool to ensure the player picks an actual class. They have no choice.

---

#### Field superUser
Bool to check if the player is a superuser.

---

#### Field counter
An integer that counts up when the player inputs something invalid, when it reaches 5 it clears and retypes the statement, just for quality of life so the player doesn't lose their actual input.

---

#### Property Size
Returns the inventory length

---

#### Method characterCreation()
A method used to make the player input a name and a class to create their character with.

---

#### Method GetClass()
Returns the player class, used for the shopkeepers  comment.

---

#### Method GetSuper()
Used to check if the user is a superuser or not.

---

#### Method ValidClassToFalse()
Sets the players valid class bool to false.

### Type 'Inventory'
The inventory management class, used for both the store and player. Its called by instancing it in whatever needs an inventory.

#### Field item
Gets the item class

#### Field Inventory
Constructor that sets the inventory classes parameters to always have an array of a variable size

#### Method Add()
Add function that adds an item to an inventory array and increases the size by one.

#### Method Remove()
Remove function that removes an item from an inventory array and decreases the size by one.

#### Field Inv
Property that returns _inventory, just used for shortening the workload and avoiding a return inventory function.

---

#### Field _gold
An integer to be forever overwritten by the property of gold.

---

#### Property Gold
A property used for managing the players gold, public so the actual variable doesn't have to be.

---

### Type 'Item'
A class used to manage and construct new items.

---

#### Field _name
A string to set the items name.

---

#### Field _cost_
A string to set the items cost.

---

#### Field _description
A string to set the items description.

---

#### Method GetName()
Returns the items name

---

#### Method GetCost()
Returns the items cost

---

#### Method GetDesc()
Returns the items description

---

### Type 'Potion'
A subclass of item, used to construct potions

---

#### Method Potion()
Constructor used to create potions.

---

### Type 'Program'
Main program class, used to call the game class

---

#### Field game
Calls the game class

---

#### Method game.Run()
Runs the game

---

### Type 'Store'
The store class and where alot of the game actually takes place, since this is a store RPG after all.

---

#### Field inventory
The stores inventory, used to give it an inventory to be able to sell objects from.

---

#### Field item
Grabs the item class

---

#### Field _player
Passes the player into the store class.

---

#### Method Store()
Used to set the player field to the actual player created.

---

#### Method Start()
Adds in the table of items that are initally avalible to the player.

---

#### Method Buy()
Displays all items in the shops inventory, if the player selects to buy an item the store checks the players gold and inventory size, if applicable the player is then granted the item by removing it from the shop inventory and placing it in the player inventory.

---

#### Method Cost()
Subtracts the money from the player, the amount depends on the item value.

---

#### Method Gain()
Adds money to the player, the amount depends on the items value.

---

#### Method Sell()
Displays all items in the players inventory, if the player has an item and chooses to sell it they gain its value back. The sell Method removes an item from the players inventory and adds it to the shop inventory.

---

### Type 'Weapon'
Used to add the damage field and constructor for weapons.

---

#### Field _damage
Int damage value of weapons.

---

#### Method Weapon()
Constructor for creating new weapons, intakes fields from the item class.
---
