using ScriptableObjects;
using UnityEngine;

namespace PetSkills
{
    public class AddClickCountToDps : PetSkill
    {
        public override void AddSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isAddClickCountToDPS = true;
        }

        public override void RemoveSkill(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.isAddClickCountToDPS = false;
            PlayerPrefs.SetInt("TapCount", 0);
        }
    }
}