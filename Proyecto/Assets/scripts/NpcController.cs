using UnityEngine;
using System.Collections;

public class NpcController : Character
{

    private const int maxStingLenght = 38;
    private float time, movementTime;
    public string speakText;
    private ArrayList textArrayList;
    private int counter;
    private Vector2 movement;
    public bool movable, speakable;
    public int direction;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        speed = 0.5f;
        counter = 0;
        textArrayList = new ArrayList();
        if (speakable)
        {
            movementTime = 0;
            fillArrayList();
        }
        if (!movable)
        {
            animator.SetInteger("direction", direction);
        } 
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (movable)
        {
            if (movementTime > 0.5f)
            {
                movement = Vector2.zero;
           
                if (movementTime > 1)
                {
                    movement = generateMovement();
                    movementTime = 0;
                }
            }

            ManageMovement(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);
            movementTime += Time.deltaTime;
        }
    }

    public string getText()
    {
        string auxText = textArrayList [counter].ToString();
        counter++;
        if (counter >= textArrayList.Count)
        {
            counter = 0;
        }
        return auxText;
    }

    private void fillArrayList()
    {
        int counter = 0;
        bool isSecondLine = false;
        string aux = "";
        string [] split1, split2; 
        split1 = speakText.Split(new char [] {'|'});
        foreach (string s1 in split1)
        {
            if (s1.Trim() != "")
            {
                split2 = speakText.Split();
                //print(split2.Length);
                foreach (string s2 in split2)
                {
                    if (s2.Trim() != "")
                    {
                        if (aux.Length + s2.Length < 76)
                        {
                            if ((aux.Length + s2.Length > 38) && (!isSecondLine))
                            {
                                isSecondLine = true;
                                aux = aux.Insert(aux.Length, "\n" + s2 + " ");
                                counter += s2.Length + 1;
                            } else if (isSecondLine)
                            {
                                if (counter + s2.Length > 38)
                                {
                                    textArrayList.Add(aux);
                                    aux = s2 + " ";
                                    isSecondLine = false;
                                    counter = 0;
                                } else
                                {
                                    aux = aux.Insert(aux.Length, s2 + " ");
                                    counter += s2.Length + 1;
                                }
                            } else
                            {
                                aux = aux.Insert(aux.Length, s2 + " ");
                            }
                        } else
                        {
                            textArrayList.Add(aux);
                            aux = s2 + " ";
                            isSecondLine = false;
                            counter = 0;
                        }
                    }
                }
                textArrayList.Add(aux);
                isSecondLine = false;
                counter = 0;
            }
            textArrayList.Add("");
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        movement = -1 * movement;
    }

}

