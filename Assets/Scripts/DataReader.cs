using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Skill;
using SpecialAttacks;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    #region Singleton

    private static DataReader _instance;

    public static DataReader Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Missing GameManager");

            return _instance;
        }
    }

    #endregion

    private List<SkillUpgrade> _skillData;

    public List<SkillUpgrade> SkillData => _skillData;

    private List<SpecialAttackUpgrade> _specialAttackData;

    public List<SpecialAttackUpgrade> SpecialAttackData => _specialAttackData;

    private void Awake()
    {
        _instance = this;
    }

    public async UniTask ReadAllData()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        ReadSkillData();
        ReadSpecialAttackData();

        cts.Cancel();
    }

    private void ReadSkillData()
    {
        var path = $"Data/SkillData";

        var textAsset = Resources.Load<TextAsset>(path);

        var i = 0;

        _skillData = new List<SkillUpgrade>();

        foreach (var line in textAsset.text.Split('\n'))
        {
            if (i == 0)
            {
                i++;

                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var values = line.Replace("\r", string.Empty).Split(',');

            _skillData.Add(new SkillUpgrade(values[0], values[1], values[2], values[3], values[4]));
            i++;
        }
    }

    private void ReadSpecialAttackData()
    {
        var path = $"Data/SpecialAttackData";

        var textAsset = Resources.Load<TextAsset>(path);

        var i = 0;

        _specialAttackData = new List<SpecialAttackUpgrade>();

        foreach (var line in textAsset.text.Split('\n'))
        {
            if (i == 0)
            {
                i++;

                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var values = line.Replace("\r", string.Empty).Split(',');

            _specialAttackData.Add(new SpecialAttackUpgrade(values[0], values[1], values[2], values[3], values[4]));
            i++;
        }
    }
}