using UnityEngine;
using System.Collections;

public class TimerDestroyer : MonoBehaviour {

	public float timeBack;

	// Use this for initialization
	void Start(){
		timeBack = timeBack + Time.time;
	}

	// Update is called once per frame
	void Update () {
		if(Time.time > timeBack){
			Destroy(gameObject);
		}
	}
}
