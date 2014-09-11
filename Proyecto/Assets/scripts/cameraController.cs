using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	private GameObject mapaBase;
	private float cameraX,cameraY,height,width;
	private Camera cam;
	private GameObject player;
	// Use this for initialization
	void Start () {
		cameraX = cameraY = 0;
		mapaBase = GameObject.Find ("base");
		player = GameObject.Find ("player");
		cam = Camera.main;
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (mapaBase == null) {
			mapaBase = GameObject.Find ("base");
		}
		cameraX = player.renderer.bounds.center.x;
		cameraY = player.renderer.bounds.center.y;
		if (cameraX - width/2 < -1*mapaBase.renderer.bounds.size.x/2)
		{
			cameraX = width/2-mapaBase.renderer.bounds.size.x/2;
		}
		if (cameraY - height / 2 < -1*mapaBase.renderer.bounds.size.y/2)
		{
			cameraY = height / 2-mapaBase.renderer.bounds.size.y/2;
		}
		if (cameraX + width/2 > mapaBase.renderer.bounds.size.x/2)
		{
			cameraX = mapaBase.renderer.bounds.size.x/2 - width/2;
		}
		if (cameraY + height / 2 > mapaBase.renderer.bounds.size.y / 2) {
						cameraY = mapaBase.renderer.bounds.size.y / 2 - height / 2;
				}
		Vector3 tal = new Vector3 (cameraX, cameraY, -10);
		this.transform.position = tal;
	}	
}