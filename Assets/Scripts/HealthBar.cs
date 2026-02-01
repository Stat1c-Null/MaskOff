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
        anim.SetInteger("HealthStatus", 5 - Mathf.CeilToInt(player.GetHealth() / 20f));
     }

}
