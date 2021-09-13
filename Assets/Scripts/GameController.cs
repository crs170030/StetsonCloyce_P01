using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //to do: add pause button
    void Update()
    {
        /////ESC: Exit the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape was pressed");
            Application.Quit();
        }

        /////Backspace: Restart Level
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Backspace was pressed");
            //Application.LoadLevel("Level01"); //warning: need to make system more robust if more levels are added
            SceneManager.LoadScene("Level01");
        }
    }
}
