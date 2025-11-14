using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel3 : MonoBehaviour{
    public Image bgover;                        //底圖
    public TextMeshProUGUI startCountdownText;  //倒數
    public TextMeshProUGUI levelText;           //時間
    public TextMeshProUGUI un;

    //新增音效欄位
    public AudioSource audioSource;
    public AudioClip countdownClip;

    void Start(){
        Debug.Log("遊戲開始3");

        startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);
        un.gameObject.SetActive(false);
    }

    public void LevelButtonClicked3(){
        startCountdownText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    }

    //進入協程
    IEnumerator GameTimer(){
        //設置文字內容
        levelText.text = "Level 3";

        //倒數 3 秒
        for (int i = 3; i > 0; i--){
            //更新倒數文字
            startCountdownText.text = i.ToString();

            //播放倒數音效
            if (countdownClip != null && audioSource != null){
                audioSource.PlayOneShot(countdownClip);
            }

            //每秒更新一次
            yield return new WaitForSecondsRealtime(1f);
        }

        Debug.Log("倒數三秒結束 遊戲開始");

        //加載遊戲場景
        SceneManager.LoadScene("level3");
    }
}
