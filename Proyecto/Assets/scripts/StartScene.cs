using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
   	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter().PostNotification(this,"sceneChanged");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
