using Assets.Scripts;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    public bool IsOnGround => _characterController.isGrounded;
    public Vector3 Velocity { get; set; }

    public void Init()
    {
        GameEvents.OnStartGame += Enable;
    }

    private void Update()
    {
        _characterController.Move(Velocity * Time.deltaTime);
    }

    private void Enable()
    {
        enabled = true;
    }

    private void OnDestroy()
    {
        GameEvents.OnStartGame -= Enable;
    }
}
