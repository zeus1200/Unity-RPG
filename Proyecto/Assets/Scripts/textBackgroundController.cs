using UnityEngine;
using System.Collections;

public class textBackgroundController : MonoBehaviour
{

    private GUITexture texture;


    // Use this for initialization
    void Start()
    {
        texture = GetComponent("GUITexture") as GUITexture;
        NotificationCenter.DefaultCenter().AddObserver(this, "drawText");
        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
    }

    void drawText(Notification notification)
    {
        texture.enabled = true;
    }
    
    void hideText(Notification notification)
    {
        texture.enabled = false;
    }
}
