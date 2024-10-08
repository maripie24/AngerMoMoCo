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
    [SerializeField] private AudioSource runSource; // Player移動音用
    private AudioSource enemyRunSource; // Enemy移動用

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
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄
        }

        enemyRunSource = enemy.GetComponent<AudioSource>(); // Enemyプレハブからｵｰﾃﾞｨｵｿｰｽを取得
    }

    private void Start()
    {
        // スライダーの初期値を設定
    }

    // BGMを再生
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmSource.clip = normalBGM;

        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        bgmSource.loop = loop; // BGMなのでループ
        bgmSource.Play();
    }

    // BGMを停止 memo;ゲームクリアで停止したい
    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // 移動音を再生
    public void PlayNormalRun()
    {
        if (runSource != null)
        {
            if (!runSource.isPlaying)
            {
                runSource.clip = normalRun;
                runSource.loop = true;      // ループ再生
                runSource.Play();
            }
        }
    }

    // 移動音を停止
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
        // memo;ゲームクリアで再生する
        seSource.PlayOneShot(gameClear);
    }

    // Playerの移動音が再生中かどうかを確認するプロパティ
    public bool IsPlayerRunSoundPlaying
    {
        get { return runSource != null && runSource.isPlaying; }
    }

    // Enemyの移動音が再生中かどうかを確認するプロパティ
    public bool IsEnemyRunSoundPlaying
    {
        get { return enemyRunSource != null && enemyRunSource.isPlaying; }
    }
}
