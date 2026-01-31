using UnityEngine;

public class DeflectionHandling : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Attack hit enemy!");
        }

        if(collision.gameObject.CompareTag("Projectile")) 
        {
            //Deflect projectile in opposite direction
            Debug.Log("Projectile collision detected!");
            //Destroy(collision.gameObject);
            /*Rigidbody2D projectileRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                projectileRb.linearVelocity = -projectileRb.linearVelocity;
                Debug.Log("Projectile deflected!");
            }*/
        }
    }

    /*void OnCollisionStay2D(Collision2D collision)
    {
        // Called every frame while collider is touching
        if (playerController != null && playerController.isAttackActive)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Still touching enemy during attack");
            }
        }
    }*/
}
