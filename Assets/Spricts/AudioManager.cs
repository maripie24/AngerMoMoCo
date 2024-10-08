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
    private AudioSource enemyRunSource; // Enemy�ړ��p

    [Header("SE Player Audio Clip")]
    [SerializeField] private AudioClip normalRun;
    [SerializeField] private AudioClip normalShot;
    [SerializeField] private AudioClip normalJump;

    [Header("SE others")]
    [SerializeField] private AudioClip enemyRun;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip gameClear;

    [Header("BGM")]
    [SerializeField] private AudioClip normalBGM;


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

        enemyRunSource = enemy.GetComponent<AudioSource>(); // Enemy�v���n�u���絰�ި�������擾
    }

    private void Start()
    {
        // �X���C�_�[�̏����l��ݒ�
    }

    // BGM���Đ�
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmSource.clip = normalBGM;

        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
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
    public void PlayNormalRun()
    {
        if (runSource != null)
        {
            if (!runSource.isPlaying)
            {
                runSource.clip = normalRun;
                runSource.loop = true;      // ���[�v�Đ�
                runSource.Play();
            }
        }
    }

    // �ړ������~
    public void StopNormalRun()
    {
        if (runSource != null && runSource.isPlaying)
        {
            runSource.Stop();
        }
    }

    public void SENormalShot()
    {
        shotSource.PlayOneShot(normalShot);
    }

    public void SENormalJump()
    {
        seSource.PlayOneShot(normalJump);
    }

    public void PlayEnemyRun()
    {
        if (enemyRunSource != null && enemyRun != null)
        {
            enemyRunSource.clip = enemyRun;
            enemyRunSource.loop = true;
            enemyRunSource.Play();
        }
    }

    public void StopEnemyRun()
    {
        if (enemyRunSource != null && enemyRunSource.isPlaying)
        {
            enemyRunSource.Stop();
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

    // Enemy�̈ړ������Đ������ǂ������m�F����v���p�e�B
    public bool IsEnemyRunSoundPlaying
    {
        get { return enemyRunSource != null && enemyRunSource.isPlaying; }
    }
}
