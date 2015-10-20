using UnityEngine;
using System.Collections;

public class Tubes : MonoBehaviour {

    Vector3 vSpeed;
    Vector3 vDistance;
    bool hasPlayed = false;
    
    public float speed;
    public float distance;


	// Use this for initialization
	void Start(){
		vSpeed.x = speed;
		vDistance.x = distance;
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

        	//New respawn postion 'x'
            Vector3 newRespawn = this.transform.position + vDistance;
            
            //Set a random range of 'y' for respawn
            newRespawn.y = Random.Range (1.3f, 5f);
            
            //Move the old tube to the respawn area
            this.transform.position = newRespawn;

            //Reset sfx flag
            hasPlayed = false;
        }

        //Play audio
		if( (this.transform.position.x <= -5f) && (hasPlayed == false) ){
			GetComponent<AudioSource>().Play();
			hasPlayed = true;
        }

    }
}
