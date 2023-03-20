using System;
using Sirenix.Serialization;

namespace ScriptableObjects
{
    [Serializable]
    public abstract class PetSkill : OdinSerializeAttribute
    {
        public abstract void AddSkill(HeroDamageDataSO heroDamageDataSo);

        public abstract void RemoveSkill(HeroDamageDataSO heroDamageDataSo);
    }
}