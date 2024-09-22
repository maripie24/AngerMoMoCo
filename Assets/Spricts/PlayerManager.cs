using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerBullet; // インスペクター上で設定する
    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    private GroundCheck groundCheck;
    private GroundCheck headCheck;

    [SerializeField] float speed = 1;
    [SerializeField] float gravity = 1;
    [SerializeField] float jumpSpeed = 10; // ジャンプスピード
    [SerializeField] float jumpPosition; // ジャンプを始めた位置
    [SerializeField] float jumpHeight; // ジャンプできる高さ 
    [SerializeField] AnimationCurve dashCurve;
    [SerializeField] AnimationCurve jumpCurve;
    private float dashTime, jumpTime;
    private float beforeKey;

    private bool isGround = false;
    private bool isHead = false;
    private bool isJumping = false;
    public bool isFacingRight = true;　// プレイヤーが右を向いていますか？


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
    /// プレイヤーの前方から弾を発射する
    /// </summary>
    private void Shoothing()
    {
         // 左クリックで弾を発射
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("throw");

            Vector3 spawnOffset = new Vector3(isFacingRight ? 1.5f : -1.5f, 0f, 0f); // プレイヤーの向きに応じて
            Vector3 spawnPosition = this.transform.position + spawnOffset;
            Instantiate(playerBullet, spawnPosition, Quaternion.identity);
        }
    }

    private void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // 横の入力取得
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

        // 前回の入力から反転ていれば、速度をリセットする
        if (hKey > 0 && beforeKey < 0)
        {
            dashTime = 0f;
        }

        else if (hKey < 0 && beforeKey > 0)
        {
            dashTime = 0f;
        }

        // アニメーションカーブを速度に適用
        xSpeed *= dashCurve.Evaluate(dashTime);
        playerRigidbody2D.velocity = new Vector2(xSpeed, playerRigidbody2D.velocity.y);
    }

    private void Jump()
    {
        isGround = groundCheck.IsGround();
        isHead = headCheck.IsGround();


        float vKey = Input.GetAxis("Vertical"); // 縦の入力取得

        float ySpeed = -gravity; // 初期値は重力の反対
        // rigidbody2D.velocity = new Vector2(xSpeed, ySpeed)

        // ジャンプの設定
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
            bool pushUpKey = vKey > 0; // 上キー入力
            bool canJumpHeight = jumpPosition + jumpHeight > transform.position.y; // 飛べる高さより下かどうか

            // isHeadは地面が頭に来たらTrueとなる。
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


        // アニメーションカーブを速度に適用
        if (isJumping)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        animator.SetBool("jump", isJumping);
        animator.SetBool("ground", isGround);
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, ySpeed);
    }
}
