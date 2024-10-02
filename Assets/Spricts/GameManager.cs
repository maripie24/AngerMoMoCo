using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // インスペクター上で設定
    private bool isPaused = false; // ゲームがポーズ中かどうかを管理するフラグ
    public static bool IsPaused { get; private set;} // 他のスプリクトから参照するためのプロパティ

    void Awake()
    {
        menuPanel.SetActive(false);

        Time.timeScale = 1f; // jゲーム内時間の初期化
        IsPaused = false;
    }

    void Update()
    {
        // ESCキーが押されたらメニュー画面をONOFFにする
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
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
}

