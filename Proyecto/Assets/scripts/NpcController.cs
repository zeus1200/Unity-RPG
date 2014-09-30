using UnityEngine;
using System.Collections;

public class NpcController : Character
{

    private const int maxStingLenght = 38;
    private float time;
    private GameObject player;
    public string speakText;
    private ArrayList textArrayList;
    private int counter;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //player = GameObject.Find ("player");
        speed = 1f;
        counter = 0;
        textArrayList = new ArrayList();
        fillArrayList();
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
                print(split2.Length);
                foreach (string s2 in split2)
                {
                    if (s2.Trim() != "")
                    {
                        if (aux.Length + s2.Length < 76)
                        {
                            if ((aux.Length + s2.Length > 38) && (!isSecondLine))
                            {
                                isSecondLine = true;
                                aux = aux.Insert(aux.Length, "\n" + s2+" ");
                                counter+=s2.Length+1;
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


        //textArrayList.Add(Variables.textNpc1);
        for (int i = 0; i<textArrayList.Count; i++)
        {
            print(textArrayList [i]);
        }
    }

   

}

