using UnityEngine;
using UnityEngine.UI;

public class GamePauseController : MonoBehaviour
{
    public Button pauseButton;
    public Button playButton;

    public GameObject gameOverPanel;      // 拖入 GameOver 畫面
    public GameObject nextLevelPanel;     // 拖入 NextLevel 畫面

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1; // 遊戲一開始正常
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);

        pauseButton.onClick.AddListener(PauseGame);
        playButton.onClick.AddListener(PlayGame);
    }

    void Update()
    {
        // 如果遊戲結束畫面或下一關畫面出現，就隱藏暫停按鈕
        if ((gameOverPanel != null && gameOverPanel.activeSelf) ||
            (nextLevelPanel != null && nextLevelPanel.activeSelf))
        {
            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(false);
        }
        else if (!isPaused)
        {
            // 只有在遊戲進行中才顯示暫停鍵
            pauseButton.gameObject.SetActive(true);
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        Debug.Log("遊戲暫停了");
    }

    void PlayGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        Debug.Log("遊戲繼續");
    }
}
