using System;
using System.Text;
using Enums;
using PetSkills;
using ScriptableObjects;
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
                    return stringBuilder.Append("j Crit. Dmg");
                case SkillTypes.CriticalAttackChance:
                    return stringBuilder.Append("+j % Crit. Attack Chance");
                case SkillTypes.FireDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=4>");
                case SkillTypes.WaterDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=1>");
                case SkillTypes.LightningDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=2>");
                case SkillTypes.GoldenTap:
                    return stringBuilder.Append("Earn coin with every tap");
                case SkillTypes.AutoTapSpecial:
                    return stringBuilder.Append("Auto Tap for j minutes");
                case SkillTypes.RageBoost:
                    return stringBuilder.Append("50% more DPS");
                case SkillTypes.HolyDmgSpecial:
                    return stringBuilder.Append("Deals jx Dmg to <sprite=6>");
                default:
                    Debug.LogWarning("GetDescription Default geldi");
                    return stringBuilder.Append("empty");
            }
        }
        
        public static StringBuilder GetDescription(PetSkill petSkill)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (petSkill == null)
            {
                return stringBuilder.Append("Select Pet");
            }
            
            if (petSkill.GetType() == typeof(SkillCoolDown))
            {
                return stringBuilder.Append("Reduces Ulti cooldown by 10%");
            }
            
            if (petSkill.GetType() == typeof(AddClickCountToDps))
            {
                return stringBuilder.Append("Adds Tap Count to DPS");
            }
            
            if (petSkill.GetType() == typeof(AddTimeToDamage))
            {
                return stringBuilder.Append("Adds Game Minutes to DPS");
            }

            if (petSkill.GetType() == typeof(CriticTap))
            {
                return stringBuilder.Append("Tap Damage can do Critical Damage");
            }
            
            if (petSkill.GetType() == typeof(BossTimeBoost))
            {
                return stringBuilder.Append("Add 5 sec. To Boss Time");
            }
            
            return stringBuilder;
        }

        public static string ConvertToMinutes(float milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds((int)milliseconds);
            var minutesString = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";

            return minutesString;
        }
    }
}