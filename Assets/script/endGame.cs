using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour {

    //結束
    public void Quit(){

      //Debug.Log("Quit");
       Application.Quit();

    }

    //重新跳到start scene
    public void Restart(){
        SceneManager.LoadScene("start");
    }

    //跳到end scence
    public void endGameLast(){
        SceneManager.LoadScene("end");
    }

}
