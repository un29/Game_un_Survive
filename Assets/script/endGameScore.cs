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


    // �R�A��k�G�M�����w�������̰���
    public static void ResetHighScoreForScene()
    {
        string[] allSceneNames = { "main", "level2", "level3" }; // ��ʦC�X�Ҧ������W��
        foreach (string sceneName in allSceneNames)
        {
            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);
        }
        PlayerPrefs.Save();
        Debug.Log("HighScore Reset");
    }

    // �D�R�A��k�G���m�����ç�s UI
    public void ResetAllHighScoresAndUpdateUI()
    {
        ResetHighScoreForScene(); // ���m�Ҧ�����������
        getHighScore(); // ��s UI
    }
}
