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
    // Use this for initialization
    void Start()
    {
        cameraX = cameraY = 0;
        mapaBase = GameObject.Find(Variables.mapName);
        player = GameObject.Find("player");
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        mapChange = false;
        animTime = 0.5f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Variables.changeScene)
        {
            mapaBase = GameObject.Find(Variables.mapName);
            if(mapaBase!=null)
            {
                Variables.changeScene=false;
            }
        } else
        {

           
            if (mapaBase.name != Variables.mapName)
            {
                mapaBase = GameObject.Find(Variables.mapName);
                mapChange = true;
                time = Time.time;
                origin = this.transform.position;
            }
            //print(mapaBase.name);
            cameraX = player.renderer.bounds.center.x;
            cameraY = player.renderer.bounds.center.y;
            if (cameraX - width / 2 < -1 * mapaBase.renderer.bounds.size.x / 2 + mapaBase.transform.position.x)
            {
                cameraX = width / 2 - mapaBase.renderer.bounds.size.x / 2 + mapaBase.transform.position.x;
            }
            if (cameraY - height / 2 < -1 * mapaBase.renderer.bounds.size.y / 2 + mapaBase.transform.position.y)
            {
                cameraY = height / 2 - mapaBase.renderer.bounds.size.y / 2 + mapaBase.transform.position.y;
            }
            if (cameraX + width / 2 > mapaBase.renderer.bounds.size.x / 2 + mapaBase.transform.position.x)
            {
                cameraX = mapaBase.renderer.bounds.size.x / 2 - width / 2 + mapaBase.transform.position.x;
            }
            if (cameraY + height / 2 > mapaBase.renderer.bounds.size.y / 2 + mapaBase.transform.position.y)
            {
                cameraY = mapaBase.renderer.bounds.size.y / 2 - height / 2 + mapaBase.transform.position.y;
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
    }   
}