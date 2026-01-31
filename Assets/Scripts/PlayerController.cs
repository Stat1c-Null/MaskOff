using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveInput;
    public float speed = 1.2f;
    private Animator anim;
    InputAction attackAction;

    void Start()
    {
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
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetBool("isWalking", true);
    }



    


}
