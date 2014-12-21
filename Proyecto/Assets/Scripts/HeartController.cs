using UnityEngine;
using System.Collections;

public class HeartController : MonoBehaviour
{

    private const float TIMEACTIVE = 5;
    private float time;
    // Use this for initialization
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > TIMEACTIVE)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "lifeTaken");
            GameObject.Destroy(this.gameObject);
        }
    }
}
