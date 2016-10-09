using UnityEngine;
using System.Collections;

public class MoveLineDetector : MonoBehaviour {
    //The reference to the established position the line will be placed before moving
    private Vector2 UP_POSITION = new Vector2(0.1f,5.17f);
    private Vector2 DOWN_POSITION = new Vector2(0.1f,-5.17f);
    private Vector2 LEFT_POSITION = new Vector2(-6.83f,0f);
    private Vector2 RIGHT_POSITION = new Vector2(6.83f,0f);

    //The reference to the line object
    public GameObject LineDetector;

    //The function to move the line detector
    void MoveLine()
    {
        //Generate a random number for the position and direction
        int move = Random.Range(0, 4);

        //Switch between the case
        switch(move){

            //Move up
            case 0:
                LineDetector.transform.position = UP_POSITION;

                break;

            //Move down
            case 1:
                LineDetector.transform.position = DOWN_POSITION;

                break;

            //Move left
            case 2:
                LineDetector.transform.position = LEFT_POSITION;

                break;

            //Move right
            case 3:
                LineDetector.transform.position = RIGHT_POSITION;

                break;

        }


    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
