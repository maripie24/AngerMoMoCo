using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // �X�^�[�g�{�^�����������Ǝ��s�����
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
