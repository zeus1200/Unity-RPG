using UnityEngine;
using System.Collections;

public class MonsterBlueController : Enemy
{

    //private int direccion = 1;
    private const float ATTACKDISTANCE = 1f;
    private const float DEATHTIME = 300f;


    // public GameObject blood;
    // Use this for initialization
    new void Start()
    {
        base.Start();
        // player = GeneralController.DefaultController().getPlayer();
        speed = 1f;
        dead = false;
        lives = 3;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }
       
        //rigidbody2D.mass = 999999f;
        //transform.position +=0*new Vector3 (Random.Range(-1,2), Random.Range(-1,2), 0) * Time.deltaTime * direccion;
         
        if (!dead)
        {
            if (!hitted)
            {

                int h = 0;
                int v = 0;
                float xDistance = player.transform.position.x - transform.position.x;
                float yDistance = player.transform.position.y - transform.position.y;
                if (Vector3.Distance(transform.position, player.transform.position) <= ATTACKDISTANCE)
                {
                    speed = 1f;
                
                    if (xDistance > 0.1f)
                    {
                        h = 1;
                    } else if (xDistance < -0.1f)
                    {
                        h = -1;
                    }
                
                
                    if (yDistance > 0.1f)
                    {
                        v = 1;
                    } else if (yDistance < -0.1f)
                    {
                        v = -1;
                    }

                    movement = new Vector2(h, v);
                    ManageMovement(movement * speed);
                
                } else
                {
                    speed = 0.75f;
                    move();
                }
            } else
            {
                if (Time.time - timeHit > HITTIME)
                {
                    hitted = false;
                } else
                {
                    rigid.velocity = attackDir * speed;
                }
            }
        }else if(Time.time-timeDead>DEATHTIME)
        {
            
            collider2D.enabled = true;
            renderer.enabled = true;
            dead = false;
            gameObject.transform.position=initPos;
        }else
        {
            rigid.velocity=Vector2.zero;
        }
    }


}


