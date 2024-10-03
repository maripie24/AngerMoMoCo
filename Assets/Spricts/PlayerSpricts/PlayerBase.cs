using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GameObject[] playerBullets; // �C���X�y�N�^�[��Őݒ肷��
    [SerializeField] protected AnimationCurve dashCurve; // �C���X�y�N�^�[��Őݒ肷��
    [SerializeField] protected AnimationCurve jumpCurve; // �C���X�y�N�^�[��Őݒ肷��
    protected Rigidbody2D playerRigidbody2D;
    protected Animator animator;
    protected GroundCheck groundCheck;
    protected GroundCheck headCheck;

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float gravity = 4f;
    [SerializeField] protected float jumpSpeed = 10f;
    [SerializeField] protected float jumpHeight = 1.8f;

    protected float jumpPosition = 0f;
    protected float dashTime;
    protected float jumpTime;
    protected float jumpkeyPressTime;
    // protected float beforeKey; �����E���]�ű�Ұ��ݶ��ނ����Z�b�g���邩�ǂ���

    protected bool isGround = false;
    protected bool isHead = false;
    protected bool isJumping = false;
    protected bool canControl = true;
    public bool isFacingRight = true; // �v���C���[���E�������Ă��邩


    protected virtual void Start()
    {
        // �e��擾����
        playerRigidbody2D = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<GroundCheck>();
        headCheck = GameObject.Find("HeadCheck").GetComponent<GroundCheck>();
    }

    protected virtual void Update()
    {
        Shoothing();
    }

    protected virtual void FixedUpdate()
    {
        if (canControl)
        {
            Movement();
            Jump();
        }
    }

    // ���͂��󂯕t���邩�ǂ���
    protected virtual void SwitchCanControl()
    {
        canControl = !canControl;
    }

    /// <summary>
    /// �v���C���[�O������e�𔭎˂��鏈��
    /// </summary>
    protected virtual void Shoothing()
    {
        // ���N���b�N�Œe�𔭎�(�|�[�Y������Ȃ��Ƃ�)
        if (Input.GetMouseButtonDown(0) && !GameManager.IsPaused)
        {
            Fire();
        }
    }

    // �e�̐ݒ�
    protected virtual void Fire()
    {
        animator.SetTrigger("throw");

        int index = Random.Range(0, playerBullets.Length);
        //  Debug.Log($"�I�����ꂽ�C���f�b�N�X: {index}");

        GameObject bulletToSpawn = playerBullets[index];

        // �v���C���[�̌����ɉ����Ĕ��˓_�����E�ɕς���
        Vector3 spawnOffset = new Vector3(isFacingRight ? 1.5f : -1.5f, 1f, 0f);
        Vector3 spawnPosition = this.transform.position + spawnOffset;

        // spawnPosition����e�𐶐�����
        Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    protected virtual void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // ���\���L�[(A,D)�̎擾
        float xSpeed = 0f;

        // �ڒn���Ă���Ƃ��̂ݍ��E�̈ړ����ł���
        if (isGround)
        {
            // �E�̏ꍇ
            if (hKey > 0)
            {
                // �L�����N�^�[�͉E������(�f�t�H���g�ŉE�������Ă���)
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

                animator.SetBool("Run", true);
                dashTime += Time.deltaTime;
                xSpeed = speed;
                isFacingRight = true;
            }

            // ���̏ꍇ
            else if (hKey < 0)
            {
                // �L�����N�^�[�͍�������(���]������)
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

                animator.SetBool("Run", true);
                dashTime += Time.deltaTime;
                xSpeed = -speed;
                isFacingRight = false;
            }

            // ƭ���ق̏ꍇ
            else
            {
                animator.SetBool("Run", false); // �����Ұ��݂����߂�
                xSpeed = 0f;
                dashTime = 0f;
            }

            // ��Ұ��ݶ��ނ𑬓x�ɓK�p����
            xSpeed *= dashCurve.Evaluate(dashTime);
            playerRigidbody2D.velocity = new Vector2(xSpeed, playerRigidbody2D.velocity.y);
        }
    }

    /// <summary>
    /// �v���C���[�̃W�����v����
    /// </summary>
    protected virtual void Jump()
    {
        isGround = groundCheck.IsGround();
        isHead = headCheck.IsGround();

        float vKey = Input.GetAxis("Vertical"); // �c�\���L�[(W,S)�̎擾
        float ySpeed = -gravity; // �d�͂̔���

        if (isGround)
        {
            if(vKey > 0)
            {
                ySpeed = jumpSpeed;
                // �ެ��ߎ��_�̍������擾
                jumpPosition = this.transform.position.y;
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
            // ��������
            this.transform.localScale = new Vector3 ( 2,2,2);

            bool pushUpkey = vKey > 0;
            // �W�����v�\�ȍ������ǂ���
            bool canJumpHeight = jumpPosition + jumpHeight > this.transform.position.y;

            if(pushUpkey && canJumpHeight && !isHead)
            {
                isJumping = true;
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
                // ��Ұ��ݶ��ނ�ؾ�Ă���
                jumpTime = 0f;
            }
        }

        // ��Ұ��ݶ��ނ𑬓x�ɓK�p
        if (isJumping)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        animator.SetBool("jump", isJumping);
        animator.SetBool("ground", isGround);
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, ySpeed);
    }
}
