using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	//The Target
    public Transform target;


	//Update after everything else in the Scene was updated
	void LateUpdate(){
		//Go to the position of my target
		transform.position = new Vector3(
			target.position.x + 4,
            transform.position.y,
            transform.position.z
        );
	}
}
