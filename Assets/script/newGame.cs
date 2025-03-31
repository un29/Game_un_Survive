using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour
{   
    //重置的最高分 (靜態方法)
    public static void ResetHighScoreForScene()
    {
        //手動列出所有場景名稱
        string[] allSceneNames = { "main", "level2", "level3" }; 

        //加前綴
        foreach (string sceneName in allSceneNames){

            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);

        }

        PlayerPrefs.Save();
        Debug.Log("HighScore Reset");
    }

    //重置高分並更新UI(非靜態方法)
    public void ResetAllHighScores(){

        ResetHighScoreForScene(); //重置所有場景的高分
    }
}
