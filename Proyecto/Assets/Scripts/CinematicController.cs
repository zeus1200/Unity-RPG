using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CinematicController : MonoBehaviour
{
    private GameObject player;
    private Animator playerAnim;
    private PlayerController playerController;
    private GameObject lady;
    private Animator ladyAnim;
    private NpcController ladyController;
    private GameObject canvas;
    private Animator canvasAnim;
    private GameObject shine;
    private Animator shineAnim;
    private Vector3 playerFrom, playerTo, ladyFrom, ladyTo;
    private float time, timeShine;
    private bool bShine, stopCinematic;
    public bool isFinished;

    // Use this for initialization
    void Start()
    {

        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
        NotificationCenter.DefaultCenter().AddObserver(this, "BossDestroyed");
        player = GeneralController.DefaultController().getPlayer();
        playerAnim = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        lady = GameObject.Find("Ladyuvita");
        ladyAnim = lady.GetComponent<Animator>();
        ladyController = lady.GetComponent<NpcController>();
        canvas = GameObject.Find("Canvas");

        canvasAnim = canvas.GetComponent<Animator>();
        shine = GameObject.Find("Shine");

        shineAnim = shine.GetComponent<Animator>();
        stopCinematic = true;

        //ladyAnim.SetBool("startAnim", true);
        // playerAnim.SetBool("startAnim", true);
        canvasAnim.SetBool("startAnim", true);

        playerFrom = new Vector3(0, -2.3f, 0); 
        playerTo = new Vector3(0, -0.05f, 0);
        ladyFrom = new Vector3(0, 1.56f, 0);
        ladyTo = new Vector3(0, 0.25f, 0);
        time = Time.time;

        playerController.animated = true;
        playerAnim.SetInteger("direction", 1);
        playerAnim.SetBool("moving", true);
        ladyController.animated = true;
        ladyAnim.SetBool("moving", true);
        ladyAnim.SetInteger("direction", 3);
        lady.tag = "Npc";
        bShine = false;
        isFinished=false;
    }
    
    // Update is called once per frame
    
    void Update()
    {
        if (isFinished) {
            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(go);
            }
            Application.LoadLevel("theEnd");
        }


        if ((Time.time - time) <= 5f)
        {
            // player.transform.position += new Vector3(0,0f*Time.deltaTime,0); 
            player.transform.position = Vector3.Lerp(playerFrom, playerTo, (Time.time - time) / 5f);
            /*if (Time.time - time > animTime)
        {

        }*/
            lady.transform.position = Vector3.Lerp(ladyFrom, ladyTo, (Time.time - time) / 5f);

        } else
        {
            if (stopCinematic)
            {
                // playerController.animated = false;
                playerAnim.SetBool("moving", false);
                ladyAnim.SetBool("moving", false);
                //canvas.SetActive(false);
                canvasAnim.SetBool("startAnim", false);
                GameObject.Find("UpperScroll").GetComponent<Image>().enabled = false;
                GameObject.Find("LowerScroll").GetComponent<Image>().enabled = false;
                lady.tag = "TalkingNpc";
                stopCinematic = false;
            }

        }

        if (bShine)
        {
            if ((Time.time - timeShine) > 1f)
            {
                bShine = false;
                playerController.animated = false;
                GameObject.Instantiate(Resources.Load("prefabs/Boss"), new Vector3(0f, 0.6f, 0), Quaternion.identity);
                GameObject.Destroy(lady);
                GameObject.Destroy(shine);
            }
        }
    }

    void hideText(Notification notification)
    {
        shineAnim.SetBool("startAnim", true);
        lady.tag = "Npc";
        playerController.animated = true;
        bShine = true;
        timeShine = Time.time;
    }

    void BossDestroyed(Notification notification)
    {
        playerController.animated = true;
        canvasAnim.SetBool("finishAnim", true);

    }
}
