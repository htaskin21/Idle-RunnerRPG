using DG.Tweening;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public Tweener StartRunning()
    {
        var go = gameObject;
        return go.transform.DOMoveX(go.transform.position.x + 1, 1).SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}