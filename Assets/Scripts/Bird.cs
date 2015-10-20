using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	//Movement speed
	//public float speed = 2;

	//Flap force
    //public float force = 300;

	Vector3 speed = Vector3.zero;
	Vector3 vForce;
	Vector3 vJump;
	bool flap = false;
	AudioSource bloob;
	AudioSource peow;

	public float force;
	public float jump;
	public float maxSpeed;


	//Use this for initialization
	void Start(){
		
		// Fly towards the right
    	//GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

    	vForce.y = force;
    	vJump.y = jump;

    	AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
	    bloob = allMyAudioSources[0];
	    peow = allMyAudioSources[1];

	}
	
	//Update is called once per frame
	void Update(){
	
		//Flap
        /*if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }*/

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
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
	void OnCollisionEnter2D(Collision2D coll){

		//bloob sound
        peow.Play();
	
	    //Restart Current Scene
	    Application.LoadLevel(Application.loadedLevel);
	
	}
}
