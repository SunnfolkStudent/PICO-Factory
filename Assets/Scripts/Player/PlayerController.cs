using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    public float moveSpeed;
    public float jumpSpeed;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    void Start()
    {
        _input = GetComponent<InputController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
        
        if (_input.Jump && isGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    private void UpdateAnimation()
    {
        if (isGrounded)
        {
            if (_input.Horizontal != 0)
            {
                _animator.Play("Run");
            }
            else
            {
                _animator.Play("Idle");
            }
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0)
            {
                _animator.Play("Jump");
            }
            else if (_rigidbody2D.linearVelocityY == 0)
            {
                _animator.Play("Apex_Jump");
            }
            else if (_rigidbody2D.linearVelocityY < 0)
            {
                _animator.Play("Fall");
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
        
}
