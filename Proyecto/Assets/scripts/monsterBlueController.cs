using UnityEngine;
using System.Collections;

public class monsterBlueController : MonoBehaviour {
	private float time;
	private int direccion = 1;
	private const float ATTACKDISTANCE=6;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		//rigidbody2D.mass = 999999f;
		//transform.position +=0*new Vector3 (Random.Range(-1,2), Random.Range(-1,2), 0) * Time.deltaTime * direccion;
		if ((!collider2D.enabled) && (Time.time - time >= 5f)) {
						collider2D.enabled = true;
						renderer.enabled = true;
				} else {
			float xDistance = player.transform.position.x - transform.position.x;
			float yDistance = player.transform.position.y - transform.position.y;
						if (Vector3.Distance(transform.position,player.transform.position) < ATTACKDISTANCE) {
				
								if (xDistance >= 0) {
										collitionBox.left = collitionBox.left + vX * dTicks / 1000;
										if (mPlayer->getCollitionBox ().intersects (collitionBox)) {
												collitionBox.left = prevX;
												mPlayer->setHurt ();
										}
										substate1 = RIGHT;
										checkCollitionObjectsLeftRight ();
								} else {
										collitionBox.left = collitionBox.left - vX * dTicks / 1000;
										if (mPlayer->getCollitionBox ().intersects (collitionBox)) {
												collitionBox.left = prevX;
												mPlayer->setHurt ();
										}
										substate1 = LEFT;
										checkCollitionObjectsLeftRight ();
								}
				
				
								if (yDistance >= 0) {
										collitionBox.top = collitionBox.top + vY * dTicks / 1000;
										if (mPlayer->getCollitionBox ().intersects (collitionBox)) {
												collitionBox.top = prevY;
												mPlayer->setHurt ();
										}
										substate2 = DOWN;
										checkCollitionObjectsUpDown ();
					
								} else {
										collitionBox.top = collitionBox.top - vY * dTicks / 1000;
										if (mPlayer->getCollitionBox ().intersects (collitionBox)) {
												collitionBox.top = prevY;
												mPlayer->setHurt ();
										}
										substate2 = UP;
										checkCollitionObjectsUpDown ();
					
								}
				
								if (yDistance * yDistance > xDistance * xDistance) {
										state = substate2;
								} else {
										state = substate1;
								}
				
				
				
						} else {
								if (isUp) {
										state = UP;
										if (collitionBox.top < 0) {
												collitionBox.top = 0;
												isDown = true;
												isUp = false;
										} else {
						
												collitionBox.top = collitionBox.top - vY * dTicks / 1000;
						
										}
					
										if (mPlayer->getCollitionBox ().intersects (collitionBox)) {
												collitionBox.top = prevY;
										}
					
										checkCollitionObjectsUpDown ();
										/*objectPos = objects->begin();
				while (objectPos != objects->end())
				{
				if ((*objectPos)->GetAABB().intersects(collitionBox))
				{
				collitionBox.top = prevY;
				}
				objectPos++;
				}*/
					
					
								}	
		
						}

				}




	
	}

	void ManageMovement(float horizontal,float vertical) {
		/*if (horizontal != 0f || vertical != 0f) {
			animator.SetBool ("moving", true);
			animateWalk (horizontal, vertical);
		} else {
			animator.SetBool ("moving", false);
		}*/
		Vector3 movement = new Vector3 (horizontal,vertical, 0);
		//rigidbody2D.velocity = movement;
		transform.position += movement;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "sword") {
			collider2D.enabled=false;
			renderer.enabled=false;
			time=Time.time;
				}


	}
	
	
	void OnCollisionEnter2D(Collision2D coll) {
		//direccion *= -1;
	}
}
