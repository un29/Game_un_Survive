using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstInGame : MonoBehaviour{

    public Image bgover;                    //底圖
    public TextMeshProUGUI FirstInfoText;
    public TextMeshProUGUI un;          

    void Start(){

        //顯示
        bgover.gameObject.SetActive(true);
        FirstInfoText.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    }

    //協程 (封面進入遊戲)
    IEnumerator GameTimer(){

        //等待2.5s
        yield return new WaitForSecondsRealtime(2.5f);

        bgover.gameObject.SetActive(false);
        FirstInfoText.gameObject.SetActive(false);
        Debug.Log("封面進入遊戲");
    }
 }
