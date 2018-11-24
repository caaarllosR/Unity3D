using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCtrl : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            //When a key is pressed down it see if it was the escape key if it was it will execute the code
            Application.Quit(); // Quits the game
        }
    }
}
