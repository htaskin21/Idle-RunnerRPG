using ScriptableObjects;

namespace PetSkills
{
    public class CriticTap : PetSkill
    {
        public override void AddSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isCriticalTapActive = true;
        }

        public override void RemoveSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isCriticalTapActive = false;
        }
    }
}