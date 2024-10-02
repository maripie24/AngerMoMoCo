using UnityEngine;
using System.Collections;

public class PlayerAnger : PlayerBase
{
    [SerializeField] private GameObject punchAreaObject; // �C���X�y�N�^�[�Őݒ�
    private BoxCollider2D punchCollider;
    [SerializeField] private AngerGauge angerGauge;
    [SerializeField] private float punchDuration = 0.5f; // �p���`�̗L�����ԁi�b�j
    [SerializeField] private float punchSpeed = 30f; // �p���`���̑O�i���x

    private bool isPunching = false; // �p���`�����ǂ����̃t���O

    protected override void Start()
    {
        base.Start();

        jumpSpeed = 30;
        gravity *= 1.5f;

        punchCollider = punchAreaObject.GetComponent<BoxCollider2D>(); // PunchArea��BoxCollider2D���擾
        punchCollider.enabled = false; // ������Ԃł͖�����

        angerGauge = GameObject.Find("AngerGauge").GetComponent<AngerGauge>();

    }

    protected override void Update()
    {
        if (GameManager.IsPaused){ return;} // �|�[�Y���͈ȉ��̏������X�L�b�v

        if (!isPunching)
        {
            base.Update();

            angerGauge.DecreaseAnger();
        }

        // �p���`���͑��̓��͂𖳌�������
        if (isPunching)
        {
            angerGauge.DecreaseAnger();
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
        if (!isPunching)
        {
            base.Jump();

            // �o�����̃W�����v�͂𑝉�������
            if (isJumping)
            {
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed * 3); // �o������1.5�{�̃W�����v��
            }
        }
    }

    protected override void Shoothing()
    {
        // shooting�͎g��Ȃ��H�H�i�󒆂̂Ƃ��̂ݎg����悤�������j
        HandlePunchInput();
    }

    /// <summary>
    /// �p���`�̓��͂�����
    /// </summary>
    private void HandlePunchInput()
    {
        // ���N���b�N(�p���`���łȂ��|�[�Y���ł��Ȃ��Ƃ��j
        if(Input.GetMouseButtonDown(0) && !isPunching && !GameManager.IsPaused)
        {
            StartCoroutine("PunchRoutine");
        }
    }

    private IEnumerator PunchRoutine()
    {
        Debug.Log("Punch started");
        isPunching = true;

        // Animator�̃p�����[�^'punch'��true�ɂ���
        animator.SetBool("punch", true);

        // �p���`�̃R���C�_�[��L���ɂ���
        punchCollider.enabled = true;
            
        // ��葬�x�ɌŒ肷��
        Vector2 punchDirection = new Vector2 (isFacingRight ? 1f : -1f, 0f);
        playerRigidbody2D.velocity = punchDirection * punchSpeed;

        Debug.Log("Force applied for punch");

        yield return new WaitForSeconds(punchDuration);

        // Animator,�����̺ײ�ް�A���I�t�ɂ���
        animator.SetBool("punch", false); 
        punchCollider.enabled = false;

        isPunching = false;
        Debug.Log("Punch ended");
    }
}
