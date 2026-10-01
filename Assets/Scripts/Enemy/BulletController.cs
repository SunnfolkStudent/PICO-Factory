using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float moveSpeed = -3f;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
}
