using UnityEngine;

public class WakeUpAnimController : MonoBehaviour
{
    // Audio Clips and reference to the Source VVV 
    public AudioClip fallThump; 
    public AudioClip groundThump;
    private AudioSource _audioSource; 
    
    // 'Methods' for Animation Event systems VVV 
    public void FallSound()
    {
        _audioSource.PlayOneShot(fallThump);
    }
    
    public void FallGroundSound()
    {
        _audioSource.PlayOneShot(groundThump);
    }
    
    // Essential. Give Audio Source VVV 
    void Start()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }
}
