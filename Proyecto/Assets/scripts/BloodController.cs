using UnityEngine;
using System.Collections;

public class BloodController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if((renderer as SpriteRenderer).sprite.name == "sangre_8"){
			Destroy(gameObject);
		}
	}
}
