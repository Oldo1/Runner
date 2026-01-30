using Assets.Scripts;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IService
{
    [SerializeField] private CharacterController _characterController;

    public bool IsOnGround => _characterController.isGrounded;
    public Vector3 Velocity { get; set; }

    public void Init()
    {
        ServiceLocator.Register(this);
    }

    private void Update()
    {
        _characterController.Move(Velocity * Time.deltaTime);
    }
}
