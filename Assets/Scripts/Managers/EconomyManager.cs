using System;
using UI;
using UnityEngine;

namespace Managers
{
    public class EconomyManager : MonoBehaviour
    {
        private double _totalCoin;
        private int _totalGem;

        public static Action<double> OnCollectCoin;
        public static Action<double> OnSpendCoin;

        public static Action<int> OnCollectGem;
        public static Action<int> OnSpendGem;

        private void Awake()
        {
            OnCollectCoin = delegate(double d) { };
            OnSpendCoin = delegate(double d) { };

            OnCollectGem = delegate(int d) { };
            OnSpendGem = delegate(int d) { };

            OnCollectCoin += AddCoin;
            OnSpendCoin += SpendCoin;

            OnCollectGem += AddGem;
            OnSpendGem += SpendGem;
        }

        private void Start()
        {
            _totalCoin = SaveLoadManager.Instance.LoadCoin();
            _totalGem = SaveLoadManager.Instance.LoadGem();
        }

        private void AddCoin(double collectedCoin)
        {
            _totalCoin += collectedCoin;
            UIManager.OnUpdateCoinHud.Invoke(_totalCoin);
        }

        private void SpendCoin(double collectedCoin)
        {
            _totalCoin += collectedCoin;
            UIManager.OnUpdateCoinHud.Invoke(_totalCoin);
        }

        private void AddGem(int collectedGem)
        {
            _totalGem += collectedGem;
            UIManager.OnUpdateGemHud.Invoke(_totalGem);
        }

        private void SpendGem(int collectdGem)
        {
            _totalGem += collectdGem;
            UIManager.OnUpdateGemHud.Invoke(_totalGem);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                AddCoin(100);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                AddGem(100);
            }
        }
    }
}