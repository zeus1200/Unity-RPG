using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameObject mapaBase;
    private float cameraX, cameraY, height, width, time, animTime;
    private Camera cam;
    private GameObject player;
    private bool mapChange;
    private Vector3 origin;
    public bool animated;
    // Use this for initialization
    void Start()
    {
        cameraX = cameraY = 0;
        mapaBase = GameObject.Find(Variables.mapName);
        player = GeneralController.DefaultController().getPlayer();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        mapChange = false;
        animTime = 0.5f;
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
        NotificationCenter.DefaultCenter().AddObserver(this, "mapChanged");
    }
    
    // Update is called once per frame
    void Update()
    {
        /* if (Variables.changeScene)
        {
            mapaBase = GameObject.Find(Variables.mapName);
            if(mapaBase!=null)
            {
                Variables.changeScene=false;
            }
        } else
        {

           
           /if (mapaBase.name != Variables.mapName)
            {
                mapaBase = GameObject.Find(Variables.mapName);
                mapChange = true;
                time = Time.time;
                origin = this.transform.position;
            }*/
        //print(mapaBase.name);
        cameraX = player.transform.position.x;
        cameraY = player.transform.position.y;

           
            if (cameraX - width / 2f < -1 * mapaBase.renderer.bounds.size.x / 2f + mapaBase.transform.position.x)
            {
                cameraX = width / 2f - mapaBase.renderer.bounds.size.x / 2f + mapaBase.transform.position.x;
            }
            if (cameraY - height / 2f < -1 * mapaBase.renderer.bounds.size.y / 2f + mapaBase.transform.position.y)
            {
                cameraY = height / 2f - mapaBase.renderer.bounds.size.y / 2f + mapaBase.transform.position.y;
            }
            if (cameraX + width / 2f > mapaBase.renderer.bounds.size.x / 2f + mapaBase.transform.position.x)
            {
                cameraX = mapaBase.renderer.bounds.size.x / 2f - width / 2f + mapaBase.transform.position.x;
            }
            if (cameraY + height / 2f > mapaBase.renderer.bounds.size.y / 2f + mapaBase.transform.position.y)
            {
                cameraY = mapaBase.renderer.bounds.size.y / 2f - height / 2f + mapaBase.transform.position.y;
            }

            if (!mapChange)
        {
            Vector3 tal = new Vector3(cameraX, cameraY, -10);
            this.transform.position = tal;

        } else
        {
            Vector3 tal = new Vector3(cameraX, cameraY, -10);
            this.transform.position = Vector3.Slerp(origin, tal, (Time.time - time) / animTime);
            if (Time.time - time > animTime)
            {
                mapChange = false;
            }
        }
    }

    void sceneChanged(Notification notification)
    {
        mapaBase = GameObject.Find(Variables.mapName);
    }
    
    void mapChanged(Notification notification)
    {
        mapaBase = GameObject.Find(Variables.mapName);
        mapChange = true;
        time = Time.time;
        origin = this.transform.position;
    }
}