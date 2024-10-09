using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private GameObject enemy;

    [Header("Audio Mixers")]
    public AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource shotSource;
    [SerializeField] private AudioSource runSource; // Player�ړ����p

    [Header("SE Player Audio Clip")]
    [SerializeField] private AudioClip normalRun;
    [SerializeField] private AudioClip normalShot;
    [SerializeField] private AudioClip normalJump;
    [SerializeField] private AudioClip angerRun;
    [SerializeField] private AudioClip angerPunch;
    [SerializeField] private AudioClip angerJump;

    [Header("SE others")]
    [SerializeField] private AudioClip gameClear;

    [Header("BGM")]
    public AudioClip normalBGM;
    public AudioClip angerBGM;


    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // ���ɃC���X�^���X�����݂���ꍇ�͔j��
        }
    }

    private void Start()
    {
        // �X���C�_�[�̏����l��ݒ�
    }

    // BGM���Đ�
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        bgmSource.clip = clip;
        bgmSource.loop = loop; // BGM�Ȃ̂Ń��[�v
        bgmSource.Play();
    }

    // BGM���~ memo;�Q�[���N���A�Œ�~������
    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // �ړ������Đ�
    public void PlayRun(bool isAngerMode)
    {
        if (runSource != null)
        {
            AudioClip clipToPlay = isAngerMode ? angerRun : normalRun;

            if (!runSource.isPlaying || runSource.clip != clipToPlay)
            {
                runSource.clip = clipToPlay;
                runSource.loop = true;
                runSource.Play();
            }
        }
    }

    // �ړ������~
    public void StopRun()
    {
        if (runSource != null && runSource.isPlaying)
        {
            runSource.Stop();
        }
    }

    public void PlayShot()
    {
        shotSource.PlayOneShot(normalShot);
    }

    public void PlayJump(bool isAngerMode)
    {
        if(seSource != null)
        {
            AudioClip clipToPlay = isAngerMode ? angerJump : normalJump;
            seSource.PlayOneShot(clipToPlay);
        }
    }

    public void SEGameClear()
    {
        // memo;�Q�[���N���A�ōĐ�����
        seSource.PlayOneShot(gameClear);
    }

    // Player�̈ړ������Đ������ǂ������m�F����v���p�e�B
    public bool IsPlayerRunSoundPlaying
    {
        get { return runSource != null && runSource.isPlaying; }
    }
}
