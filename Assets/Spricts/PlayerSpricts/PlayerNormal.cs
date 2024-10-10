using UnityEngine;

public class PlayerNormal : PlayerBase
{
    // 前フレームの移動状態を記録
    private bool wasMoving = false;

    protected override void Fire()
    {
        base.Fire();
       
    }

    protected override void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // 横方向の入力を取得

        // プレイヤーが移動中かどうかを判断
        bool isCurrentlyMoving = hKey != 0 && isGround && !isJumping;

        if (isCurrentlyMoving && !wasMoving)
        {
            // 移動を開始した瞬間に移動音を再生
            AudioManager.Instance.PlayRun(false);
        }
        else if (!isCurrentlyMoving && wasMoving)
        {
            // 移動を停止した瞬間に移動音を停止
            AudioManager.Instance.StopRun();
        }

        // ジャンプ中の場合、移動音を停止
        if (isJumping && AudioManager.Instance.IsPlayerRunSoundPlaying)
        {
            AudioManager.Instance.StopRun();
        }

        // 現在の移動状態を記録
        wasMoving = isCurrentlyMoving;

        // 親クラスの Movement メソッドを呼び出す
        base.Movement();
    }
    protected override void Jump()
    {
        bool wasJumping = false;
        wasJumping = isJumping; // ジャンプ状態の前フレームの状態を記録

        // ベースクラスのJumpメソッドを呼び出す
        base.Jump();

        // 前のフレームでジャンプしていなくて、今ジャンプしている場合
        if (!wasJumping && isJumping)
        {
            // ジャンプ音を再生
            AudioManager.Instance.PlayJump(false);
        }
    }
}

