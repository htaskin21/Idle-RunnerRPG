using System;
using Enums;

namespace Quests
{
    [Serializable]
    public abstract class Quest
    {
        protected QuestType _questType { get; private set; }

        protected int _startValue { get; private set; }

        protected int _endValue { get; private set; }

        protected double _prizeAmount { get; private set; }

        protected Quest(QuestType questType, int startValue, int endValue, double prizeAmount)
        {
            _questType = questType;
            _startValue = startValue;
            _endValue = endValue;
            _prizeAmount = prizeAmount;
        }

        protected abstract void AddToEvent();

        protected abstract void RemoveFromEvent();
    }
}