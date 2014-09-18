using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

//	public Texture backgroundTexture;
	public GUIStyle stylePlay;
	public GUIStyle styleOptions;
	public GUIStyle styleExit;
	private float width;
	private float height;


	void Start () {
		width = 286f;
		height = 62f;
	}

	void OnGUI()
	{
	//	GUI.DrawTexture (new Rect (0, 0, 480, 480), backgroundTexture);
		if (GUI.Button (new Rect ((Screen.width-width)/2f, Screen.height*0.4f-height/2f, width, height), "", stylePlay)) {
			Application.LoadLevel("map0-0");
		}
		if (GUI.Button (new Rect ((Screen.width-width)/2f, Screen.height*0.6f-height/2f, width, height), "", styleOptions)) {
			
		}
		if (GUI.Button (new Rect ((Screen.width-width)/2f, Screen.height*0.8f-height/2f, width, height), "", styleExit)) {
			Application.Quit();
			
		}
	}
}
