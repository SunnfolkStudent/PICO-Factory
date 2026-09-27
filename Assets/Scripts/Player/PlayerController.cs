using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController _input;
    private Rigidbody2D _rigidbody2D;

    public float moveSpeed;
    public float jumpSpeed;
    void Start()
    {
        _input = GetComponent<InputController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_input.Jump)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }
}
