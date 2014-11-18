using UnityEngine;
using System.Collections;

public class MonsterRedController : Enemy
{

    //private int direccion = 1;
    private const float ATTACKDISTANCE = 2f;
    private const float SHOOTDISTANCE = 1f;
    private const float DEATHTIME = 300f;
    // public GameObject blood;
    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 1f;
        dead = false;
        lives = 3;

        shootTime = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }


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
                    if (Vector3.Distance(transform.position, player.transform.position) <= SHOOTDISTANCE)
                    {
                        movement = Vector2.zero;
                        ManageMovement(movement * speed);
                        if (shootTime == 0)
                        {
                            GameObject fireball = null;
                            switch (animator.GetInteger("direction"))
                            {
                                case 1:
                                    fireball = GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x, transform.position.y + 0.126f, 0), Quaternion.identity) as GameObject;
                                    break;
                                case 2:
                                    fireball = GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x + 0.15f, transform.position.y + 0.025f, 0), Quaternion.identity)as GameObject;
                                    break;
                                case 3:
                                    fireball = GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity)as GameObject;
                                    break;
                                case 4:
                                    fireball = GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x - 0.15f, transform.position.y - 0.025f, 0), Quaternion.identity)as GameObject;
                                    break;
                            }
                            fireball.name = "Fireball";
                        }
                        shootTime += Time.deltaTime;
                        if (shootTime > 1f)
                        {
                            shootTime = 0;
                        }
                    } else
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
                    }
                
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
                    ;
                }
            }
        } else if (Time.time - timeDead > DEATHTIME)
        {

            collider2D.enabled = true;
            renderer.enabled = true;
            dead = false;
            gameObject.transform.position=initPos;
        } else
        {
            rigid.velocity=Vector2.zero;
        }
    
    }
    
   
}


