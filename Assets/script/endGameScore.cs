using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class endGameScore : MonoBehaviour
{
    public TextMeshProUGUI level1;
    public TextMeshProUGUI level2;
    public TextMeshProUGUI level3;

    public TextMeshProUGUI total;

    void Start(){

        level1.gameObject.SetActive(true);
        level2.gameObject.SetActive(true);
        level3.gameObject.SetActive(true);

        total.gameObject.SetActive(true);
        getHighScore();
    }

    //獲取最高分
    void getHighScore(){
        int highScoreForScene1 = HighScore.GetHighScoreForScene("main");
        level1.text = "Level1:" + highScoreForScene1;
        
        int highScoreForScene2 = HighScore.GetHighScoreForScene("level2");
        level2.text = "Level2:" + highScoreForScene2;
        
        int highScoreForScene3 = HighScore.GetHighScoreForScene("level3");
        level3.text = "Level3:" + highScoreForScene3;

        total.text = "Total:"+(highScoreForScene1 + highScoreForScene2 + highScoreForScene3);
    }


    //清除最高分 (靜態方法)
    public static void ResetHighScoreForScene(){

        //手動列出所有場景名稱
        string[] allSceneNames = { "main", "level2", "level3" }; 

        //加上前綴 
        foreach (string sceneName in allSceneNames){
            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);
        }

        //存玩家資訊
        PlayerPrefs.Save();
    }

    //重置高分並更新UI (非靜態方法)
    public void ResetAllHighScoresAndUpdateUI(){

        //重置所有場景的高分
        ResetHighScoreForScene();

        //更新 UI
        getHighScore(); 
    }
}
