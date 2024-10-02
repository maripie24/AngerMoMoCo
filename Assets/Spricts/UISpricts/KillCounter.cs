using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static KillCounter killCounter;
    private TextMeshProUGUI counterText;

    private int enemyCount = 0;
    public int EnemyCount // �ǂݎ���p�ɂ���v���p�e�B
    {
        get { return enemyCount; }
        private set
        {
            enemyCount = value;
            UpdateCounterUI(); // �l���ς������\�����X�V
        }
    }

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (killCounter == null)
        {
            killCounter = this;
        }
        else // ���ɃC���X�^���X�����݂���ꍇ
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
