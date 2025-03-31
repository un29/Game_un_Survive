using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstInGame : MonoBehaviour{

    public Image bgover;                    //����
    public TextMeshProUGUI FirstInfoText;
    public TextMeshProUGUI un;          

    void Start(){

        //���
        bgover.gameObject.SetActive(true);
        FirstInfoText.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    }

    //��{ (�ʭ��i�J�C��)
    IEnumerator GameTimer(){

        //����2.5s
        yield return new WaitForSecondsRealtime(2.5f);

        bgover.gameObject.SetActive(false);
        FirstInfoText.gameObject.SetActive(false);
        Debug.Log("�ʭ��i�J�C��");
    }
 }
