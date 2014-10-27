using UnityEngine;
using System.Collections;

public class PlayerController : Character
{
    private const float hitTime = 0.175f;
    private const float animTime = 0.5f;
    private float time;
    private bool hitted, mapChange;
    private Vector3 from, to;
    private string mapName;
    private GameObject fromMap, toMap;
    private float time2;
    private Vector3 from2, to2;
    private bool attacking = false;
    private Vector2 vector = Vector2.zero;
    private Vector2 playerTransform1, playerTransform2;
    // Use this for initialization
    void Start()
    {
        speed = 1.5f;
        animator = this.GetComponent<Animator>();
        hitted = false;
        mapName = Variables.mapName;
        mapChange = false;
        //animTime = 0.5f;
        // animator.SetInteger("direction", Variables.playerDirection);
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
        NotificationCenter.DefaultCenter().AddObserver(this, "mapChanged");
        rigidbody2D.mass = 999999f;

    }
    // Update is called once per frame
    void Update()
    {


        /* if (Variables.changeScene)
        {
            mapName = Variables.mapName;
        }*/

        /*  if (mapName != Variables.mapName)
        {
            fromMap = GameObject.Find(mapName);
            mapChange = true;
            mapName = Variables.mapName;
            toMap = GameObject.Find(mapName);
            from2 = transform.position;
            time2 = Time.time;
        }*/
        if (Input.GetKeyDown(KeyCode.Space) && !transform.Find("sword").gameObject.activeSelf)
        {
            transform.Find("sword").gameObject.SetActive(true);
            attacking = true;

        } else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");
                
            }

            //transform.rotation = Quaternion.identity;
            //rigidbody2D.velocity = Vector3.zero;

            float v = speed * Input.GetAxis("Vertical") * Time.deltaTime;
            float h = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            if (!attacking)
            {
                playerTransform1 = transform.position;
                playerTransform2 = transform.position;
                switch (animator.GetInteger("direction"))
                {
                    case 1:
                        playerTransform1 = new Vector2(playerTransform1.x + 0.12f, playerTransform1.y + 0.13f);
                        playerTransform2 = new Vector2(playerTransform2.x - 0.12f, playerTransform2.y + 0.13f);
                        vector = transform.up;
                        break;
                    case 2:
                        playerTransform1 = new Vector2(playerTransform1.x + 0.13f, playerTransform1.y + 0.12f);
                        playerTransform2 = new Vector2(playerTransform2.x + 0.13f, playerTransform2.y - 0.12f);
                        vector = transform.right;
                        break;
                    case 3:
                        playerTransform1 = new Vector2(playerTransform1.x + 0.12f, playerTransform1.y - 0.13f);
                        playerTransform2 = new Vector2(playerTransform2.x - 0.12f, playerTransform2.y - 0.13f);
                        vector = -transform.up;
                        break;
                    case 4:
                        playerTransform1 = new Vector2(playerTransform1.x - 0.13f, playerTransform1.y + 0.12f);
                        playerTransform2 = new Vector2(playerTransform2.x - 0.13f, playerTransform2.y - 0.12f);
                        vector = -transform.right;
                        break;
                        
                }
                RaycastHit2D hit1 = Physics2D.Raycast(playerTransform1, vector, 0.03f);
                RaycastHit2D hit2 = Physics2D.Raycast(playerTransform2, vector, 0.03f);
                if (((hit1.collider != null) && (!hit1.collider.isTrigger)) || ((hit2.collider != null) && (!hit2.collider.isTrigger)))
                {
                    if (((animator.GetInteger("direction") == 1) && (!Input.GetKey(KeyCode.UpArrow))) || ((animator.GetInteger("direction") == 2) && (!Input.GetKey(KeyCode.RightArrow))) || ((animator.GetInteger("direction") == 3) && (!Input.GetKey(KeyCode.DownArrow))) || ((animator.GetInteger("direction") == 4) && (!Input.GetKey(KeyCode.LeftArrow))))
                    {
                        ManageMovement(h, v);
                    } else
                    {
                        animator.SetBool("moving", false);
                    }
                } else
                {
                    ManageMovement(h, v);
                }
            } else if (transform.Find("sword").gameObject.activeSelf == false)
            {
                attacking = false;
            }
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
                to2 = new Vector3(from2.x, toMap.transform.position.y - toMap.renderer.bounds.size.y / 2f + 0.23f, from2.z);
            } else
            {
                to2 = new Vector3(from2.x, toMap.transform.position.y + toMap.renderer.bounds.size.y / 2f - 0.23f, from2.z);
            }
            
            transform.position = Vector3.Slerp(from2, to2, (Time.time - time2) / animTime);
            if (Time.time - time2 > animTime)
            {
                mapChange = false;
            }
        }

        //print (this.renderer.bounds.size.x);
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

    void sceneChanged(Notification notification)
    {
        mapName = Variables.mapName;
    }

    void mapChanged(Notification notification)
    {
        fromMap = GameObject.Find(mapName);
        mapChange = true;
        mapName = Variables.mapName;
        toMap = GameObject.Find(mapName);
        from2 = transform.position;
        time2 = Time.time;
    }
}

