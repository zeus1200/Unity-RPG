using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

	private Quaternion from;
	private Quaternion to;
	private float time,speed;
	private bool isActive;
	// Use this for initialization
	void Start () {
		time = Time.time;
		speed = 0.25f;
		isActive=false;
	}
	
	// Update is called once per frame
	void Update () {
		int direction = transform.parent.GetComponent<Animator> ().GetInteger ("direction");
		if (!isActive) {
						
			if (direction == 1) {
				from=Quaternion.Euler(0, 0, 315);
				to=Quaternion.Euler(0, 0, 45);
				transform.localPosition=new Vector3(0,0.06f,0);
				renderer.sortingOrder=transform.parent.renderer.sortingOrder-1;
			}
			if (direction == 2) {
				from=Quaternion.Euler(0, 0, 225);
				to=Quaternion.Euler(0, 0, 315);
				transform.localPosition=new Vector3(0.06f,-0.06f,0);
				renderer.sortingOrder=transform.parent.renderer.sortingOrder+1;
			}
			if (direction == 3) {
				from=Quaternion.Euler(0, 0, 135);
				to=Quaternion.Euler(0, 0, 225);
				transform.localPosition=new Vector3(0,-0.06f,0);
				renderer.sortingOrder=transform.parent.renderer.sortingOrder+1;
				//print(renderer.sortingOrder);
			}
			if (direction == 4) {
				from=Quaternion.Euler(0, 0, 45);
				to=Quaternion.Euler(0, 0, 135);
				transform.localPosition=new Vector3(-0.06f,-0.06f,0);
				renderer.sortingOrder=transform.parent.renderer.sortingOrder+1;
			}
			isActive = true;
			time = Time.time;
			renderer.enabled = true;
				}

		//transform.rotation = from;
		transform.rotation = Quaternion.Lerp(from, to, (Time.time-time)/speed);
		if(Time.time - time > speed){
			isActive=false;
			renderer.enabled = false;
			gameObject.SetActive(false);

		}
	}
}
