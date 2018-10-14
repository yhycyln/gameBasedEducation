using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonConnection : MonoBehaviour {

    public Balloon Balloon;
    public Text NumberText;
    public int Number;

    public void BalloonClicked(){
        Balloon.NumbersController.CheckValid(Number, Balloon.gameObject, gameObject);
    }

}
