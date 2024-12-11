using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the Item class to represent items in the game
    public class Item
    {
        // Properties to store an item's ID, name, and plural name
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }

        // Constructor to initialize a new instance of Item
        public Item(int id, string name, string namePlural)
        {
            ID = id;
            Name = name;
            NamePlural = namePlural;
        }
    }

    // Define the EnergyDrink class which inherits from the Item class
    public class EnergyDrink : Item
    {
        // Property to store the healing amount provided by the energy drink
        public int HealingAmount { get; set; }

        // Constructor to initialize a new instance of EnergyDrink and invoke the base Item constructor
        public EnergyDrink(int id, string name, string namePlural, int healingAmount) : base(id, name, namePlural)
        {
            HealingAmount = healingAmount;
        }
    }

    // Define the Weapon class which inherits from the Item class
    public class Weapon : Item
    {
        // Properties to store the weapon's minimum and maximum damage
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        // Constructor to initialize a new instance of Weapon and invoke the base Item constructor
        public Weapon(int id, string name, string namePlural, int minimumDamage, int maximumDamage) : base(id, name, namePlural)
        {
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
        }
    }
}
