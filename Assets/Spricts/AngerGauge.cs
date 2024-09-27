using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AngerGauge : MonoBehaviour
{
    [SerializeField] private Image angerImage;

    private AngerSwitcher angerSwitcher;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    public float debugAngerRate = 0.2f;
    public float currentRate = 0f;
    public float angerTime = 20f; // アンガー状態の期間

    public bool isFull = false; // ゲージが満タンになったらtrue
    public bool isZero = false; // ゲージがゼロになったらtrue
    private bool isPulsing = false; // パルス効果のフラグ
    private Tween pulseTween; // Tweenの参照

    private void Start()
    {
        angerSwitcher = GameObject.Find("Player").GetComponent<AngerSwitcher>();    
        SetGauge(1f); // ゲージのゼロにセットする
    }

    private void Update()
    {
        Debug.Log($"{currentRate}です"); // ゲージの数値チェック

        if (currentRate >= 1f) // ゲージ満タン
        {
            isFull = true;
            isZero = false;

            SetGauge(1f); // AngerRateを1以上にしないように固定する

            // ノーマル、アンガー状態のトリガーを外す
            angerSwitcher.EnableAngerSwitch();
            angerSwitcher.EnableNormalSwitch();

            StartPulsing(); // パルス効果を開始

            // 既存のShake処理を維持
            transform.DOShakePosition(duration * 0.5f, strength, vibrate);
        }
        else if (currentRate > 0f)
        {
            isFull = false;
            isZero = false;
            StopPulsing(); // パルス効果を停止
        }
        else // ゲージゼロ
        {
            isFull = false;
            isZero = true;
            StopPulsing(); // パルス効果を停止

            SetGauge(0f); // ゲージをゼロで固定
            // ゲージがゼロになったらNormal状態に切り替える
            angerSwitcher.SwitchToNormal();
        }

        // デバッグ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddAnger(debugAngerRate);
        }
    }

    /// <summary>
    /// パルス効果を開始するメソッド
    /// </summary>
    private void StartPulsing()
    {
        isPulsing = true;
        // **追加: パルス効果のTweenを設定**
        pulseTween = transform.DOScale(1.1f, 0.5f) // 拡大
            .SetLoops(-1, LoopType.Yoyo) // 繰り返し
            .SetEase(Ease.InOutSine);
    }

    /// <summary>
    /// パルス効果を停止するメソッド
    /// </summary>
    private void StopPulsing()
    {
        isPulsing = false;
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill(); // Tweenを停止
        }

        // **追加: ゲージのスケールを元に戻す**
        transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
    }

    public void SetGauge(float targetRate)
    {
        angerImage.DOFillAmount(targetRate, duration * 0.5f);

        currentRate = targetRate;
    }

    // ゲージを増やす
    public void AddAnger(float rate)
    {
        SetGauge(currentRate + rate);
    }

    // ゲージを減らす
    public void DecreaseAnger()
    {
        // ゲージ数値を減らす
        currentRate -= 0.001f;
        SetGauge(currentRate);
    }
}
