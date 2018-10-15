using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoneyController : MonoBehaviour {
    public Canvas canvas;
    static int collected;
    int gameSelection;
    static int[] kurusValue = { 5, 10, 25, 50, 100 };
    static int[] liraValue = { 5, 10, 20, 50};
    static int[] liraCount = { 0, 0, 0, 0 };
    static int[] kurusCount = { 0, 0, 0, 0, 0};
    static int target;
    public void NewGameButtonClicked(Button button) {
        GenerateGame();
    }
    public void addButtonClicked(Button button) {
        //Debug.Log("zaaa");

        if (gameSelection == 0)
        {
            int index = button.transform.parent.GetSiblingIndex();
            index -= 4;
            collected = collected + kurusValue[index];
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: " + collected + "krs";
            kurusCount[index] += 1;

            button.transform.parent.GetChild(3).GetComponentInChildren<Text>().text = kurusCount[index].ToString();
        }
        else if(gameSelection == 1) {
            int index = button.transform.parent.GetSiblingIndex();
            collected = collected + liraValue[index];
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: " + collected + "TL";
            liraCount[index] += 1;
            button.transform.parent.GetChild(3).GetComponentInChildren<Text>().text = liraCount[index].ToString();
        }

        if (checkIfFinished())
        {
            canvas.transform.Find("CongText").GetComponent<Text>().enabled = true;
            canvas.transform.Find("NewGamePanel").gameObject.transform.hideFlags = HideFlags.None;

        }
        //cong. text;

    }
   
    public void subButtonClicked(Button button) {
        //Debug.Log("djkglsjg");
        int index = button.transform.parent.GetSiblingIndex();
        Debug.Log("ksgjda");
        //if (kurusCount[index] == 0 && liraCount[index] == 0)
         //   return;
        if (gameSelection == 0)
        {
            index -= 4;
            collected = collected - kurusValue[index];
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: " + collected + "krs";
            kurusCount[index] -= 1;

            button.transform.parent.GetChild(3).GetComponentInChildren<Text>().text = kurusCount[index].ToString();
            //button.transform.parent.GetChild(index).GetComponentInChildren<Text>().text = collected.ToString();
        }
        else if (gameSelection == 1)
        {
            collected = collected - liraValue[index];
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: " + collected + "TL";
            liraCount[index] -= 1;

            button.transform.parent.GetChild(3).GetComponentInChildren<Text>().text = liraCount[index].ToString();
        }
        if (checkIfFinished())
        {
            canvas.transform.Find("CongText").GetComponent<Text>().enabled = true;
            canvas.transform.Find("NewGamePanel").gameObject.transform.hideFlags = HideFlags.None;
            //canvas.transform.Find("NewGamePanel").gameObject.SetActive(true);

        }
            //cong. text;
    }
    public bool checkIfFinished() {
        Debug.Log(target);
        Debug.Log(collected);
        return collected == target;
    }
   // Use this for initialization
    void Start () {
        Debug.Log("Start called");
        GenerateGame();


        //canvas.transform.Find("TargetText").GetComponent<Text>().text = "Target: 0 TL";

    }
    void GenerateGame() {
        canvas.transform.Find("CongText").GetComponent<Text>().enabled = false;
        canvas.transform.Find("NewGamePanel").gameObject.hideFlags = HideFlags.HideInHierarchy;
        //canvas.transform.Find("NewGamePanel").GetComponentInChildren<Text>().enabled = false;
        //canvas.transform.Find("NewGamePanel").gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
            liraCount[i] = 0;
        for (int i = 0; i < 5; i++)
            kurusCount[i] = 0;

        //set texts to 0
        for (int i = 0; i < 9; i++)
        {

            canvas.transform.GetChild(i).GetChild(3).GetComponentInChildren<Text>().text = "0";

        }
        //Debug.Log("Collected: 0");
        collected = 0;



        for (int i = 0; i < 9; i++) {
            canvas.transform.GetChild(i).gameObject.SetActive(true);
        }
        //decide kuruş or lira
        gameSelection = Random.Range(0, 2);   //0->kuruş, 1->lira
        target = Random.Range(1, 21) * 5;
        if (gameSelection == 0) {
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: 0 krs";
            for (int i = 0; i < 4; i++) {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            canvas.transform.Find("TargetText").GetComponent<Text>().text = "Target: " + target.ToString() + " krs";
        }
        else if(gameSelection == 1) {
            canvas.transform.Find("CollectedText").GetComponent<Text>().text = "Collected: 0 TL";
            for (int i = 4; i < 9; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            canvas.transform.Find("TargetText").GetComponent<Text>().text = "Target: " + target.ToString() + "TL";
        }




    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            // Insert Code Here (I.E. Load Scene, Etc)
            // OR Application.Quit();

            return;
        }
    }
}
