using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel3 : MonoBehaviour{
    public Image bgover; // 底圖
    public TextMeshProUGUI startCountdownText;
    public TextMeshProUGUI levelText; //時間
    public TextMeshProUGUI un;

    void Start()
    {
        Debug.Log("遊戲開始3");
        startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);
        un.gameObject.SetActive(false);
    }

    public void LevelButtonClicked3()
    {
        startCountdownText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        Debug.Log("進入協程");
        levelText.text = "Level 3";// 設置文字內容

        // 倒數 3 秒
        for (int i = 3; i > 0; i--)
        {
            startCountdownText.text = i.ToString(); // 更新倒數文字
            yield return new WaitForSecondsRealtime(1f);    // 每秒更新一次
        }

        Debug.Log("倒數三秒結束");

        // 加載遊戲場景
        SceneManager.LoadScene("level3");

        /*startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);*/

        Debug.Log("遊戲開始");
    }
}
