using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GameObject[] playerBullets; // インスペクター上で設定する
    [SerializeField] protected AnimationCurve dashCurve; // インスペクター上で設定する
    [SerializeField] protected AnimationCurve jumpCurve; // インスペクター上で設定する
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
    // protected float beforeKey; ←左右反転でｱﾆﾒｰｼｮﾝｶｰﾌﾞをリセットするかどうか

    protected bool isGround = false;
    protected bool isHead = false;
    protected bool isJumping = false;
    protected bool canControl = true;
    public bool isFacingRight = true; // プレイヤーが右を向いているか


    protected virtual void Start()
    {
        // 各種取得する
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

    // 入力を受け付けるかどうか
    protected virtual void SwitchCanControl()
    {
        canControl = !canControl;
    }

    /// <summary>
    /// プレイヤー前方から弾を発射する処理
    /// </summary>
    protected virtual void Shoothing()
    {
        // 左クリックで弾を発射(ポーズ中じゃないとき)
        if (Input.GetMouseButtonDown(0) && !GameManager.IsPaused)
        {
            Fire();
        }
    }

    // 弾の設定
    protected virtual void Fire()
    {
        animator.SetTrigger("throw");

        int index = Random.Range(0, playerBullets.Length);
        //  Debug.Log($"選択されたインデックス: {index}");

        GameObject bulletToSpawn = playerBullets[index];

        // プレイヤーの向きに応じて発射点を左右に変える
        Vector3 spawnOffset = new Vector3(isFacingRight ? 1.5f : -1.5f, 1f, 0f);
        Vector3 spawnPosition = this.transform.position + spawnOffset;

        // spawnPositionから弾を生成する
        Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    protected virtual void Movement()
    {
        float hKey = Input.GetAxis("Horizontal"); // 横十字キー(A,D)の取得
        float xSpeed = 0f;

        // 接地しているときのみ左右の移動ができる
        if (isGround)
        {
            // 右の場合
            if (hKey > 0)
            {
                // キャラクターは右を向く(デフォルトで右を向いている)
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

                animator.SetBool("Run", true);
                dashTime += Time.deltaTime;
                xSpeed = speed;
                isFacingRight = true;
            }

            // 左の場合
            else if (hKey < 0)
            {
                // キャラクターは左を向く(反転させる)
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

                animator.SetBool("Run", true);
                dashTime += Time.deltaTime;
                xSpeed = -speed;
                isFacingRight = false;
            }

            // ﾆｭｰﾄﾗﾙの場合
            else
            {
                animator.SetBool("Run", false); // 走りｱﾆﾒｰｼｮﾝを辞める
                xSpeed = 0f;
                dashTime = 0f;
            }

            // ｱﾆﾒｰｼｮﾝｶｰﾌﾞを速度に適用する
            xSpeed *= dashCurve.Evaluate(dashTime);
            playerRigidbody2D.velocity = new Vector2(xSpeed, playerRigidbody2D.velocity.y);
        }
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    protected virtual void Jump()
    {
        isGround = groundCheck.IsGround();
        isHead = headCheck.IsGround();

        float vKey = Input.GetAxis("Vertical"); // 縦十字キー(W,S)の取得
        float ySpeed = -gravity; // 重力の反対

        if (isGround)
        {
            if(vKey > 0)
            {
                ySpeed = jumpSpeed;
                // ｼﾞｬﾝﾌﾟ時点の高さを取得
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
            // すぐ消す
            this.transform.localScale = new Vector3 ( 2,2,2);

            bool pushUpkey = vKey > 0;
            // ジャンプ可能な高さかどうか
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
                // ｱﾆﾒｰｼｮﾝｶｰﾌﾞをﾘｾｯﾄする
                jumpTime = 0f;
            }
        }

        // ｱﾆﾒｰｼｮﾝｶｰﾌﾞを速度に適用
        if (isJumping)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        animator.SetBool("jump", isJumping);
        animator.SetBool("ground", isGround);
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, ySpeed);
    }
}
