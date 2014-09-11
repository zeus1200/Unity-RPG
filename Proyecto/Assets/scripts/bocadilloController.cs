using UnityEngine;
using System.Collections;

public class bocadilloController : MonoBehaviour {
	private GameObject player;
	private GameObject[] npcs;
	private GameObject npc;
	private const float DISTANCE = 0.5f;
	//private GameObject bocadillo;
	private float min;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		npcs = GameObject.FindGameObjectsWithTag ("TalkingNpc");
		//bocadillo = GameObject.FindGameObjectWithTag("Bocadillo");
		renderer.enabled = false;
		//bocadillo.SetActive (false);
		//min = 2000f;
	}
	
	// Update is called once per frame
	void Update () {
		float aux=1000f;
		min = DISTANCE*2f;
		foreach (GameObject i in npcs) {

			aux = Vector3.Distance(i.transform.position,player.transform.position);
			if(aux<min)
			{
				min=aux;
				npc=i;

			}
		}
		if (min <= DISTANCE) {

						renderer.enabled = true;
						transform.position = new Vector3 (npc.transform.position.x, npc.transform.position.y + 0.32f, npc.transform.position.z);
				} else {
			renderer.enabled = false;
		}
	}
}
