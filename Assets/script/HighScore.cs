using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public CollisionS playercollision;      //�}��
    private int currentHighScore;           //�x�s������
    private string sceneKey;                //�x�s�������������

    void Start(){

        //�ͦ������������x�s��(�����W��)
        sceneKey = $"HighScore_{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}";

        //�qPlayerPrefs��Ū������
        currentHighScore = PlayerPrefs.GetInt(sceneKey, 0); //�w�]��
        Debug.Log("High Score: " + currentHighScore);

        //�榡 High:����
        highScore.text = $"High:{currentHighScore}";  
        
    }

    public void Update(){

        //�����e����
        int currentScore = playercollision.score;

        //�p�G��e���ư���w�x�s������
        if (currentScore > currentHighScore){

                currentHighScore = currentScore;

                //��s�������x�s
                PlayerPrefs.SetInt(sceneKey, currentHighScore);
                PlayerPrefs.Save();
                
                //��s����UI�榡�� High:����
                highScore.text = $"High:{currentHighScore}"; 
            }
    }

    //�s�W��k (������w�������̰���)
    public static int GetHighScoreForScene(string sceneName){

        string key = $"HighScore_{sceneName}";
        return PlayerPrefs.GetInt(key, 0);
    }
}
