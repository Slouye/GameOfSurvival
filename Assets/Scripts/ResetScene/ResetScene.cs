using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {
    private bool IsEnter = false;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (IsEnter == false)
            {
                IsEnter = true;
                SceneManager.LoadScene("Game");
            }
        }
	}
}
