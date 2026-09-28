using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dash Variables VV
    private bool canDash = true;
    private bool isDashing; 
    private float dashingPower = 11.5f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 0.3f;
    [SerializeField] private TrailRenderer _trailRenderer;
    
    // Landing Variables
    private bool landing;
    
    // component variables VV
    private InputController _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    // movement and jump speed
    public float moveSpeed;
    public float jumpSpeed;

    
    [SerializeField] private float minimumImpactSpeed = 10f;
    
    
    // used for jumping and ground checks vv
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    
    void Start()
    {
        // Adds necessary components to code
        _input = GetComponent<InputController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox
        (groundCheck.position, 
            groundBoxSize, 0f, groundLayer);
        
        if (_input.Jump && isGrounded)
        {
            // Code for jumping
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        
        if (_input.Horizontal > 0)
        {
            // flips character sprite right when moving to the right
            // also holds the character looking right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_input.Horizontal < 0)
        {
            // flips character sprite left when moving to the left
            // also holds the character looking left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (_input.Jump && !isGrounded)
        {
            StartCoroutine(Dash());
        }
        // plays the correct animation every frame
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        //checks if the player is currently dashing, making the player unable to move
        if (isDashing) {return;}
        
        //checks if the player is currently landing, making the player unable to move
        if (landing) {return;}
        
        // movement code
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && isGrounded)
        {
            float impactSpeed = Mathf.Abs(collision.relativeVelocity.y);
            if (impactSpeed >= minimumImpactSpeed)
            {
                Land();
            }
        }
    }
    // Used for Animation Updating and Playing Animations.
    private void UpdateAnimation()
    {
        if (isGrounded)
        {
            if (_input.Horizontal != 0)
            {
                // Plays the "run" animation when moving (have a velocity x of either 1 or -1)
                _animator.Play("Run");
            }
            else
            {
                // Plays the "Idle" animation when not doing anything and touching the ground.
                _animator.Play("Idle");
            }
        }
        else
        {
            if (_rigidbody2D.linearVelocityY > 0)
            {
                // Plays the "Jump" animation when moving upwards and not touching the ground
                _animator.Play("Jump");
            }
            else if (_rigidbody2D.linearVelocityY == 0)
            {
                // Plays the "Apex_Jump" animation when not moving up or down and not touching the ground.
                _animator.Play("Apex_Jump");
            }
            else if (_rigidbody2D.linearVelocityY < 0)
            {
                // Plays the "Fall" animation when falling.
                _animator.Play("Fall");
            }
        }
        
    }

    private void Land()
    {
        StartCoroutine(LandStun());
    }
    // Visualizes the hitbox for "groundCheck".
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
    private IEnumerator Dash()
    {
        if (_input.Horizontal == 0 && _input.Vertical == 0)
        {
            yield break;
        }
        if (_input.Horizontal != 0 && _input.Vertical == 0)
        {//Horizontal 
            canDash = false;
            isDashing = true;
            _rigidbody2D.gravityScale = 1f;
            _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower * 1.6f, _input.Vertical * dashingPower);
            _trailRenderer.emitting = true;
        }
        if (_input.Horizontal != 0 && _input.Vertical != 0)
        {//Diagonal 
            canDash = false;
            isDashing = true;
            _rigidbody2D.gravityScale = 1.2f;
            _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower * 1.3f, _input.Vertical * dashingPower * 1.3f);
            _trailRenderer.emitting = true;
        }
        if (_input.Horizontal == 0 && _input.Vertical != 0)
        {//Vertical 
            canDash = false;
            isDashing = true;
            _rigidbody2D.gravityScale = 5f;
            _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower, _input.Vertical * dashingPower * 1.8f);
            _trailRenderer.emitting = true;
        }
        yield return new WaitForSeconds(dashingTime);
        canDash = true;
        isDashing = false; 
        _rigidbody2D.gravityScale = 2f;
        _trailRenderer.emitting = false;
    }

    private IEnumerator LandStun()
    {
        landing = true;
        _animator.Play("Land");
        yield return new WaitForSeconds(0.5f);
        landing = false;
    }
}
