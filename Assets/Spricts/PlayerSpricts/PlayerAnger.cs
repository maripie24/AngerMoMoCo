using UnityEngine;
using System.Collections;

public class PlayerAnger : PlayerBase
{
    [SerializeField] private GameObject punchAreaObject; // インスペクターで設定
    private BoxCollider2D punchCollider;
    [SerializeField] private AngerGauge angerGauge;
    [SerializeField] private float punchDuration = 0.5f; // パンチの有効時間（秒）
    [SerializeField] private float punchSpeed = 30f; // パンチ時の前進速度

    private bool isPunching = false; // パンチ中かどうかのフラグ

    protected override void Start()
    {
        base.Start();

        jumpSpeed = 30;
        gravity *= 1.5f;

        punchCollider = punchAreaObject.GetComponent<BoxCollider2D>(); // PunchAreaのBoxCollider2Dを取得
        punchCollider.enabled = false; // 初期状態では無効化

        angerGauge = GameObject.Find("AngerGauge").GetComponent<AngerGauge>();

    }

    protected override void Update()
    {
        if (GameManager.IsPaused){ return;} // ポーズ中は以下の処理をスキップ

        if (!isPunching)
        {
            base.Update();

            angerGauge.DecreaseAnger();
        }

        // パンチ中は他の入力を無効化する
        if (isPunching)
        {
            angerGauge.DecreaseAnger();
            // 無効化処理
        }
    }

    protected override void FixedUpdate()
    {
        if (!isPunching)
        {
            base.FixedUpdate();
        }
        else
        {
            // パンチ中は通常の移動を制限する場合はここに記述
            // パンチ中は移動速度を固定したい
        }
    }
    protected override void Jump()
    {
        if (!isPunching)
        {
            base.Jump();

            // 覚醒時のジャンプ力を増加させる
            if (isJumping)
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed * 3); // 覚醒時は1.5倍のジャンプ力
            }
        }
    }

    protected override void Shoothing()
    {
        // shootingは使わない？？（空中のときのみ使えるようしたい）
        HandlePunchInput();
    }

    /// <summary>
    /// パンチの入力を処理
    /// </summary>
    private void HandlePunchInput()
    {
        // 左クリック(パンチ中でなくポーズ中でもないとき）
        if(Input.GetMouseButtonDown(0) && !isPunching && !GameManager.IsPaused)
        {
            StartCoroutine("PunchRoutine");
        }
    }

    private IEnumerator PunchRoutine()
    {
        Debug.Log("Punch started");
        isPunching = true;

        // Animatorのパラメータ'punch'をtrueにする
        animator.SetBool("punch", true);

        // パンチのコライダーを有効にする
        punchCollider.enabled = true;
            
        // 一定速度に固定する
        Vector2 punchDirection = new Vector2 (isFacingRight ? 1f : -1f, 0f);
        playerRigidbody2D.velocity = punchDirection * punchSpeed;

        Debug.Log("Force applied for punch");

        yield return new WaitForSeconds(punchDuration);

        // Animator,ﾊﾟﾝﾁのｺﾗｲﾀﾞｰ、をオフにする
        animator.SetBool("punch", false); 
        punchCollider.enabled = false;

        isPunching = false;
        Debug.Log("Punch ended");
    }
}
