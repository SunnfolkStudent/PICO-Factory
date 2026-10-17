using UnityEngine;

public class FireballV2 : MonoBehaviour
{
    public float _moveSpeed = 2f;

    public AudioClip fireClear; 
    private AudioSource _audioSource;
    
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D.linearVelocityY = -_moveSpeed; 
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            transform.position = new Vector3(transform.position.x, 9.5f);
        }
    }
}

/*
 using UnityEngine;

public class FireballHorController : MonoBehaviour
{
    private float _moveSpeed = 3f;

    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.linearVelocityX = -_moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            transform.position = new Vector3(10.5f, 4.5f, 0);
        }

    }
}
*/