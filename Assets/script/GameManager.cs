using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���չC��
    public void TryAgainGame()
    {
        Time.timeScale = 1; // ��_�C���t��
        // ���s�[����e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
