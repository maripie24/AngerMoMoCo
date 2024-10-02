using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static KillCounter killCounter;

    private int enemyCount = 0;
    private TextMeshProUGUI counterText;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (killCounter == null)
        {
            killCounter = this;
        }
        else // 既にインスタンスが存在する場合
        {
            Destroy(killCounter);
        }
    }

    private void Start()
    {
        counterText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        UpdateCounterUI();
    }

    public void IncrementCount()
    {
        enemyCount++;
        UpdateCounterUI();

    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = $"{enemyCount}";
        }
        else
        {
            Debug.LogWarning("textがアタッチされてないかも");
        }
    }
}
