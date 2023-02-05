using System;
using System.Globalization;
using Enums;

namespace Skill
{
    public class SkillUpgrade : UpgradableStat
    {
        public SkillTypes SkillTypes { get; }

        public double StartAmount { get; }

        public double BaseIncrementAmount { get; }

        public double BaseIncrementCost { get; }

        public SkillUpgrade(string id, string skillType, string startAmount, string baseIncrementAmount,
            string baseIncrementCost)
        {
            ID = int.Parse(id);
            SkillTypes = (SkillTypes) Enum.Parse(typeof(SkillTypes), skillType, true);
            StartAmount = double.Parse(startAmount, CultureInfo.InvariantCulture);
            BaseIncrementAmount = double.Parse(baseIncrementAmount, CultureInfo.InvariantCulture);
            BaseIncrementCost = double.Parse(baseIncrementCost, CultureInfo.InvariantCulture);
        }
    }
}