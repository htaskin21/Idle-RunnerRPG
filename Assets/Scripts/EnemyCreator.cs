using System;
using Enemy;
using Enums;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private DamageIconDataSO damageIconDataSo;

    [SerializeField]
    private HeroDamageDataSO _heroDamageDataSo;
    
    [SerializeField]
    private float _enemySpawnDistance;

    private void RemoveActionsFromEnemy(EnemyController enemyController)
    {
        if (enemyController != null)
        {
            foreach (var d in enemyController.enemyHealth.OnEnemyDie.GetInvocationList())
            {
                enemyController.enemyHealth.OnEnemyDie -= (Action) d;
            }

            Destroy(enemyController.gameObject);
        }
    }

    public EnemyController CreateEnemy(EnemyController enemyController, EnemyController selectedEnemyController, float heroPositionX)
    {
        RemoveActionsFromEnemy(enemyController);
        
        enemyController = Instantiate(selectedEnemyController,
            new Vector3((heroPositionX + _enemySpawnDistance),
                selectedEnemyController.transform.position.y, 0),
            quaternion.identity);

        return enemyController;
    }

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