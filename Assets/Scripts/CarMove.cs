using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMove : MonoBehaviour
{
    private GameObject[] cars; //Hold gameobject and its position on the road.
    private int[] positions;
    private int leftPointer, rightPointer;

    private const float TRANSITION_TIME = 0.5f;
    private const float LANE_SIZE = 2.0f;

    // Use this for initialization
    void Start ()
    {
        cars = new GameObject[3];
        cars[0] = GameObject.FindGameObjectWithTag("PlayerCarLeft");
        cars[1] = GameObject.FindGameObjectWithTag("PlayerCarMid");
        cars[2] = GameObject.FindGameObjectWithTag("PlayerCarRight");

        positions = new int[3];
        positions[0] = 1;
        positions[1] = 2;
        positions[2] = 3;

        leftPointer = 0;
        rightPointer = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
}

    public void MoveLeft()
    {
        if (GameManagerScript.State != GameState.Running)
            return;

        if (leftPointer != 3) // Cars available to move left
        {
            cars[leftPointer].GetComponent<CarScript>().am.SetTrigger("LeftTurn");

            Vector3 carPos = cars[leftPointer].transform.position;
            Vector3 newPos = new Vector3(--positions[leftPointer] * LANE_SIZE, carPos.y, carPos.z);
            StartCoroutine(MoveToPosition(cars[leftPointer], newPos, TRANSITION_TIME));
            rightPointer = leftPointer;
            leftPointer++;
        }

        CheckCenter();
    }

    public void MoveRight()
    {
        if (GameManagerScript.State != GameState.Running)
            return;

        if (rightPointer != -1) // Cars available to move right
        {
            cars[rightPointer].GetComponent<CarScript>().am.SetTrigger("RightTurn");

            Vector3 carPos = cars[rightPointer].transform.position;
            Vector3 newPos = new Vector3(++positions[rightPointer] * LANE_SIZE, carPos.y, carPos.z);
            StartCoroutine(MoveToPosition(cars[rightPointer], newPos, TRANSITION_TIME));
            leftPointer = rightPointer;
            rightPointer--;
        }

        CheckCenter();
    }

    //If the cars have recentered, reset the pointers (Total hack, i know.)
    void CheckCenter()
    {
        if (positions[0] == 1 && positions[1] == 2 && positions[2] == 3)
        {
            leftPointer = 0;
            rightPointer = 2;
        }
    }

    //Lerp the car to the new position
    IEnumerator MoveToPosition(GameObject obj, Vector3 newPos, float time)
    {
        float elapsedTime = 0;
        Vector3 startPos = obj.transform.position;
        while (elapsedTime < time)
        {
            obj.transform.position = Vector3.Lerp(startPos, newPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
