using UnityEngine;
using System.Collections;

public class CinematicController : MonoBehaviour {
    private GameObject player;
    private Animator playerAnim;
    private PlayerController controller;
    private GameObject lady;
    private Animator ladyAnim;
    private GameObject canvas;
    private Animator canvasAnim;
    private GameObject shine;
    private Animator shineAnim;

	// Use this for initialization
	void Start () {
        player = GeneralController.DefaultController().getPlayer();
        playerAnim=player.GetComponent<Animator>();
        controller = player.GetComponent<PlayerController>();
        lady = GameObject.Find("Ladyuvita");
        ladyAnim=lady.GetComponent<Animator>();
        canvas = GameObject.Find("Canvas");;
        canvasAnim=canvas.GetComponent<Animator>();
        shine = GameObject.Find("Shine");;
        shineAnim=shine.GetComponent<Animator>();

        //controller.animated = true;

        //ladyAnim.SetBool("startAnim", true);
       // playerAnim.SetBool("startAnim", true);
        canvasAnim.SetBool("startAnim", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
