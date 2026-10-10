using System;
using UnityEngine;

public class KeyControllerAU : MonoBehaviour
{
    public AudioClip pickup;
    public AudioSource _audioSource;

    public bool key;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                key = true;
                AudioSource.PlayClipAtPoint(pickup, transform.position);
                Destroy (gameObject);
            }
        }
}
