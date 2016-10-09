using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonFunctionality : MonoBehaviour {


    //Checks which Button was clicked
    public void onClick(string buttonName)
    {
        //Start the game
        if (buttonName == "PlayButton")
        {
            SceneManager.LoadScene(1,LoadSceneMode.Single);
        }

        if (buttonName == "PracticeMode")
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        if (buttonName == "Quit")
        {

            Application.Quit();
        }

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
