using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Create LevelDataSO", fileName = "LevelDataSO", order = 0)]
public class LevelDataSO : ScriptableObject
{
    public TilemapRenderer groundObject;

    public SkyImage skyImage;

    public List<EnemyController> regularEnemies;

    public EnemyController bossEnemy;
}