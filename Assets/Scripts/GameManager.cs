using Hero;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeroController _heroController;

    public HeroController HeroController => _heroController;

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

    private void Awake()
    {
        _instance = this;
    }
}