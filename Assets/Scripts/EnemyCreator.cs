using Enemy;
using UI;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private IconDataSO iconDataSo;

    public void SetEnemyData(EnemyController enemyController, int level)
    {
        enemyController.enemyLevel = level;

        var icon = iconDataSo.GetIcon(enemyController.enemyDamageType);
        enemyController.enemyDamageTypeIcon.sprite = icon;
    }
}