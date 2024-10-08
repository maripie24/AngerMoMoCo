using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // インスペクター上で設定
    [SerializeField] private GameObject gameClearImage; // インスペクター上で設定
    [SerializeField] private int crearKill = 50;

    private bool isPaused = false; // ゲームがポーズ中かどうかを管理するフラグ
    public static bool IsPaused { get; private set;} // 他のスプリクトから参照するためのプロパティ

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.normalBGM); // Normal用のBGMを再生
    }

    private void Awake()
    {
        menuPanel.SetActive(false);
        gameClearImage.SetActive(false);

        Time.timeScale = 1f; // jゲーム内時間の初期化
        IsPaused = false;
    }

    private void Update()
    {
        // ESCキーが押されたらメニュー画面をONOFFにする
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        // 50キルでゲームクリア
        if(KillCounter.killCounter.EnemyCount >= crearKill)
        {
            StartCoroutine(StartGameClearSequence());
        }
    }

    // メニューの表示・非表示を切り替える関数
    public void ToggleMenu()
    {
        isPaused = !isPaused;
        menuPanel.SetActive(isPaused);

        // ゲーム再開、中断の設定
        Time.timeScale = isPaused ? 0f : 1f;

        IsPaused = isPaused; // プロパティIsPausedにも現在の状態を反映する
    }

    // スタート画面に戻る関数
    public void ReturnToStartMenu()
    {
        // ゲームの時間を通常速度に戻す
        Time.timeScale = 1f;
        // シーンのロードをする
        SceneManager.LoadScene("StartScene");
    }

    // ゲームクリア時の処理
    private IEnumerator StartGameClearSequence()
    {
        gameClearImage.SetActive(true);
        Time.timeScale = 0.2f;

        // リアルタイムで3秒待つ（スロー再生なので実際は10倍遅く感じる）
        yield return new WaitForSecondsRealtime(3f);

        ReturnToStartMenu();
    }
}

