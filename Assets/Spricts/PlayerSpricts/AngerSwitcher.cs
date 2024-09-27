using UnityEngine;

public class AngerSwitcher : MonoBehaviour
{
    private PlayerNormal playerNormal;
    private PlayerAnger playerAnger;
    private AngerGauge angerGauge;
    public bool canSwitchToAnger = false;
    private bool transitionToNomal = false;

    void Start()
    {
        playerNormal = GetComponent<PlayerNormal>();
        playerAnger = GetComponent<PlayerAnger>();

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("AngerCanvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();

        // 初期状態を設定
        playerNormal.enabled = true;
        playerAnger.enabled = false;
    }

    void Update()
    {
        // デバッグ
        if (canSwitchToAnger && Input.GetKeyDown(KeyCode.E))
        {
            SwitchToAnger();
        }

        if (transitionToNomal)
        {
            SwitchToNormal();
        }
    }

    public void SwitchToAnger()
    {
        // 状態の切り替え
        playerNormal.enabled = false;
        playerAnger.enabled = true;
    }

    public void SwitchToNormal()
    {
        // 状態の切り替え
        playerNormal.enabled = true;
        playerAnger.enabled = false;
    }

    public void EnableNormalSwitch()
    {
        transitionToNomal = angerGauge.isZero;
    }

    public void EnableAngerSwitch()
    {
        canSwitchToAnger = angerGauge.isFull;
    }
}