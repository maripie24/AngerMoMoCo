using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AngerSwitcher : MonoBehaviour
{
    private PlayerNormal playerNormal;
    private PlayerAnger playerAnger;
    private AngerGauge angerGauge;
    private Animator animator;
    private PostProcessVolume postProcessVolume;
    public bool canSwitchToAnger = false;
    public bool isAngerMode = false;

    void Start()
    {
        playerNormal = GetComponent<PlayerNormal>();
        playerAnger = GetComponent<PlayerAnger>();
        animator = GetComponent<Animator>();
        postProcessVolume = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();

        // AngerCanvas�̉��ɂ���AngerGauge�I�u�W�F�N�g������
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();

        // ������Ԃ�ݒ�
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = false;
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
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = true;
        AudioManager.Instance.PlayBGM(AudioManager.Instance.angerBGM); // Anger�p��BGM���Đ�
    }

    public void SwitchToNormal()
    {
        // ��Ԃ̐؂�ւ�  
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = false;
        AudioManager.Instance.PlayBGM(AudioManager.Instance.normalBGM); // Normal�p��BGM���Đ�
    }
}