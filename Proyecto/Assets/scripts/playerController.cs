using UnityEngine;
using System.Collections;
public class playerController : MonoBehaviour {
	private Animator animator;
	private float speed=1.5f;
	private float time;
	private bool hitted;
	private Vector3 from, to;
	private float hitTime=0.175f;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		hitted = false;
	}
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.identity;
		//rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.mass = 999999f;
		float v = speed*Input.GetAxis("Vertical")* Time.deltaTime;
		float h = speed*Input.GetAxis("Horizontal")* Time.deltaTime;
		ManageMovement(h, v);
		//ManageMovement(h, v);
		if (Input.GetKeyDown (KeyCode.Space)&&!transform.Find("sword").gameObject.activeSelf) {
			transform.Find("sword").gameObject.SetActive(true);
				}

		if (hitted) {
			transform.position=Vector3.Slerp(from,to,(Time.time-time)/hitTime);
			if(Time.time - time > hitTime){
				hitted=false;
			}
		
		}
		//print (this.renderer.bounds.size.x);
	}
	void ManageMovement(float horizontal,float vertical) {
		if (horizontal != 0f || vertical != 0f) {
			animator.SetBool ("moving", true);
			animateWalk (horizontal, vertical);
		} else {
			animator.SetBool ("moving", false);
		}
		Vector3 movement = new Vector3 (horizontal,vertical, 0);
		//rigidbody2D.velocity = movement;
		transform.position += movement;
	}
	void animateWalk(float h,float v) {
		if(animator){
			if ((v > 0)&&(v>h)) {
				animator.SetInteger ("direction", 1);
			}
			if ((h > 0)&&(v<h)) {
				animator.SetInteger ("direction", 2);
			}
			if ((v < 0)&&(v<h)) {
				animator.SetInteger ("direction", 3);
			}
			if ((h < 0 )&&(v>h)) {
				animator.SetInteger ("direction", 4);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		//return;
		if (coll.gameObject.tag == "Enemy") {
			if(!hitted)
			{
				hitted=true;
				time=Time.time;
				from=transform.position;
				float xDistance= coll.transform.position.x-transform.position.x;
				float yDistance= coll.transform.position.y-transform.position.y;
				if(Mathf.Abs(xDistance)>Mathf.Abs(yDistance)){
					if (xDistance<0) {
						to=new Vector3(from.x+0.32f,from.y,from.z);
					}else
					{
						to=new Vector3(from.x-0.32f,from.y,from.z);
					}
				}else
				{
					if (yDistance<0) {
						to=new Vector3(from.x,from.y+0.32f,from.z);
					}else
					{
						to=new Vector3(from.x,from.y-0.32f,from.z);
					}
				}
			}

		}
		
	}
}
