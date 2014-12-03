using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    // Use this for initialization

    private Text text;
    private Image texture;

    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        texture = GameObject.Find("TextTexture").GetComponent<Image>();
        NotificationCenter.DefaultCenter().AddObserver(this, "drawText");
        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void drawText(Notification notification)
    {
        if (text.enabled != true)
        {
            text.enabled = true;
        }
        text.text = Variables.text;
        texture.enabled = true;
    }

    void hideText(Notification notification)
    {
        text.enabled = false;
        texture.enabled = false;
    }
}
