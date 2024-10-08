using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // �C���X�y�N�^�[��Őݒ�
    [SerializeField] private GameObject gameClearImage; // �C���X�y�N�^�[��Őݒ�
    [SerializeField] private int crearKill = 50;

    private bool isPaused = false; // �Q�[�����|�[�Y�����ǂ������Ǘ�����t���O
    public static bool IsPaused { get; private set;} // ���̃X�v���N�g����Q�Ƃ��邽�߂̃v���p�e�B

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.normalBGM); // Normal�p��BGM���Đ�
    }

    private void Awake()
    {
        menuPanel.SetActive(false);
        gameClearImage.SetActive(false);

        Time.timeScale = 1f; // j�Q�[�������Ԃ̏�����
        IsPaused = false;
    }

    private void Update()
    {
        // ESC�L�[�������ꂽ�烁�j���[��ʂ�ONOFF�ɂ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        // 50�L���ŃQ�[���N���A
        if(KillCounter.killCounter.EnemyCount >= crearKill)
        {
            StartCoroutine(StartGameClearSequence());
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

    // �Q�[���N���A���̏���
    private IEnumerator StartGameClearSequence()
    {
        gameClearImage.SetActive(true);
        Time.timeScale = 0.2f;

        // ���A���^�C����3�b�҂i�X���[�Đ��Ȃ̂Ŏ��ۂ�10�{�x��������j
        yield return new WaitForSecondsRealtime(3f);

        ReturnToStartMenu();
    }
}

