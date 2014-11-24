using UnityEngine;
using System.Collections;

public class PlayerController : Character
{

    private const float animTime = 0.5f;
    private float timeMap, timeHit;
    private bool hitted, mapChange;
    private string mapName;
    private GameObject fromMap, toMap;
    private Vector3 from2, to2;
    private bool attacking = false;
    private Vector2 playerTransform1, playerTransform2;
    private bool U, L, D, R;
    private Vector2 idle;
    private Vector2 up, down, left, right;
    private Vector2 UR, UL, DR, DL;
    public bool animated;

    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 1.5f;
        timeHit = 0;
        hitted = false;
        mapName = Variables.mapName;
        mapChange = false;
        animated = false;
        //animTime = 0.5f;
        // animator.SetInteger("direction", Variables.playerDirection);
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
        NotificationCenter.DefaultCenter().AddObserver(this, "mapChanged");
        idle = Vector2.zero;
        
        up = new Vector2(0, 1);
        down = new Vector2(0, -1);
        right = new Vector2(1, 0);
        left = new Vector2(-1, 0);
        
        UR = new Vector2(1, 1);
        UL = new Vector2(-1, 1);
        DR = new Vector2(1, -1);
        DL = new Vector2(-1, -1);
       


    }
    // Update is called once per frame
    void Update()
    {
        if (!animated)
        {
            //RigidBody2D.velocity = idle;
            U = keyDown(KeyCode.UpArrow, U);
            L = keyDown(KeyCode.LeftArrow, L);
            D = keyDown(KeyCode.DownArrow, D);
            R = keyDown(KeyCode.RightArrow, R);




            if (U || D || R || L)
            {
                if (U)
                    movement = up;
                if (D)
                    movement = down;
                if (R)
                    movement = right;
                if (L)
                    movement = left;
            
            
                if (U && R)
                    movement = UR;
                if (U && L)
                    movement = UL;
                if (D && R)
                    movement = DR;
                if (D && L)
                    movement = DL;
            } else
            {
                movement = idle;
            }

            if (!hitted)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !attacking)
                {
                    transform.Find("sword").gameObject.SetActive(true);
                    attacking = true;
                    movement = idle;
                    ManageMovement(movement * speed);

                } else
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");
                
                    }

                    if (!attacking)
                    {
                        ManageMovement(movement * speed);
                
                    } else if (transform.Find("sword").gameObject.activeSelf == false)
                    {
                        attacking = false;
                    }
                    //ManageMovement(h, v);
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
       

            if (mapChange)
            {
                if (Variables.changeMapDirection == 1)
                {
                    if (toMap.transform.position.x > fromMap.transform.position.x)
                    {   
                        to2 = new Vector3(toMap.transform.position.x - toMap.renderer.bounds.size.x / 2f + 0.23f, from2.y, from2.z);

                    } else if (toMap.transform.position.x < fromMap.transform.position.x)
                    {
                        to2 = new Vector3(toMap.transform.position.x + toMap.renderer.bounds.size.x / 2f - 0.23f, from2.y, from2.z);
                    }
                } else
                {
                    if (toMap.transform.position.y > fromMap.transform.position.y)
                    {
                        to2 = new Vector3(from2.x, toMap.transform.position.y - toMap.renderer.bounds.size.y / 2f + 0.23f, from2.z);
                    } else
                    {
                        to2 = new Vector3(from2.x, toMap.transform.position.y + toMap.renderer.bounds.size.y / 2f - 0.23f, from2.z);
                    }
                }
            
                transform.position = Vector3.Slerp(from2, to2, (Time.time - timeMap) / animTime);
                if (Time.time - timeMap > animTime)
                {
                    mapChange = false;
                }
            
            }

            //print (this.renderer.bounds.size.x);
            //ManageMovement(movement * speed);
        } else
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");
                
            }
        }


    }

    private bool keyDown(KeyCode KC, bool Key)
    {
        if (Input.GetKeyDown(KC) || Key)
            Key = true;
        
        if (Input.GetKeyUp(KC))
            Key = false;
        
        return Key;
    }


    /* void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TalkingNpc" || other.gameObject.tag == "Npc")
        {print(rigid.velocity);
            movement=idle;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "TalkingNpc" || other.gameObject.tag == "Npc")
        {print(rigid.velocity);
            rigid.velocity=idle;
        }
    }*/


    
    void OnCollisionEnter2D(Collision2D coll)
    {
        //return;
        if (coll.gameObject.tag == "Enemy")
        {
           
            if (!hitted)
            {
                hitted = true;
                timeHit = Time.time;
                calculateAttackDirection(coll.transform);
            }

        } else
        {      
            movement = idle;
        }

    }

    void sceneChanged(Notification notification)
    {
        mapName = Variables.mapName;
        U = D = R = L = false;
        rigid.velocity = Vector2.zero;
    }

    void mapChanged(Notification notification)
    {
        fromMap = GameObject.Find(mapName);
        mapChange = true;
        mapName = Variables.mapName;
        toMap = GameObject.Find(mapName);
        from2 = transform.position;
        timeMap = Time.time;
    }
}







