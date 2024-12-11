using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the Quest class to represent quests in the game
    public class Quest
    {
        // Properties to store a quest's ID, name, description, reward experience points, reward essence, reward item, and quest completion items
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardEssence { get; set; }
        public Item RewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }

        // Constuctor to initialize a new instance of Quest
        public Quest(int id, string name, string description, int rewardExperiencePoints, int rewardEssence)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardEssence = rewardEssence;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
