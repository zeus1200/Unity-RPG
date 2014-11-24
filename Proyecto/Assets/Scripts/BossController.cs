using UnityEngine;
using System.Collections;

public class BossController :  Enemy {
    private Vector2 movementBoss;
	// Use this for initialization 
	void Start () {
        base.Start();
        lives = 2;
        speed = 1f;
        movementBoss = new Vector2(transform.right.x, transform.right.y);
        shootTime = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        if(lives==0){
            GameObject.Destroy(this);
            NotificationCenter.DefaultCenter().PostNotification(this,"BossDestroyed");

        }
        ManageMovement(movementBoss * speed);
        transform.position = new Vector3(transform.position.x, 0.6f, 0);
        if (shootTime == 0)
        {
            GameObject fireball = null;
            fireball = GameObject.Instantiate(Resources.Load("prefabs/FireBall"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            fireball.name = "Fireball";

           
        }
        shootTime += Time.deltaTime;
        if (shootTime > 1f)
        {
            shootTime = 0;
        }


	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        movementBoss = -1 * movementBoss;
    }
}
