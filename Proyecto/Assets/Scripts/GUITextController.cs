using UnityEngine;
using System.Collections;

public class GUITextController : MonoBehaviour
{

    private GUIText text;
    // Use this for initialization
    void Start()
    {
    
        text = GetComponent("GUIText") as GUIText;
        NotificationCenter.DefaultCenter().AddObserver(this, "drawText");
        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
    }

    void drawText(Notification notification)
    {
        if (text.enabled != true)
        {
            text.enabled = true;
        }
        text.text = Variables.text;
    }

    void hideText(Notification notification)
    {
        text.enabled = false;
    }
}
