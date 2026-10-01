using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
   // Audio Variables VV
   public AudioClip deathSound;
   public AudioClip[] jumpSounds;
   public AudioClip[] hurtSounds;
   public AudioClip[] moveSounds;
   private AudioSource _audioSource;


   // Dash Variables VV
   private int canDash = 1;
   private bool isDashing;
   private float dashingPower = 10f;
   private float dashingTime = 0.3f;
   private float dashingCooldown = 0.3f;
   [SerializeField] private TrailRenderer _trailRenderer;
  
   // Landing Variables
   public bool landing;
   public float VelocityY;
  
   // component and other game Object variables VV
   private InputController _input;
   private Rigidbody2D _rigidbody2D;
   private Animator _animator;
   private EnemyPatrol _enemyPatrol;
   private BoxCollider2D _boxCollider2D;
   private GameObject _spawnpoint;
   private Animator _DoorAnimator;

   // movement and jump speed and death lol VVV
   public float moveSpeed;
   public float jumpSpeed;
   public bool isDead;
   public bool key;
  
   // used for jumping and ground checks vv
   public bool isGrounded;
   public Transform groundCheck;
   public LayerMask groundLayer;
   public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);

   void Awake()
   {
       transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"));
   }
   
   void Start()
   {
       // Adds necessary components to code
       _input = GetComponent<InputController>();
       _rigidbody2D = GetComponent<Rigidbody2D>();
       _animator = GetComponent<Animator>();
       _trailRenderer = GetComponent<TrailRenderer>();
       _audioSource = GetComponent<AudioSource>();
       _boxCollider2D = GetComponent<BoxCollider2D>();
       _spawnpoint = GameObject.Find("Spawnpoint");
       _DoorAnimator = GameObject.Find("Grid").transform.GetChild(0).transform.GetComponent<Animator>();
   }
   private IEnumerator AfterDeath()
   {
       yield return new WaitForSeconds(0.5f);
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   private void GetCheckpoint()
   {
       PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
       PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
   }

   private void resetPrefs()
   {
       PlayerPrefs.DeleteAll();
   }
   
   private void Update()
   {
       isGrounded = Physics2D.OverlapBox
       (groundCheck.position,
           groundBoxSize, 0f, groundLayer);
       if (isGrounded)
       {
           canDash = 1;
       }
       if (_input.Jump && isGrounded)
       {
           // Code for jumping
           _rigidbody2D.linearVelocityY = jumpSpeed;
           PlayRandomAudio(jumpSounds);
       }
       if (_input.Jump && !isGrounded)
       {
           StartCoroutine(Dash());
       }
       EnemyBounce();
       if (landing) return;
       // plays the correct animation every frame
       UpdateAnimation();
      
       if (isDead)
       {
           _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
           _boxCollider2D.enabled = false;
           StartCoroutine(AfterDeath());
           return;
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
   }
  
   // Audio Code VVV
   public void WalkSound()
   {
       PlayRandomAudio(moveSounds);
   }
  
   // I THINK this might change the pitch of **every** sfx on the player VVV
   private void PlayRandomAudio(AudioClip[] randomSounds)
   {
       var i = Random.Range(0, randomSounds.Length);
       _audioSource.pitch = Random.Range(0.8f, 1.3f);
       _audioSource.PlayOneShot(randomSounds[i]);
   }
   
   private void FixedUpdate()
   {
       //checks if the player is currently dashing, making the player unable to move
       if (isDashing) return;
      
       //checks if the player is currently landing, making the player unable to move
       if (landing) return;
      
       // movement code
       _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
   }   
   
   // is Called Last of all updates
   public void LateUpdate()
   {
       VelocityY = _rigidbody2D.linearVelocityY;
   }
  
  
   // Used for Animation Updating and Playing Animations.
   private void UpdateAnimation()
   {
       if (isDead)
       {
           _animator.Play("Death Explosion");
           return;
       }
       if (isGrounded)
       {
           if (VelocityY <= -16f)
           {
               Land();
               return;
           }
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
   
   // function for starting the "LandStun" Enumerator
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
   
   // function for dashing and logic VV
   private IEnumerator Dash()
   {
       if (_input.Horizontal == 0 && _input.Vertical == 0)
       {
           yield break;
       }
       if (_input.Horizontal != 0 && _input.Vertical == 0)
       {//Horizontal
           if (canDash > 0)
           {
               canDash--;
               isDashing = true;
               _rigidbody2D.gravityScale = 1f;
               _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower * 1.6f, _input.Vertical * dashingPower);
               _trailRenderer.emitting = true;
           }
       }
       if (_input.Horizontal != 0 && _input.Vertical != 0)
       {//Diagonal
           if (canDash > 0)
           {
               canDash --;
               isDashing = true;
               _rigidbody2D.gravityScale = 1.2f;
               _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower * 1.3f, _input.Vertical * dashingPower * 1.3f);
               _trailRenderer.emitting = true;
           }
       }
       if (_input.Horizontal == 0 && _input.Vertical != 0)
       {//Vertical
           if (canDash > 0)
           {
               canDash --;
               isDashing = true;
               _rigidbody2D.gravityScale = 5f;
               _rigidbody2D.linearVelocity = new Vector2(_input.Horizontal * dashingPower, _input.Vertical * dashingPower * 1.8f);
               _trailRenderer.emitting = true;
           }
       }
       yield return new WaitForSeconds(dashingTime);
       canDash --;
       isDashing = false;
       _rigidbody2D.gravityScale = 2f;
       _trailRenderer.emitting = false;
   }
   
   //function for Land animation and logic
   private IEnumerator LandStun()
   {
       _rigidbody2D.linearVelocity = Vector2.zero;
       _animator.Play("Land");
       landing = true;
       yield return new WaitForSeconds(0.5f);
       landing = false;
   }
   
   // allow the Death animation to play?
   private IEnumerator DeathAnimation()
   {
       yield return new WaitForSeconds(1f);
   }
   
   //function for bouncing on enemies (also calls the function in enemy code to stun him)
   private void EnemyBounce()
   {
       var hitInfo = Physics2D.OverlapCircle(groundCheck.position, 0.3f, LayerMask.GetMask("Enemy"));
       if (hitInfo != null)
       {
           hitInfo.TryGetComponent(out EnemyPatrol enemy);
           enemy.Squish();
          
           _rigidbody2D.linearVelocityY = jumpSpeed * 1.5f;
       }
   }
   
   private void OnCollisionEnter2D(Collision2D other)
   {    // plays death animation and does death logic if touched "death"
       if (other.transform.CompareTag("Death"))
       {
           isDead = true;
           StartCoroutine(DeathAnimation());
           //PlayRandomAudio(deathSound); --- Doesn't Work!
           _audioSource.PlayOneShot(deathSound);
       }
       // open door if the player has the key of the level
       if (other.transform.CompareTag("Door"))
       {
           if (key)
           { 
               _DoorAnimator.Play("Door Open");
           }
       }
   }
   // functions for the fan air to push the player up
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.transform.CompareTag("Fan Air"))
       {
           _rigidbody2D.gravityScale = -4f;
       }
       
       if (other.transform.CompareTag("Key"))
       {
           Destroy(other.gameObject);
           key = true;
       }

       if (other.transform.CompareTag("Checkpoint"))
       {
           GetCheckpoint();
       }
   }
   private void OnTriggerExit2D(Collider2D other)
   {
       if (other.transform.CompareTag("Fan Air"))
       {
           _rigidbody2D.gravityScale = 2f;
       }
   }
  
}
