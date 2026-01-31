using UnityEngine;

public class DeflectionHandling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    
    void OnCollisionStay2D(Collision2D collision)
    {
        // Called every frame while collider is touching
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Still touching enemy");
        }

        /*if(collision.gameObject.CompareTag("Projectile"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 incident = rb.linearVelocity;
            rb.AddForce(-1f*incident);
        }*/

    }
}
