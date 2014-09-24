﻿using UnityEngine;
using System.Collections;

public class NpcController : Character
{
    /// <summary>
    /// //flecha hacia abajo &#9660;
    /// </summary>
    private float time;
    private GameObject player;
    public string speakText;
    private ArrayList text;
    private int counter;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //player = GameObject.Find ("player");
        speed = 1f;
        counter = 0;
        text=new ArrayList();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }

        float v = 0;
        float h = 0;
        float xDistance = player.transform.position.x - transform.position.x;
        float yDistance = player.transform.position.y - transform.position.y;
      
        ManageMovement(h, v);
  
    }

    public string getText()
    {
        string auxText = text[counter].ToString();
        counter++;
        if (counter >= text.Count)
        {
            counter = 0;
        }
        return auxText;
    }

}

/*using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour
{

    private float time;
    private GameObject player;
    private float speed;
    private Animator animator;
    public string[] speakText;
    private int counter;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //player = GameObject.Find ("player");
        speed = 1f;
        counter = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }

        float v = 0;
        float h = 0;
        float xDistance = player.transform.position.x - transform.position.x;
        float yDistance = player.transform.position.y - transform.position.y;
      
        ManageMovement(h, v);
  
    }
    
    void ManageMovement(float horizontal, float vertical)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("moving", true);
            animateWalk(horizontal, vertical);
        } else
        {
            animator.SetBool("moving", false);
        }
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        //rigidbody2D.velocity = movement;
        transform.position += movement;
    }
    
    void animateWalk(float h, float v)
    {
        //print("pene1");
        if (animator)
        {
            //  print("Horizontal: "+h+", Vertical: "+v);
            if ((v > 0) && (v > h))
            {
                animator.SetInteger("direction", 1);
            }
            if ((h > 0) && (v < h))
            {
                animator.SetInteger("direction", 2);
            }
            if ((v < 0) && (v < h))
            {
                animator.SetInteger("direction", 3);
            }
            if ((h < 0) && (v > h))
            {
                animator.SetInteger("direction", 4);
            }
            //  print(animator.GetInteger("direction"));
        }
    }

    public string getText()
    {
        string text=speakText[counter];
        counter++;
        if (counter >= speakText.Length)
        {
            counter=0;
        }
        return text;
    }

}*/
