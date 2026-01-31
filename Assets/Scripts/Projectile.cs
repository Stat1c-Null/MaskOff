using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(col.gameObject.GetComponent<PlayerController>().canGetHit == true)
            {
                col.gameObject.GetComponent<PlayerController>().Hit();
                Debug.Log("Player hit by projectile!");
            }      
            Destroy(gameObject);
        }
    }
}
