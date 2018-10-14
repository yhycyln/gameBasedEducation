using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 movementDirection;
	public float movementSpeed;
	public float lifetime = 1;
	private GameObject gameSceneController;

	void Start(){
		this.gameSceneController = GameObject.Find("GameSceneController");

		if(transform.position.x > 3){
			transform.Rotate(0, 0, 180);
		}
	}

	void Update ()
	{
		this.GetComponent<Rigidbody2D> ().velocity = movementDirection * movementSpeed;

		lifetime -= Time.deltaTime;
		if (lifetime <= 0)
		{ 
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D( Collider2D c )
	{
		if ( c.tag =="Shape" )
		{
			// Debug.Log(c.transform.name);
			// Debug.Log(this.gameSceneController.GetComponent<GameSceneController>().targetShape.ToLower() + "(Clone)");
			this.GivePoint(c);

			Destroy( c.gameObject );
			Destroy( this.gameObject );
		}
	}

	void GivePoint(Collider2D c){
		if( c.transform.name == this.gameSceneController.GetComponent<GameSceneController>().targetShape.ToLower() + "(Clone)" ){
			this.gameSceneController.GetComponent<GameSceneController>().Score += 100;
		} else {
			this.gameSceneController.GetComponent<GameSceneController>().Score -= 50;
		}
	}
}
