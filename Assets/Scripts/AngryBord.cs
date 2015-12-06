using UnityEngine;
using System.Collections;

public class AngryBord : MonoBehaviour {

	public float speed;
	public float movement;

	Vector3 vSpeed;
	Vector3 vMovement;
	Vector3 vMovementLimit = Vector3.zero;
	bool limit;
	int limit_random;

	// Use this for initialization
	void Start(){

		vSpeed.x = speed;
		vMovement.y = movement;
		vMovementLimit.y = this.transform.position.y + vMovement.y;

		//Set a random range of 'y' for respawn
		Vector3 newRespawn = this.transform.position;
		newRespawn.z = -11;
		newRespawn.y = Random.Range (-1.5f, 2.5f);
		this.transform.position = newRespawn;

		//set a random start movement
		limit_random = Random.Range(1, 10);
		if(limit_random < 5){
			limit = false;
		}
		else{
			limit = true;
		}
	}

	
	// Update is called once per frame
	void Update(){

		scrollBord();
	}


	//Update more slow than normal
	void FixedUpdate(){

		bordMovement();
	}


	void scrollBord(){

		//Scroll
        this.transform.position = this.transform.position + (vSpeed * Time.deltaTime);

		//Out of camera position
        if (this.transform.position.x <= -8.22f){
			Destroy(gameObject);
        }
	}


	void bordMovement(){
		if(this.transform.position.y < vMovementLimit.y && limit == false){
			this.transform.position = this.transform.position + (vMovement * (Time.deltaTime/1.2f));
		}
		else if(this.transform.position.y > vMovementLimit.y && limit == false){
			limit = true;
		}

		if(this.transform.position.y > -vMovementLimit.y && limit == true){
			this.transform.position = this.transform.position - (vMovement * (Time.deltaTime/1.05f));
		}

		if(this.transform.position.y < vMovementLimit.y && this.transform.position.y < -vMovementLimit.y){
			limit = false;
		}
	}


	//When Collide
    void OnCollisionEnter2D(Collision2D Collission){

    	if(Collission.gameObject.name == "Bird"){
		
			//Get publics components of Bird.cs script
			Bird bird = Collission.gameObject.GetComponent<Bird>();

			//Destroy when bird is unstopable
			if(bird.birdState == "unstopable"){
				Destroy(gameObject);
	        }
        }
        else if(Collission.gameObject.name == "Bullet(Clone)"){
        	Destroy(gameObject);
        }
    }
}
