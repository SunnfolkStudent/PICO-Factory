using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class BoomAController : MonoBehaviour
{
    public GameObject bullet;
    private Animator _animator;
    private float waitTime;
    private float waitTime2;
    private Transform spawn;
    private Vector2  spawnPosition;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spawn = transform.GetChild(0).transform;
    }

    private void Start()
    {
        spawnPosition = spawn.position;
        waitTime = 0.7f;
        waitTime2 = 0.35f;
        Shoot();
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(bullet, spawnPosition, quaternion.identity);
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
