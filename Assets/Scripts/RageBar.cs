using UnityEngine;

public class RageBar : MonoBehaviour
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
        anim.SetInteger("RageBarStatus", Mathf.FloorToInt(player.GetRage() / 25f));
    }
}
