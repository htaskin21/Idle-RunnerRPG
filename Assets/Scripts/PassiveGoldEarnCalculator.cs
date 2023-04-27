using System.Threading;
using Cysharp.Threading.Tasks;
using Managers;
using ScriptableObjects;

public class PassiveGoldEarnCalculator
{
    private readonly HeroDamageDataSO _heroDamageDataSo;

    public PassiveGoldEarnCalculator(HeroDamageDataSO heroDamageDataSo)
    {
        _heroDamageDataSo = heroDamageDataSo;
    }

    public async UniTask EarnPassiveGold(CancellationToken token)
    {
        while (token.IsCancellationRequested == false)
        {
            EconomyManager.OnCollectCoin.Invoke(_heroDamageDataSo.passiveGoldAmount);
            await UniTask.Delay(5000, cancellationToken: token);
        }
    }
}