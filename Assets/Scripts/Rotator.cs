using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Awake()
    {
        //transform.DORotate(new Vector3(0, 360, 0), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);   
    }

    private void Update()
    {
        transform.Rotate(_rotateSpeed * Time.deltaTime % 360 * Vector3.up);
    }
}
