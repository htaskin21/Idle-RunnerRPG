using System.Globalization;
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
                    return stringBuilder.Append("Deals jx Dmg to <sprite=5>");
                case SkillTypes.WaterDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=2>");
                case SkillTypes.LightningDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=3>");
                case SkillTypes.AutoTapSpecial:
                    return stringBuilder.Append("Auto Tap for j minutes");
                default:
                    Debug.LogWarning("GetDescription Default geldi");
                    return stringBuilder.Append("empty");
            }
        }
        
        public static float ConvertToMinutes(float milliseconds)
        {
            var a=  milliseconds / 60_000;

            var y = a.ToString("{0:F3}",CultureInfo.InvariantCulture);
            
            return 1;
        }
    }
    
   
}