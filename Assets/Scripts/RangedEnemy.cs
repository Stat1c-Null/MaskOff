using UnityEngine;

public class RangedEnemy : Enemy
{
    private bool inRange = false;
    public float turnSpeed; // speed at which enemy turns towards player (deg/sec)

    public Vector3 left; // direction that the enemy aims (set on line 42)
    public Transform spawn; //spawn location for projectiles
    public GameObject projectile; //thrown item
    public float projSpeed;

    //used to control fire rate
    private float timer = 0f; 
    [SerializeField] private float cooldown = .75f;
    Vector2 dist;

    void Update()
    {
        dist = player.transform.position - tf.position; //vector between player and enemy
        inRange = (dist.magnitude <= aggroRange);

        if (inRange && currentState != State.Attacking)
        {
            //Debug.Log("Player in range! Distance Attacking!");
            currentState = State.Attacking;
        }

        //Switch behaviour based on currentState
        if (currentState == State.Wandering)
        {
            anim.Play("RangedIdle");

            if (!choosingDirection)
            {
                choosingDirection = true;
                StartCoroutine(Wander());
            }
        }
        else if (currentState == State.Attacking)
        {
            anim.Play("RangedAttack");
            RangeAttack();
        }

        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer < 0) timer = 0;
    }

    void RangeAttack()
    {
        left = -tf.right;
        float theta = Mathf.Atan2( left.x*dist.y - left.y*dist.x, left.x*dist.x + left.y*dist.y ); //angle between player direction and aim direction
        theta *= (180 / Mathf.PI); //degrees conversion
        //Debug.Log("theta: " + theta);

        if(Mathf.Abs(theta) <= 10 && timer == 0)
        {
            Attack();
            timer = cooldown;
        }

        int turnDir = (theta > 0) ? 1 : -1;
        float turnThisMuch = turnDir * turnSpeed * Time.deltaTime;

        if ( Mathf.Abs(theta) < Mathf.Abs(turnSpeed*Time.deltaTime) )
        {
            tf.Rotate(0, 0, theta * turnDir);
        }
        else
        {
            tf.Rotate(0, 0, turnThisMuch);
        }
    }

    void Attack()
    {
        GameObject proj = Instantiate(projectile, spawn.position, spawn.rotation);
        proj.transform.Rotate(0f, 0f, 90f);
        proj.GetComponent<Rigidbody2D>().linearVelocity = left * projSpeed;
        Destroy(proj, 5f);
    }
}
