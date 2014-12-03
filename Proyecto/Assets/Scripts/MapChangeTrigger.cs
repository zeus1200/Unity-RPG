using UnityEngine;
using System.Collections;

public class MapChangeTrigger : MonoBehaviour
{

    public string map1, map2;
    public enum Direction
    {
        Vertical = 0,
        Horizontal = 1
    }
    public Direction direction;
    // Use this for initialization
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
            Variables.changeMapDirection=(int)direction;
            NotificationCenter.DefaultCenter().PostNotification(this, "mapChanged");
        }
        
    }

}
