using Enums;

namespace Quests
{
    public class HeroLevelQuest : Quest
    {
        public HeroLevelQuest(QuestType questType, int startValue, int endValue, double prizeAmount) : base(questType,
            startValue, endValue, prizeAmount)
        {
            
        }

        protected override void AddToEvent()
        {
            throw new System.NotImplementedException();
        }

        protected override void RemoveFromEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}