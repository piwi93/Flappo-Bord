using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	Vector3 speed = Vector3.zero;
	Vector3 vForce;
	Vector3 vJump;

	public float force;
	public float jump;
	public float maxSpeed;
	public string birdState;

	bool flap = false;
	bool hasPlayed = false;

	string[] states = {"normal", "unstopable", "tiny", "killer"};
	Vector3 powerupPosition = Vector3.zero;

	AudioSource bloob;
	AudioSource peow;
	AudioSource pipe;
	AudioSource boup;


	//Use this for initialization
	void Start(){

    	vForce.y = force;
    	vJump.y = jump;

    	//Set normal state to bird
    	birdState = states[0];

    	//Set all audio sources
    	AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
	    bloob = allMyAudioSources[0];
	    peow = allMyAudioSources[1];
		pipe = allMyAudioSources[2];
		boup = allMyAudioSources[3];

	}

	//PowerUp timer to get normal
	IEnumerator powerUpTimer() {

		//Restore Values after 10'
		yield return new WaitForSeconds(9F);

		//Loosing powerup sound
		if(hasPlayed == false){
			pipe.Play();
			hasPlayed = true;
			yield return new WaitForSeconds(0.5F);
		}

		//For Unstopable
		if(birdState == states[1]){
			transform.localScale = new Vector3(6.064453F, 7.314585F, 1F);
			powerupPosition.x = 1.5F;
			transform.position -= powerupPosition;

			//Restore mass values
			vForce.y = -18;
			vJump.y = 7;
			maxSpeed = 7;
		}

		//Normal again
		birdState = states[0];
	}

	//Update is called once per frame
	void Update(){
		
		//Check if I got any power up
		if(birdState != states[0]){
			StartCoroutine(powerUpTimer());
		}

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            flap = true;

            //bloob sound
            bloob.Play();
        }
	
	}


	//Update more slow than normal
	void FixedUpdate(){

		speed += vForce * Time.deltaTime;

		if(flap == true){
			flap = false;
			speed.y = vJump.y;
		}

		transform.position += speed * Time.deltaTime;

		//Transform angle when bird is falling down & is flappying
		float rotation = 0;

		if(speed.y >= 0){
			rotation = Mathf.Lerp (0, 40, speed.y / maxSpeed);
		}
		else{
			rotation = Mathf.Lerp (0, -70, -speed.y / maxSpeed);
		}

		transform.rotation = Quaternion.Euler(0, 0, rotation);
	}


	//When Collide
	void OnCollisionEnter2D(Collision2D Collission){

		//Collide with the sky
		if(Collission.gameObject.name == "Sky"){
			//boup sound
			boup.Play ();

			//falling
			Vector3 falling = new Vector3(0,3.0F,0);
			speed.y = -falling.y;
		}

		//Or Im not unstopable and I collide with the rest
		else if( birdState != states[1] ){
			//bloob sound
			peow.Play();

			//Restart Current Scene (Pause?)
			Application.LoadLevel(Application.loadedLevel);
		}

		//Else Im unstopable bitch, but better if I dont collide with the ground 
		else{
			if(Collission.gameObject.name == "Ground"){

				//bloob sound
				peow.Play();

				//Restart Current Scene (Pause?)
				Application.LoadLevel(Application.loadedLevel);
			}
		}

	}


	//Used in Unstopable Power Up
	IEnumerator waitGrow(){
		
        yield return new WaitForSeconds(0.200F);

		//Change position & scale
		powerupPosition.x = 1.5F;
		transform.position += powerupPosition;
        transform.localScale = new Vector3(32, 32, 1);

		//Changes mass value
		vForce.y = -20;
		vJump.y = 6;
		maxSpeed = 20;
    }

	//When Trigger
	void OnTriggerEnter2D(Collider2D Collider){
		
		//Unstopable Power Up
		if(Collider.gameObject.name == "UnstopablePowerUp"){

			//Set unstopable state to bird
	    	birdState = states[1];
			hasPlayed = false;

			//First size
			transform.localScale = new Vector3(15, 15, 1);
			
			//Delay of 0.3ms and then get 2nd size
			StartCoroutine(waitGrow());
		}

	}

}
