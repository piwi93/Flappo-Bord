using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	Vector3 speed = Vector3.zero;
	Vector3 vForce;
	Vector3 vJump;
	Score gameScore;

	public float force;
	public float jump;
	public float maxSpeedUp;
	public float maxSpeedDown;
	[HideInInspector]
	public string birdState;

	[HideInInspector]
	public bool flap = false;
	bool hasPlayed = false;

	string[] states = {"normal", "unstopable", "tiny", "rlauncher"};
	Vector3 xPosition = Vector3.zero;

	AudioSource bloob;
	AudioSource peow;
	AudioSource pipe;
	AudioSource boup;
	AudioSource rockpush;
	Animator anim;


	//Set Bird to Normal
	void setBirdToNormal(){
		
		//Set normal state
		birdState = states[0];

		//Set normal animation
		anim.SetBool("RL", false);
		
		//Set scale
		transform.localScale = new Vector3(6.064453F, 7.314585F, 1F);
		
		//Set mass values
		vForce.y = -19;
		vJump.y = 7;
		maxSpeedUp = 5.5f;
		maxSpeedDown = 8.2f;
		
		//Set position
		xPosition = this.gameObject.transform.position;
		xPosition.x = -4.73F;
		transform.position = xPosition;
	}



	//Use this for initialization
	void Start(){

		//Set thhe public vars
    	vForce.y = force;
    	vJump.y = jump;

    	//Set all audio sources
    	AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
	    bloob = allMyAudioSources[0];
	    peow = allMyAudioSources[1];
		pipe = allMyAudioSources[2];
		boup = allMyAudioSources[3];
		rockpush = allMyAudioSources[4];

		anim = GetComponent<Animator>();

		//Set bird
		//setBirdToNormal();
	}



	//Update is called once per frame
	void Update(){
		
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            flap = true;
        }
	
	}


	//Update more slow than normal
	void FixedUpdate(){

		//Bird physics and movement
		speed += vForce * Time.deltaTime;
		if(flap == true){
			flap = false;
			speed.y = vJump.y;

			//bloob sound
			bloob.Play();
		}
		transform.position += speed * Time.deltaTime;

		//Transform angle when bird is falling down & is flappying
		float rotation = 0;
		if(speed.y >= 0){
			rotation = Mathf.Lerp (0, 50, speed.y / maxSpeedUp);
			speed.y += 0.018f;
		}
		else{
			rotation = Mathf.Lerp (0, -75, -speed.y / maxSpeedDown);
			//Adding a falling force
			speed.y -= 0.045f;
		}
		transform.rotation = Quaternion.Euler(0, 0, rotation);
	}



/****** COLLISIONS ******/

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
			//peow sound
			peow.Play();

			//reset game score
			gameScore = GameObject.Find("Score").GetComponent<Score>();
			gameScore.resetScore();

			//Restart Current Scene (Pause?)
			Application.LoadLevel(Application.loadedLevel);
			//Time.timeScale = 0.0f;
		}

		else{

			//Im unstopable bitch, but better if I dont collide with the ground
			if(Collission.gameObject.name == "Ground"){

				//rockpush sound
				rockpush.Play();

				//flying near the ground
				Vector3 falling = new Vector3(0,3.0F,0);
				speed.y = falling.y;
			}

			else{
				//peow sound
				peow.Play();
			}
		}

	}



/****** POWER UPS ******/

	//PowerUp timer
	IEnumerator powerUpTimer(){
		
		//Restore Values after 7'
		yield return new WaitForSeconds(7F);
		setBirdToNormal();
		
		//Loosing powerup sound
		if(hasPlayed == false){
			pipe.Play();
			hasPlayed = true;
			yield return new WaitForSeconds(0.5F);
		}
	}


	//Used in Unstopable Power Up
	IEnumerator waitGrow(){
		
		yield return new WaitForSeconds(0.200F);
		
		//Change position & scale
		xPosition = this.gameObject.transform.position;
		xPosition.x = -3.2F;
		transform.position = xPosition;
		transform.localScale = new Vector3(32, 32, 1);
		
		//Changes mass value
		vForce.y = -20;
		vJump.y = 6;
		maxSpeedUp = 8;
		maxSpeedDown = 18;
	}


	//When Trigger with a powerup
	void OnTriggerEnter2D(Collider2D Collider){;
		
		//Unstopable Power Up
		if(Collider.gameObject.name == "UnstopablePowerUp(Clone)"){

			//Set unstopable state to bird
	    	birdState = states[1];
			hasPlayed = false;

			//First size
			transform.localScale = new Vector3(15, 15, 1);
			
			//Delay of 0.3ms and then get 2nd size
			StartCoroutine(waitGrow());

			StartCoroutine(powerUpTimer());
		}

		//Tiny Power Up
		else if(Collider.gameObject.name == "TinyPowerUp(Clone)"){

			//Set tiny state
			birdState = states[2];
			hasPlayed= false;

			//Size
			transform.localScale = new Vector3(4, 4, 1);

			//Mass value
			vForce.y = -14;
			vJump.y = 5;
			maxSpeedUp = 10;
			maxSpeedDown = 10;

			StartCoroutine(powerUpTimer());
		}

		//Rocket Launcher Power Up
		if(Collider.gameObject.name == "RLauncherPowerUp(Clone)"){

			//Animator anim = GetComponent<Animator>();
			anim.SetBool("RL", true);

			//Set rlauncher state to bird
			birdState = states[2];
			hasPlayed = false;

			StartCoroutine(powerUpTimer());
		}
	}

} //End
