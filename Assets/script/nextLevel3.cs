using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextLevel3 : MonoBehaviour{
    public Image bgover; // ����
    public TextMeshProUGUI startCountdownText;
    public TextMeshProUGUI levelText; //�ɶ�
    public TextMeshProUGUI un;

    void Start()
    {
        Debug.Log("�C���}�l3");
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
        Debug.Log("�i�J��{");
        levelText.text = "Level 3";// �]�m��r���e

        // �˼� 3 ��
        for (int i = 3; i > 0; i--)
        {
            startCountdownText.text = i.ToString(); // ��s�˼Ƥ�r
            yield return new WaitForSecondsRealtime(1f);    // �C���s�@��
        }

        Debug.Log("�˼ƤT����");

        // �[���C������
        SceneManager.LoadScene("level3");

        /*startCountdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        bgover.gameObject.SetActive(false);*/

        Debug.Log("�C���}�l");
    }
}
