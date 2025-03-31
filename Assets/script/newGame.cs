using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour
{   
    //���m���̰��� (�R�A��k)
    public static void ResetHighScoreForScene()
    {
        //��ʦC�X�Ҧ������W��
        string[] allSceneNames = { "main", "level2", "level3" }; 

        //�[�e��
        foreach (string sceneName in allSceneNames){

            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);

        }

        PlayerPrefs.Save();
        Debug.Log("HighScore Reset");
    }

    //���m�����ç�sUI(�D�R�A��k)
    public void ResetAllHighScores(){

        ResetHighScoreForScene(); //���m�Ҧ�����������
    }
}
