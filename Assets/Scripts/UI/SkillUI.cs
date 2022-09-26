using System;
using System.Globalization;

namespace UI
{
    public class SkillUI
    {
        public int ID { get; }

        public SkillTypes SkillTypes { get; }

        public double BaseIncrementAmount { get; }

        public double BaseIncrementCost { get; }

        public SkillUI(string id, string skillType, string baseIncrementAmount, string baseIncrementCost)
        {
            ID = int.Parse(id);
            SkillTypes = (SkillTypes) Enum.Parse(typeof(SkillTypes), skillType, true);
            BaseIncrementAmount = double.Parse(baseIncrementAmount, CultureInfo.InvariantCulture);
            BaseIncrementCost = double.Parse(baseIncrementCost, CultureInfo.InvariantCulture);
        }
    }
}