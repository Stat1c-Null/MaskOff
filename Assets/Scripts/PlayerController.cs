using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
    InputAction rageAction;
    InputAction dashAction;
    InputAction move;
    public bool facingRight = true;
    PlayerInput PI;
    bool canDash = true;
    bool canRage = true; //WILL BE CHANGED LATER
    bool secondDash = false;


    bool inRage = false;

    [SerializeField] protected float health;
    public bool canGetHit = true;
    private float damageCD = 2f;


    void Start()
    {
        //getting components, you know how it be.
        PI = GetComponent<PlayerInput>();
        AttackHB = Attack.GetComponent<BoxCollider2D>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
        rageAction = InputSystem.actions.FindAction("RAGE");
        move = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Sprint"); 
    }

    void Update()
    {
        
        anim.SetBool("RAttack", false); //set bool to false to allow attacks once animation is over
        rb.linearVelocity = moveInput * speed;
        if (rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0)
        {
            anim.SetBool("isWalking", false);
        }

        if (rageAction.IsPressed() && !inRage && canRage)
        {
            anim.Play("GoatTransform");
            PI.actions.Disable();
            Invoke("EnableActions", 1.5f);
            inRage = true;
            secondDash = true;



        }
        if (rageAction.IsPressed() && inRage /*|| rageIsOver*/) 
        {
            anim.Play("PigIdleRight");
            inRage = false;
            secondDash = false;
            StartCoroutine("RageCooldown");
        }


        if (attackAction.IsPressed() && !inRage)
        {
            anim.Play("PigSwingRight");
            
        }

        if (attackAction.IsPressed() && !inRage)
        {
            //anim.Play("GoatSwing1");

        }

        if (dashAction.IsPressed() && canDash  && !inRage)
        {
            //anim.Play("PigDash");
            rb.MovePosition(rb.position + moveInput * 3.5f);
            StartCoroutine(DashCooldown());
        }
        else if (dashAction.IsPressed() && canDash && inRage)
        {
            //gets two dashes
            //anim.Play("GoatDash");
            rb.MovePosition(rb.position + moveInput * 3.5f);
            if(secondDash)
            {
                secondDash = false;
                StartCoroutine(SecondDashBuffer());
            }
            else { StartCoroutine(DashCooldown()); }
            


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


    void EnableActions()
    {
        PI.actions.Enable();
    }

    IEnumerator DashCooldown()
    {
        //when ui is worked on, pass it into here to deplete bar
        canDash = false;
        yield return new WaitForSeconds(1f); //cooldown duration
        canDash = true;
        if (inRage)
        {
            secondDash = true;
        }
    }
    IEnumerator SecondDashBuffer()
    {
        //keeps player from accidentally using two dashes instantly
        canDash = false;
        yield return new WaitForSeconds(0.15f); //cooldown duration
        canDash = true;
        
        
    }

    IEnumerator RageCooldown()
    {
        canRage = false;
        yield return new WaitForSeconds(2f); // WILL BE MUCH LONGER
        canRage = true;
    }

    IEnumerator Damage()
    {
        health -= 10;
        Debug.Log(health);
        canGetHit = false;
        yield return new WaitForSeconds(damageCD);
        canGetHit = true;
    }

    public void Hit()
    {
        StartCoroutine(Damage());
    }

}
