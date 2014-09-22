using UnityEngine;
using System.Collections;

public class SceneChangeTrigger : MonoBehaviour
{

    public string scene, mapName;
    public float posX, posY;
    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Variables.posX = posX;
            Variables.posY = posY;
            Variables.mapName = mapName;
            //Variables.changeScene = true;
          //  Variables.playerDirection=GameObject.Find("player").GetComponent<Animator>().GetInteger("direction");
            Application.LoadLevel(scene);    
        }
        
    }
}