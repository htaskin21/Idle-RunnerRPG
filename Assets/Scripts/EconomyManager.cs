using System;
using UI;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private double _totalCoin;

    public static Action<double> OnCollectCoin;
    public static Action<double> OnSpendCoin;

    private void Awake()
    {
        OnCollectCoin = delegate(double d) { };
        OnSpendCoin = delegate(double d) { };

        OnCollectCoin += AddCoin;
        OnSpendCoin += SpendCoin;
    }

    private void Start()
    {
        _totalCoin = SaveLoadManager.Instance.LoadCoin();
    }

    private void AddCoin(double collectedCoin)
    {
        _totalCoin += collectedCoin;
        UIManager.OnUpdateCoinHud.Invoke(_totalCoin);
    }

    private void SpendCoin(double collectedCoin)
    {
        _totalCoin -= collectedCoin;
        UIManager.OnUpdateCoinHud.Invoke(_totalCoin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoin(100);
        }
    }
}