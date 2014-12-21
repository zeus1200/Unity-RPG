using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    protected float speed, movementTime, movementTimeRandomMove, movementTimeRandomStop;
    protected Vector2 movement;
    protected Animator animator;
    protected Rigidbody2D rigid;
    protected Vector2 attackDir;
    protected const float HITTIME = 0.25f;
    protected int lives;



    // Use this for initialization
    protected void Start()
    {

        animator = this.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        movementTime = 0;
        movementTimeRandomStop = Random.Range(0.5f, 1f);
        movementTimeRandomMove = Random.Range(1.5f, 2f);


    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
   
    protected void ManageMovement(Vector2 vel)
    {
        if (vel.x != 0f || vel.y != 0f)
        {
            animator.SetBool("moving", true);
            animateWalk(vel.x, vel.y);
        } else
        {
            animator.SetBool("moving", false);
        }
        rigid.velocity = vel;
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
        ManageMovement(movement * speed);
        movementTime += Time.deltaTime;
    }

    protected void calculateAttackDirection(Transform other)
    {
        float xDistance = other.position.x - transform.position.x;
        float yDistance = other.position.y - transform.position.y;
        if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
        {
            if (xDistance < 0)
            {
                attackDir = new Vector2(1, 0);
            } else
            {
                attackDir = new Vector2(-1, 0);
            }
        } else
        {
            if (yDistance < 0)
            {
                attackDir = new Vector2(0, 1);
            } else
            {
                attackDir = new Vector2(0, -1);
            }
        }
        
    }
}
