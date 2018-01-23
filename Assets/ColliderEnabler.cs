using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour {
	Collider CollideEnabler;
	GameObject player ;
	// Use this for initialization
	void Start () {
		CollideEnabler =  GetComponent<Collider> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			CollideEnabler.enabled = !CollideEnabler.enabled;
			Debug.Log ("Collider.enable = " + CollideEnabler.enabled);
		}
	}	
}
