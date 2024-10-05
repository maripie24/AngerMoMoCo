using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    private SearchAreaManager searchArea;
    [SerializeField] float chasingSpeed = 5;
    [SerializeField] float knockbackForce = 3;
    [SerializeField] int maxHP = 100; 
    private int HP;
    [SerializeField] Slider sliderHP; // インスペクターで設定

    [SerializeField] GameObject attention;
    private Vector3 attentionSpawnOffset = new Vector3(0.4f, 4f, 0f);
    public Vector3 attentionSpawnPosition;


    private AngerGauge angerGaugeScript;
    private Rigidbody2D rb;
    private Animator animator;

    public bool isChasing;
    private bool isDead;
    private void Awake()
    {
        player = GameObject.Find("Player");
        searchArea = this.transform.GetChild(0).GetComponent<SearchAreaManager>(); // SearchAreaを取得

        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGaugeScript = angerGaugeObject.GetComponent<AngerGauge>();
    }
    void Start()
    {
        sliderHP.value = maxHP; // Sliderを最大HPに設定する
        HP = maxHP; // HPを最大にする

        isDead = false;
        animator.SetBool("die", isDead);

        attentionSpawnOffset = new Vector3(0.4f, 4f, 0f);
    }

    void Update()
    {
        if (isDead) return; // 死亡時は何もせずに終了

        attentionSpawnPosition = this.transform.position + attentionSpawnOffset;
        Chasing();
        Die();
    }

    public void Chasing()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;

        // プレイヤーを追いかける
        if (isChasing)
        {
            this.transform.position += (Vector3)direction * chasingSpeed * Time.deltaTime;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        // プレイヤーの方向に応じて敵の向きを変更する
        float scaleX = Mathf.Abs(transform.localScale.x); // 元のスケールを保持
        transform.localScale = new Vector3(direction.x >= 0 ? -scaleX : scaleX, transform.localScale.y, transform.localScale.z);
    }

    public void StartChase()
    {
        isChasing = true;
    }
    public void StopChase()
    {
        isChasing = false;
    }

    // ｱﾃﾝｼｮﾝを生成する
    public void InstantiateAttention()
    {
        // ｱﾃﾝｼｮﾝを生成する
        Instantiate(attention, attentionSpawnPosition, Quaternion.identity);
    }


    // 弾丸のノックバックとダメージ   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ノックバックの実装
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            TakeDamage();
        }
        
    }

    // パンチとの衝突
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunchArea"))
        {
            // Debug.Log("敵を検出しました");
            // パンチエリアからのノックバック方向
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position);
            rb.AddForce(knockbackDirection * knockbackForce * 10, ForceMode2D.Impulse);

            TakeDamage();
        }
    }

    // ダメージの実装
    private void TakeDamage()
    {
        HP -= 20;
        sliderHP.value = (float)HP; // slider側のスプリクトのメソッドで減らす
    }

    private void Die()
    {
        if (!isDead && HP <= 0)
        {
            isDead = true; // ここでフラグを立てる
            Debug.Log("Starting EnemyDie coroutine."); 
            StartCoroutine(EnemyDie());
        }
    }

    private IEnumerator EnemyDie()
    {
        isDead = true; // 死亡フラグを立てる
        animator.SetBool("die", isDead);

        GetComponent<Collider2D>().isTrigger = true; // 当たり判定を無効化する
        rb.gravityScale = 0;

        angerGaugeScript.AddAnger(angerGaugeScript.debugAngerRate); // Angerゲージが貯まる(敵を倒すと)
        KillCounter.killCounter.IncrementCount(); // キルカウントのインクリメント

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
