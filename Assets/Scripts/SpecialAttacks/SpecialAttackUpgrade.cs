using System;
using System.Globalization;
using Enums;

namespace SpecialAttacks
{
    public class SpecialAttackUpgrade : UpgradableStat
    {
        public SkillTypes SkillTypes { get; }

        public double StartAmount { get; }

        public double BaseIncrementAmount { get; }

        public double BaseIncrementCost { get; }

        public SpecialAttackUpgrade(string id, string skillType, string startAmount, string baseIncrementAmount,
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