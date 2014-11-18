using UnityEngine;
using System.Collections;

public class GeneralController : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    private GameObject bocadillo;
    private GameObject textBackground;
    private GameObject textGUI;
    private bool pause=false;
    // Use this for initialization
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
        NotificationCenter.DefaultCenter().AddObserver(this, "drawText");
        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
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

        if (textBackground == null)
        {
            textBackground = GameObject.Instantiate(Resources.Load("prefabs/TextBackground"))as GameObject;
            DontDestroyOnLoad(textBackground);
        }

        if (textGUI == null)
        {
            textGUI = GameObject.Instantiate(Resources.Load("prefabs/GUIText"))as GameObject;
            DontDestroyOnLoad(textGUI);
        }

    }

    public GameObject getPlayer()
    {

        if (player == null)
        {
            player = GameObject.Instantiate(Resources.Load("prefabs/Player"), new Vector3(Variables.posX, Variables.posY, 0), Quaternion.identity)as GameObject;
            DontDestroyOnLoad(player);
        }
        return player;
    }



    void drawText(Notification notification)
    {
        Time.timeScale = 0;
    }
    
    void hideText(Notification notification)
    {
        Time.timeScale = 1;
    }

} 