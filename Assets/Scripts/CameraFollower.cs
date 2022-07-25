using DG.Tweening;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Transform character;

    private void Start()
    {
        mainCamera.transform.DOMoveX(-character.position.x - .3f, 1f).SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}