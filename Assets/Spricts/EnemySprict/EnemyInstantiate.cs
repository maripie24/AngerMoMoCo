using UnityEngine;
using System.Collections;
using System.Threading;

public class EnemyInstantiate : MonoBehaviour
{
    [SerializeField] GameObject enemy; // �ݽ�߸����Őݒ肷��
    [SerializeField] float spawnInterval = 0.2f;
    void Start()
    {

        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        int enemyCount = 0; // �X�|�[�������G�̐����J�E���g����
        while (true)
        {
            enemyCount++;

            Instantiate(enemy, this.transform.position, Quaternion.identity);

            // Debug.Log($"{enemyCount}�̖ڂ̓G����");

            yield return new WaitForSeconds(spawnInterval);

            //yield break;
        }
    }
}
