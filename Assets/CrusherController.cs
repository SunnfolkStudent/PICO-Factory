using UnityEngine;

public class AnimationAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpecificClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    public void PlaySoundEffect()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}