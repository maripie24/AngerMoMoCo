using UnityEngine;

public class SearchAreaManager : MonoBehaviour
{
    private EnemyManager enemy;

    void Start()
    {
        enemy = this.transform.parent.gameObject.GetComponent<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.StartChase();
            enemy.InstantiateAttention();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.StartChase();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.StopChase();
        }
    }
}
