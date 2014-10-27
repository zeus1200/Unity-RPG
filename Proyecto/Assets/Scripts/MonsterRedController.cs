using UnityEngine;
using System.Collections;

public class MonsterRedController : Character
{
    private float timeDead, timeHit;
    //private int direccion = 1;
    private const float ATTACKDISTANCE = 2f;
    private const float SHOOTDISTANCE = 1f;
    private GameObject player;
    private bool dead, hitted;
    private Vector3 from, to;
    private float hitTime = 0.175f;
    private int lives;
    private float movementTime, shootTime;
    private Vector2 movement;
    // public GameObject blood;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        speed = 1f;
        dead = false;
        lives = 3;
        movementTime = 0;
        shootTime = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }

        if (dead && (Time.time - timeDead >= 5f))
        {
            collider2D.enabled = true;
            renderer.enabled = true;
            dead = false;
            lives = 3;
        } 
        if (!dead)
        {
            float v = 0;
            float h = 0;
            float xDistance = player.transform.position.x - transform.position.x;
            float yDistance = player.transform.position.y - transform.position.y;
            if (Vector3.Distance(transform.position, player.transform.position) <= ATTACKDISTANCE)
            { 
                if (Vector3.Distance(transform.position, player.transform.position) <= SHOOTDISTANCE)
                {
                    if (shootTime == 0)
                    {

                        switch (animator.GetInteger("direction"))
                        {
                            case 1:
                                GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x, transform.position.y + 0.126f, 0), Quaternion.identity);
                                break;
                            case 2:
                                GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x + 0.15f, transform.position.y + 0.025f, 0), Quaternion.identity);
                                break;
                            case 3:
                                GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                break;
                            case 4:
                                GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x - 0.15f, transform.position.y - 0.025f, 0), Quaternion.identity);
                                break;
                        }
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
                        h = speed * (1f) * Time.deltaTime;
                    } else if (xDistance < -0.1f)
                    {
                        h = speed * (-1f) * Time.deltaTime;
                    }
                
                
                    if (yDistance > 0.1f)
                    {
                        v = speed * (1f) * Time.deltaTime;
                    } else if (yDistance < -0.1f)
                    {
                        v = speed * (-1f) * Time.deltaTime;
                    }
                
                    ManageMovement(h, v);
                }
                
            } else
            {
                speed = 0.75f;
                if (movementTime > 0.5f)
                {
                    movement = Vector2.zero;
                    
                    if (movementTime > 1)
                    {
                        movement = generateMovement();
                        movementTime = 0;
                    }
                }
                
                ManageMovement(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);
                movementTime += Time.deltaTime;
            }
            
            
            
            
            
            if (hitted)
            {
                transform.position = Vector3.Slerp(from, to, (Time.time - timeHit) / hitTime);
                if (Time.time - timeHit > hitTime)
                {
                    hitted = false;
                }
                
            }
        }
        
        
        
        
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "sword")
        { 
            lives--;
            if (lives <= 0)
            {
                shootTime = 0;
                collider2D.enabled = false;
                renderer.enabled = false;
                timeDead = Time.time;
                dead = true;
                GameObject.Instantiate(Resources.Load("prefabs/Blood"), transform.position, Quaternion.identity);
            }
            
            if (!hitted)
            {
                
                hitted = true;
                timeHit = Time.time;
                from = transform.position;
                float xDistance = other.transform.parent.transform.position.x - transform.position.x;
                float yDistance = other.transform.parent.transform.position.y - transform.position.y;
                if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
                {
                    if (xDistance < 0)
                    {
                        to = new Vector3(from.x + 0.32f, from.y, from.z);
                    } else
                    {
                        to = new Vector3(from.x - 0.32f, from.y, from.z);
                    }
                } else
                {
                    if (yDistance < 0)
                    {
                        to = new Vector3(from.x, from.y + 0.32f, from.z);
                    } else
                    {
                        to = new Vector3(from.x, from.y - 0.32f, from.z);
                    }
                }
            }
            
        }
    }
}


