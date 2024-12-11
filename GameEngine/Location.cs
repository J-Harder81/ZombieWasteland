using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the Location class to represent locations in the gam
    public class Location
    {
        // Properties to stor the location's ID, name, description, required item to enter, if there's a quest, and if there's a zombie
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Zombie ZombieLivingHere { get; set; }
        
        // Properties to store adjacent locations in the game
        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        // Constructor to initialize a new instance of Location
        public Location(int id, string name, string description, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Zombie zombieLivingHere = null)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            ZombieLivingHere = zombieLivingHere;
        }
    }
}
