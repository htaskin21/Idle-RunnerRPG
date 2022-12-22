using Enemy;
using ScriptableObjects;
using UI;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private DamageIconDataSO damageIconDataSo;

    public void SetEnemyData(EnemyController enemyController, int level)
    {
        enemyController.enemyLevel = level;

        var icon = damageIconDataSo.GetIcon(enemyController.enemyDamageType);
        enemyController.enemyDamageTypeIcon.sprite = icon;
    }
}