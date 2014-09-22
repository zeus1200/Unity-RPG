using UnityEngine;
using System.Collections;

public class GeneralController : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    private GameObject bocadillo;
    private bool pause=false;
    // Use this for initialization
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause){
                Time.timeScale=1;
            }else{Time.timeScale=0;}
            pause=!pause;
        }
    }
    private static GeneralController generalController;

    public static GeneralController DefaultController()
    {
        // If the defaultCenter doesn't already exist, we need to create it
        if (!generalController)
        {
            // Because the NotificationCenter is a component, we have to create a GameObject to attach it to.
            GameObject notificationObject = new GameObject("Default General Controller");
            // Add the NotificationCenter component, and set it as the defaultCenter
            generalController = notificationObject.AddComponent<GeneralController>();
            DontDestroyOnLoad(generalController);
        }
        
        return generalController;
    }

    void sceneChanged(Notification notification)
    {
        if (player == null)
        {
            player = GameObject.Instantiate(Resources.Load("prefabs/Player"), new Vector3(Variables.posX, Variables.posY, 0), Quaternion.identity)as GameObject;
            DontDestroyOnLoad(player);
        } else
        {
            player.transform.position = new Vector3(Variables.posX, Variables.posY, 0);
        }

        if (mainCamera == null)
        {
            mainCamera = GameObject.Instantiate(Resources.Load("prefabs/Main Camera"))as GameObject;
            DontDestroyOnLoad(mainCamera);
        }

        if (bocadillo == null)
        {
            bocadillo = GameObject.Instantiate(Resources.Load("prefabs/Bocadillo"))as GameObject;
            DontDestroyOnLoad(bocadillo);
        }

    }

    public GameObject getPlayer()
    {
        return player;
    }

} 


/*  void Start()
    {
        player = GameObject.Find("player");
        if (player != null)
        {
            player.transform.position = new Vector3(Variables.posX, Variables.posY, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("player");
            player.transform.position = new Vector3(Variables.posX, Variables.posY, 0);
        }

    }
}*/

/*
using UnityEngine;
using System.Collections;

public class GeneralController : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    // Use this for initialization

    void Start()
    {
        player = GameObject.Find("player");
        if (player != null)
        {
            player.transform.position = new Vector3(Variables.posX, Variables.posY, 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("player");
            player.transform.position = new Vector3(Variables.posX, Variables.posY, 0);
        }

    }
}
*/