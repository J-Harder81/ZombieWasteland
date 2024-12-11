using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the World class to hold lists of items, zombies, quests, and locations
    public static class World
    {
        // Static readonly lists to store all items, zombies, quests, and locations in the game
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Zombie> Zombies = new List<Zombie>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        // Constants for item IDs
        public const int ITEM_ID_BASEBALL_BAT = 1;
        public const int ITEM_ID_MACHETE = 2;
        public const int ITEM_ID_SHOTGUN = 3;
        public const int ITEM_ID_FOOD_CANS = 4;
        public const int ITEM_ID_WEAPON_PARTS = 5;
        public const int ITEM_ID_BLUE_PRINTS = 6;
        public const int ITEM_ID_MEDICAL_KITS = 7;
        public const int ITEM_ID_VIRUS_SAMPLE = 8;
        public const int ITEM_ID_ANTIVIRUS = 9;
        public const int ITEM_ID_ENERGY_DRINK = 10;
        public const int ITEM_ID_SURVIVOR_BADGE = 11;

        // Constants for zombie IDs
        public const int ZOMBIE_ID_CREEPY_CRAWLER = 1;
        public const int ZOMBIE_ID_FRANKEN_FARMER = 2;
        public const int ZOMBIE_ID_UNDEAD_SOLDIER = 3;
        public const int ZOMBIE_ID_INFECTED_NURSE = 4;
        public const int ZOMBIE_ID_PLAGUE_DOCTOR = 5;

        // Constants for quest IDs
        public const int QUEST_ID_SURVIVOR_HOPE = 1;
        public const int QUEST_ID_WEAPON_CACHE = 2;
        public const int QUEST_ID_SALVATION = 3;

        // Constants for location IDs
        public const int LOCATION_ID_HOME_BASE = 1;
        public const int LOCATION_ID_DOWNTOWN_RUINS = 2;
        public const int LOCATION_ID_SUBWAY_TUNNELS = 3;
        public const int LOCATION_ID_CITY_PARK = 4;
        public const int LOCATION_ID_HOSPITAL = 5;
        public const int LOCATION_ID_RECEPTION_DESK = 6;
        public const int LOCATION_ID_HOSPITAL_LAB = 7;
        public const int LOCATION_ID_RURAL_FARMSTEAD = 8;
        public const int LOCATION_ID_FARM_FIELD = 9;
        public const int LOCATION_ID_FACTORY_DISTRICT = 10;
        public const int LOCATION_ID_HIDDEN_BUNKER = 11;

        // Static constructor for the World class to populate items, zombies, quests and locations
        static World()
        {
            PopulateItems();
            PopulateZombies();
            PopulateQuests();
            PopulateLocations();
        }

        // Method to populate the list of items in the game
        private static void PopulateItems()
        {
            // Add various items used for gameplay to the Items list
            Items.Add(new Weapon(ITEM_ID_BASEBALL_BAT, "Baseball Bat", "Baseball Bats", 0, 3));
            Items.Add(new Weapon(ITEM_ID_MACHETE, "Machete", "Machetes", 1, 5));
            Items.Add(new Weapon(ITEM_ID_SHOTGUN, "Shotgun", "Shotguns", 5, 10));
            Items.Add(new Item(ITEM_ID_FOOD_CANS, "Food Can", "Food Cans"));
            Items.Add(new Item(ITEM_ID_WEAPON_PARTS, "Weapon Part", "Weapon Parts"));
            Items.Add(new Item(ITEM_ID_BLUE_PRINTS, "Blue Print", "Blue Prints"));
            Items.Add(new Item(ITEM_ID_MEDICAL_KITS, "Medical Kit", "Medical Kits"));
            Items.Add(new Item(ITEM_ID_ANTIVIRUS, "Anti-Virus", "Anti-Virus"));
            Items.Add(new EnergyDrink(ITEM_ID_ENERGY_DRINK, "Energy Drink", "Energy Drinks", 5));
        }

        // Method to populate the list of zombies in the game
        private static void PopulateZombies()
        {
            // Create and configure the Creepy Crawler zombie
            Zombie creepyCrawler = new Zombie(ZOMBIE_ID_CREEPY_CRAWLER, "Creepy Crawler", "From the shadows, a skittering nightmare lunges, its maw stretched unnaturally wide.", 1, 2, 5, 1, 1);
            creepyCrawler.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_DRINK), 25, true));
            
            // Create and configure the Franken Farmer zombie
            Zombie frankenFarmer = new Zombie(ZOMBIE_ID_FRANKEN_FARMER, "Franken Farmer", "Across the moonlit field, a hulking figure drags its pitchfork, eyes glowing with malice.", 2, 3, 10, 3, 3);
            frankenFarmer.LootTable.Add(new LootItem(ItemByID(ITEM_ID_FOOD_CANS), 75, false));
            frankenFarmer.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_DRINK), 75, true));

            // Create and configure the Undead Soldier zombie
            Zombie undeadSoldier = new Zombie(ZOMBIE_ID_UNDEAD_SOLDIER, "Undead Soldier", "In the suffocating dark of the bunker, a masked corpse rises, clutching a bloodied blade.", 5, 3, 10, 3, 5);
            undeadSoldier.LootTable.Add(new LootItem(ItemByID(ITEM_ID_WEAPON_PARTS), 75, false));
            undeadSoldier.LootTable.Add(new LootItem(ItemByID(ITEM_ID_BLUE_PRINTS), 50, false));
            undeadSoldier.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_DRINK), 50, true));

            // Create and configure the Infected Nurse zombie
            Zombie infectedNurse = new Zombie(ZOMBIE_ID_INFECTED_NURSE, "Infected Nurse", "Behind the reception desk, the nurse twitches violently, her grin splitting her face open.", 5, 5, 20, 5, 7);
            infectedNurse.LootTable.Add(new LootItem(ItemByID(ITEM_ID_MEDICAL_KITS), 50, true));
            infectedNurse.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_DRINK), 25, false));

            // Create and configure the Plague Doctor zombie
            Zombie plagueDoctor = new Zombie(ZOMBIE_ID_PLAGUE_DOCTOR, "Plague Doctor", "Beneath the lab's flickering lights, a masked figure advances, whispering death as its scalpel glints.", 10, 7, 40, 10, 10);
            plagueDoctor.LootTable.Add(new LootItem(ItemByID(ITEM_ID_VIRUS_SAMPLE), 25, true));
            plagueDoctor.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_DRINK), 25, false));

            // Add the zombies to the Zombies list
            Zombies.Add(creepyCrawler);
            Zombies.Add(frankenFarmer);
            Zombies.Add(undeadSoldier);
            Zombies.Add(infectedNurse);
            Zombies.Add(plagueDoctor);
        }

        // Method to populate the list of quests in the game
        private static void PopulateQuests()
        {
            // Create and configure the Survivor's Hope quest
            Quest survivorHope =
                new Quest(
                    QUEST_ID_SURVIVOR_HOPE,
                    "Survivor's Hope",
                    "In the City Outskirts, food is scarce, and you need to gather food cans from the zombies roaming the fields and barns. These provisions are vital to maintain the morale and health of the survivors in your group.", 20, 10);

            survivorHope.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_FOOD_CANS), 3));

            survivorHope.RewardItem = ItemByID(ITEM_ID_MACHETE);

            // Create and configure the Weapon's Cache quest
            Quest weaponCache =
                new Quest(
                    QUEST_ID_WEAPON_CACHE,
                    "Weapon's Cache",
                    "The Factory District holds the parts needed to upgrade your weapons. You need to retrieve the weapon parts and blueprints from the zombies lurking within the hidden military bunker. Every part and blueprint is crucial for your defense.", 20, 20);

            weaponCache.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_WEAPON_PARTS), 3));
            weaponCache.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_BLUE_PRINTS), 2));

            weaponCache.RewardItem = ItemByID(ITEM_ID_SHOTGUN);

            // Create and configure the Salvation quest
            Quest salvation =
                new Quest(
                    QUEST_ID_SALVATION,
                    "Salvation",
                    "In the final push to save humanity, you must venture into the Hospital to retrieve the life-saving anti-virus. The hospital's dangerous corridors are filled with the undead, and you must navigate through the wards and operating rooms to collect medical kits, and virus samples from the Plague Doctor. These items are essential for crafting the anti-virus, which is critical to the survival of your group and potentially the future of humanity.", 40, 40);
            salvation.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_MEDICAL_KITS), 5));
            salvation.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_VIRUS_SAMPLE), 3));

            salvation.RewardItem = ItemByID(ITEM_ID_ANTIVIRUS);

            // Add the quests to the Quests list
            Quests.Add(survivorHope);
            Quests.Add(weaponCache);
            Quests.Add(salvation);
        }

        // Method to populate the list of locations in the game
        private static void PopulateLocations()
        {
            // Create and configure the Home Base location
            Location homeBase = new Location(LOCATION_ID_HOME_BASE, "Home Base", "A fortified sanctuary amidst the chaos, providing solace and safety.");

            // Create and configure the Downtown Ruins location
            Location downtownRuins = new Location(LOCATION_ID_DOWNTOWN_RUINS, "Downtown Ruins", "Once vibrant streets now lie in eerie, desolate silence.");

            // Create and configure the Subway Tunnels location
            Location subwayTunnels = new Location(LOCATION_ID_SUBWAY_TUNNELS, "Subway Tunnels", "Dimly lit tunnels filled with abandoned trains and lurking dangers.");
            subwayTunnels.ZombieLivingHere = ZombieByID(ZOMBIE_ID_CREEPY_CRAWLER);

            // Create and configure the City Park location
            Location cityPark = new Location(LOCATION_ID_CITY_PARK, "City Park", "An overgrown green escape, now hiding unseen threats.");
            cityPark.ZombieLivingHere = ZombieByID(ZOMBIE_ID_CREEPY_CRAWLER);

            // Create and configure the Hospital location
            Location hospital = new Location(LOCATION_ID_HOSPITAL, "Hospital", "A nightmarish labyrinth of bloodstained walls and undead patients.", ItemByID(ITEM_ID_SHOTGUN));
            hospital.QuestAvailableHere = QuestByID(QUEST_ID_SALVATION);

            // Create and configure the Reception Desk location
            Location receptionDesk = new Location(LOCATION_ID_RECEPTION_DESK, "Reception Desk", "A desolate reception area, where Nurse Nightshade waits in eerie silence.");
            receptionDesk.ZombieLivingHere = ZombieByID(ZOMBIE_ID_INFECTED_NURSE);

            // Create and configure the Hospital Lab location
            Location hospitalLab = new Location(LOCATION_ID_HOSPITAL_LAB, "Hospital Lab", "A sterile lab, where Doctor Dread conducts deadly experiments.");
            hospitalLab.ZombieLivingHere = ZombieByID(ZOMBIE_ID_PLAGUE_DOCTOR);

            // Create and configure the Rural Farmstead location
            Location ruralFarmstead = new Location(LOCATION_ID_RURAL_FARMSTEAD, "Rural Farmstead", "An old farmhouse, sheltering a monstrous experiment gone wrong.");
            ruralFarmstead.QuestAvailableHere = QuestByID(QUEST_ID_SURVIVOR_HOPE);

            // Create and configure the Farm Field location
            Location farmField = new Location(LOCATION_ID_FARM_FIELD, "Farm Field", "Overgrown fields that sway with more than just the wind.");
            farmField.ZombieLivingHere = ZombieByID(ZOMBIE_ID_FRANKEN_FARMER);

            // Create and configure the Factory District location
            Location factoryDistrict = new Location(LOCATION_ID_FACTORY_DISTRICT, "Factory District", "Rusting machinery and decaying warehouses filled with unease.");
            factoryDistrict.QuestAvailableHere = QuestByID(QUEST_ID_WEAPON_CACHE);

            // Create and configure the Hidden Bunker location
            Location hiddenBunker = new Location(LOCATION_ID_HIDDEN_BUNKER, "Hidden Bunker", "A fortified military bunker, filled with old secrets and new dangers.", ItemByID(ITEM_ID_MACHETE));
            hiddenBunker.ZombieLivingHere = ZombieByID(ZOMBIE_ID_UNDEAD_SOLDIER);

            // Set adjacent locations for each location in the game
            homeBase.LocationToNorth = downtownRuins;

            downtownRuins.LocationToNorth = cityPark;
            downtownRuins.LocationToSouth = homeBase;
            downtownRuins.LocationToEast = subwayTunnels;
            downtownRuins.LocationToWest = ruralFarmstead;

            ruralFarmstead.LocationToEast = downtownRuins;
            ruralFarmstead.LocationToWest = farmField;

            farmField.LocationToEast = ruralFarmstead;

            cityPark.LocationToNorth = hospital; 
            cityPark.LocationToSouth = downtownRuins;

            hospital.LocationToSouth = cityPark;
            hospital.LocationToEast = receptionDesk;

            receptionDesk.LocationToNorth = hospitalLab;
            receptionDesk.LocationToWest = hospital;

            hospitalLab.LocationToSouth = receptionDesk;

            subwayTunnels.LocationToEast = factoryDistrict;
            subwayTunnels.LocationToWest = cityPark;

            factoryDistrict.LocationToSouth = hiddenBunker;
            factoryDistrict.LocationToWest = subwayTunnels;

            hiddenBunker.LocationToNorth = factoryDistrict;

            // Add all locations to the Locations list
            Locations.Add(homeBase);
            Locations.Add(downtownRuins);
            Locations.Add(ruralFarmstead);
            Locations.Add(farmField);
            Locations.Add(cityPark);
            Locations.Add(hospital);
            Locations.Add(receptionDesk);
            Locations.Add(hospitalLab);
            Locations.Add(subwayTunnels);
            Locations.Add(factoryDistrict);
            Locations.Add(hiddenBunker);
        }

        // Method to find an item by its ID from the Items list
        public static Item ItemByID(int id)
        {
            // Iterate through each item in the Items list
            foreach (Item item in Items)
            {
                // If the item's ID matches the specified ID, return the item
                if (item.ID == id)
                {
                    return item;
                }
            }

            // If no item is found with the specified ID, return null
            return null;
        }

        // Method to find a zombie by its ID from the Zombies list
        public static Zombie ZombieByID(int id)
        {
            // Iterate through each zombie in the Zombies list
            foreach (Zombie zombie in Zombies)
            {
                // If the zombie's ID matches the specified ID, return the zombie
                if (zombie.ID == id)
                {
                    return zombie;
                }
            }

            // If no zombie is found with the specified ID, return null
            return null;
        }

        // Method to find a quest by its ID from the Quests list
        public static Quest QuestByID(int id)
        {
            // Iterate through each quest in the Quests list
            foreach (Quest quest in Quests)
            {
                // If the quest's ID matches the specified ID, return the quest
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            // If no quest is found with the specified ID, return null
            return null;
        }

        // Method to find a location by its ID from the Locations list
        public static Location LocationByID(int id)
        {
            // Iterate through each location in the Locations list
            foreach (Location location in Locations)
            {
                // If the location's ID matches the specified ID, return the location
                if (location.ID == id)
                {
                    return location;
                }
            }

            // If no location is found with the specified ID, return null
            return null;
        }
    }
}

