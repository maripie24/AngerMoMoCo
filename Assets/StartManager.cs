using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // スタートボタンが押されると実行される
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
