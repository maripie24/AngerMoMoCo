using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static KillCounter killCounter;
    private TextMeshProUGUI counterText;

    private int enemyCount = 0;
    public int EnemyCount // 読み取り専用にするプロパティ
    {
        get { return enemyCount; }
        private set
        {
            enemyCount = value;
            UpdateCounterUI(); // 値が変わったら表示を更新
        }
    }

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
        EnemyCount++;

    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = $"{enemyCount}";
        }
        else
        {
            Debug.LogWarning("Counter Text is not assigned in the Inspector.");
        }
    }
}
