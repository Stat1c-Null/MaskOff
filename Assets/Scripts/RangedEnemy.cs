using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform player;
    private Transform tf;

    public float range; //sight range for player
    private bool inRange = false;
    public float moveSpeed; // movement speed (currently unused)
    public float turnSpeed; // speed at which enemy turns towards player (deg/sec)

    public Vector3 left; // direction that the enemy aims (set on line 42)

    public Transform spawn; //spawn location for projectiles
    public GameObject projectile; //thrown item
    public float projSpeed;

    //used to control fire rate
    private float timer = 0f; 
    private float cooldown = .75f;

    void Start()
    {
        if (range <= 0)
            range = 5;
        tf = GetComponent<Transform>();
    }

    void Update()
    {
        Vector2 dist = player.position - tf.position; //vector between player and enemy
        inRange = (dist.magnitude <= range);

        //Color col = inRange ? Color.red : Color.blue;
        //Debug.DrawRay(tf.position, dist, col);
        //Debug.DrawRay(tf.position, -tf.right * 1.5f, Color.cyan);

        if (inRange)
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

        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer < 0) timer = 0;
    }

    void Attack()
    {
        GameObject proj = Instantiate(projectile, spawn.position, spawn.rotation);
        proj.transform.Rotate(0f, 0f, 90f);
        proj.GetComponent<Rigidbody2D>().linearVelocity = left * projSpeed;
    }
}
