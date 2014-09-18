using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float speed = 1.5f;
    private float time;
    private bool hitted, mapChange;
    private Vector3 from, to;
    private float hitTime = 0.175f;
    private string mapName;
    private GameObject fromMap, toMap;
    private float animTime, time2;
    private Vector3 from2, to2;
    private bool pause=false;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        hitted = false;
        mapName = Variables.mapName;
        mapChange = false;
        animTime = 0.5f;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause){
                Time.timeScale=1;
            }else{Time.timeScale=0;}
            pause=!pause;
        }

        if (mapName != Variables.mapName)
        {
            fromMap = GameObject.Find(mapName);
            mapChange = true;
            mapName = Variables.mapName;
            toMap = GameObject.Find(mapName);
            from2 = transform.position;
            time2 = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !transform.Find("sword").gameObject.activeSelf)
        {
            transform.Find("sword").gameObject.SetActive(true);
        } else
        {
            //transform.rotation = Quaternion.identity;
            //rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.mass = 999999f;
            float v = speed * Input.GetAxis("Vertical") * Time.deltaTime;
            float h = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            ManageMovement(h, v);
            //ManageMovement(h, v);
        }

        if (hitted)
        {
            transform.position = Vector3.Slerp(from, to, (Time.time - time) / hitTime);
            if (Time.time - time > hitTime)
            {
                hitted = false;
            }
        
        }

        if (mapChange)
        {
            if (toMap.transform.position.x > fromMap.transform.position.x)
            {   //Right
                //toMap.renderer.bounds.size.x
                to2 = new Vector3(toMap.transform.position.x - toMap.renderer.bounds.size.x / 2f + 0.23f, from2.y, from2.z);

            } else if (toMap.transform.position.x < fromMap.transform.position.x)
            {
                to2 = new Vector3(toMap.transform.position.x + toMap.renderer.bounds.size.x / 2f - 0.23f, from2.y, from2.z);
            } else if (toMap.transform.position.y > fromMap.transform.position.y)
            {
                to2 = new Vector3(toMap.transform.position.y - toMap.renderer.bounds.size.y / 2f + 0.23f, from2.y, from2.z);
            } else
            {
                to2 = new Vector3(toMap.transform.position.y + toMap.renderer.bounds.size.y / 2f - 0.23f, from2.y, from2.z);
            }
            
            transform.position = Vector3.Slerp(from2, to2, (Time.time - time2) / animTime);
            if (Time.time - time2 > animTime)
            {
                mapChange = false;
            }
        }

        //print (this.renderer.bounds.size.x);
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
        if (animator)
        {
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
        }
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        //return;
        if (coll.gameObject.tag == "Enemy")
        {
            if (!hitted)
            {
                hitted = true;
                time = Time.time;
                from = transform.position;
                float xDistance = coll.transform.position.x - transform.position.x;
                float yDistance = coll.transform.position.y - transform.position.y;
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

    /*void OnTriggerEnter2D(Collider2D other) {
        print ("turururu");
        if (other.gameObject.tag == "Maps") {
            print ("tararara");
            Variables.nombreMapa="0-1";     
        }

    }*/
}
