using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // �X�^�[�g�{�^�����������Ǝ��s�����
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Exit�{�^�����������Ǝ��s�����
    public void QuitGame()
    {
        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
