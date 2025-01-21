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
    void Start()
    {
        level1.gameObject.SetActive(true);
        level2.gameObject.SetActive(true);
        level3.gameObject.SetActive(true);
        getHighScore();
    }

    void getHighScore()
    {
        int highScoreForScene1 = HighScore.GetHighScoreForScene("main");
        level1.text = "Level1:" + highScoreForScene1;
        
        int highScoreForScene2 = HighScore.GetHighScoreForScene("level2");
        level2.text = "Level2:" + highScoreForScene2;
        
        int highScoreForScene3 = HighScore.GetHighScoreForScene("level3");
        level3.text = "Level3:" + highScoreForScene3;
    }


    // 靜態方法：清除指定場景的最高分
    public static void ResetHighScoreForScene()
    {
        string[] allSceneNames = { "main", "level2", "level3" }; // 手動列出所有場景名稱
        foreach (string sceneName in allSceneNames)
        {
            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);
        }
        PlayerPrefs.Save();
        Debug.Log("HighScore Reset");
    }

    // 非靜態方法：重置高分並更新 UI
    public void ResetAllHighScoresAndUpdateUI()
    {
        ResetHighScoreForScene(); // 重置所有場景的高分
        getHighScore(); // 更新 UI
    }
}
