using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Mathematics;

public class BoomAController : MonoBehaviour
{
    public GameObject bullet;
    private Animator _animator;
    private float waitTime;
    private float waitTime2;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        waitTime = 0.7f;
        waitTime2 = 0.35f;
        Shoot();
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(bullet, transform.position, quaternion.identity);
        yield return new WaitForSeconds(waitTime2);
        StartCoroutine(ShootCooldown());
    }
    
    private IEnumerator ShootCooldown()
    {
        _animator.Play("Idle");
        yield return new WaitForSeconds(2);
        Shoot();
    }
    public void Shoot()
    {
        StartCoroutine(ShootTimer());
        _animator.Play("Attack");
    }
}
