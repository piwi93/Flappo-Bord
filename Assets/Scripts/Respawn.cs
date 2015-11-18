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

	bool powerIsAvailable = true;

	//
	IEnumerator AvailablePowerUp(){
		yield return new WaitForSeconds(10F);
		powerIsAvailable = true;
	}

	//Update more slow than normal
	void FixedUpdate(){

		respawnAux = Random.Range(1, 50);
		powerAux = Random.Range(0, 10);

		if(Time.time > tiempo){
			
			if((respawnAux > 45) && (powerAux <= 4) && (powerIsAvailable)){
				Instantiate(unstopable, transform.position, transform.rotation);
				powerIsAvailable = false;
				StartCoroutine(AvailablePowerUp());
			}
			
			else if((respawnAux > 45) && (powerAux >= 5) && (powerIsAvailable)){
				Instantiate(tiny, transform.position, transform.rotation);
				powerIsAvailable = false;
				StartCoroutine(AvailablePowerUp());
			}
			
			else{
				Instantiate(tube, transform.position, transform.rotation);
			}
			
			tiempo = Time.time + speed;
		}
	}
}
