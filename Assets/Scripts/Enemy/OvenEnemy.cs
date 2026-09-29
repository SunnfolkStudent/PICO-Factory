using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class OvenEnemy : MonoBehaviour
{
    private bool turning;
    public float turnTime;
    private bool attacking;
    public float attackWindUp;
    public float explosionTime;
    public float towerTime;
    public float moveSpeed;
    
    public LayerMask whatIsWall;
    public Transform wallCheck;
    public Transform fallCheck;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriterenderer;
    private BoxCollider2D flameTowerHitBox;
    public BoxCollider2D flameExplosionHitBox;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriterenderer = GetComponent<SpriteRenderer>();
        flameTowerHitBox = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        flameExplosionHitBox = transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        flameTowerHitBox.enabled = false;
        flameExplosionHitBox.enabled = false;
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
        if (attacking) 
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (turning) return;
        if (attacking) return;
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
        yield return new WaitForSeconds(turnTime);
        _spriterenderer.flipX = false;
        turning = false;
        moveSpeed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(AttackTime());
            attacking = true;
        }
    }
    private IEnumerator AttackTime()
    {
        attacking = true;
        yield return new WaitForSeconds(attackWindUp);
        flameTowerHitBox.enabled = true;
        flameExplosionHitBox.enabled = true;
        yield return new WaitForSeconds(explosionTime);
        flameExplosionHitBox.enabled = false;
        yield return new WaitForSeconds(towerTime);
        attacking = false;
    }
    private void UpdateAnimation()
    {
        if (turning)
        {
            _spriterenderer.flipX = true;
            _animator.Play("Turn");
            return;
        }

        if (attacking)
        {
            _animator.Play("Attack");
        }
        if (turning) return;
        if (attacking) return;
     
        _animator.Play("Walk");
    }
}
