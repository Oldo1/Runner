using Assets.Scripts;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private CharacterController _characterController;
    public bool IsOnGround => _characterController.isGrounded;
    public Vector3 Velocity { get; set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        GameEvents.OnStartGame += () => enabled = true;
    }

    private void Update()
    {
        _characterController.Move(Velocity * Time.deltaTime);
    }
}
