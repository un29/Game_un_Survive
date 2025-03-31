using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    // 重試遊戲
    public void TryAgainGame(){

        // 恢復遊戲速度
        Time.timeScale = 1; 

        //重新加載當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
