using UnityEngine;
using System.Collections;

public class Enemy :Character
{
    protected float timeDead, timeHit;
    protected GameObject player;
    protected bool dead, hitted;
    // protected Vector3 from, to;
    protected int lives;
    protected float shootTime;
    protected Vector3 initPos;

    //protected virtual  float ATTACKDISTANCE;
    new void Start()
    {
        base.Start();
        initPos = gameObject.transform.position;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {  
        if (coll.transform.tag == "Player")
        {
            hitted=true;
            timeHit = Time.time;
            calculateAttackDirection(coll.transform);
        } else
        {
            movement = -1 * movement;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "sword")
        { 
            lives--;
            if (lives <= 0)
            {
                shootTime = 0;
                collider2D.enabled = false;
                renderer.enabled = false;
                timeDead = Time.time;
                dead = true;
                GameObject.Instantiate(Resources.Load("prefabs/Blood"), transform.position, Quaternion.identity);
            }
            
            if (!hitted)
            {
                
                hitted = true;
                timeHit = Time.time;
                calculateAttackDirection(other.transform.parent.transform);
               
            }
            
        } else
        {
            movement = -1 * movement;
        }
    }


}
