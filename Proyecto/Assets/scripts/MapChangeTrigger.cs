using UnityEngine;
using System.Collections;

public class MapChangeTrigger : MonoBehaviour
{

    public string map1, map2;
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
            if (Variables.mapName == map1)
            {
                Variables.mapName = map2;
            } else
            {
                Variables.mapName = map1;
            }   
        }
        
    }

}
