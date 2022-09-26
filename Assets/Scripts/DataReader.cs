using System.Threading;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    private SkillUI[] _skillData;

    public SkillUI[] SkillData => _skillData;

    public async UniTask ReadAllData()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        ReadSkillData();

        cts.Cancel();
    }


    private void ReadSkillData()
    {
        var path = $"Data/SkillData";

        var textAsset = Resources.Load<TextAsset>(path);

        var i = 0;

        _skillData = new SkillUI[textAsset.text.Split('\n').Length];

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

            _skillData[i] = new SkillUI(values[0], values[1], values[2], values[3]);
            i++;
        }
    }
}