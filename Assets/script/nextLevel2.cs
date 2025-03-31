using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel2 : MonoBehaviour
{
    public Image bgover;                        //����
    public TextMeshProUGUI startCountdownText;  //�˼�
    public TextMeshProUGUI levelText;           //�ɶ�
    public TextMeshProUGUI un;

    void Start(){

        Debug.Log("�C���}�l2");

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

    //�i�J��{
    IEnumerator GameTimer(){

        // �]�m��r���e
        levelText.text = "Level 2";

        //�˼� 3 ��
        for (int i = 3; i > 0; i--){

            //��s�˼Ƥ�r ("�˼�: " + i)
            startCountdownText.text = i.ToString();

            // �C���s�@��
            yield return new WaitForSecondsRealtime(1f);    
        }

        Debug.Log("�˼ƤT���� �C���}�l");

        //�[���C������
        SceneManager.LoadScene("level2");
    }
}
