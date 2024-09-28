using UnityEngine;

public class AngerSwitcher : MonoBehaviour
{
    private PlayerNormal playerNormal;
    private PlayerAnger playerAnger;
    private AngerGauge angerGauge;
    public bool canSwitchToAnger = false;
    public bool isAngerMode = false;

    void Start()
    {
        playerNormal = GetComponent<PlayerNormal>();
        playerAnger = GetComponent<PlayerAnger>();

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();

        // 初期状態を設定
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
    }

    void Update()
    {
        // デバッグ
        if (canSwitchToAnger && Input.GetKeyDown(KeyCode.E))
        {
            SwitchToAnger();
        }
    }

    public void SwitchToAnger()
    {
        // 状態の切り替え
        playerNormal.enabled = false;
        playerAnger.enabled = true;
        isAngerMode = true;
    }

    public void SwitchToNormal()
    {
        // 状態の切り替え
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
    }

}