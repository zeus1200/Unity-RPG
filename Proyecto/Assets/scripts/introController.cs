using UnityEngine;
using System.Collections;

public class introController : MonoBehaviour {
	private float startTime,animDuration,time,changeTime,splashTime,timeAnim;
	private GameObject water;
	private GameObject blackScreen;
	private Color toColor,fromColor;
	private bool startAnim;
	//private Animator water;
	// Use this for initialization
	void Start () {
		startTime = 1f;
		animDuration = 4.02f;
		splashTime = 2.5f;
		changeTime = 4f;
		time = Time.time;
		water = GameObject.Find ("water");
		water.SetActive(false);
		blackScreen = GameObject.Find ("blackScreen");
		fromColor= blackScreen.renderer.material.color;
		toColor = fromColor;
		toColor.a = 0;
		startAnim = false;
		//water.animation.playAutomatically = false;
		//water.animation.Stop ();
		Screen.SetResolution(480, 480,false);
		Screen.SetResolution(480, 480,false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > startTime) {
			water.SetActive(true);		
		}
		if (Time.time - time > startTime + animDuration) {
			if(!startAnim)
			{
				startAnim=true;
				timeAnim=Time.time;
			}
			blackScreen.renderer.material.color=Color.Lerp(fromColor,toColor,(Time.time-timeAnim)/splashTime);
		}
		if (Time.time - time > startTime + animDuration+changeTime) {
			Application.LoadLevel("mainMenu");
		}
	}
}
