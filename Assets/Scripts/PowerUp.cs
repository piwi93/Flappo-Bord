using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {


	Vector3 vSpeed;
    public float speed;
    public AudioClip sfx;


	// Use this for initialization
	void Start () {
		vSpeed.x = speed;

		//Set a random range of 'y' for respawn
		Vector3 newRespawn = this.transform.position;
		newRespawn.y = Random.Range (1.3f, 4.8f);
		this.transform.position = newRespawn;
	}
	

	// Update is called once per frame
	void Update () {
		scrollPowerup();
	}


	//Scroll
	private void scrollPowerup(){
        this.transform.position = this.transform.position + (vSpeed * Time.deltaTime);

        //Out of camera position
        if (this.transform.position.x <= -8.22f){
			Destroy(gameObject);
        }
    }


    //When Trigger
	void OnTriggerEnter2D(Collider2D Collider){

		//Play sfx
		AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, 0.2f);

		//Destroy powerup
		if(Collider.gameObject.name == "Bird"){
			Destroy(gameObject);
		}
	}

} //End