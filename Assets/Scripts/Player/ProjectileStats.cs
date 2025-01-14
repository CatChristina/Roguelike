using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage;
    public float velocity;
    private Rigidbody rb;

    private void Start()
    {
        GunController gun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<GunController>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * velocity * 10 + new Vector3(Random.Range(-gun._spread, gun._spread), Random.Range(-gun._spread, gun._spread), 1), ForceMode.VelocityChange);

        hitColliders = new HashSet<Collider>();
        damage = gun._damage;

        Destroy(gameObject, 3);
    }

    private Vector3 previousPosition;
    private HashSet<Collider> hitColliders;

    private void FixedUpdate()
    {
        DoRaycastCheck();
    }

    // Does the raycast for collision detection
    private void DoRaycastCheck()
    {
        RaycastHit hit;
        Vector3 direction = transform.position - previousPosition;
        float distance = direction.magnitude;
        float radius = 0.2f; // Projectile hitbox radius

        if (Physics.SphereCast(previousPosition, radius, direction, out hit, distance))
        {
            if (hit.collider != null)
            {
                DoCollisionCheck(hit.collider);
            }
        }

        previousPosition = transform.position;
    }

    // Checks for collision with enemies and deals damage, divides damage upon any collision and continues until damage is less than 1
    private void DoCollisionCheck(Collider collider)
    {
        if (collider == null || hitColliders.Contains(collider) || collider.CompareTag("Player"))
        {
            return;
        }

        hitColliders.Add(collider);

        if (collider.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        if (collider.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
                DoRaycastCheck();
            }
            else
            {
                Debug.LogError("EnemyStats component not found on the collider.");
            }
        }

        damage /= 2;

        if (damage < 1)
        {
            Destroy(gameObject, 0.001f);
        }
    }
}
