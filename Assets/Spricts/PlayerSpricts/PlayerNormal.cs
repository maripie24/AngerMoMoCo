using UnityEngine;

public class PlayerNormal : PlayerBase
{
    // �O�t���[���̈ړ���Ԃ��L�^
    private bool wasMoving = false;

    protected override void Fire()
    {
        base.Fire();
       
    }

    protected override void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // �������̓��͂��擾

        // �v���C���[���ړ������ǂ����𔻒f
        bool isCurrentlyMoving = hKey != 0 && isGround && !isJumping;

        if (isCurrentlyMoving && !wasMoving)
        {
            // �ړ����J�n�����u�ԂɈړ������Đ�
            AudioManager.Instance.PlayRun(false);
        }
        else if (!isCurrentlyMoving && wasMoving)
        {
            // �ړ����~�����u�ԂɈړ������~
            AudioManager.Instance.StopRun();
        }

        // �W�����v���̏ꍇ�A�ړ������~
        if (isJumping && AudioManager.Instance.IsPlayerRunSoundPlaying)
        {
            AudioManager.Instance.StopRun();
        }

        // ���݂̈ړ���Ԃ��L�^
        wasMoving = isCurrentlyMoving;

        // �e�N���X�� Movement ���\�b�h���Ăяo��
        base.Movement();
    }
    protected override void Jump()
    {
        bool wasJumping = false;
        wasJumping = isJumping; // �W�����v��Ԃ̑O�t���[���̏�Ԃ��L�^

        // �x�[�X�N���X��Jump���\�b�h���Ăяo��
        base.Jump();

        // �O�̃t���[���ŃW�����v���Ă��Ȃ��āA���W�����v���Ă���ꍇ
        if (!wasJumping && isJumping)
        {
            // �W�����v�����Đ�
            AudioManager.Instance.PlayJump(false);
        }
    }
}

