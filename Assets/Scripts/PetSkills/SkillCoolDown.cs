using ScriptableObjects;
using UnityEngine;

namespace PetSkills
{
    [System.Serializable]
    public class SkillCoolDown : PetSkill
    {
        public override void AddSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.goldenTapCooldown -= Mathf.RoundToInt(heroDamageDataSo.goldenTapCooldown * 0.1f);

            heroDamageDataSo.rageCoolDown -= Mathf.RoundToInt(heroDamageDataSo.rageCoolDown * 0.1f);

            heroDamageDataSo.autoTapAttackCooldown -= Mathf.RoundToInt(heroDamageDataSo.autoTapAttackCooldown * 0.1f);

            heroDamageDataSo.fireSpecialAttackCoolDown -=
                Mathf.RoundToInt(heroDamageDataSo.fireSpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.holySpecialAttackCoolDown -=
                Mathf.RoundToInt(heroDamageDataSo.holySpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.lightningSpecialAttackCoolDown -=
                Mathf.RoundToInt(heroDamageDataSo.lightningSpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.waterSpecialAttackCoolDown -=
                Mathf.RoundToInt(heroDamageDataSo.waterSpecialAttackCoolDown * 0.1f);
        }

        public override void RemoveSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.goldenTapCooldown += Mathf.RoundToInt(heroDamageDataSo.goldenTapCooldown * 0.1f);

            heroDamageDataSo.rageCoolDown += Mathf.RoundToInt(heroDamageDataSo.rageCoolDown * 0.1f);

            heroDamageDataSo.autoTapAttackCooldown += Mathf.RoundToInt(heroDamageDataSo.autoTapAttackCooldown * 0.1f);

            heroDamageDataSo.fireSpecialAttackCoolDown +=
                Mathf.RoundToInt(heroDamageDataSo.fireSpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.holySpecialAttackCoolDown +=
                Mathf.RoundToInt(heroDamageDataSo.holySpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.lightningSpecialAttackCoolDown +=
                Mathf.RoundToInt(heroDamageDataSo.lightningSpecialAttackCoolDown * 0.1f);

            heroDamageDataSo.waterSpecialAttackCoolDown +=
                Mathf.RoundToInt(heroDamageDataSo.waterSpecialAttackCoolDown * 0.1f);
        }
    }
}