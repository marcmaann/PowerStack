using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

    private GameObject[] theStack;
    private int scoreCount = 0;
    private int stackIndex;
    private const float BOUNDS_SIZE = 3.5f;
    private const float STACK_MOVING_SPEED = 5.0f;

    private float tileTransition = 1000.0f;
    private float tileSpeed = 1.5f;

    private bool isMovingOnX = true;

    private float secondaryPosition;
    private Vector3 desiredPosition;

    private void Start () {
        theStack = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            theStack[i] = transform.GetChild(i).gameObject;
        }
        stackIndex = transform.childCount - 1;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceTile())
            {

           
            SpawnTile();
            scoreCount++;
        }
            else
            {
                EndGame();
            }
                }
        MoveTile();
        //Move stack
        transform.position = Vector3.Lerp(transform.position, desiredPosition, STACK_MOVING_SPEED * Time.deltaTime);
	}
    private void MoveTile()
    {
        tileTransition += Time.deltaTime * tileSpeed;
        if (isMovingOnX)
        {
            theStack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition) * BOUNDS_SIZE, scoreCount, secondaryPosition);
        }
        else
        {
            theStack[stackIndex].transform.localPosition = new Vector3(secondaryPosition, scoreCount, Mathf.Sin(tileTransition) * BOUNDS_SIZE);
        }
    }

    private void SpawnTile()
    {
        stackIndex--;
        if(stackIndex < 0)
        {
            stackIndex = transform.childCount - 1;
        }
        desiredPosition = (Vector3.down) * scoreCount;
   theStack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
    }

    private bool PlaceTile()
    {
        Transform t = theStack[stackIndex].transform;
        secondaryPosition = isMovingOnX ? t.localPosition.x : t.localPosition.z;
        isMovingOnX = !isMovingOnX;
        
        return true; 
     
        
    }

    private void EndGame()
    {

    }
}
