using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // スタートボタンが押されると実行される
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Exitボタンが押されると実行される
    public void QuitGame()
    {
        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
