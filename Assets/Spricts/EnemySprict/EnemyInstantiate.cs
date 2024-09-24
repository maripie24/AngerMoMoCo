using UnityEngine;
using System.Collections;
using System.Threading;

public class EnemyInstantiate : MonoBehaviour
{
    [SerializeField] GameObject enemy; // ｲﾝｽﾍﾟｸﾀｰ上で設定する
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
        int enemyCount = 0; // スポーンした敵の数をカウントする
        while (true)
        {
            enemyCount++;

            Instantiate(enemy, this.transform.position, Quaternion.identity);

            // Debug.Log($"{enemyCount}体目の敵だよ");

            yield return new WaitForSeconds(spawnInterval);

            //yield break;
        }
    }
}
