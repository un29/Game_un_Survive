using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour {

    //����
    public void Quit(){

      //Debug.Log("Quit");
       Application.Quit();

    }

    //���s����start scene
    public void Restart(){
        SceneManager.LoadScene("start");
    }

    //����end scence
    public void endGameLast(){
        SceneManager.LoadScene("end");
    }

}
