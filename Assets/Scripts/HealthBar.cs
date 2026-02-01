using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Animator anim;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(player.GetHealth() <= 80 && player.GetHealth() >= 61)
        {
            anim.SetInteger("HealthStatus", 1);
        }
        else if (player.GetHealth() <= 60 && player.GetHealth() >= 41)
        {
            anim.SetInteger("HealthStatus", 2);
        }
        else if (player.GetHealth() <= 40 && player.GetHealth() >= 21)
        {
            anim.SetInteger("HealthStatus", 3);
        }
        else if (player.GetHealth() <= 20 && player.GetHealth() >= 1)
        {
            anim.SetInteger("HealthStatus", 4);
            
        }
        else if (player.GetHealth() == 0)
        {
            
            StartCoroutine("DeathSequence");
            anim.SetInteger("HealthStatus", 5);
        }*/
        anim.SetInteger("HealthStatus", 5 - Mathf.CeilToInt(player.GetHealth() / 20f));
        Debug.Log(Mathf.CeilToInt(player.GetHealth() / 20f));
     }

}
