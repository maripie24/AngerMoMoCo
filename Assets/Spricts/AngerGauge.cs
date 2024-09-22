using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AngerGauge : MonoBehaviour
{
    [SerializeField] private Image angerImage;
    [SerializeField] private Image burnImage;

    private AngerSwitcher angerSwitcher;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    public float debugAngerRate = 0.2f;
    public float currentRate = 0f;
    public float angerTime = 20f; // アンガー状態の期間

    private bool isPulsing = false; // パルス効果のフラグ
    private Tween pulseTween; // Tweenの参照

    private void Start()
    {
        angerSwitcher = GameObject.Find("Player").GetComponent<AngerSwitcher>();    
        SetGauge(1f); // ゲージのゼロにセットする
    }

    private void Update()
    {
        if (currentRate >= 1f)
        {
            // アンガー状態のトリガーを外す
            angerSwitcher.EnableAngerSwitch();

            if (!isPulsing)
            {
                StartPulsing(); // パルス効果を開始
            }

            // 既存のShake処理を維持
            transform.DOShakePosition(duration * 0.5f, strength, vibrate);


        }
        else
        {
            if (isPulsing)
            {
                StopPulsing(); // パルス効果を停止
            }
        }

        // デバッグ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddAnger(debugAngerRate);
        }
    }

    /// <summary>
    /// ゲージの色を設定するためのメソッド
    /// </summary>
    private void SetGaugeColor(bool isFull)
    {
        if (isFull)
        {
            // **追加: 満タン時の色に変更**
            angerImage.color = Color.red; // 例: 赤色に変更
            burnImage.color = Color.red;
        }
        else
        {
            // **追加: 通常時の色に戻す**
            angerImage.color = Color.blue; // 例: 青色に変更
            burnImage.color = Color.blue;
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

        // **追加: ゲージの色を変更**
        SetGaugeColor(true);
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
        // **追加: ゲージの色を通常時に戻す**
        SetGaugeColor(false);

        // **追加: ゲージのスケールを元に戻す**
        transform.localScale = Vector3.one;
    }

    public void SetGauge(float targetRate)
    {
        burnImage.DOFillAmount(targetRate, duration).OnComplete(() =>
        {
            angerImage.DOFillAmount(targetRate, duration * 0.5f).SetDelay(0.1f);
        });

        currentRate = targetRate;
    }

    // ゲージを増やす
    public void AddAnger(float rate)
    {
        SetGauge(currentRate + rate);
    }

    // ゲージを減らす
    public void DecreaseAnger(float rate)
    {
        burnImage.DOFillAmount(0f, angerTime);
        angerImage.DOFillAmount(0f, angerTime).SetDelay(0.1f);

        // アンガーゲージの初期化
        currentRate = 0f;
    }
}
