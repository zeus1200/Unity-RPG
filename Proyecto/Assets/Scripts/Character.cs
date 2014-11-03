using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    protected float speed, movementTime, movementTimeRandomMove, movementTimeRandomStop;
    protected Animator animator;
    protected Vector2 movement;


    // Use this for initialization
    void Start()
    {
        movementTime = 0;
        movementTimeRandomStop = Random.Range(0.5f, 1f);
        movementTimeRandomMove = Random.Range(1.5f, 2f);
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    protected void ManageMovement(float horizontal, float vertical)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("moving", true);
            animateWalk(horizontal, vertical);
        } else
        {
            animator.SetBool("moving", false);
        }
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        //rigidbody2D.velocity = movement;
        transform.position += movement;
    }
    
    protected void animateWalk(float h, float v)
    {
        if (animator)
        {
            if ((v > 0) && (v >= h))
            {
                animator.SetInteger("direction", 1);
            }
            if ((h > 0) && (v <= h))
            {
                animator.SetInteger("direction", 2);
            }
            if ((v < 0) && (v <= h))
            {
                animator.SetInteger("direction", 3);
            }
            if ((h < 0) && (v >= h))
            {
                animator.SetInteger("direction", 4);
            }
        }
    }

    protected Vector2 generateMovement()
    {
        float h = Random.Range(-1, 2);
        float v = Random.Range(-1, 2);
        return new Vector2(h, v);
    }

    protected void move()
    {
        if (movementTime > movementTimeRandomStop)
        {
            movement = Vector2.zero;
            
            if (movementTime > movementTimeRandomMove)
            {
                movement = generateMovement();
                movementTime = 0;
                movementTimeRandomStop = Random.Range(0.5f, 1f);
                movementTimeRandomMove = Random.Range(1.5f, 2f);
            }
        }
        ManageMovement(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);
        movementTime += Time.deltaTime;
    }

}
