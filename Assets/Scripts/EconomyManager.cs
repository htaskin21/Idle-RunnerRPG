using System;
using UI;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private double _totalCoin;

    public static Action<double> OnCollectCoin;

    private void Awake()
    {
        OnCollectCoin = delegate(double d) { };
        OnCollectCoin += AddCoin;
    }

    private void AddCoin(double collectedCoin)
    {
        _totalCoin += collectedCoin;
        UIManager.OnUpdateCoinHud.Invoke(_totalCoin);
    }
}