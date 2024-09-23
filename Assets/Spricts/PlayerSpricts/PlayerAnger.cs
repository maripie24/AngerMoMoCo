using UnityEngine;

public class PlayerAnger : PlayerBase
{
    [SerializeField] private GameObject punchAreaObject; // インスペクターで設定
    private BoxCollider2D punchCollider;
    [SerializeField] private float punchDuration = 0.5f; // パンチの有効時間（秒）
    [SerializeField] private float punchSpeed = 10f; // パンチ時の前進速度

    private bool isPunching = false; // パンチ中かどうかのフラグ

    protected override void Start()
    {
        base.Start();

        jumpSpeed = 30;
        gravity *= 1.5f;

        punchCollider = punchAreaObject.GetComponent<BoxCollider2D>(); // PunchAreaのBoxCollider2Dを取得
        punchCollider.enabled = false; // 初期状態では無効化
    }

    protected override void Update()
    {
        base.Update();

        // パンチ中は他の入力を無効化する
        if (isPunching)
        {
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
        base.Jump();

        // 覚醒時のジャンプ力を増加させる
        if (isJumping)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed * 3); // 覚醒時は1.5倍のジャンプ力
        }
    }

    protected override void Shoothing()
    {
        // 左クリック（マウスボタン0）が押されている間
        if (Input.GetMouseButton(0))
        {
            // Animatorに'punch'パラメータをtrueに設定
            animator.SetBool("punch", true);

            // PunchColliderを有効化
            if (punchCollider != null)
            {
                punchCollider.enabled = true;
            }
        }
        else
        {
            // 左クリックが離されたら'punch'をfalseに設定
            animator.SetBool("punch", false);

            // PunchColliderを無効化
            if (punchCollider != null)
            {
                punchCollider.enabled = false;
            }
        }
    }
}