//using UnityEngine;
//using System.Collections;
//
//public class PlayerController : Character
//{
//    private const float hitTime = 0.175f;
//    private const float animTime = 0.5f;
//    private float time;
//    private bool hitted, mapChange;
//    private Vector3 from, to;
//    private string mapName;
//    private GameObject fromMap, toMap;
//    private float time2;
//    private Vector3 from2, to2;
//    private bool attacking = false;
//    private Vector2 vector = Vector2.zero;
//    private Vector2 playerTransform1, playerTransform2;
//    // Use this for initialization
//    void Start()
//    {
//        speed = 1.5f;
//        animator = this.GetComponent<Animator>();
//        hitted = false;
//        mapName = Variables.mapName;
//        mapChange = false;
//        //animTime = 0.5f;
//        // animator.SetInteger("direction", Variables.playerDirection);
//        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
//        NotificationCenter.DefaultCenter().AddObserver(this, "mapChanged");
//        rigidbody2D.mass = 999999f;
//        
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        
//        /* if (Variables.changeScene)
//        {
//            mapName = Variables.mapName;
//        }*/
//        
//        /*  if (mapName != Variables.mapName)
//        {
//            fromMap = GameObject.Find(mapName);
//            mapChange = true;
//            mapName = Variables.mapName;
//            toMap = GameObject.Find(mapName);
//            from2 = transform.position;
//            time2 = Time.time;
//        }*/
//        if (Input.GetKeyDown(KeyCode.Space) && !transform.Find("sword").gameObject.activeSelf)
//        {
//            transform.Find("sword").gameObject.SetActive(true);
//            attacking = true;
//            
//        } else
//        {
//            if (Input.GetKeyDown(KeyCode.F))
//            {
//                NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");
//                
//            }
//            
//            //transform.rotation = Quaternion.identity;
//            //rigidbody2D.velocity = Vector3.zero;
//            
//            float v = speed * Input.GetAxis("Vertical") * Time.deltaTime;
//            float h = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
//            if (!attacking)
//            {
//                playerTransform1 = transform.position;
//                playerTransform2 = transform.position;
//                switch (animator.GetInteger("direction"))
//                {
//                    case 1:
//                        playerTransform1 = new Vector2(playerTransform1.x + 0.12f, playerTransform1.y + 0.13f);
//                        playerTransform2 = new Vector2(playerTransform2.x - 0.12f, playerTransform2.y + 0.13f);
//                        vector = transform.up;
//                        break;
//                    case 2:
//                        playerTransform1 = new Vector2(playerTransform1.x + 0.13f, playerTransform1.y + 0.12f);
//                        playerTransform2 = new Vector2(playerTransform2.x + 0.13f, playerTransform2.y - 0.12f);
//                        vector = transform.right;
//                        break;
//                    case 3:
//                        playerTransform1 = new Vector2(playerTransform1.x + 0.12f, playerTransform1.y - 0.13f);
//                        playerTransform2 = new Vector2(playerTransform2.x - 0.12f, playerTransform2.y - 0.13f);
//                        vector = -transform.up;
//                        break;
//                    case 4:
//                        playerTransform1 = new Vector2(playerTransform1.x - 0.13f, playerTransform1.y + 0.12f);
//                        playerTransform2 = new Vector2(playerTransform2.x - 0.13f, playerTransform2.y - 0.12f);
//                        vector = -transform.right;
//                        break;
//                        
//                }
//                RaycastHit2D hit1 = Physics2D.Raycast(playerTransform1, vector, 0.03f);
//                RaycastHit2D hit2 = Physics2D.Raycast(playerTransform2, vector, 0.03f);
//                if (((hit1.collider != null) && (!hit1.collider.isTrigger)) || ((hit2.collider != null) && (!hit2.collider.isTrigger)))
//                {
//                    if (((animator.GetInteger("direction") == 1) && (!Input.GetKey(KeyCode.UpArrow))) || ((animator.GetInteger("direction") == 2) && (!Input.GetKey(KeyCode.RightArrow))) || ((animator.GetInteger("direction") == 3) && (!Input.GetKey(KeyCode.DownArrow))) || ((animator.GetInteger("direction") == 4) && (!Input.GetKey(KeyCode.LeftArrow))))
//                    {
//                        ManageMovement(h, v);
//                    } else
//                    {
//                        animator.SetBool("moving", false);
//                    }
//                } else
//                {
//                    ManageMovement(h, v);
//                }
//            } else if (transform.Find("sword").gameObject.activeSelf == false)
//            {
//                attacking = false;
//            }
//            //ManageMovement(h, v);
//        }
//        
//        if (hitted)
//        {
//            transform.position = Vector3.Slerp(from, to, (Time.time - time) / hitTime);
//            if (Time.time - time > hitTime)
//            {
//                hitted = false;
//            }
//            
//        }
//        
//        
//        if (mapChange)
//        {
//            if (toMap.transform.position.x > fromMap.transform.position.x)
//            {   //Right
//                //toMap.renderer.bounds.size.x
//                to2 = new Vector3(toMap.transform.position.x - toMap.renderer.bounds.size.x / 2f + 0.23f, from2.y, from2.z);
//                
//            } else if (toMap.transform.position.x < fromMap.transform.position.x)
//            {
//                to2 = new Vector3(toMap.transform.position.x + toMap.renderer.bounds.size.x / 2f - 0.23f, from2.y, from2.z);
//            } else if (toMap.transform.position.y > fromMap.transform.position.y)
//            {
//                to2 = new Vector3(from2.x, toMap.transform.position.y - toMap.renderer.bounds.size.y / 2f + 0.23f, from2.z);
//            } else
//            {
//                to2 = new Vector3(from2.x, toMap.transform.position.y + toMap.renderer.bounds.size.y / 2f - 0.23f, from2.z);
//            }
//            
//            transform.position = Vector3.Slerp(from2, to2, (Time.time - time2) / animTime);
//            if (Time.time - time2 > animTime)
//            {
//                mapChange = false;
//            }
//        }
//        
//        //print (this.renderer.bounds.size.x);
//    }
//    
//    
//    // @override
//    void OnCollisionEnter2D(Collision2D coll)
//    {
//        //return;
//        if (coll.gameObject.tag == "Enemy")
//        {
//            
//            if (!hitted)
//            {
//                hitted = true;
//                time = Time.time;
//                from = transform.position;
//                float xDistance = coll.transform.position.x - transform.position.x;
//                float yDistance = coll.transform.position.y - transform.position.y;
//                if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
//                {
//                    if (xDistance < 0)
//                    {
//                        to = new Vector3(from.x + 0.32f, from.y, from.z);
//                    } else
//                    {
//                        to = new Vector3(from.x - 0.32f, from.y, from.z);
//                    }
//                } else
//                {
//                    if (yDistance < 0)
//                    {
//                        to = new Vector3(from.x, from.y + 0.32f, from.z);
//                    } else
//                    {
//                        to = new Vector3(from.x, from.y - 0.32f, from.z);
//                    }
//                }
//            }
//            
//        } else
//        {
//            hitted=false;
//        }
//        
//    }
//    
//    void sceneChanged(Notification notification)
//    {
//        mapName = Variables.mapName;
//    }
//    
//    void mapChanged(Notification notification)
//    {
//        fromMap = GameObject.Find(mapName);
//        mapChange = true;
//        mapName = Variables.mapName;
//        toMap = GameObject.Find(mapName);
//        from2 = transform.position;
//        time2 = Time.time;
//    }
//}
//
