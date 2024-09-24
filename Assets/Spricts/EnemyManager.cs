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
    [SerializeField] Slider sliderHP; // �C���X�y�N�^�[�Őݒ�

    private AngerGauge angerGaugeScript;
    private Animator anim;

    public bool isChasing;
    void Start()
    {
        player = GameObject.Find("Player");
        searchArea = this.transform.GetChild(0).GetComponent<SearchAreaManager>(); // SearchArea���擾
        anim = this.GetComponent<Animator>();

        // AngerCanvas�̉��ɂ���AngerGauge�I�u�W�F�N�g������
        GameObject angerGaugeObject = GameObject.Find("AngerCanvas/AngerGauge");
        angerGaugeScript = angerGaugeObject.GetComponent<AngerGauge>();

        sliderHP.value = maxHP; // Slider���ő�HP�ɐݒ肷��
        HP = maxHP; // HP���ő�ɂ���
    }

    void Update()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;

        // �v���C���[��ǂ�������
        if (isChasing)
        {
            this.transform.position += (Vector3)direction * chasingSpeed * Time.deltaTime;

            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        // �v���C���[�̕����ɉ����ēG�̌�����ύX����
        if (direction.x > 0)
        {
            // �E����
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            // ������
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    // �e�ۂ̃m�b�N�o�b�N�ƃ_���[�W
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // �m�b�N�o�b�N�̎���
            Vector2 knockbackDirection = (this.transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

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
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce * 10, ForceMode2D.Impulse);

            TakeDamage();
        }
    }

    // �_���[�W�̎���
    private void TakeDamage()
    {
        HP -= 20;
        sliderHP.value = (float)HP; // slider���̃X�v���N�g�̃��\�b�h�Ō��炷
        if (HP <= 0)
        {
            angerGaugeScript.AddAnger(angerGaugeScript.debugAngerRate); // Anger�Q�[�W�����܂�(�G��|����)
            Destroy(this.gameObject);
        }
    }

}
