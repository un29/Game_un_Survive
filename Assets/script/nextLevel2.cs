using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel2 : MonoBehaviour
{
    public Image bgover;                        //底圖
    public TextMeshProUGUI startCountdownText;  //倒數
    public TextMeshProUGUI levelText;           //時間
    public TextMeshProUGUI un;

    void Start(){

        Debug.Log("遊戲開始2");

        startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);
        un.gameObject.SetActive(false);
    }


    public void LevelButtonClicked2(){

        startCountdownText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        un.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    }

    //進入協程
    IEnumerator GameTimer(){

        // 設置文字內容
        levelText.text = "Level 2";

        //倒數 3 秒
        for (int i = 3; i > 0; i--){

            //更新倒數文字 ("倒數: " + i)
            startCountdownText.text = i.ToString();

            // 每秒更新一次
            yield return new WaitForSecondsRealtime(1f);    
        }

        Debug.Log("倒數三秒結束 遊戲開始");

        //加載遊戲場景
        SceneManager.LoadScene("level2");
    }
}
