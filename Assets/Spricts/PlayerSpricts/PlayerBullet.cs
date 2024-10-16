using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    // [SerializeField] int bulletCount = 0;
    [SerializeField] float lifeTime = 5f;
    private bool isFacingRight = true;

    private Transform playerTransform;
    private Rigidbody2D bulletRigidbody2D;
    // private PlayerBase playerBase;
    void Start()
    {

        bulletRigidbody2D = this.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        // playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();

        if (playerTransform.localScale.x >= 0)
        {
            isFacingRight = true;
        }
        else { isFacingRight = false; }

        BulletDirection(isFacingRight);
        AddRotatetion(isFacingRight);

        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            // Destroy(collision.gameObject);
        }
    }

    private void BulletDirection(bool isFacingRight)
    {
        float playerDirection = isFacingRight ? 1f : -1f; // プレイヤーの向きとたまの発射向きを合わせる
        Vector2 shootDirection = new Vector2(playerDirection, 0.1f).normalized; // 弾の初速向き
        bulletRigidbody2D.velocity = shootDirection * bulletSpeed; // 弾の初速速度をかける
    }

    private void AddRotatetion(bool isFacingRight)
    {
        // プレイヤーの方向に応じて回転の向きを変更する
        float rotateDirection = isFacingRight ? 1f : -1f;
        Vector2 torque = new Vector2(0, rotateDirection).normalized;
        bulletRigidbody2D.AddTorque(torque.y * 2, ForceMode2D.Impulse);
    }
}
