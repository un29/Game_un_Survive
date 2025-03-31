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

    //����̰���
    void getHighScore(){
        int highScoreForScene1 = HighScore.GetHighScoreForScene("main");
        level1.text = "Level1:" + highScoreForScene1;
        
        int highScoreForScene2 = HighScore.GetHighScoreForScene("level2");
        level2.text = "Level2:" + highScoreForScene2;
        
        int highScoreForScene3 = HighScore.GetHighScoreForScene("level3");
        level3.text = "Level3:" + highScoreForScene3;

        total.text = "Total:"+(highScoreForScene1 + highScoreForScene2 + highScoreForScene3);
    }


    //�M���̰��� (�R�A��k)
    public static void ResetHighScoreForScene(){

        //��ʦC�X�Ҧ������W��
        string[] allSceneNames = { "main", "level2", "level3" }; 

        //�[�W�e�� 
        foreach (string sceneName in allSceneNames){
            string key = $"HighScore_{sceneName}";
            PlayerPrefs.SetInt(key, 0);
        }

        //�s���a��T
        PlayerPrefs.Save();
    }

    //���m�����ç�sUI (�D�R�A��k)
    public void ResetAllHighScoresAndUpdateUI(){

        //���m�Ҧ�����������
        ResetHighScoreForScene();

        //��s UI
        getHighScore(); 
    }
}
