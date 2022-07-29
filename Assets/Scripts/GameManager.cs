using Enemy;
using Hero;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Missing GameManager");

            return _instance;
        }
    }

    #endregion

    [SerializeField] private HeroController _heroController;
    public HeroController HeroController => _heroController;
    
    [SerializeField] private EnemyController _enemyController;
    public EnemyController EnemyController => _enemyController;

    private void Awake()
    {
        _instance = this;
    }
}