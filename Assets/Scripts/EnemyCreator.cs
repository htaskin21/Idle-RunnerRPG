using Enemy;
using Enums;
using ScriptableObjects;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private DamageIconDataSO damageIconDataSo;

    [SerializeField]
    private HeroDamageDataSO _heroDamageDataSo;

    public void SetEnemyData(EnemyController enemyController, int level)
    {
        enemyController.enemyLevel = level;
        enemyController.enemyHealth.SetMaxHealth(level);

        if (enemyController._enemyType == EnemyType.Boss)
        {
            enemyController.enemyTimer.SetDuration(_heroDamageDataSo.bossDurationBonus);
        }

        var icon = damageIconDataSo.GetIcon(enemyController.enemyDamageType);
        enemyController.enemyDamageTypeIcon.sprite = icon;
    }
}