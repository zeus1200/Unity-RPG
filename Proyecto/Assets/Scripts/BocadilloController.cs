﻿using UnityEngine;
using System.Collections;

public class BocadilloController : MonoBehaviour
{
    private GameObject player;
    private GameObject[] npcs;
    private GameObject npc;
    private const float DISTANCE = 0.5f;
    private NpcController script;
    //private GameObject bocadillo;
    private float min;
    // Use this for initialization
    void Start()
    {
        // player = GeneralController.DefaultController().getPlayer();
        NotificationCenter.DefaultCenter().AddObserver(this, "sceneChanged");
        NotificationCenter.DefaultCenter().AddObserver(this, "playerWannaTalk");
        //bocadillo = GameObject.FindGameObjectWithTag("Bocadillo");
        renderer.enabled = false;
        //bocadillo.SetActive (false);
        //min = 2000f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GeneralController.DefaultController().getPlayer();
        }

            npcs = GameObject.FindGameObjectsWithTag("TalkingNpc");
            

        if (npcs != null)
        {
            float aux = 1000f;
            min = DISTANCE * 2f;
            foreach (GameObject i in npcs)
            {

                aux = Vector3.Distance(i.transform.position, player.transform.position);
                if (aux < min)
                {
                    min = aux;
                    npc = i;

                }
            }
            if (min <= DISTANCE)
            {

                renderer.enabled = true;
                transform.position = new Vector3(npc.transform.position.x, npc.transform.position.y + 0.32f, npc.transform.position.z);
            } else
            {
                renderer.enabled = false;
                npc = null;
            }
        }
    }

  /*  void sceneChanged(Notification notification)
    {
        npcs = GameObject.FindGameObjectsWithTag("TalkingNpc");
        npcs = null;
    }*/

    void playerWannaTalk(Notification notification)
    {
        if (npc != null)
        {
            string text;

            script = npc.gameObject.GetComponent("NpcController") as NpcController;
            text = script.getText();
            if (text != "")
            {
                Variables.text=text;
                NotificationCenter.DefaultCenter().PostNotification(this, "drawText");
            } else
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "hideText");
            }
        }
    }

}
