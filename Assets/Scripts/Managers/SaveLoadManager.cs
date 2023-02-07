using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        #region Singleton

        private static SaveLoadManager _instance;

        public static SaveLoadManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Missing SaveLoadManager");

                return _instance;
            }
        }

        #endregion

        private void Awake()
        {
            _instance = this;

            EconomyManager.OnCollectCoin += SaveCoin;
            EconomyManager.OnSpendCoin += SaveCoin;
        }

        public void SaveSkillUpgrade(int skillID, int skillLevel)
        {
            Dictionary<int, int> skillUpgradeDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("skillUpgradeSaveFile.es3");

            skillUpgradeDictionary = saveFile.Load<Dictionary<int, int>>("weaponUpgrade", skillUpgradeDictionary);

            skillUpgradeDictionary[skillID] = skillLevel;

            saveFile.Save<Dictionary<int, int>>("weaponUpgrade", skillUpgradeDictionary);

            saveFile.Sync();
        }

        public Dictionary<int, int> LoadSkillUpgrade()
        {
            Dictionary<int, int> skillUpgradeDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("skillUpgradeSaveFile.es3");

            skillUpgradeDictionary = saveFile.Load<Dictionary<int, int>>("weaponUpgrade", skillUpgradeDictionary);

            return skillUpgradeDictionary;
        }
    
        public void SaveSpecialAttackUpgrade(int specialAttackID, int skillLevel)
        {
            Dictionary<int, int> heroSectionDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("heroSectionSaveFile.es3");

            heroSectionDictionary = saveFile.Load<Dictionary<int, int>>("specialAttacks", heroSectionDictionary);

            heroSectionDictionary[specialAttackID] = skillLevel;

            saveFile.Save<Dictionary<int, int>>("specialAttacks", heroSectionDictionary);

            saveFile.Sync();
        }

        public Dictionary<int, int> LoadSpecialAttackUpgrade()
        {
            Dictionary<int, int> heroSectionDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("heroSectionSaveFile.es3");

            heroSectionDictionary = saveFile.Load<Dictionary<int, int>>("specialAttacks", heroSectionDictionary);

            return heroSectionDictionary;
        }

        private void SaveCoin(double totalCoin)
        {
            var saveFile = new ES3File("economySaveFile.es3");
            var coin = saveFile.Load<double>("totalCoin", 0);

            coin += totalCoin;

            saveFile.Save("totalCoin", coin);
            saveFile.Sync();
        }

        public double LoadCoin()
        {
            var saveFile = new ES3File("economySaveFile.es3");
            var coin = saveFile.Load<double>("totalCoin", 0);

            return coin;
        }
    }
}