using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class AngerGauge : MonoBehaviour
{
    [SerializeField] private Image angerImage;
    [SerializeField] private Image maxAngerImage;

    private AngerSwitcher angerSwitcher;

    public float duration = 0.5f;

    public float debugAngerRate = 0.2f;
    public float currentRate = 0f;
    public float angerTime = 20f; // アンガー状態の期間

    public bool isFull = false; // ゲージが満タンになったらtrue
    public bool isZero = false; // ゲージがゼロになったらtrue
    private Tween pulseTween; // Tweenの参照

    private void Start()
    {
        angerSwitcher = GameObject.Find("Player").GetComponent<AngerSwitcher>();    
        SetGauge(0.8f); // ゲージのゼロにセットする

        maxAngerImage.gameObject.SetActive(false);// 初期状態でmaxAngerImageを非アクティブにする
    }

    private void Update()
    {
        Debug.Log($"{currentRate}です"); // ゲージの数値チェック

        if (currentRate >= 1f) // ゲージ満タン
        {
            isFull = true;
            isZero = false;

            SetGauge(1f); // AngerRateを1以上にしないように固定する
            StartPulse();

            // 満タン時にangerImageを非アクティブ、maxAngerImageをアクティブにする
            angerImage.gameObject.SetActive(false);
            maxAngerImage.gameObject.SetActive(true);

            // アンガーモードへの切り替えを可能にする
            angerSwitcher.canSwitchToAnger = true;
        }
        else if (currentRate > 0f && currentRate < 1f) // ゼロから1の間
        {
            isFull = false;
            isZero = false;
            StopPulse();

            // Angerモードへの切り替えをできなくする
            angerSwitcher.canSwitchToAnger = false;

        }
        else // ゲージゼロ
        {
            isFull = false;
            isZero = true;
            StopPulse();

            SetGauge(0f); // ゲージをゼロで固定

            // ゲージがゼロでangerImageをアクティブ、maxAngerImageを非アクティブにする
            angerImage.gameObject.SetActive(true);
            maxAngerImage.gameObject.SetActive(false);

            // Angerモードへの切り替えをできなくする
            angerSwitcher.canSwitchToAnger = false;

            // 自動的にNormal状態に切り替える
            angerSwitcher.SwitchToNormal();
        }

        // デバッグ　完成後は消す
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AddAnger(debugAngerRate);
        }
    }

    private void StartPulse()
    {
        if (pulseTween != null && pulseTween.IsActive()) return;

        // スケールを1.2倍に拡大し、元に戻すループアニメーション
        pulseTween = maxAngerImage.transform.DOScale(1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopPulse()
    {
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill();
            maxAngerImage.transform.localScale = Vector3.one; // スケールを元に戻す
        }
    }

    public void SetGauge(float targetRate)
    {
        angerImage.DOFillAmount(targetRate, duration * 0.5f);
        maxAngerImage.DOFillAmount(targetRate, duration * 0.5f);

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
        currentRate -= 0.0006f;
        SetGauge(currentRate);
    }
}
