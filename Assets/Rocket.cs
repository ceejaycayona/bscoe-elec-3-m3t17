using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
	Collider c_Enabler ;
	Rigidbody _rigidbody;
	Scene currentscene;
	public Color color = Color.green;
	public Renderer rend;
	public Material material;
	public float mainThrust =500f;
	public float rcsThrust = 150f;
	AudioSource _audioSource;
	Vector3 initialpos1;
	float speed = 1.5f;
	float distance = 15f;
	GameObject rocket1; 
	GameObject targetpad;
	bool _isSoundPlaying = false;
	float range = 30f;
	float changecolor=7f;
	float DistanceToTarget;
	// Use this for initialization
	void Start () {
		currentscene = SceneManager.GetActiveScene ();
		c_Enabler = GetComponent<Collider> ();
		initialpos1 = gameObject.transform.position;
		_rigidbody = GetComponent<Rigidbody> ();
		_audioSource = GetComponent<AudioSource> ();
		rocket1 = GameObject.Find ("Rocket Ship");
		targetpad = GameObject.Find("target pad")	;
		DistanceToTarget = Vector3.Distance (rocket1.transform.position, targetpad.transform.position);
	}

	// Update is called once per frame
	void Update () {
		ProcessInput ();
		//transform.Translate (speed * Time.deltaTime, 0, 0);
	}

	private void ProcessInput(){
		DistanceToTarget = Vector3.Distance (rocket1.transform.position, targetpad.transform.position);
		float rotationSpeed = rcsThrust * Time.deltaTime;
		if (Input.GetKey (KeyCode.Space)) {
			_rigidbody.AddRelativeForce (Vector3.up * mainThrust * Time.deltaTime);
			if (!_isSoundPlaying) {
				_audioSource.Play ();
				_isSoundPlaying = true;
			}
		}
		else {
			_audioSource.Stop ();
			_isSoundPlaying = false;
		}
			
		if (Input.GetKey (KeyCode.A))
		{
			
			transform.Rotate(Vector3.forward*rotationSpeed);

		}
		if (Input.GetKey (KeyCode.D))
		{
			print (DistanceToTarget);
			transform.Rotate(-Vector3.forward*rotationSpeed);
		

		}

		else if (DistanceToTarget<=range && DistanceToTarget>changecolor) {
			DistanceToTarget = Vector3.Distance (rocket1.transform.position, targetpad.transform.position);
			targetpad.gameObject.GetComponent<Renderer> ().material.color = Color.blue;
		}
		else if (DistanceToTarget>range) {
			DistanceToTarget = Vector3.Distance (rocket1.transform.position, targetpad.transform.position);
			targetpad.gameObject.GetComponent<Renderer> ().material.color = Color.gray;
		}
	}


	void OnCollisionEnter(Collision collisionInfo)
	{

		if (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "obstacle") {
			SceneManager.LoadScene("GameLevel1");
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

			transform.position = initialpos1;
			//transform.SetPositionAndRotation = Quaternion.identity;
		} else if (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "platform") {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

			initialpos1 = gameObject.transform.position;
		} else if (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "background") {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

			initialpos1 = gameObject.transform.position;
		} else if (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "wall") {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

			initialpos1 = gameObject.transform.position;
		} else if (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "ceiling") {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
			initialpos1 = gameObject.transform.position;
		} else if (currentscene.buildIndex==0 && (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "target pad")) {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
			SceneManager.LoadScene("GameLevel2");
		} 
		else if (currentscene.buildIndex==1 && (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "target pad")) {
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
			SceneManager.LoadScene("GameLevel3");
		} 
		else if((currentscene.buildIndex==2 && DistanceToTarget<=changecolor) && (gameObject.name == "Rocket Ship" && collisionInfo.collider.name == "target pad" )){
			print ("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
			collisionInfo.gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}

	}
}