using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public CollisionS playercollision;      //腳本
    private int currentHighScore;           //儲存的高分
    private string sceneKey;                //儲存場景高分的鍵值

    void Start(){

        //生成場景對應的儲存鍵(場景名稱)
        sceneKey = $"HighScore_{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}";

        //從PlayerPrefs中讀取高分
        currentHighScore = PlayerPrefs.GetInt(sceneKey, 0); //預設值
        Debug.Log("High Score: " + currentHighScore);

        //格式 High:分數
        highScore.text = $"High:{currentHighScore}";  
        
    }

    public void Update(){

        //獲取當前分數
        int currentScore = playercollision.score;

        //如果當前分數高於已儲存的高分
        if (currentScore > currentHighScore){

                currentHighScore = currentScore;

                //更新高分並儲存
                PlayerPrefs.SetInt(sceneKey, currentHighScore);
                PlayerPrefs.Save();
                
                //更新高分UI格式為 High:分數
                highScore.text = $"High:{currentHighScore}"; 
            }
    }

    //新增方法 (獲取指定場景的最高分)
    public static int GetHighScoreForScene(string sceneName){

        string key = $"HighScore_{sceneName}";
        return PlayerPrefs.GetInt(key, 0);
    }
}
