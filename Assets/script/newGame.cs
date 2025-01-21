using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour
{// 靜態方法：清除指定場景的最高分
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
    public void ResetAllHighScores()
    {
        ResetHighScoreForScene(); // 重置所有場景的高分
    }
}
