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
            Debug.LogWarning("text���A�^�b�`����ĂȂ�����");
        }
    }
}
