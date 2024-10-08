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

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGauge = angerGaugeObject.GetComponent<AngerGauge>();

        // 初期状態を設定
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = false;
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
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = true;
        AudioManager.Instance.PlayBGM(AudioManager.Instance.angerBGM); // Anger用のBGMを再生
    }

    public void SwitchToNormal()
    {
        // 状態の切り替え  
        playerNormal.enabled = true;
        playerAnger.enabled = false;
        isAngerMode = false;
        animator.SetBool("isAnger", isAngerMode);
        postProcessVolume.enabled = false;
        AudioManager.Instance.PlayBGM(AudioManager.Instance.normalBGM); // Normal用のBGMを再生
    }
}