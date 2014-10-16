using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour
{
    private GameObject player;
    private float speed;
    // Use this for initialization
    void Start()
    {
        speed = 2f;
        player = GeneralController.DefaultController().getPlayer();
        float b = Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(player.transform.position.y - transform.position.y) / Mathf.Abs(player.transform.position.x - transform.position.x));
        float c = 90f - b;
        //print(c);
        if ((player.transform.position.y - transform.position.y) < 0)
        {
            c = 180 - c;
        }
        if (player.transform.position.x - transform.position.x > 0)
        {
            c *= -1;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, c));


    }
    
    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(transform.up.x * speed * Time.deltaTime, transform.up.y * speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        GameObject.Destroy(transform.gameObject);         
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "sword")
        {
            GameObject.Destroy(transform.gameObject);
        }
    }
}
