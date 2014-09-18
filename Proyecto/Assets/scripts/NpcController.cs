using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour {

	private float time;
	//private int direccion = 1;
	private const float ATTACKDISTANCE=1f;
	private GameObject player;
	private float speed;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		player = GameObject.Find ("player");
		speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
		//rigidbody2D.mass = 999999f;
		//transform.position +=0*new Vector3 (Random.Range(-1,2), Random.Range(-1,2), 0) * Time.deltaTime * direccion;

			float v = 0;
			float h = 0;
			float xDistance = player.transform.position.x - transform.position.x;
			float yDistance = player.transform.position.y - transform.position.y;
			if (Vector3.Distance(transform.position,player.transform.position) < ATTACKDISTANCE) {
				
				if (xDistance > 0.1f) {
					h = speed*(1f)* Time.deltaTime;
				} else if(xDistance < -0.1f){
					h=speed*(-1f)* Time.deltaTime;
				}
				
				
				if (yDistance > 0.1f) {
					v=speed*(1f)* Time.deltaTime;
				} else if (yDistance < -0.1f){
					v=speed*(-1f)* Time.deltaTime;
				}
				
				
				
			}
			ManageMovement(h,v);

		
		
		
		
		
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
		//print("pene1");
		if(animator){
			//	print("Horizontal: "+h+", Vertical: "+v);
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
			//	print(animator.GetInteger("direction"));
		}
	}

}
