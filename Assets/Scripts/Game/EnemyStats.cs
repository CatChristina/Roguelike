using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;

    public float xpOnKill = 5;

    public GameObject powerUp;
    private PlayerStats player;
    private ItemStore store;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        store = GameObject.FindGameObjectWithTag("Store").GetComponent<ItemStore>();
    }

    // Makes the enemy take damage and drop a set powerup when it dies
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            int dropChance = Random.Range(1, 100);

            player.ModifyXP(xpOnKill);

            if (dropChance <= 15)
            {
                Instantiate(powerUp, transform.position, transform.rotation);
            }


            Destroy(gameObject, 0.01f);
        }
    }
}
