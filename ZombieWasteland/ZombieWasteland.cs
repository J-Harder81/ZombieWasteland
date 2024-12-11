using GameEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieWasteland
{
    // Define the ZombieWasteLand class which is a form for the user interface
    public partial class ZombieWasteland : Form
    {
        // Private fields for the player and the current zombie
        private Player _player;
        private Zombie _currentZombie;
        
        // Constructor to initialize a new instance of ZombieWasteland
        public ZombieWasteland()
        {
            // Initialize UI components
            InitializeComponent();

            // Intitialize the player with specific values for current and maximum hit points, essence, experience and level
            _player = new Player(10, 10, 20, 0, 1);
            
            // Move the player to the Home Base location
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME_BASE));
            
            // Add a baseball bat to the player's inventory
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_BASEBALL_BAT), 1));

            // Update UI labels with the player's current stats
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblEssence.Text = _player.Essence.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        // Event handlers for clicking the direction buttons, moves the player to an adjacent location based on direction (North, East, West, Sout)
        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        // Method to handle player movement during gameplay
        private void MoveTo(Location newLocation)
        {
            // Check if the player has the required item to enter the new location
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += "You must find the " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();
                return;
            }

            // Update the player's current location
            _player.CurrentLocation = newLocation;

            // Update the visibility of direction buttons based on available adjacent locations
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            // Update the location description in the UI
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            // Handle quests available at the new location
            if (newLocation.QuestAvailableHere != null)
            {
                // Check if the player already has the quest and completed the quest
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                if (playerAlreadyHasQuest)
                {
                    if (!playerAlreadyCompletedQuest)
                    {
                        // Check if the player has all items to complete the quest
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Notify the player that they have completed the quest
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You completed the '" + newLocation.QuestAvailableHere.Name + "' quest!" + Environment.NewLine;

                            // Remove the quest completion items from the player's inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            // Reward the player for completing the quest
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardEssence.ToString() + " essence" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;
                            ScrollToBottomOfMessages();

                            // Update the player's experience points, essence, and inventory
                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Essence += newLocation.QuestAvailableHere.RewardEssence;
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            // Mark the quest as completed
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
                    }
                }
                else
                {
                    // Notify the player that they have received a new quest with the quest description
                    rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += Environment.NewLine;

                    // Inform the player of the required items to complete the quest
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;
                    ScrollToBottomOfMessages();

                    // Add the new quest to the player's quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }

            // If there is a zombies available at the new location
            if (newLocation.ZombieLivingHere != null)
            {
                // Notify the player of a zombie encounter
                rtbMessages.Text += "ZOMBIE ENCOUNTER: " + newLocation.ZombieLivingHere.Name + Environment.NewLine;
                rtbMessages.Text += newLocation.ZombieLivingHere.Encounter + Environment.NewLine;
                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();

                // Retrieve the standard zombie details by its ID
                Zombie standardZombie = World.ZombieByID(newLocation.ZombieLivingHere.ID);

                // Create a new instance of the current zombie based on the standard zombie details
                _currentZombie = new Zombie(standardZombie.ID, standardZombie.Name, standardZombie.Encounter, standardZombie.MaximumDamage,
                    standardZombie.RewardExperiencePoints, standardZombie.RewardEssence, standardZombie.CurrentHitPoints, standardZombie.MaximumHitPoints);

                // Add loot items to the current zombie's loot table
                foreach (LootItem lootItem in standardZombie.LootTable)
                {
                    _currentZombie.LootTable.Add(lootItem);
                }

                // Make weapon and energy drink options visible during combat
                cboWeapons.Visible = true;
                cboEnergyDrinks.Visible = true;
                btnUseWeapon.Visible = true;
                btnUseEnergyDrink.Visible = true;
            }
            else
            {
                // If no zombie is present, set the current zombie to null
                _currentZombie = null;

                // Hide weapon and energy drink options
                cboWeapons.Visible = false;
                cboEnergyDrinks.Visible = false;
                btnUseWeapon.Visible = false;
                btnUseEnergyDrink.Visible = false;
            }

            // Update the player's inventory, quest, weapon, and energy drink lists in the UI
            UpdateInventoryListInUI();

            UpdateQuestListInUI();

            UpdateWeaponListInUI();

            UpdateEnergyDrinkListInUI();
        }

        // Method to update the inventory list display in the UI
        private void UpdateInventoryListInUI()
        {
            // Hide row headers in the DataGridView
            dgvInventory.RowHeadersVisible = false;

            // Set the number of columns and their properties
            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Item";
            dgvInventory.Columns[0].Width = 170;
            dgvInventory.Columns[1].Name = "Quantity";

            // Clear existing rows in the inventory list display
            dgvInventory.Rows.Clear();

            // Add items to the inventory list display
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }

        // Method to update the quest list display in the UI
        private void UpdateQuestListInUI()
        {
            // Hide row headers in the DataGridView
            dgvQuests.RowHeadersVisible = false;

            // Set the number of columns and their properties
            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Quest";
            dgvQuests.Columns[0].Width = 170;
            dgvQuests.Columns[1].Name = "Complete?";

            // Clear existing rows in the quest list display
            dgvQuests.Rows.Clear();

            // Add quests to the quest list display
            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        // Method to update the weapon list display in the UI
        private void UpdateWeaponListInUI()
        {
            //Create a list to hold the weapons
            List<Weapon> weapons = new List<Weapon>();

            // Iterate through each item in the player's inventory
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                // Check if the item is a weapon
                if (inventoryItem.Details is Weapon)
                {
                    // Add the weapon to the list if the quantity is greater than 0
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }

            // Check if the player has any weapons
            if (weapons.Count == 0)
            {
                // Hide the weapon combo box and use button if no weapons are available
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                // Set the data source of the combo box to the list of weapons
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name"; // Display the name of the weapon
                cboWeapons.ValueMember = "ID"; // Use the ID as the value

                // Select the first weapon in the list by default
                cboWeapons.SelectedIndex = 0;
            }
        }

        // Method to update the energy drink list display in the UI
        private void UpdateEnergyDrinkListInUI()
        {
            // Create a list to hold the energy drinks
            List<EnergyDrink> energyDrinks = new List<EnergyDrink>();
            
            // Iterate through each item in the playr's inventory
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                // Check if the item is an energy drink
                if (inventoryItem.Details is EnergyDrink)
                {
                    // Add the energy drink to the list if the quantity is greater than 0
                    if (inventoryItem.Quantity > 0)
                    {
                        energyDrinks.Add((EnergyDrink)inventoryItem.Details);
                    }
                }
            }

            // Check if the player has any energy drinks
            if (energyDrinks.Count == 0)
            {
                // Hide the energy drink combo box and use button if no energy drinks are available
                cboEnergyDrinks.Visible = false;
                btnUseEnergyDrink.Visible = false;
            }
            else
            {
                // Set the data source of the combo box to the list of energy drinks
                cboEnergyDrinks.DataSource = energyDrinks;
                cboEnergyDrinks.DisplayMember = "Name"; // Display the name of the energy drink
                cboEnergyDrinks.ValueMember = "ID"; // Use the ID as the value

                // Select the first energy drink in the list by default
                cboEnergyDrinks.SelectedIndex = 0;
            }
        }

        // Event handler for clicking the Use Weapon button
        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            // Get the currently selected weapon from the combo box
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            // Calculate the damage to the zombie using the random number generator
            int damageToZombie = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);

            // Subtract the damage from the zombie's current hit points
            _currentZombie.CurrentHitPoints -= damageToZombie;

            // Create an attack message based on the weapon used
            string attackMessage = ""; 
            switch (currentWeapon.Name) 
            {
                case "Baseball Bat": attackMessage = "You swing the baseball bat with all your might, crushing the " + _currentZombie.Name + " for " + damageToZombie.ToString() + " points!"; 
                    break; 
                case "Machete": attackMessage = "The machete slices through the air, cleaving into the " + _currentZombie.Name + " for " + damageToZombie.ToString() + " points!"; 
                    break; 
                case "Shotgun": attackMessage = "You pull the trigger, and the shotgun blasts the " + _currentZombie.Name + " for " + damageToZombie.ToString() + " points!";
                    break; 
                default: attackMessage = "You hit the " + _currentZombie.Name + " for " + damageToZombie.ToString() + " points."; 
                    break; 
            }

            // Display the attack message in the messages text box
            rtbMessages.Text += attackMessage + Environment.NewLine;
            rtbMessages.Text += Environment.NewLine;
            ScrollToBottomOfMessages();

            // Check if the zombie's current hit points are less than or equal to zero
            if (_currentZombie.CurrentHitPoints <= 0)
            {
                // Notify the player that they have defeated the zombie
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentZombie.Name + Environment.NewLine;

                // Reward the player with experience points and essence
                _player.ExperiencePoints += _currentZombie.RewardExperiencePoints;
                rtbMessages.Text += "You receive " + _currentZombie.RewardExperiencePoints.ToString() + " experience points!" + Environment.NewLine;

                _player.Essence += _currentZombie.RewardEssence;
                rtbMessages.Text += "You receive " + _currentZombie.RewardEssence.ToString() + " essence!" + Environment.NewLine;

                // Create a list to hold the looted items
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                // Iterate through each loot item in the zombie's loot table
                foreach (LootItem lootItem in _currentZombie.LootTable)
                {
                    // Use the random number generator to determine if the loot should be dropped based on its drop percentage
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        // Add the looted item to the list of looted items
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                // If no items were looted, add the default item(s)
                if (lootedItems.Count == 0)
                {
                    // Iterate through each loot item in the zombie's loot table
                    foreach (LootItem lootItem in _currentZombie.LootTable)
                    {
                        // Add the default item to the list of looted items
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                // Iterate through each looted item
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    // Add the looted item to the player's inventory
                    _player.AddItemToInventory(inventoryItem.Details);

                    // Display a message about the looted item based on the quantity of the item
                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }

                // Update the player's stats in the UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblEssence.Text = _player.Essence.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                // Update the player's inventory, weapon and energy drinks lists in the UI
                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdateEnergyDrinkListInUI();

                // Add a newline to messages and scroll to the bottom of the text box
                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();

                // Move the player back to their current location to refresh the UI
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                // Calculate the damage to the player using the random number generator
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentZombie.MaximumDamage);

                // Create an attack message based on the zombie's name
                string zombieAttackMessage = "";
                switch (_currentZombie.Name) 
                { 
                    case "Creepy Crawler": zombieAttackMessage = "The Creepy Crawler lunges forward, biting into your flesh dealing " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                        break; 
                    case "Franken Farmer": zombieAttackMessage = "The Franken Farmer swings its pitchfork with a monstrous force dealing " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                        break; 
                    case "Undead Soldier": zombieAttackMessage = "The Undead Soldier thrusts its bloody bayonet at you with a chilling precision dealing " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                        break; 
                    case "Infected Nurse": zombieAttackMessage = "The Infected Nurse slashes at you with her infected claws dealing " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                        break; 
                    case "Plague Doctor": zombieAttackMessage = "The Plague Doctor carves into your flesh with his sinister scalpel dealing " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                        break; 
                    default: zombieAttackMessage = "The " + _currentZombie.Name + " attacks you for " + damageToZombie.ToString() + " points."; 
                        break; 
                }

                // Display the attack message in the messages text box
                rtbMessages.Text += zombieAttackMessage;
                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();
               
                // Subtract the damage from the player's current hit points
                _player.CurrentHitPoints -= damageToPlayer;

                // Update the player's hit points label in the UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                // Check if the player's current hit points are less than or equal to zero
                if (_player.CurrentHitPoints <= 0)
                {
                    // Notify the player of their defeat
                    rtbMessages.Text += "As the " + _currentZombie.Name + " delivers its final blow, a cold chill spreads through your veins. The virus has taken hold, and your fate is sealed." + Environment.NewLine;
                    rtbMessages.Text += Environment.NewLine;
                    ScrollToBottomOfMessages();

                    // Move the player back to the home base location
                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME_BASE));
                }
            }
        }

        // Event handler for clicking the Use Energy Drink button
        private void btnUseEnergyDrink_Click(object sender, EventArgs e)
        {
            // Get the currently selected energy drink from the combo box
            EnergyDrink energyDrink = (EnergyDrink)cboEnergyDrinks.SelectedItem;

            // Increase the player's current hit points by the healing amount of the energy drink
            _player.CurrentHitPoints = (_player.CurrentHitPoints + energyDrink.HealingAmount);

            // Ensure the player's current hit points do not exceed the maximum hit points
            if(_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }

            // Decrease the quantity of the used energy drink in the player's inventory
            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details.ID == energyDrink.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }

            // Display a message that the player has consumed an energy drink and restored hit points
            rtbMessages.Text += "You drink an " + energyDrink.Name + ". Your Hit Points have been restored!" + Environment.NewLine;
            rtbMessages.Text += Environment.NewLine;
            ScrollToBottomOfMessages();

            // Update the player's hit points label in the UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            
            // Update the inventory and energy drink list in the UI
            UpdateInventoryListInUI();
            UpdateEnergyDrinkListInUI();
        }

        // Method to scroll to the bottom of the messages in the RichTextBox
        private void ScrollToBottomOfMessages()
        {
            // Set the selection start to the end of the text and scroll to the caret position
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
    }
}