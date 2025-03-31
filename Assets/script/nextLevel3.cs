using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel3 : MonoBehaviour{
    public Image bgover;                        //����
    public TextMeshProUGUI startCountdownText;  //�˼�
    public TextMeshProUGUI levelText;           //�ɶ�
    public TextMeshProUGUI un;

    void Start(){

        Debug.Log("�C���}�l3");

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

    //�i�J��{
    IEnumerator GameTimer(){

        //�]�m��r���e
        levelText.text = "Level 3";

        //�˼� 3 ��
        for (int i = 3; i > 0; i--){

            //��s�˼Ƥ�r
            startCountdownText.text = i.ToString();

            //�C���s�@��
            yield return new WaitForSecondsRealtime(1f);    
        }

        Debug.Log("�˼ƤT���� �C���}�l");

        // �[���C������
        SceneManager.LoadScene("level3");
    }
}
