using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the Creature class to represent entities in the game
    public class Creature
    {
        // Properties to store a Creature's current and maximum hit points
        public int CurrentHitPoints { get; set; }
        public int MaximumHitPoints { get; set; }

        // Constructor to initialize a new instance of Creature
        public Creature(int currentHitPoints, int maximumHitPoints)
        {
            CurrentHitPoints = currentHitPoints;
            MaximumHitPoints = maximumHitPoints;
        }
    }

    // Define the Player class which inherits from the Creature class
    public class Player : Creature
    {
        // Properties to store the player's essence, experience points, level, current location in the game, inventory items, and quests
        public int Essence { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public Location CurrentLocation { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        // Constructor to initialize a new instance of Player and invoke the base Creature constructor
        public Player(int currentHitPoints, int maximumHitPoints, int essence, int experiencePoints, int level) : base(currentHitPoints, maximumHitPoints)
        {
            Essence = essence;
            ExperiencePoints = experiencePoints;
            Level = level;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        // Method to check if the player has the required item to enter a specified location
        public bool HasRequiredItemToEnterThisLocation(Location location)
        { 
            // If no item is required to enter, return true
            if(location.ItemRequiredToEnter == null)
            {
                return true;
            }
            // Check each item in the player's inventory
            foreach (InventoryItem ii in Inventory)
            {
                // If the player has the required item, return true
                if(ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    return true;
                }
            }
            // If the required item is not found, return false
            return false;
        }

        // Method to check if the player has a specific quest
        public bool HasThisQuest(Quest quest)
        {
            // Iterate through each quest in the player's quest list
            foreach(PlayerQuest playerQuest in Quests)
            {
                // If the player's quest ID matches the specified quest ID, return true
                if(playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }
            // If the quest is not found, return fals
            return false;
        }

        // Method to check if the player has completed a specific quest
        public bool CompletedThisQuest(Quest quest)
        {
            // Iterate through each quest in the player's quest list
            foreach(PlayerQuest playerQuest in Quests)
            {
                // If the player's quest ID matches the specified quest ID, return the completion status
                if(playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }
            // If the quest is not found or not completed, return false
            return false;
        }

        // Method to check if the player has all items required to complete a specific quest
        public bool HasAllQuestCompletionItems(Quest quest)
        {
            // Iterate through each quest completion item required for the quest
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                // Assume the item is not in the player's inventory initially
                bool foundItemInPlayersInventory = false;

                // Check each item in the player's inventory
                foreach(InventoryItem ii in Inventory)
                {
                    // If the player has the required item
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        foundItemInPlayersInventory = true;
                        
                        // If the player's item quantity is less than required, return false
                        if(ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }
                    }
                }

                // If the required item is not found in the player's inventory, return false
                if(!foundItemInPlayersInventory)
                {
                    return false;
                }
            }
            // If all required items are found with the right quantity, return true
            return true;
        }

        // Method to remove quest completion items from the player's inventory
        public void RemoveQuestCompletionItems(Quest quest)
        {
            // Iterate through each quest completion item required for the quest
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                // Check each item in the player's inventory
                foreach(InventoryItem ii in Inventory)
                {
                    // If the player has the required item
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        // Decrease the quantity of the item in the player's inventory
                        ii.Quantity -= qci.Quantity;
                        break; // Exit the inner loop once the item is found and the quantity is adjusted
                    }
                }
            }
        }

        // Method to add an item to the player's inventory
        public void AddItemToInventory(Item itemToAdd)
        {
            // Check each item in the player's inventory
            foreach(InventoryItem ii in Inventory)
            {
                // If the item is already in the inventory, increase its quantity
                if(ii.Details.ID == itemToAdd.ID)
                {
                    ii.Quantity++;

                    return; // Exit the the method after updating the quantity
                }
            }

            // If the item is not found in the inventory, add it as a new inventory item with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }
        
        // Method to mark a specific quest as completed
        public void MarkQuestCompleted(Quest quest)
        {
            // Iterate through each quest in the player's quest list
            foreach(PlayerQuest pq in Quests)
            {
                // If the player's quest ID matches the specified quest ID, mark it as completed
                if(pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;

                    return; // Exit the method after marking the quest as completed
                }
            }
        }
    }

    // Define the Zombie class which inherits from the Creature class
    public class Zombie : Creature
    {
        // Properties to store the zombie's ID, name, encounter description, maximum damage, reward experience points, reward essence, and loot table
        public int ID { get; set; }
        public string Name { get; set; }
        public string Encounter { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardEssence { get; set; }
        public List<LootItem> LootTable { get; set; }

        // Constructor to initialize a new instance of a zombie and invoke the base Creature constructor
        public Zombie(int id, string name, string encounter, int maximumDamage, int rewardExperiencePoints, int rewardEssence, int currentHitPoints, int maximumHitPoints) : base(currentHitPoints, maximumHitPoints)
        {
            ID = id;
            Name = name;
            Encounter = encounter;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardEssence = rewardEssence;
            LootTable = new List<LootItem>();
        }
    }
}
