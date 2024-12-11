Zombie Wasteland Game Project
Description
Zombie Wasteland is a survival adventure game set in a post-apocalyptic world teeming with zombies. Players navigate through various locations, complete quests, manage their inventory, and engage in combat with different types of zombies. The game is designed with a clear separation between the User Interface (UI) and the game logic, promoting modularity and maintainability.

Project Structure
This solution consists of two main projects:

UI Project (User Interface):

Handles all visual elements and player interactions.

Displays player stats, inventory, quest information, and game messages.

Captures player inputs and updates the interface based on game events.

Logic Project (Game Logic):

Manages the core game mechanics and calculations.

Handles player attributes, inventory management, quest progression, and combat mechanics.

Processes game rules and outcomes based on player actions and game state.

Key Features
Exploration and Quests: Players can explore different locations, encountering zombies and completing quests.

Combat System: Engage in combat with various types of zombies, each with unique attacks and behaviors.

Inventory Management: Collect and manage items such as weapons and energy drinks to aid in survival.

Dynamic UI: An interactive user interface that updates in real-time based on game events.

Classes and Methods
Key Classes:

Player: Manages player attributes and actions.

Zombie: Defines zombie characteristics and behavior.

InventoryItem: Manages items in the player's inventory.

Weapon and EnergyDrink: Special types of inventory items with specific attributes.

Key Methods:

MoveTo(): Handles player movement between locations.

UpdateInventoryListInUI(): Updates the inventory display.

btnUseWeapon_Click(): Manages player attacks on zombies.

btnUseEnergyDrink_Click(): Handles the use of energy drinks to heal the player.

ScrollToBottomOfMessages(): Ensures the latest messages are visible in the UI.
