﻿using UnityEngine;
using System.Collections;

public class GuardController : Character
{




    private Vector2 movementGuard;
    //public int direction;
    public enum Direction
    {
        Vertical = 0,
        Horizontal = 1
    }
    public Direction direction = Direction.Vertical;
    // Use this for initialization
    
    new void Start()
    {
        base.Start();
        speed = 1f;
        if (direction == 0)
        {
            movementGuard = new Vector2(transform.up.x, transform.up.y);
        }
        else
        {
            movementGuard = new Vector2(transform.right.x, transform.right.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement(movementGuard * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        movementGuard = -1 * movementGuard;
    }

}


