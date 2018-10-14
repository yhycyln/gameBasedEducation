using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NumbersController : MonoBehaviour {

    public Text scoreText;
    public Text FindNumbersText;
    int score;
    int randomNumber;
    int gameSelection;  //0 numbers counting, 1 less or greater, 2 ...
    int nextNumber;
    bool greaterOrLess;    //0 means greater, 1 means smaller
    public RectTransform BalloonLocationLayout;
    public GameObject Canvas;
    public Transform test;
    public GameObject BalloonPrefab;
    public GameObject BalloonClickablePrefab;
    public List<GameObject> balloons;
    public List<GameObject> buttons;
    public Vector3 InitialSpawnPosition;
    int level;
    List<Vector3> SpawnPositions;


    int balloonsPopped;

    public List<GameObject> Positions;

    public List<Collider2D> Collisions;
    public List<Vector3> ColliderPositions;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
            // Insert Code Here (I.E. Load Scene, Etc)
            // OR Application.Quit();

            return;
        }
    }
    void Awake()
    {

        gameSelection = ApplicationModel.numbersSceneSelection;
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
        greaterOrLess = Random.Range(0, 2) == 0 ? false : true;
        int[] numbers = new int[10];
        if (gameSelection == 0) {
            FindNumbersText.text = "Count to ";
            FindNumbersText.text += (10 + level * 10).ToString() + "!";
            for (int i = 0; i < 10; i++)
                numbers[i] = i + 1;
            int temp;
            int random;
            for (int i = 0; i < 10; i++)
            {
                temp = numbers[i];
                random = Random.Range(0, 10);
                numbers[i] = numbers[random];
                numbers[random] = temp;
            }
        }
        else if (gameSelection == 1) 
        {
            randomNumber = Random.Range(6, 96);
           
            FindNumbersText.text = "Find numbers ";
            if (greaterOrLess)
            {
                FindNumbersText.text += "Smaller ";
            }
            else
            {
                FindNumbersText.text += "Greater ";
            }
            FindNumbersText.text += "than " + randomNumber + "!";
            //Create Number:
            for (int i = 0; i < 5; i++) 
            {  //5 smaller
                int randBalloonNumber = Random.Range(1, randomNumber);
                numbers[i] = randBalloonNumber;
            }
            for (int i = 5; i < 10; i++)
            {  //5 greater
                int randBalloonNumber = Random.Range(randomNumber + 1, 100);
                numbers[i] = randBalloonNumber;
            }

        }
        for (int i = 0; i < 10; i++)
        {

            
            GameObject newBalloon = Instantiate(BalloonPrefab, Positions[i].transform.localPosition, Quaternion.identity);
            GameObject newBalloonButton = Instantiate(BalloonClickablePrefab, Vector3.zero, Quaternion.identity);
            newBalloonButton.transform.SetParent(Canvas.transform);

            newBalloon.GetComponent<ColliderScript>().CurrentDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            newBalloon.GetComponent<Balloon>().ButtonConnection = newBalloonButton.GetComponent<BalloonConnection>();
            newBalloon.GetComponent<Balloon>().NumbersController = this;
            int additionToNumber = 0;
            if(gameSelection == 0)
                additionToNumber = 10*level;
            newBalloonButton.GetComponent<BalloonConnection>().Number = numbers[i] + additionToNumber;
            newBalloonButton.transform.GetChild(0).GetComponent<Text>().text = (numbers[i] + additionToNumber).ToString();
            newBalloonButton.GetComponent<BalloonConnection>().Balloon = newBalloon.GetComponent<Balloon>();

            newBalloon.GetComponent<Balloon>().Init();
            balloons.Add(newBalloon);
            buttons.Add(newBalloonButton);
        }
    }

    public void CheckValid(int number, GameObject balloon, GameObject button)
    {
        if (gameSelection == 0) {
            if (number == nextNumber)
            {
                balloons.Remove(balloon);
                buttons.Remove(button);
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
                score -= 5;
                scoreText.text = "Score: " + score.ToString();
            }
        }
        else if(gameSelection == 1) {
            if (greaterOrLess == false) {    //looking for greater
                if (number > randomNumber) {    //destroy
                    balloons.Remove(balloon);
                    buttons.Remove(button);
                    Destroy(button);
                    Destroy(balloon);
                    balloonsPopped++;
                    score += 10;
                    scoreText.text = "Score: " + score.ToString();
                    if (balloonsPopped == 5) {

                        LoadNextLevel();
                    }
                }
                else
                {
                    score -= 5;
                    scoreText.text = "Score: " + score.ToString();
                }
            }
            else {      //looking for smaller
                if (number < randomNumber) {    //destroy
                    balloons.Remove(balloon);
                    buttons.Remove(button);
                    Destroy(button);
                    Destroy(balloon);
                    balloonsPopped++;
                    score += 10;
                    scoreText.text = "Score: " + score.ToString();
                    if (balloonsPopped == 5) {

                        LoadNextLevel();
                    }
                }
                else
                {
                    score -= 5;
                    scoreText.text = "Score: " + score.ToString();
                }
            }
        }
    }

    public void LoadNextLevel()
    {
        level++;
        balloonsPopped = 0;
        if (gameSelection == 1)
        {
            for (int i = 0; i < 5; i++)
            {

                GameObject balloonToDestroy = balloons[0];
                GameObject buttonToDestroy = buttons[0];
                balloons.RemoveAt(0);
                buttons.RemoveAt(0);
                Destroy(balloonToDestroy);
                Destroy(buttonToDestroy);

            }
        }

        GenerateBoard();
    }
        
}
