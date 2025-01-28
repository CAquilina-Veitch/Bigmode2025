using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth = 15f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemyHealth += -3;
        }

        if (enemyHealth <= 0 )
        {
            gameObject.SetActive(false);
        }
    }
}
