using UnityEngine;

public class AngerSwitcher : MonoBehaviour
{
    private PlayerNormal playerNormal;
    private PlayerAnger playerAnger;
    private AngerGauge angerGauge;
    public bool canSwitchToAnger = false;
    void Start()
    {
        playerNormal = GetComponent<PlayerNormal>();
        playerAnger = GetComponent<PlayerAnger>();

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("AngerCanvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();


        // 初期状態を設定
        if (playerNormal != null && playerAnger != null)
        {
            playerNormal.enabled = true;
            playerAnger.enabled = false;
        }
    }

    void Update()
    {
        // デバッグ
        if (canSwitchToAnger && Input.GetKeyDown(KeyCode.E))
        {
            // コンポーネントの有効/無効の切り替え
            if(playerNormal != null && playerAnger != null)
            {
                // 状態の切り替え
                playerNormal.enabled = !playerNormal.enabled;
                playerAnger.enabled = !playerAnger.enabled;

                canSwitchToAnger = false;
                //Debug.Log("falseになりました。");
            }
        }
    }

    public void EnableAngerSwitch()
    {
        canSwitchToAnger = true;
    }
}