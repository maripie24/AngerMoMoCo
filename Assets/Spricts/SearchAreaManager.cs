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
            enemy.isChasing = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.isChasing = false;
        }
    }
}
