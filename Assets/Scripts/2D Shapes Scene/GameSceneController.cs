using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour {

	//UI elements
	public Text OpeningMessageText;
	public Text ScoreText;
	public Image TargetShapeImage;

	public GameObject[] enemyPrefabs;

	public float shapeSpawnDuration = 1;

	private float shapeSpawnTimer;

	public float minimumShapeSpawnDuration = 0.2f;

	public float Score = 0;

	public string targetShape;

	public Sprite[] shapeSprites;

	void Start(){
		StartCoroutine( this.ShowMessage() );
		
		Time.timeScale = 1;
	}

	void Update()
	{
		ScoreText.text = "Score " + this.Score;

		shapeSpawnTimer -= Time.deltaTime;
		if ( shapeSpawnTimer <= 0 )
		{
			shapeSpawnDuration = Mathf.Max( shapeSpawnDuration * 0.9f, minimumShapeSpawnDuration );

			shapeSpawnTimer = shapeSpawnDuration;

			GameObject enemyObject = Instantiate( enemyPrefabs[Random.Range(0, enemyPrefabs.Length-1)] );
			enemyObject.transform.SetParent( GameObject.Find("Shapes").transform );
			enemyObject.transform.position = new Vector3( 
				Random.Range( -3.5f, 3.5f ),
				7,
				0
			);
		}

		if(Time.timeScale == 0){
			this.GameOver();
		}

		if( Input.GetKeyDown(KeyCode.R) ){
			this.Restart();
		}
	}

	private string SelectRandomShape(){
		string[] shapes = {"Rectangle", "Square", "Circle", "Triangle"};

		int randomInt = Random.Range(0, 3);

		return shapes[randomInt];
	}

	private IEnumerator ShowMessage(){
		this.targetShape = this.SelectRandomShape();
		
		OpeningMessageText.text = "Do not let " + this.targetShape + "s to reach the bottom of the road";
		
		Dictionary<string, int> shapeSpriteDict = new Dictionary<string, int>();
		shapeSpriteDict.Add("Circle", 0);
		shapeSpriteDict.Add("Rectangle", 1);
		shapeSpriteDict.Add("Square", 2);
		shapeSpriteDict.Add("Triangle", 3);

		TargetShapeImage.sprite = this.shapeSprites[ shapeSpriteDict[this.targetShape] ];
		if(this.targetShape == "Rectangle"){
			TargetShapeImage.rectTransform.sizeDelta = new Vector2(200, 100);
		}

		Time.timeScale = 0.01f;

		yield return new WaitForSeconds(2);

		OpeningMessageText.enabled = false;
		TargetShapeImage.enabled = false;

		yield return null;
	}

	private void GameOver(){
		#if UNITY_WSA
		OpeningMessageText.text = "Game Over. Press R to Restart";
		#else
		OpeningMessageText.text = "Game Over. Press Button to Restart";
		#endif
		OpeningMessageText.enabled = true;
	}

	public void Restart(){
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}
}
