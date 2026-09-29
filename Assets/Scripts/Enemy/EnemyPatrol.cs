using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private bool turning;
    private bool squished;
    public float moveSpeed;

    public LayerMask whatIsWall;
    public Transform wallCheck;
    public Transform fallCheck;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private PlayerController pc;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (DetectedWallOrFall())
        {
            moveSpeed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1,1);
        }
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = moveSpeed;
    }

    private bool DetectedWallOrFall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsWall) 
               || !Physics2D.OverlapCircle(fallCheck.position, 0.1f);
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
        _rigidbody2D.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        turning = false;
    }

    private IEnumerator SquishTime()
    {
        squished = true;
        _rigidbody2D.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        squished = false;
    }

    public void Squish()
    {
        StartCoroutine(SquishTime());
    }
    private void UpdateAnimation()
    {
        if (DetectedWallOrFall() && !turning)
        { 
            _animator.Play("Turn");
            StartCoroutine(TurnTime());
            return;
        }
        if (squished)
        {
            _animator.Play("Squish");
            return;
        }
        _animator.Play("Walk");
    }
}
