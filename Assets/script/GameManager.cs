using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    // ���չC��
    public void TryAgainGame(){

        // ��_�C���t��
        Time.timeScale = 1; 

        //���s�[����e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
