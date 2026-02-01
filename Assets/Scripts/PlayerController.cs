using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    
    public AudioClip hitSound;
    public AudioClip block;
    public AudioClip pigDash; 
    public AudioClip goatSound;
    public AudioClip goatHit;
    public AudioClip goatDash;
    public AudioClip goatWalk;
    public AudioClip pigWalk;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip woosh;
    public AudioClip transform;
    private AudioSource audioSource;

    

    
    public GameObject PigAttack;
    public GameObject PigDash;
    public GameObject PigBlock;

    BoxCollider2D PAHB;
    BoxCollider2D PDHB;
    BoxCollider2D PBHB;

    BoxCollider2D GAHB; //goat attack hit box
    BoxCollider2D GDHB; // goat dash hit box
    BoxCollider2D GGHB;//goat grab hit box

    Transform tf;
    Rigidbody2D rb;
    private Vector2 moveInput;
    public float speed = 1.2f;
    public float normalSpeed = 5f;
    public float rageSpeed = 7f;
    private Animator anim;
    InputAction secondAction;
    InputAction attackAction;
    InputAction rageAction;
    InputAction dashAction;
    InputAction move;
    
    public bool facingRight = true;
    PlayerInput PI;
    bool canDash = true;
    bool canRage = false;
    bool secondDash = false;

    bool canBlock = true;
    bool successfulBlock = true;
    public bool isAttackActive = false;
    public bool isRageAttackActive = false;

    public bool inRage = false;
    //TO WHOMEVER IS DEALING WITH DAMAGE, you can put the player into the fall animation by using anim.SetBool("isHurt",true);

    [SerializeField] protected float health = 100f;
    [SerializeField] protected float rage;
    private float rageMax = 100f;
    [SerializeField] private float rageDecrease = 5f; //By how much rage decreases when in rage mode
    public bool canGetHit = true;
    private float damageCD = 2f;
    private bool rageIsOver = false;
    //How much damage the player does in different states
    public float normalDamageAmount;
    public float rageDamageAmount;
    //How much damage the player receives in different states
    [SerializeField] private float receiveNormalStateDamage;
    [SerializeField] private float receiveRageStateDamage;
    public float rageIncrease; //Rage increase per killed enemy

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = pigWalk;
        

        //getting components, you know how it be.
        PI = GetComponent<PlayerInput>();

        PAHB = PigAttack.GetComponent<BoxCollider2D>(); //pig attack hit box
        PDHB = PigDash.GetComponent<BoxCollider2D>(); // ''  dash '' ''
        PBHB = PigBlock.GetComponent<BoxCollider2D>(); // '' block '' ''

        GAHB = GameObject.Find("GoatAttack").GetComponent<BoxCollider2D>(); //goat attack hit box
        GDHB = GameObject.Find("GoatDash").GetComponent<BoxCollider2D>(); // ''  dash '' ''
        GGHB = GameObject.Find("GoatGrab").GetComponent<BoxCollider2D>(); // '' grab '' ''

        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        secondAction = InputSystem.actions.FindAction("Second");
        attackAction = InputSystem.actions.FindAction("Attack");
        rageAction = InputSystem.actions.FindAction("RAGE");
        move = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Sprint");
        speed = normalSpeed;


    }
    void Update()
    {
        // Don't process input when dialog is active
        if (DialogController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            moveInput = Vector2.zero;
            return;
        }

        if (InputSystem.actions.FindAction("RageDebug").IsPressed())
        {
            rage = 100f;
            canRage = true;
        }
       
        anim.SetBool("RAttack", false); //set bool to false to allow attacks once animation is over
        rb.linearVelocity = moveInput * speed;
        if (rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0)
        {
            audioSource.loop = false;
            //if (audioSource.clip.name == pigWalk.name || audioSource.clip.name == goatWalk.name)
            //{
            //    audioSource.Pause;
            //}

            anim.SetBool("isWalking", false);
        }



        //Set rage to be active
        if (rageAction.IsPressed() && !inRage && canRage)
        {
            
            audioSource.PlayOneShot(transform);
            
            inRage = true;
            secondDash = true;
            anim.Play("GoatTransform");
            anim.SetBool("Rage", true);
            speed = rageSpeed;
            PI.actions.Disable();
            Invoke("EnableActions", 1.5f);
            canRage = false;
            SetHealth(100f); //restore health on rage
            audioSource.clip = goatSound;
            audioSource.Play();
        }
        //End rage
        if ((rageAction.IsPressed() && inRage) || rageIsOver)
        {
            
            anim.Play("PigHurt");
            anim.SetBool("Rage", false);
            speed = normalSpeed;
            inRage = false;
            secondDash = false;
            rageIsOver = false;
        }
        //Rage Decrease Over Time
        if (inRage)
        {
            rage -= rageDecrease * Time.deltaTime;
            if (rage <= 0)
            {
                rageIsOver = true;
                rage = 0;
            }
        }
        if (secondAction.IsPressed() && !inRage && canBlock) //block for pig
        {
            audioSource.PlayOneShot(block);
            canBlock = false;
            successfulBlock = false;
            
            anim.Play("PigBlock");
            while (anim.GetCurrentAnimatorStateInfo(0).IsName("PigBlock"))
            {
                
                if (successfulBlock)
                {
                    //CODE HERE WILL VALIDATE HIT AND IF TRUE



                    PBHB.enabled = true;
                    anim.Play("GoodBlock");
                    EnableActions();

                    //AND can
                    //++BIG RAGE INCREASE++

                }
            }

            PBHB.enabled = false;
            successfulBlock = true;
            StartCoroutine(BadBlockCooldown());

            PI.actions.Disable();
            Invoke("EnableActions", 0.5f);
        }

        if (secondAction.IsPressed() && inRage) //grab for goat guy
        {
            //anim.Play("GoatGrab");

        }

        if (attackAction.IsPressed() && !inRage)
        {
            if (audioSource.clip.name != "Bat Hit" && audioSource.clip.name != "Goat Hit")
            {
                audioSource.PlayOneShot(hitSound);
            }


                
            
            
            anim.Play("PigSwingRight");
        }

        if (attackAction.IsPressed() && inRage)
        {
            if (audioSource.clip.name != "Bat Hit" && audioSource.clip.name != "Goat Hit")
            {
                audioSource.PlayOneShot(goatHit);
            }


            
            
            
            anim.Play("GoatSwing");

        }

        if (dashAction.IsPressed() && canDash && !inRage && !(rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0))
        {
            
            audioSource.PlayOneShot(pigDash);
            anim.Play("PigDash");
            rb.MovePosition(rb.position + moveInput * 3.5f);
            StartCoroutine(DashCooldown());
        }
        else if (dashAction.IsPressed() && canDash && inRage && !(rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0))
        {
            
            audioSource.PlayOneShot(pigDash);
            anim.Play("GoatDash");
            rb.MovePosition(rb.position + moveInput * 3.5f);
            if (secondDash)
            {
                secondDash = false;
                StartCoroutine(SecondDashBuffer());
            }
            else { StartCoroutine(DashCooldown()); }
        }

        //Series of if statements that toggle on and off hit boxes for their respective moves depending on if the animation is playing
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PigSwingRight")) 
        {
            
            PAHB.enabled = true;
            isAttackActive = true;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("GoatSwing"))
        {
            
            GAHB.enabled = true;
            isRageAttackActive = true;

        }
        else
        {
            
            PAHB.enabled = false;
            isAttackActive = false;
            GAHB.enabled = false;
            isRageAttackActive = false;
        }
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PigDash"))
        {
            PDHB.enabled = true;

        }
        else
        {
            PDHB.enabled = false;
        }

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GoatDash"))
        {
            GAHB.enabled = true;

        }
        else
        {
            GAHB.enabled = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GoatGrab"))
        {
            GAHB.enabled = true;

        }
        else
        {
            GAHB.enabled = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Ignore input when dialog is active
        if (DialogController.IsGamePaused)
            return;

        moveInput = context.ReadValue<Vector2>();
        audioSource.loop = true;
        if (!inRage)
        {
            audioSource.clip = pigWalk;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = goatWalk;
            audioSource.Play();
        }
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

    /*IEnumerator RageCooldown()
    {
        canRage = false;
        yield return new WaitForSeconds(2f); // WILL BE MUCH LONGER
        canRage = true;
    }*/

    IEnumerator BadBlockCooldown()
    {
        canBlock = false;
        yield return new WaitForSeconds(4f); // WILL BE MUCH LONGER
        canBlock = true;
    }

    IEnumerator Damage()
    {
        float damage = inRage ? receiveRageStateDamage : receiveNormalStateDamage;
        health -= damage;
        if (!inRage)
        {
            anim.Play("PigHurt");
        }
        else
        {
            anim.Play("GoatHurt");
        }
        PI.actions.Disable();
        Invoke("EnableActions", 0.5f);
        canGetHit = false;
        yield return new WaitForSeconds(damageCD);
        canGetHit = true;
    }

    public void Hit()
    {
        StartCoroutine(Damage());
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }

    public float GetRage()
    {
        return rage;
    }

    public void IncreaseRage(float amount)
    {
        if(!inRage) {
            rage += amount;
            if (rage > rageMax)
            {
                rage = rageMax;
            }
            if (rage >= rageMax)
            {
                canRage = true;
            }
        }   
    }

    
    
}



