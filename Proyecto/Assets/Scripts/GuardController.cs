using UnityEngine;
using System.Collections;

public class GuardController : Character
{

    private const int maxStingLenght = 38;
    private float time;
    private int counter;
    private Vector2 movement;
    //public int direction;
    public enum Direction { Vertical = 0, Horizontal = 1}
    public Direction direction = Direction.Vertical;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        speed = 1f;
        if (direction == 0)
        {
            movement=new Vector2(transform.up.x,transform.up.y);
        } else
        {
            movement=new Vector2(transform.right.x,transform.right.y);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
       
            
            ManageMovement(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);

    }
    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        movement = -1 * movement;
    }
    
}


