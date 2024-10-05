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
    [SerializeField] Slider sliderHP; // �C���X�y�N�^�[�Őݒ�

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
        searchArea = this.transform.GetChild(0).GetComponent<SearchAreaManager>(); // SearchArea���擾

        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        // AngerCanvas�̉��ɂ���AngerGauge�I�u�W�F�N�g������
        GameObject angerGaugeObject = GameObject.Find("Canvas/AngerGauge");
        angerGaugeScript = angerGaugeObject.GetComponent<AngerGauge>();
    }
    void Start()
    {
        sliderHP.value = maxHP; // Slider���ő�HP�ɐݒ肷��
        HP = maxHP; // HP���ő�ɂ���

        isDead = false;
        animator.SetBool("die", isDead);

        attentionSpawnOffset = new Vector3(0.4f, 4f, 0f);
    }

    void Update()
    {
        if (isDead) return; // ���S���͉��������ɏI��

        attentionSpawnPosition = this.transform.position + attentionSpawnOffset;
        Chasing();
        Die();
    }

    public void Chasing()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;

        // �v���C���[��ǂ�������
        if (isChasing)
        {
            this.transform.position += (Vector3)direction * chasingSpeed * Time.deltaTime;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        // �v���C���[�̕����ɉ����ēG�̌�����ύX����
        float scaleX = Mathf.Abs(transform.localScale.x); // ���̃X�P�[����ێ�
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

    // ��ݼ�݂𐶐�����
    public void InstantiateAttention()
    {
        // ��ݼ�݂𐶐�����
        Instantiate(attention, attentionSpawnPosition, Quaternion.identity);
    }


    // �e�ۂ̃m�b�N�o�b�N�ƃ_���[�W   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // �m�b�N�o�b�N�̎���
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            TakeDamage();
        }
        
    }

    // �p���`�Ƃ̏Փ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunchArea"))
        {
            // Debug.Log("�G�����o���܂���");
            // �p���`�G���A����̃m�b�N�o�b�N����
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position);
            rb.AddForce(knockbackDirection * knockbackForce * 10, ForceMode2D.Impulse);

            TakeDamage();
        }
    }

    // �_���[�W�̎���
    private void TakeDamage()
    {
        HP -= 20;
        sliderHP.value = (float)HP; // slider���̃X�v���N�g�̃��\�b�h�Ō��炷
    }

    private void Die()
    {
        if (!isDead && HP <= 0)
        {
            isDead = true; // �����Ńt���O�𗧂Ă�
            Debug.Log("Starting EnemyDie coroutine."); 
            StartCoroutine(EnemyDie());
        }
    }

    private IEnumerator EnemyDie()
    {
        isDead = true; // ���S�t���O�𗧂Ă�
        animator.SetBool("die", isDead);

        GetComponent<Collider2D>().isTrigger = true; // �����蔻��𖳌�������
        rb.gravityScale = 0;

        angerGaugeScript.AddAnger(angerGaugeScript.debugAngerRate); // Anger�Q�[�W�����܂�(�G��|����)
        KillCounter.killCounter.IncrementCount(); // �L���J�E���g�̃C���N�������g

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
