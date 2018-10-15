using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour {

    public NumbersController NumbersController;
    public BalloonConnection ButtonConnection;
    public Vector3 CurrentDirection;
    public float speedMultiplier = 5;


    bool initialized = false;

    void Awake()
    {
        initialized = false;
    }

    public void Init()
    {
        NumbersController = GameObject.FindWithTag("NumbersController").GetComponent<NumbersController>();
        initialized = true;
    }

    void FixedUpdate()
    {
        if (!initialized)
        {
            return;
        }
        transform.position = transform.position + CurrentDirection * speedMultiplier;
        Vector3 emptyVector = Vector3.zero;
        emptyVector.Set(transform.position.x, transform.position.y + 15.0f, -5.0f);
        ButtonConnection.transform.position = emptyVector;
    }
}
