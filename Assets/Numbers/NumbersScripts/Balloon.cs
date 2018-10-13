using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour {

    public NumbersController NumbersController;
    public BalloonConnection ButtonConnection;

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
        Vector3 emptyVector = Vector3.zero;
        emptyVector.Set(transform.position.x, transform.position.y + 20, transform.position.z);
        ButtonConnection.transform.position = emptyVector;
    }
}
