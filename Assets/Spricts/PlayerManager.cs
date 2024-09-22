using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerBullet; // �C���X�y�N�^�[��Őݒ肷��
    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    private GroundCheck groundCheck;
    private GroundCheck headCheck;

    [SerializeField] float speed = 1;
    [SerializeField] float gravity = 1;
    [SerializeField] float jumpSpeed = 10; // �W�����v�X�s�[�h
    [SerializeField] float jumpPosition; // �W�����v���n�߂��ʒu
    [SerializeField] float jumpHeight; // �W�����v�ł��鍂�� 
    [SerializeField] AnimationCurve dashCurve;
    [SerializeField] AnimationCurve jumpCurve;
    private float dashTime, jumpTime;
    private float beforeKey;

    private bool isGround = false;
    private bool isHead = false;
    private bool isJumping = false;
    public bool isFacingRight = true;�@// �v���C���[���E�������Ă��܂����H


    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<GroundCheck>();
        headCheck = GameObject.Find("HeadCheck").GetComponent<GroundCheck>();

    }

    void Update()
    {
        Shoothing();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    /// <summary>
    /// �v���C���[�̑O������e�𔭎˂���
    /// </summary>
    private void Shoothing()
    {
         // ���N���b�N�Œe�𔭎�
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("throw");

            Vector3 spawnOffset = new Vector3(isFacingRight ? 1.5f : -1.5f, 0f, 0f); // �v���C���[�̌����ɉ�����
            Vector3 spawnPosition = this.transform.position + spawnOffset;
            Instantiate(playerBullet, spawnPosition, Quaternion.identity);
        }
    }

    private void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // ���̓��͎擾
        float xSpeed = 0f;

        if (hKey > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            animator.SetBool("Run", true);
            dashTime += Time.deltaTime;
            xSpeed = speed;
            isFacingRight = true;
        }

        else if (hKey < 0)
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            animator.SetBool("Run", true);
            dashTime += Time.deltaTime;
            xSpeed = -speed;
            isFacingRight = false;
        }

        else
        {
            animator.SetBool("Run", false);
            xSpeed = 0f;
            dashTime = 0f;
        }

        // �O��̓��͂��甽�]�Ă���΁A���x�����Z�b�g����
        if (hKey > 0 && beforeKey < 0)
        {
            dashTime = 0f;
        }

        else if (hKey < 0 && beforeKey > 0)
        {
            dashTime = 0f;
        }

        // �A�j���[�V�����J�[�u�𑬓x�ɓK�p
        xSpeed *= dashCurve.Evaluate(dashTime);
        playerRigidbody2D.velocity = new Vector2(xSpeed, playerRigidbody2D.velocity.y);
    }

    private void Jump()
    {
        isGround = groundCheck.IsGround();
        isHead = headCheck.IsGround();


        float vKey = Input.GetAxis("Vertical"); // �c�̓��͎擾

        float ySpeed = -gravity; // �����l�͏d�͂̔���
        // rigidbody2D.velocity = new Vector2(xSpeed, ySpeed)

        // �W�����v�̐ݒ�
        if (isGround)
        {
            if (vKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPosition = transform.position.y;

                isJumping = true;
                jumpTime = 0f;
            }
            else
            {
                isJumping = false;
            }
        }
        if (isJumping)
        {
            bool pushUpKey = vKey > 0; // ��L�[����
            bool canJumpHeight = jumpPosition + jumpHeight > transform.position.y; // ��ׂ鍂����艺���ǂ���

            // isHead�͒n�ʂ����ɗ�����True�ƂȂ�B
            if (pushUpKey && canJumpHeight && !isHead)
            {
                ySpeed = jumpSpeed;
                isJumping = true;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
                jumpTime = 0f;
            }
        }


        // �A�j���[�V�����J�[�u�𑬓x�ɓK�p
        if (isJumping)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        animator.SetBool("jump", isJumping);
        animator.SetBool("ground", isGround);
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, ySpeed);
    }
}
