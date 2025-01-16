using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float scaleRadius;
    private SphereCollider col;

    void Start()
    {
        col = GetComponent<SphereCollider>();
        Destroy(gameObject, 2f);
        Invoke(nameof(DisableCollider), 1f);
        InvokeRepeating(nameof(IncreaseRadius), 0, 0.1f);
    }

    // Increases the collider radius over time
    void IncreaseRadius()
    {
        col.radius += scaleRadius;
    }

    // Disables the collider but keeps the object active so the particles can dissipate
    void DisableCollider()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }
    }
}
