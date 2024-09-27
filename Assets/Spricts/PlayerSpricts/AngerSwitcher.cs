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

        // AngerCanvas�̉��ɂ���AngerGauge�I�u�W�F�N�g������
        GameObject angerGaugeObject = GameObject.Find("AngerCanvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();


        // ������Ԃ�ݒ�
        if (playerNormal != null && playerAnger != null)
        {
            playerNormal.enabled = true;
            playerAnger.enabled = false;
        }
    }

    void Update()
    {
        // �f�o�b�O
        if (canSwitchToAnger && Input.GetKeyDown(KeyCode.E))
        {
            // �R���|�[�l���g�̗L��/�����̐؂�ւ�
            if(playerNormal != null && playerAnger != null)
            {
                // ��Ԃ̐؂�ւ�
                playerNormal.enabled = !playerNormal.enabled;
                playerAnger.enabled = !playerAnger.enabled;

                canSwitchToAnger = false;
                //Debug.Log("false�ɂȂ�܂����B");
            }
        }
    }

    public void EnableAngerSwitch()
    {
        canSwitchToAnger = true;
    }
}