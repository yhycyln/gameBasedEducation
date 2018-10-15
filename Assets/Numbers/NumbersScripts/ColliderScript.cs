using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {
    public Vector3 CurrentDirection;
    public float speedMultiplier;

    NumbersController NumbersController;
    Collider2D Collider;
    Balloon Balloon;

    void Awake()
    {
        Balloon = gameObject.GetComponent<Balloon>();
        speedMultiplier = 5;
        NumbersController = GameObject.FindWithTag("NumbersController").GetComponent<NumbersController>();
        Collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("horizontalWall") || other.CompareTag("verticalWall"))
        {
            BorderColision(other);

        }
        if (other.CompareTag("balloon"))
        {
            int index = NumbersController.Collisions.IndexOf(Collider);
            if (index == -1)
            {
                NumbersController.Collisions.Add(other);
                NumbersController.ColliderPositions.Add(CurrentDirection);
                CurrentDirection = other.GetComponent<ColliderScript>().CurrentDirection;
            }
            else
            {
                NumbersController.Collisions.RemoveAt(index);
                CurrentDirection = NumbersController.ColliderPositions[index];
                NumbersController.ColliderPositions.RemoveAt(index);
            }
            Balloon.CurrentDirection = CurrentDirection;
        }
    }


    public void BorderColision(Collider2D other)
    {
        Vector3 newDirection = Vector3.zero;
        if (other.CompareTag("horizontalWall"))
        {
            newDirection.Set(CurrentDirection.x, -1.0f * CurrentDirection.y, 0.0f);
        }
        else
        {
            newDirection.Set(-1.0f * CurrentDirection.x, CurrentDirection.y, 0.0f);
        }
        CurrentDirection = newDirection;
        Balloon.CurrentDirection = CurrentDirection;
        
    }
    public void ChangeDirection()
    {
        Vector3 newDirection = -1.0f * CurrentDirection;

        //newDirection.Set(newDirection.y, newDirection.x, 0.0f);
        Debug.Log("currentdirection: " + CurrentDirection);
        Debug.Log("newdirection: " + newDirection);
        CurrentDirection = newDirection;
    }

    //public void FixedUpdate()
    //{
    //    transform.position = transform.position + CurrentDirection * speedMultiplier;
    //}
}
