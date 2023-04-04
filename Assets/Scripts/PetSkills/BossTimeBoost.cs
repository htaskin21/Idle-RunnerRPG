using ScriptableObjects;

namespace PetSkills
{
    public class BossTimeBoost : PetSkill
    {
        public override void AddSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.bossDurationBonus = 5;
        }

        public override void RemoveSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.bossDurationBonus = 0;
        }
    }
}