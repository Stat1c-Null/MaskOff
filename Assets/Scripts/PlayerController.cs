using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject Attack;
    BoxCollider2D AttackHB;
    Transform tf;
    Rigidbody2D rb;
    private Vector2 moveInput;
    public float speed = 1.2f;
    private Animator anim;
    InputAction attackAction;
    public bool facingRight = true;


    void Start()
    {
        AttackHB = Attack.GetComponent<BoxCollider2D>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        anim.SetBool("RAttack", false);
        rb.linearVelocity = moveInput * speed;
        if (rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0)
        {
            anim.SetBool("isWalking", false);
        }
        
        if (attackAction.IsPressed())
        {
            anim.Play("PigSwingRight");
            
            
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PigSwingRight"))
        {
            AttackHB.enabled = true;

        }
        else
        {
            AttackHB.enabled = false;
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.x < 0)
        {
            facingRight = false;
            tf.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (moveInput.x > 0)
        {
            facingRight = true;
            tf.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        anim.SetBool("isWalking", true);
    }



    


}
