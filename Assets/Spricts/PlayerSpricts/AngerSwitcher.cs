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

        // AngerCanvas�̉��ɂ���AngerGauge�I�u�W�F�N�g������
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();

        // ������Ԃ�ݒ�
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
    }

    void Update()
    {
        // �f�o�b�O
        if (canSwitchToAnger && Input.GetKeyDown(KeyCode.E))
        {
            SwitchToAnger();
        }
    }

    public void SwitchToAnger()
    {
        // ��Ԃ̐؂�ւ�
        playerNormal.enabled = false;
        playerAnger.enabled = true;
        isAngerMode = true;
    }

    public void SwitchToNormal()
    {
        // ��Ԃ̐؂�ւ�
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
    }

}