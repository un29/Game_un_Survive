using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class StartGame : MonoBehaviour{
    public Image bgover; // ����
    public TextMeshProUGUI startCountdownText;
    public TextMeshProUGUI levelText; //�ɶ�
    public TextMeshProUGUI un;

    void Start()
    {
        // �b�C���}�l�����í˼Ƭ����� UI
        startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);
        un.gameObject.SetActive(false);
    }

    public void StartButtonClicked(){
        startCountdownText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        bgover.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        StartCoroutine(GameTimer());
    
    }

    IEnumerator GameTimer()
    {
        Debug.Log("�i�J��{");
        levelText.text = "Level 1";// �]�m��r���e

        // �˼� 3 ��
        for (int i = 3; i > 0; i--)
        {
            startCountdownText.text = i.ToString(); // ��s�˼Ƥ�r
            //Debug.Log("�˼�: " + i); // ��ܨC���˼�
            yield return new WaitForSecondsRealtime(1f);    // �C���s�@��
        }

        Debug.Log("�˼ƤT����");

        // �[���C������
        SceneManager.LoadScene("main"); // ������ "main" ����

        /*startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);*/

        Debug.Log("�C���}�l");
    }
}
