using UnityEngine;
using UnityEngine.InputSystem;

//Basic 2D movement
public class PlayerMove : MonoBehaviour
{
    public float speed;
    private Transform tf;
    private Vector2 movement;
    
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xDir = movement.x;
        float yDir = movement.y;

        tf.position += xDir * tf.right * speed * Time.deltaTime;
        tf.position += yDir * tf.up * speed * Time.deltaTime;
    }

    void OnMove(InputValue IV)
    {
        movement = IV.Get<Vector2>();
    }
}
