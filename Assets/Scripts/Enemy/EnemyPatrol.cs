using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private bool turning;
    public float turnTime;
    private bool squished;
    public float squishTime;
    public float moveSpeed;

    public AudioClip[] moveSounds;
    public AudioClip[] squishSounds;
    private AudioSource _audioSource;
    
    public LayerMask whatIsWall;
    public LayerMask whatIsEnemy;
    public Transform wallCheck;
    public Transform fallCheck;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public SpriteRenderer _spriterenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriterenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (DetectedWallOrFall())
        {
            transform.localScale = new Vector2(transform.localScale.x * -1,1);
            StartCoroutine(TurnTime());
        }

        if (turning) 
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
        if (squished) 
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
        UpdateAnimation();
    }
    
    public void WalkSound()
    {
        PlayRandomAudio(moveSounds);
    }

    public void SquishSound()
    {
        PlayRandomAudio(squishSounds);
    }

    private void PlayRandomAudio(AudioClip[] randomSounds)
    {
        var i = Random.Range(0, randomSounds.Length);
        _audioSource.pitch = Random.Range(0.9f, 1.3f);
        _audioSource.PlayOneShot(randomSounds[i]);
    }

    private void FixedUpdate()
    {
        if (turning) return;
        if (squished) return;
        _rigidbody2D.linearVelocityX = moveSpeed;
    }

    private bool DetectedWallOrFall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsWall) 
               || !Physics2D.OverlapCircle(fallCheck.position, 0.1f) 
               || Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsEnemy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(fallCheck.position, 0.1f);
    }
    private IEnumerator TurnTime()
    {
        turning = true;
        yield return new WaitForSeconds(turnTime);
        _spriterenderer.flipX = false;
        turning = false;
        moveSpeed *= -1;
    }

    private IEnumerator SquishTime()
    {
        squished = true;
        yield return new WaitForSeconds(squishTime);
        squished = false;
    }

    public void Squish()
    {
        StartCoroutine(SquishTime());
    }
    private void UpdateAnimation()
    {
        if (turning)
        {
            _spriterenderer.flipX = true;
            _animator.Play("Turn");
            return;
        }
        if (squished)
        {
            _animator.Play("Squish");
            return;
        }

        if (turning) return;
        if (squished) return;
     
        _animator.Play("Walk");
    }
}
