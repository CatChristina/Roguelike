using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int health;

    public float xpOnKill = 5;

    public GameObject powerUp;
    private PlayerStats player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            int damage = other.GetComponent<ProjectileStats>().damage;
            TakeDamage(damage);
        }
    }

    // Makes the enemy take damage and drop a set powerup when it dies
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            int dropChance = Random.Range(1, 100);

            player.ModifyXP(xpOnKill);

            if (dropChance <= 10)
            {
                Instantiate(powerUp, transform.position, transform.rotation);
            }

            Destroy(gameObject, 0.01f);
        }
    }
}