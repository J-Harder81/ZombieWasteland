using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the InventoryItem class to represent items in the player's inventory
    public class InventoryItem
    {
        // Properties to store the item's details and quantity
        public Item Details { get; set; }
        public int Quantity { get; set; }
        
        // Constructor to initialize a new instance of InventoryItem
        public InventoryItem(Item details, int quantity) 
        {
            Details = details;
            Quantity = quantity;
        }
    }
}
