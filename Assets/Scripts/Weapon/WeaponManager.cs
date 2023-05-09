using UI.Weapon;
using UnityEngine;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        private WeaponUIPanel _weaponUIPanel;

       
        void Start()
        {
            _weaponUIPanel.LoadData();
        }

        
    }
}