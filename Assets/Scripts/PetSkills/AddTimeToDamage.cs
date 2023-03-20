using ScriptableObjects;

namespace PetSkills
{
    public class AddTimeToDamage : PetSkill
    {
        public override void AddSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isAddTimeToDamageActive = true;
        }

        public override void RemoveSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isAddTimeToDamageActive = false;
        }
    }
}