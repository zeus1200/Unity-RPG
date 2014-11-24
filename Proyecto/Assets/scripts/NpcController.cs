using UnityEngine;
using System.Collections;

public class NpcController : Character
{

    private const int maxStingLenght = 38;
    private float time;
    public string speakText;
    private ArrayList textArrayList;
    private int counter;
    public bool movable, speakable;
    public int direction;
    private bool playerCollision;
    public bool animated;

    // Use this for initialization
    void Start()
    {
        base.Start();
        playerCollision = false;
        speed = 0.5f;
        counter = 0;
        textArrayList = new ArrayList();
        if (speakable)
        {
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
       if (!animated)
        {
            if (movable && !playerCollision)
            {
                move();

            } 
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "sword")
        {
       
            movement = -1 * movement;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            rigid.isKinematic = true;
            playerCollision = true;
        } 
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            rigid.isKinematic = false;
            playerCollision = false;
        }
    }

}

