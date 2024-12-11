using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the PlayerQuest class to represent quests the player is undertaking
    public class PlayerQuest
    {
        // Properties to store the quest's details and completion status
        public Quest Details { get; set; }
        public bool IsCompleted { get; set; }
        
        // Constructor to initialize a new instance of PlayerQuest
        // By default, a new quest is not completed
        public PlayerQuest(Quest details) 
        {
            Details = details;
            IsCompleted = false;
        }
    }
}
