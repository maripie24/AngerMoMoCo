using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // �C���X�y�N�^�[��Őݒ�
    private bool isPaused = false; // �Q�[�����|�[�Y�����ǂ������Ǘ�����t���O
    public static bool IsPaused { get; private set;} // ���̃X�v���N�g����Q�Ƃ��邽�߂̃v���p�e�B

    void Awake()
    {
        menuPanel.SetActive(false);

        Time.timeScale = 1f; // j�Q�[�������Ԃ̏�����
        IsPaused = false;
    }

    void Update()
    {
        // ESC�L�[�������ꂽ�烁�j���[��ʂ�ONOFF�ɂ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // ���j���[�̕\���E��\����؂�ւ���֐�
    public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);

        // �Q�[���ĊJ�A���f�̐ݒ�
        Time.timeScale = isPaused ? 0f : 1f;

        IsPaused = isPaused; // �v���p�e�BIsPaused�ɂ����݂̏�Ԃ𔽉f����
    }

    // �X�^�[�g��ʂɖ߂�֐�
    public void ReturnToStartMenu()
    {
        // �Q�[���̎��Ԃ�ʏ푬�x�ɖ߂�
        Time.timeScale = 1f;
        // �V�[���̃��[�h������
        SceneManager.LoadScene("StartScene");
    }
}

