using UnityEngine;
using System.Collections;

public class Tubes : MonoBehaviour {

	public float speed;

    Vector3 vSpeed;
    bool hasPlayed = false;
	Score gameScore;


	// Use this for initialization
	void Start(){

		//Set variables
		vSpeed.x = speed;
		hasPlayed = false;

		//Set gameScore functions
		gameScore = GameObject.Find("Score").GetComponent<Score>();

		//Set a random range of 'y' for respawn
		Vector3 newRespawn = this.transform.position;
		newRespawn.y = Random.Range (1.3f, 5f);
		this.transform.position = newRespawn;
	}


	// Update is called once per frame
	void Update(){
		scrollTubes();
	}


	private void scrollTubes(){

        //Scroll
        this.transform.position = this.transform.position + (vSpeed * Time.deltaTime);
 
        //Out of camera position
        if (this.transform.position.x <= -8.22f){
			Destroy(gameObject);
        }

        //Bird position
		if( (this.transform.position.x <= -5f) && (hasPlayed == false) ){

			//Play audio
			GetComponent<AudioSource>().Play();
			hasPlayed = true;

			//Set game score +1
			gameScore.addOne();
        }

    }


	//Destroy tube anim
	IEnumerator detroyTube(){

		GetComponent<Animation>().Play("TubesGone");

		//Set game score +1
		gameScore.addOne();

		//Wait for destroy
		yield return new WaitForSeconds(1F);
		Destroy(gameObject);
	}


    //When Collide
    void OnCollisionEnter2D(Collision2D Collission){

		//Get publics components of Bird.cs script
		Bird bird = Collission.gameObject.GetComponent<Bird>();

		//Destroy when bird is unstopable
		if(bird.birdState == "unstopable"){
			StartCoroutine(detroyTube());
        }

    }

} //End