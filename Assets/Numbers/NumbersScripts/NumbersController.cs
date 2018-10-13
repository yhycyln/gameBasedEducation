using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbersController : MonoBehaviour {

    public Text scoreText;
    int score;
    
    int nextNumber;
    public RectTransform BalloonLocationLayout;
    public GameObject Canvas;
    public Transform test;
    public GameObject BalloonPrefab;
    public GameObject BalloonClickablePrefab;
    public Vector3 InitialSpawnPosition;
    int level;
    List<Vector3> SpawnPositions;

    int balloonsPopped;

    public List<GameObject> Positions;

    public List<Collider2D> Collisions;
    public List<Vector3> ColliderPositions;

    void Awake()
    {
        scoreText.text = "Score: 0";
        score = 0;
        nextNumber = 1;
        level = 0;
        balloonsPopped = 0;
        GenerateBoard();
        Collisions = new List<Collider2D>();
        ColliderPositions = new List<Vector3>();
    }

    void GenerateBoard()
    {
        int[] randomNumbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        int temp;
        int random;
        for (int i = 0; i < 10; i++)
        {
            temp = randomNumbers[i];
            random = Random.Range(0, 10);
            randomNumbers[i] = randomNumbers[random];
            randomNumbers[random] = temp;
        }

        for (int i = 0; i < 10; i++)
        {

            
            GameObject newBalloon = Instantiate(BalloonPrefab, Positions[i].transform.localPosition, Quaternion.identity);
            GameObject newBalloonButton = Instantiate(BalloonClickablePrefab, Vector3.zero, Quaternion.identity);
            newBalloonButton.transform.SetParent(Canvas.transform);

            newBalloon.GetComponent<ColliderScript>().CurrentDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            newBalloon.GetComponent<Balloon>().ButtonConnection = newBalloonButton.GetComponent<BalloonConnection>();
            newBalloon.GetComponent<Balloon>().NumbersController = this;
            newBalloonButton.GetComponent<BalloonConnection>().Number = randomNumbers[i] + 10*level;
            newBalloonButton.transform.GetChild(0).GetComponent<Text>().text = (randomNumbers[i] + 10*level).ToString();
            newBalloonButton.GetComponent<BalloonConnection>().Balloon = newBalloon.GetComponent<Balloon>();

            newBalloon.GetComponent<Balloon>().Init();
        }
    }

    public void CheckValid(int number, GameObject balloon, GameObject button)
    {
        if (number == nextNumber)
        {
            Destroy(button);
            Destroy(balloon);
            balloonsPopped++;
            nextNumber++;
            score += 10;
            scoreText.text = "Score: " + score.ToString();
            if (balloonsPopped == 10)
            {
                LoadNextLevel();
            }
        }
        else
        {
            score -= 3;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void LoadNextLevel()
    {
        level++;
        balloonsPopped = 0;
        GenerateBoard();
    }
        
}
