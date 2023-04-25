using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
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

            EconomyManager.OnCollectGem += SaveGem;
            EconomyManager.OnSpendGem += SaveGem;
        }

        public void SaveGameStartTime(DateTime startTime)
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            saveFile.Save<DateTime>("gameStartTime", startTime);

            saveFile.Sync();
        }

        public DateTime LoadGameStartTime()
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            var currentTime = saveFile.Load<DateTime>("gameStartTime", DateTime.UtcNow);

            return currentTime;
        }

        public void SaveStageProgress(int stage)
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            saveFile.Save<int>("stageProgress", stage);

            saveFile.Sync();
        }

        public int LoadStageProgress()
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            var stageProgress = saveFile.Load<int>("stageProgress", 1);

            return stageProgress;
        }
        
        public void SaveLastStage(LevelDataSO levelDataSo)
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            saveFile.Save<LevelDataSO>("lastStage", levelDataSo);

            saveFile.Sync();
        }

        public LevelDataSO LoadLastStage()
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            var lastStage = saveFile.Load<LevelDataSO>("lastStage", null);

            return lastStage;
        }
        

        public void SavePrestigeCount()
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            var prestigeCount = LoadPrestigeCount() + 1;
            saveFile.Save<int>("prestigeCount", prestigeCount);

            saveFile.Sync();
        }

        public int LoadPrestigeCount()
        {
            var saveFile = new ES3File("InGameSaveFile.es3");

            var stageProgress = saveFile.Load<int>("prestigeCount", 0);

            return stageProgress;
        }

        public void SaveSkillUpgrade(int skillID, int skillLevel)
        {
            Dictionary<int, int> skillUpgradeDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("skillUpgradeSaveFile.es3");

            skillUpgradeDictionary = saveFile.Load<Dictionary<int, int>>("skillUpgrade", skillUpgradeDictionary);

            skillUpgradeDictionary[skillID] = skillLevel;

            saveFile.Save<Dictionary<int, int>>("skillUpgrade", skillUpgradeDictionary);

            saveFile.Sync();
        }

        public Dictionary<int, int> LoadSkillUpgrade()
        {
            Dictionary<int, int> skillUpgradeDictionary = new Dictionary<int, int>();

            var saveFile = new ES3File("skillUpgradeSaveFile.es3");

            skillUpgradeDictionary = saveFile.Load<Dictionary<int, int>>("skillUpgrade", skillUpgradeDictionary);

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


        public void SavePetData(int petID)
        {
            List<int> pets = new List<int>();

            var saveFile = new ES3File("PetData.es3");

            pets = saveFile.Load<List<int>>("unlockedPet", pets);

            pets.Add(petID);

            saveFile.Save<List<int>>("unlockedPet", pets);

            saveFile.Sync();
        }

        public List<int> LoadPetData()
        {
            List<int> pets = new List<int>();

            var saveFile = new ES3File("PetData.es3");

            pets = saveFile.Load<List<int>>("unlockedPet", pets);

            return pets;
        }

        public void SaveSelectedPetData(int petID, bool isSelected)
        {
            List<int> pets = new List<int>();

            var saveFile = new ES3File("PetData.es3");

            pets = saveFile.Load<List<int>>("selectedPet", pets);

            if (isSelected)
            {
                pets.Add(petID);
            }
            else
            {
                if (pets.Contains(petID))
                {
                    pets.Remove(petID);
                }
            }

            saveFile.Save<List<int>>("selectedPet", pets);

            saveFile.Sync();
        }

        public List<int> LoadSelectedPetData()
        {
            List<int> pets = new List<int>();

            var saveFile = new ES3File("PetData.es3");

            pets = saveFile.Load<List<int>>("selectedPet", pets);

            return pets;
        }

        public void SaveSpecialAttackCoolDown(int specialAttackID, DateTime coolDownTime)
        {
            Dictionary<int, DateTime> specialAttackCoolDown = new Dictionary<int, DateTime>();

            var saveFile = new ES3File("specialAttackTimeSaveFile.es3");

            specialAttackCoolDown =
                saveFile.Load<Dictionary<int, DateTime>>("specialAttackCoolDown", specialAttackCoolDown);

            specialAttackCoolDown[specialAttackID] = coolDownTime;

            saveFile.Save<Dictionary<int, DateTime>>("specialAttackCoolDown", specialAttackCoolDown);
            saveFile.Sync();
        }

        public Dictionary<int, DateTime> LoadSpecialAttackCoolDown()
        {
            Dictionary<int, DateTime> specialAttackCoolDown = new Dictionary<int, DateTime>();

            var saveFile = new ES3File("specialAttackTimeSaveFile.es3");

            specialAttackCoolDown =
                saveFile.Load<Dictionary<int, DateTime>>("specialAttackCoolDown", specialAttackCoolDown);

            return specialAttackCoolDown;
        }

        public void SaveSpecialAttackDuration(int specialAttackID, DateTime durationTime)
        {
            Dictionary<int, DateTime> specialAttackDuration = new Dictionary<int, DateTime>();

            var saveFile = new ES3File("specialAttackTimeSaveFile.es3");

            specialAttackDuration =
                saveFile.Load<Dictionary<int, DateTime>>("specialAttackDuration", specialAttackDuration);

            specialAttackDuration[specialAttackID] = durationTime;

            saveFile.Save<Dictionary<int, DateTime>>("specialAttackDuration", specialAttackDuration);
            saveFile.Sync();
        }

        public Dictionary<int, DateTime> LoadSpecialAttackDuration()
        {
            Dictionary<int, DateTime> specialAttackDuration = new Dictionary<int, DateTime>();

            var saveFile = new ES3File("specialAttackTimeSaveFile.es3");

            specialAttackDuration =
                saveFile.Load<Dictionary<int, DateTime>>("specialAttackDuration", specialAttackDuration);

            return specialAttackDuration;
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

        public void SaveGem(int totalGem)
        {
            var saveFile = new ES3File("economySaveFile.es3");
            var gem = saveFile.Load<int>("totalGem", 0);

            gem += totalGem;

            saveFile.Save("totalGem", gem);
            saveFile.Sync();
        }

        public int LoadGem()
        {
            var saveFile = new ES3File("economySaveFile.es3");
            var gem = saveFile.Load<int>("totalGem", 0);

            return gem;
        }

        public void SavePotion(PotionType potionType, int amount)
        {
            var desc = potionType.ToString();

            var saveFile = new ES3File("potionSaveFile.es3");
            var potionCount = saveFile.Load<int>(desc, 0);

            potionCount += amount;

            saveFile.Save(desc, potionCount);
            saveFile.Sync();
        }

        public int LoadPotion(PotionType potionType)
        {
            var desc = potionType.ToString();

            var saveFile = new ES3File("potionSaveFile.es3");
            var potionCount = saveFile.Load<int>(desc, 0);

            return potionCount;
        }

        public void SaveStrengthBoostTime(int milliSeconds)
        {
            var saveFile = new ES3File("boostTimeSaveFile.es3");
            var boostTime = saveFile.Load<DateTime>("strengthBoostTime", DateTime.UtcNow);

            if (boostTime < DateTime.UtcNow)
            {
                boostTime = DateTime.UtcNow;
            }

            var addedBoostTime = boostTime.AddMilliseconds(milliSeconds);

            saveFile.Save("strengthBoostTime", addedBoostTime);
            saveFile.Sync();
        }

        public DateTime LoadStrengthBoostTime()
        {
            var saveFile = new ES3File("boostTimeSaveFile.es3");
            var boostTime = saveFile.Load<DateTime>("strengthBoostTime", DateTime.UtcNow);

            return boostTime;
        }
    }
}