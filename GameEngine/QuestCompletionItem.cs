using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the QuestCompletionItem class to represent items required to complete a quest
    public class QuestCompletionItem
    {
        // Properties to store the details and quantity of the item required
        public Item Details { get; set; }
        public int Quantity { get; set; }

        // Constructor to initialize a new instance of QuestCompletionItem
        public QuestCompletionItem(Item details, int quantity) 
        {
            Details = details;
            Quantity = quantity;
        }
    }
}
