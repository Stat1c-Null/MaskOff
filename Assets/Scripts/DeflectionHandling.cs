using UnityEngine;

public class DeflectionHandling : MonoBehaviour
{
    private PlayerController playerController;
    private float damageAmount;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (playerController != null)
        {
            damageAmount = playerController.inRage ? playerController.rageDamageAmount : playerController.normalDamageAmount;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && playerController != null && (playerController.isAttackActive || playerController.isRageAttackActive))
        {
            //Debug.Log("Attack hit enemy!");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
        }

        if(collision.gameObject.CompareTag("Projectile")) 
        {
            Rigidbody2D projectileRb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            // Check if player is attacking
            if (playerController != null && (playerController.isAttackActive || playerController.isRageAttackActive) && projectileRb != null)
            {
                // Deflect projectile in opposite direction
                projectileRb.linearVelocity = -projectileRb.linearVelocity * 2f;
                //Debug.Log("Projectile deflected!");
            }
            else
            {
                // Projectile hits player while not attacking - deal damage
                //Debug.Log("Player hit by projectile!");
                playerController.Hit();
                Destroy(collision.gameObject);
            }
        }
    }
}
