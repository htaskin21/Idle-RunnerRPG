using DG.Tweening;
using UnityEngine;

public class SkyImage : MonoBehaviour
{
    public SpriteRenderer firstLayer;

    public SpriteRenderer secondLayer;

    public SpriteRenderer thirdLayer;

    private void Start()
    {
        var go = gameObject;
        go.transform.DOMoveX(go.transform.position.x + 1, 1.05f).SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}