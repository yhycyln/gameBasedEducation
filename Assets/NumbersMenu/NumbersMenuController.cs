﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NumbersMenuController : MonoBehaviour {


    public void countingNumbersClicked(Button button) {
        int index = button.transform.GetSiblingIndex();
        ApplicationModel.numbersSceneSelection = index;
        SceneManager.LoadScene(2);
        
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
