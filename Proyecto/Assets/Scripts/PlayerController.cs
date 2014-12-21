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
    new void Start()
    {
        base.Start();
        lives = 10;
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
        NotificationCenter.DefaultCenter().AddObserver(this, "lifeTaken");
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
            }
            else
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

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");

                    }

                    if (!attacking)
                    {
                        ManageMovement(movement * speed);

                    }
                    else if (transform.Find("sword").gameObject.activeSelf == false)
                    {
                        attacking = false;
                    }
                    //ManageMovement(h, v);
                }
            }
            else
            {
                if (Time.time - timeHit > HITTIME)
                {
                    hitted = false;
                }
                else
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

                    }
                    else if (toMap.transform.position.x < fromMap.transform.position.x)
                    {
                        to2 = new Vector3(toMap.transform.position.x + toMap.renderer.bounds.size.x / 2f - 0.23f, from2.y, from2.z);
                    }
                }
                else
                {
                    if (toMap.transform.position.y > fromMap.transform.position.y)
                    {
                        to2 = new Vector3(from2.x, toMap.transform.position.y - toMap.renderer.bounds.size.y / 2f + 0.23f, from2.z);
                    }
                    else
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
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "playerWannaTalk");

            }
        }


        if (lives <= 0)
        {
            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(go);
            }
            Application.LoadLevel("gameOver");
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


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {

            if (!hitted)
            {

                hitted = true;
                /* timeHit = Time.time;*/
                lives -= 1;
                NotificationCenter.DefaultCenter().PostNotification(this, "playerHitted");

                //  calculateAttackDirection(other.transform);
            }

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Enemy")
        {

            if (!hitted)
            {
                hitted = true;
                timeHit = Time.time;
                lives -= 1;
                NotificationCenter.DefaultCenter().PostNotification(this, "playerHitted");
                calculateAttackDirection(coll.transform);

            }

        }
        else
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

    public int getLives()
    {

        return lives;
    }

    void lifeTaken(Notification notification)
    {
        if (lives < 10)
        {
            lives += 1;
        }
    }
}