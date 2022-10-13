using System.Text;

namespace Utils
{
    public static class DescriptionUtils
    {
        public static StringBuilder GetDescription(SkillTypes skillTypes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch (skillTypes)
            {
                case SkillTypes.BaseAttackBoost:
                    return stringBuilder.Append("x DPS");
                case SkillTypes.TapDamageBoost:
                    return stringBuilder.Append("x Tap Damage");
                case SkillTypes.CriticalAttackBoost:
                    return stringBuilder.Append("x Critical Damage");
                case SkillTypes.CriticalAttackChance:
                    return stringBuilder.Append("+x % Critical Attack Chance");
                default:
                    return stringBuilder;
            }
        }
    }
}