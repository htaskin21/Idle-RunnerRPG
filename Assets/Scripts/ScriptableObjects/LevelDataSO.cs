using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Level Data", fileName = "LevelDataSO", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        public TilemapRenderer groundObject;

        public SkyImage skyImage;

        public List<EnemyController> regularEnemies;

        public EnemyController bossEnemy;
    }
}