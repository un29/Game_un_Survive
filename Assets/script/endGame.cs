using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour {

    public void Quit()
    {
     Debug.Log("Quit");
     Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("start_again");
    }
    public void endGameLast()
    {
        SceneManager.LoadScene("end");
    }

}
