using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    private void Awake()
    {
        transform.DOScale(new Vector3(1.25f, 1.25f, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOComplete();
    }
}
