using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public Transform jumpSign;

	float timeaux = 4;
	Vector3 jumpSignPosition = new Vector3(-2.27f,1.03f,1);
	bool gameStart = false;

	GameObject bird;
	Bird birdScript;
	GameObject respawn;
	GameObject ground;
	

	// Use this for initialization
	void Start () {
		bird = GameObject.Find("Bird");
		respawn = GameObject.Find("Respawn");
		ground = GameObject.Find("Ground");
	}
	
	// Update is called once per frame
	void Update(){
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && gameStart == false) {
			gameStart = true;
			bird.GetComponent<Bird>().enabled = true;
			respawn.GetComponent<Respawn>().enabled = true;
			ground.GetComponent<Scroll>().enabled = true;

			birdScript = GameObject.Find("Bird").GetComponent<Bird>();
			birdScript.flap = true;
		}

		if(Time.time > timeaux && gameStart == false){
			timeaux = Time.time + 5;
			Instantiate(jumpSign, jumpSignPosition, transform.rotation);
		}
	}
}
