using UnityEngine;

namespace Hero
{
    public class SpecialAttackAnimator : MonoBehaviour
    {
        public void DeactivateSpecialAttackPrefab()
        {
            this.gameObject.SetActive(false);
        }
    }
}