using UnityEngine;

public class DeflectionHandling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 incident = rb.linearVelocity;
            rb.AddForce(-1f*incident);
        }

    }
}
