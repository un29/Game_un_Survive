using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstInGame : MonoBehaviour
{
    public Image bgover;                    //底圖
    public TextMeshProUGUI FirstInfoText;
    public TextMeshProUGUI un;

    // 音效
    public AudioSource audioSource;
    public AudioClip soundClip;

    void Start()
    {
        // 顯示封面
        bgover.gameObject.SetActive(true);
        FirstInfoText.gameObject.SetActive(true);
        un.gameObject.SetActive(true);

        // 播放音效
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }

        // 自動隱藏封面
        StartCoroutine(HideCover());
    }

    //協程 (封面進入遊戲)
    IEnumerator HideCover()
    {
        //等待2.5s
        yield return new WaitForSecondsRealtime(2.5f);

        bgover.gameObject.SetActive(false);
        FirstInfoText.gameObject.SetActive(false);
        un.gameObject.SetActive(false);

        Debug.Log("封面進入遊戲");
    }
}
