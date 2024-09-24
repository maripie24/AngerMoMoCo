using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    private SearchAreaManager searchArea;
    [SerializeField] float chasingSpeed = 5;
    [SerializeField] float knockbackForce = 3;
    [SerializeField] int maxHP = 100; 
    private int HP;
    [SerializeField] Slider sliderHP; // インスペクターで設定

    private AngerGauge angerGaugeScript;
    private Animator anim;

    public bool isChasing;
    void Start()
    {
        player = GameObject.Find("Player");
        searchArea = this.transform.GetChild(0).GetComponent<SearchAreaManager>(); // SearchAreaを取得
        anim = this.GetComponent<Animator>();

        // AngerCanvasの下にあるAngerGaugeオブジェクトを検索
        GameObject angerGaugeObject = GameObject.Find("AngerCanvas/AngerGauge");
        angerGaugeScript = angerGaugeObject.GetComponent<AngerGauge>();

        sliderHP.value = maxHP; // Sliderを最大HPに設定する
        HP = maxHP; // HPを最大にする
    }

    void Update()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;

        // プレイヤーを追いかける
        if (isChasing)
        {
            this.transform.position += (Vector3)direction * chasingSpeed * Time.deltaTime;

            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        // プレイヤーの方向に応じて敵の向きを変更する
        if (direction.x > 0)
        {
            // 右向き
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            // 左向き
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    // 弾丸のノックバックとダメージ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ノックバックの実装
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

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
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce * 10, ForceMode2D.Impulse);

            TakeDamage();
        }
    }

    // ダメージの実装
    private void TakeDamage()
    {
        HP -= 20;
        sliderHP.value = (float)HP; // slider側のスプリクトのメソッドで減らす
        if (HP <= 0)
        {
            angerGaugeScript.AddAnger(angerGaugeScript.debugAngerRate); // Angerゲージが貯まる(敵を倒すと)
            Destroy(this.gameObject);
        }
    }

}
