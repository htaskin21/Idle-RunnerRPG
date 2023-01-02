using System.Text;
using UnityEngine;

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
                    return stringBuilder.Append("j DPS");
                case SkillTypes.TapDamageBoost:
                    return stringBuilder.Append("j Tap Damage");
                case SkillTypes.CriticalAttackBoost:
                    return stringBuilder.Append("j Critical Damage");
                case SkillTypes.CriticalAttackChance:
                    return stringBuilder.Append("+j % Critical Attack Chance");
                case SkillTypes.FireDmgSpecial:
                    return stringBuilder.Append("xj more <sprite=2> Damage");
                case SkillTypes.WaterDmgSpecial:
                    return stringBuilder.Append("xj more <sprite=3> Damage");
                case SkillTypes.LightningDmgSpecial:
                    return stringBuilder.Append("xj more <sprite=4> Damage");
                default:
                    Debug.LogWarning("GetDescription Default geldi");
                    return stringBuilder.Append("empty");
            }
        }
    }
}