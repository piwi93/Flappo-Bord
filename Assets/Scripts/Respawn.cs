using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	public Transform tube;
	public Transform unstopable;
	public Transform tiny;
	public float speed;

	float tiempo = 0;
	int respawnAux;
	int powerAux;

	//Update more slow than normal
	void FixedUpdate(){

		respawnAux = Random.Range(1, 50);
		powerAux = Random.Range(0, 10);

		if(Time.time > tiempo){
			if((respawnAux > 45) && (powerAux <= 5)){
				Instantiate(unstopable, transform.position, transform.rotation);
			}
			else if((respawnAux > 45) && (powerAux >= 6)){
				Instantiate(tiny, transform.position, transform.rotation);
			}
			else{
				Instantiate(tube, transform.position, transform.rotation);
			}
			tiempo = Time.time + speed;
		}

		//Out of camera position
		if (this.transform.position.x <= -8.22f){
			Destroy(gameObject);
		}
	}
}
