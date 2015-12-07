using UnityEngine;
using System.Collections;

public class MessageScroll : MonoBehaviour {

	public float speed;
	Vector3 vSpeed;


	// Use this for initialization
	void Start(){
		
		//Set variables
		vSpeed.x = speed;
	}

	// Update is called once per frame
	void Update () {

		//Scroll
		this.transform.position = this.transform.position + (vSpeed * Time.deltaTime);

		//Destroy if the game start
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)){
			Destroy(gameObject);
		}

		//Out of camera position
		if (this.transform.position.x <= -19f){
			Destroy(gameObject);
		}
	}
}
