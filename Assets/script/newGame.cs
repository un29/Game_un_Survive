using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour
{// �R�A��k�G�M�����w�������̰���
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
    public void ResetAllHighScores()
    {
        ResetHighScoreForScene(); // ���m�Ҧ�����������
    }
}
