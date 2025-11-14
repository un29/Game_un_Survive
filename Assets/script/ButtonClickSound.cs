using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;         // 點擊音效
    private static AudioSource audioSource;  // 使用靜態 AudioSource，跨場景共用

    void Awake()
    {
        // 如果還沒有全域的 AudioSource，就創建一個並設為常駐物件
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("GlobalUIAudioManager");
            audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.8f; // 可自行調整音量
            DontDestroyOnLoad(audioObj);
        }
    }

    void OnEnable()
    {
        // 每次啟用都綁定事件（避免場景重載失效）
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveListener(PlayClickSound);
        btn.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        Debug.Log(" Button {gameObject.name} 被按下！");
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning(" {gameObject.name} 沒有設定 clickSound 或 AudioSource 無法使用");
        }
    }
}
