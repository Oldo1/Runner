using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    public bool IsOnGround => _characterController.isGrounded;
    public Vector3 Velocity { get; set; }

    private void Update()
    {
        _characterController.Move(Velocity * Time.deltaTime);
    }
}
