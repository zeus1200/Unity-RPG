using UnityEngine;
using System.Collections;

public class MonsterBlueController : Character
{
    private float timeDead, timeHit;
    //private int direccion = 1;
    private const float ATTACKDISTANCE = 1f;
    private GameObject player;
    private bool dead, hitted;
    private Vector3 from, to;
    private float hitTime = 0.175f;
    private int lives;
    // public GameObject blood;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
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
            if (Vector3.Distance(transform.position, player.transform.position) < ATTACKDISTANCE)
            {
                
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


                
            }
            ManageMovement(h, v);

            
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
                collider2D.enabled = false;
                renderer.enabled = false;
                timeDead = Time.time;
                dead = true;
                GameObject.Instantiate(Resources.Load("prefabs/Blood"), transform.position, Quaternion.identity);
            }
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


/*using UnityEngine;
using System.Collections;

public class MonsterBlueController : MonoBehaviour
{
    private float timeDead, timeHit;
    //private int direccion = 1;
    private const float ATTACKDISTANCE = 1f;
    private GameObject player;
    private float speed;
    private Animator animator;
    private bool dead, hitted;
    private Vector3 from, to;
    private float hitTime = 0.175f;
    private int lives;
   // public GameObject blood;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
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
            if (Vector3.Distance(transform.position, player.transform.position) < ATTACKDISTANCE)
            {
                
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


                
            }
            ManageMovement(h, v);

            
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

    void ManageMovement(float horizontal, float vertical)
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

    void animateWalk(float h, float v)
    {
        //print("pene1");
        if (animator)
        {
            //  print("Horizontal: "+h+", Vertical: "+v);
            if ((v > 0) && (v > h))
            {
                animator.SetInteger("direction", 1);
            }
            if ((h > 0) && (v < h))
            {
                animator.SetInteger("direction", 2);
            }
            if ((v < 0) && (v < h))
            {
                animator.SetInteger("direction", 3);
            }
            if ((h < 0) && (v > h))
            {
                animator.SetInteger("direction", 4);
            }
            //  print(animator.GetInteger("direction"));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "sword")
        {
            lives--;
            if (lives <= 0)
            {
                collider2D.enabled = false;
                renderer.enabled = false;
                timeDead = Time.time;
                dead = true;
                GameObject.Instantiate(Resources.Load("prefabs/Blood"), transform.position, Quaternion.identity);
            }
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
*/