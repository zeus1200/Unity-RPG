using UnityEngine;
using System.Collections;

public class GeneralController : MonoBehaviour
{
    private GameObject player;
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
