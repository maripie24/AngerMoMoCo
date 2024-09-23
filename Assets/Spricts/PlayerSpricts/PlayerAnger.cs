using UnityEngine;

public class PlayerAnger : PlayerBase
{
    [SerializeField] private GameObject punchAreaObject; // �C���X�y�N�^�[�Őݒ�
    private BoxCollider2D punchCollider;
    [SerializeField] private float punchDuration = 0.5f; // �p���`�̗L�����ԁi�b�j
    [SerializeField] private float punchSpeed = 10f; // �p���`���̑O�i���x

    private bool isPunching = false; // �p���`�����ǂ����̃t���O

    protected override void Start()
    {
        base.Start();

        jumpSpeed = 30;
        gravity *= 1.5f;

        punchCollider = punchAreaObject.GetComponent<BoxCollider2D>(); // PunchArea��BoxCollider2D���擾
        punchCollider.enabled = false; // ������Ԃł͖�����
    }

    protected override void Update()
    {
        base.Update();

        // �p���`���͑��̓��͂𖳌�������
        if (isPunching)
        {
            // ����������
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
            // �p���`���͒ʏ�̈ړ��𐧌�����ꍇ�͂����ɋL�q
            // �p���`���͈ړ����x���Œ肵����
        }
    }
    protected override void Jump()
    {
        base.Jump();

        // �o�����̃W�����v�͂𑝉�������
        if (isJumping)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed * 3); // �o������1.5�{�̃W�����v��
        }
    }

    protected override void Shoothing()
    {
        // ���N���b�N�i�}�E�X�{�^��0�j��������Ă����
        if (Input.GetMouseButton(0))
        {
            // Animator��'punch'�p�����[�^��true�ɐݒ�
            animator.SetBool("punch", true);

            // PunchCollider��L����
            if (punchCollider != null)
            {
                punchCollider.enabled = true;
            }
        }
        else
        {
            // ���N���b�N�������ꂽ��'punch'��false�ɐݒ�
            animator.SetBool("punch", false);

            // PunchCollider�𖳌���
            if (punchCollider != null)
            {
                punchCollider.enabled = false;
            }
        }
    }
}
