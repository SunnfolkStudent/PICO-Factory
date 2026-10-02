using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BoomAController : MonoBehaviour
{
    public GameObject bullet;
    private Animator _animator;
    private float waitTime;
    private float waitTime2;
    private Transform spawn;
    private Vector2  spawnPosition;

    public AudioClip[] shootSounds; 
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        spawn = transform.GetChild(0).transform;
    }
    
    public void ShootSounds()
    {
        PlayRandomAudio(shootSounds);
    }
    
    private void PlayRandomAudio(AudioClip[] randomSounds)
     {
        var i = Random.Range(0, randomSounds.Length);
        _audioSource.pitch = Random.Range(2.5f, 2.5f);
        _audioSource.PlayOneShot(randomSounds[i]);
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
        PlayRandomAudio(shootSounds);
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
