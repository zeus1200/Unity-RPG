using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    // Use this for initialization

    private Text text;
    private Image texture;
    private Slider slider;
    
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        texture = GameObject.Find("TextTexture").GetComponent<Image>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        NotificationCenter.DefaultCenter().AddObserver(this, "drawText");
        NotificationCenter.DefaultCenter().AddObserver(this, "hideText");
        NotificationCenter.DefaultCenter().AddObserver(this, "playerHitted");
        NotificationCenter.DefaultCenter().AddObserver(this, "lifeTaken");
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

    void playerHitted(Notification notification)
    {
        slider.value = GeneralController.DefaultController().getPlayer().GetComponent<PlayerController>().getLives();
    }

    void lifeTaken(Notification notification)
    {
        slider.value = GeneralController.DefaultController().getPlayer().GetComponent<PlayerController>().getLives();
    }
}
