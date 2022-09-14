using Enemy;
using UI;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private IconData iconData;

    public void SetEnemyData(EnemyController enemyController)
    {
        var icon = iconData.GetIcon(enemyController.enemyDamageType);
        enemyController.enemyDamageTypeIcon.sprite = icon;
    }
}