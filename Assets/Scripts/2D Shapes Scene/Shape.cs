using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	private float movementSpeed = 2f;
	private float lifeTime = 8f;
	private GameObject gameSceneController;

	// Use this for initialization
	void Start(){
		this.gameSceneController = GameObject.Find("GameSceneController");
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody2D>().velocity = Vector2.down * movementSpeed;

		if(this.transform.position.y < -6f){
			if( this.gameSceneController.GetComponent<GameSceneController>().targetShape.ToLower() + "(Clone)" == this.transform.name ){
				//Game Over
				Time.timeScale = 0;
			}
		}

		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			Destroy(gameObject);
		}
	}
}
