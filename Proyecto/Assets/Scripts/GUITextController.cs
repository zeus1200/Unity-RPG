#if false
using UnityEngine;
using System.Collections;

public class GUITextController : MonoBehaviour
{

    
    // Use this for initialization
    void Start()
    {
    
       
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
#endif